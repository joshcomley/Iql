# IQL Release Plan — review, refactor, ship

**Written:** 2026-07-07, by the review session in workspace `00001__iql_review-and-refactor-iql-for-release`.
**Executor:** a Claude Sonnet 5 session (or successor) working in this workspace. Work the PRs in order; each merges to `master` before the next begins (fleet auto-merge discipline). The persistent todo index is `todos/TODO-00001.md`; each PR has a sidecar with working detail.
**Status of this document:** the assessment (§1–§2) is evidence, gathered 2026-07-07; the rulings (§4) are decided — do not relitigate them; the PR program (§5) is the work.

---

## 1. What IQL is

IQL (Intermediate Query Language) is a dual-language query and data-access abstraction. Expressions written as C# lambdas or JavaScript expressions are parsed into a shared IQL expression tree (73 node kinds in `Code/Core/Iql`), which converts to OData query strings, JavaScript, .NET `Expression` trees, or C# source. On top of the tree sits a full client-side data framework (`Iql.Data`: `DataContext`, change tracking, offline queueing, validation, permissions, inferred values) and entity metadata system (`Iql.Entities`), with ASP.NET Core server glue (`Iql.Server`, `Iql.Server.OData.Net`, `Iql.Server.Azure`) and an OData-`$metadata` → TypeScript/C# client generator (`Iql.Client.ContextGenerator`).

The C# codebase is written to be transpiled to TypeScript by **TypeSharp** (sibling repo, `joshcomley/TypeSharp`, private; local clone at `D:\Servers\Cmd\Storage\clones\TypeSharp`). This dual-compile design explains the codebase's most unusual idioms — they are load-bearing, not cruft:

- `#if TypeScript` / `#if !TypeScript` conditional compilation (~555 blocks) and the solution configurations `TypeScript Debug` / `Debug TypeScript CSharp`.
- The `[DoNotConvert]` attribute (from `TypeSharp.Extensions`) marking types the transpiler skips (7 uses).
- The "DelayedInitialized" lazy-static pattern (190 occurrences in 51 files) — sidesteps TS/JS static-initialization-order differences.
- An optional `EvaluateContext evaluateContext` parameter appearing 259 times under `#if TypeScript`.
- A hand-written C# port of JSEP (MIT, attributed in-file) at `Code/Core/Iql.JavaScript/JavaScriptExpressionToExpressionTree/JavaScriptExpressionStringToExpressionTreeParser.cs` — parses JS expression strings into IQL. In the TS build, a lambda's `.ToString()` is JS source fed to this parser.

**Docs site:** http://iql.rocks/ is alive (Angular/Scully, source in the private `Iql.Web` repo — out of scope here). **Repo:** `github.com/joshcomley/Iql`, public, default branch `master`, 830 commits Sept 2017 → Aug 2024, dormant since (a partial .NET 8 migration was the last real work).

## 2. Assessment (evidence, 2026-07-07)

**Verdict: architecturally sound, operationally unshippable today.** The expression model, adapter/registry/per-kind-parser backends, and test coverage are real assets. But the repo does not build on any machine, has never been published anywhere public, targets EOL frameworks at its core, and is welded to a dead private package feed.

### 2.1 Build is broken everywhere

- `Code/NuGet.config` declares a **single** package source: `%BrandlessNuGet%` — an env var unset on every fleet machine, so NuGet treats it as a literal relative path and **every project fails restore with NU1301** (even `Newtonsoft.Json` can't come down; nuget.org isn't a declared source).
- Private packages, none on nuget.org, none in any fleet NuGet cache (hub/fir/studio/pixie swept clean 2026-07-07; p16 offline), MyGet feed presumed dead:
  - `TypeSharp.Extensions` 0.0.6-preview0128-1-Debug — referenced by `Code/Core/Iql/Iql.csproj` (the ONLY private dep of the ten core libraries).
  - `TypeSharp.Core` 0.0.6-preview0128-1-Debug — ContextGenerator + Iql.Server.ConsoleApp.
  - `Brandless.ObjectSerializer`, `Brandless.Data`, `Brandless.Data.EntityFramework`, `Brandless.Data.Mptt`, `Brandless.AspNetCore.OData.Extensions`, `Brandless.AspNetCore.OData.NetTopology` — server/extension/test projects, three different version pins of ObjectSerializer across projects.
  - Privately patched Microsoft packages `Microsoft.EntityFrameworkCore.Design`/`SqlServer.NetTopologySuite` `3.0.3-alpha0003-1-Debug` — only in the orphaned `IqlSampleApp.Migrations`.
- **Source availability for all of the above is solved:** local TypeSharp clone is at the exact needed version (`dc67c8b`, matches 0.0.6-preview0128); `TypeSharp.Extensions` is 3 trivial files with zero dependencies; `TsBeautify` (blocks TypeSharp.Core) is public at `joshcomley/TsBeautify`; `Brandless.ObjectSerializer` is public; `Brandless.Data` and `Brandless.AspNetCore.OData.Extensions` are **private** repos (pushed 2024-08-14). None of Josh's repos carry a LICENSE file.
- **Signing is broken by construction:** `Code/Build/Build.props` sets `SignAssembly=True` with `Key.snk` that was **never committed** (no `*.snk` anywhere; `git log --all -- Code/Build/Key.snk` is empty). Restore failures have masked this; any successful build would fail at signing.
- **Brandless usage is shallow** (the excision is tractable):
  - `Iql.Tests` / `Iql.Tests.DotNet`: package refs are **vestigial** — the only `using Brandless.ObjectSerializer` in ODataUriTests.cs feeds commented-out code; `Brandless.Data.EntityFramework` has zero code hits. Delete the refs.
  - `Iql.Server`: 1 file (`AllConfigurator.cs`, 2 hits, `Brandless.Data`).
  - `Iql.Server.OData.Net`: 4 files; the real surface is `CrudManager` (`EntityKey`, `FindQuery<T>`, `KeyEqualsExpression<T>`, `DbSet<T>`, `Context`), `CrudBase<TSecured,TUnsecured,T>` (`Secured`/`Unsecured`/`NewUnsecuredDb`), plus `Brandless.Data.Mptt` types in `IqlODataController.cs` and `Brandless.AspNetCore.OData.Extensions(.Binding)` in `ServerTestsBase.cs`.
  - `Iql.Client.ContextGenerator`: 2 files, 1 hit each (ObjectSerializer).
  - Repo sizes if vendoring: Brandless.Data 46 .cs, OData.Extensions 48, ObjectSerializer 39, TsBeautify 24.

### 2.2 Target-framework and csproj rot

- Core libraries (`Code/Core/Build/Build.props`): `netcoreapp3.1;net471;net5.0` — **all end-of-life**. Tests/Extensions/Apps: `net8.0` (EOL 2026-11-10 — four months away). `Iql.Forms` + `Tools` inherit the EOL core trio. TestBed has **no resolvable TFM**. Orphans pin `netcoreapp2.1`.
- `Code/Extensions/Iql.Server.OData.Net.csproj:9-18`: hardcoded machine-specific `HintPath`s into `C:\Program Files\dotnet\packs\...\3.1.0\...`.
- `Code/Extensions/Iql.Server.ConsoleApp.csproj:17-52`: 13 `<Content Update>` entries with absolute paths across **three different user profiles** (`Josh Comley`, `josh-xps`, `joshc`) pointing at `.tt` templates inside per-user NuGet caches.
- `Code/Core/Iql.OData/Iql.OData.csproj:6`: garbled `DefineConstants` — `TRACE;TYPESCRIPT DEBUG;NETSTANDARD2_0;ABC;TypeScript;TS;TYPESCRIPT DEBUG;NETSTANDARD2_0` (stray `ABC`, `TS`, duplicates).
- Mixed EF Core 3.1 / 8.0.8, MSTest 1.1.18 / 2.2.8, `Microsoft.AspNetCore` 2.2.0 pinned on net8.0 targets, `LangVersion 7.1` pins on console apps.
- CI is a hello-world `azure-pipelines.yml` (echo only). `pack.bat`/`push.bat` depend on `%NuGetSynchroniser%` / `%BrandlessNuGet%` (private, dead).

### 2.3 Dead weight inventory

- **8 orphaned csproj** on disk, absent from `Code/Iql.sln`: `TestBed/*` ×3 (2018, no TFM), `Tests/Iql.Tests.ConsoleApp` (benchmark harness, dev-only, hardcoded `D:\Code\Iql` path), `Tests/Server/Iql.Tests.Server.Old` (netcoreapp2.1, MSTest 1.1.18, one vestigial test), `Apps/EntityFramework.ReferenceApp.ConsoleApp` (EF 3.1 experiment, 2020), `Extensions/Iql.Client.ContextGenerator.ConsoleApp` ("Hello World" stub — the real harness is the one under `Apps/`), `Apps/IqlSampleApp/IqlSampleApp.Migrations` (netcoreapp2.1 + patched-alpha EFCore + references the net8 Data project — broken).
- **22 empty stub `.cs` files** (3 bytes each — a UTF-8 BOM) directly in `Code/` root (`EntityConfiguration.cs`, `Property.cs`, `Relationship*.cs`, `ValidationRule*.cs`, `MediaKey*.cs`, `CustomReport.cs`, …) — IDE artifact from commit `daa76321`; referenced by nothing.
- `Code/Core/Iql/Test.txt`, `Code/Extensions/Iql.Client.ContextGenerator/EntityConfiguration/Generated/Class1.cs` (VS-default name, committed).
- **SampleApps are museum pieces** (all 2017-09-17, none compiles — the TypeSharp-generated `Iql/` TS library they import is gitignored and absent): `Iql-TypeScript` (Angular 4 / CLI 1.4 / TS 2.3), `Iql-TypeScript.Compiled` (near-byte-identical twin), `Iql-NativeScript` (tns 3.x). `Code/Dart/iql` is a 2021 Flutter counter-app spike (~250 lines of real code, pre-null-safety Dart) — not a port. They also account for **all 8 open GitHub dependabot alerts** on the repo (1 critical — babel-traverse in Iql-NativeScript — plus 7 moderate, all npm, all inside `Code/SampleApps/*/package.json`): deleting the samples in PR3 clears the security tab outright.
- `Apps/Iql.Client.ContextGenerator.ConsoleApp/Program.cs` is a personal scratchpad: `switch ("safesite")` over hardcoded literals with output paths into `D:\code\SafeSite\`, `D:\code\Cutter\`, `C:\Users\joshc\...`, ~60% commented-out, plus a `OutputType.TypeScript;n` typo.

### 2.4 Core code health (the ten `Code/Core` libraries, 944 files)

- Architecture is coherent: per-kind expression nodes (73 kinds, `IqlExpressionKind`), JSON serialization with a `Kind`-discriminated custom deserializer, adapter+registry+ActionParser pattern per backend (OData/JS/.NET/C#-string), `IqlReducer` constant folding, `TypeResolver` metadata registry replacing reflection for the TS build. `Iql.Queryable` is **load-bearing** (base of `DbQueryable<T>`), not vestigial.
- Scar tissue to clean: **10 defeated conditionals** `#if TypeScript || CustomEvaluate || true` (the `|| true` makes them unconditional — leftover debugging; e.g. `JavaScriptExpressionConverter.cs:22,119`, `IqlReducer.cs`); **8 `#if SILVERLIGHT` blocks** (all in `ExpressionCSharpStringBuilder.cs`); **~590 commented-out code lines** (326 in Iql.Data, 114 in Iql.Entities); **49 `NotImplementedException`** (some intentional-unsupported, e.g. OData's reverse lambda→IQL direction, should be `NotSupportedException` with messages; others need triage); **20 TODOs**, 2 `// HACK ALERT` (in `DataContextGenerator.cs:1323,1454`); misspelled `JsonSerializableExensions.cs`; `SptialFunctionsTests`/`SptialInMemoryTests` typos; empty `#if TypeScript`/`#endif` pairs.
- **Namespace↔assembly mismatches**: the `Iql` assembly contains `Iql.Entities.*` namespaces; `Iql.Entities` contains `Iql.Data.*`; `Iql.Conversion` contains `Iql.Entities`/`Iql.Parsing`/`Iql.Queryable.Extensions`; `Iql.Queryable` contains `Iql.Data.Context`. Types were moved between projects without namespace updates.
- Only `Iql.Entities` has `<Nullable>enable</Nullable>` + `LangVersion latest`.
- Large files: `DataContext.cs` (2,378 lines), `DbQueryable.cs` (2,027) — acceptable; no action required.
- Two code generators feed the core: `Apps/Iql.ExpressionMethodGenerator.ConsoleApp` (generates `Flatten`/`Clone`/`Replace` methods) and `Tools/Iql.CloneMethodGenerator.ConsoleApp`. Keep + document.

### 2.5 Tests

- MSTest only. `Iql.Tests`: 67 test classes, ~664 `[TestMethod]`s covering OData URI/verbs, JS parsing, serialization/metadata, data context/tracking/saving, validation, permissions, offline, events, spatial, display formatting. `Iql.Tests.DotNet`: 1 test + a 31k-line generated fixture. `Iql.Tests.Data`: fixture library (no tests) with giant generated seed files (`HazceptionDataStore.cs`, 83,795 lines — fine, it's data).
- **The main suites run fully offline**: in-memory data store + `ODataFakeHttpProvider` (canned URL→JSON). The `http://localhost:28000/odata` strings are base-URI config/assertions, never contacted.
- `Iql.Tests.Server` (10 tests): boots in-process Kestrel + **EF Core against real SQL Server** — `Server=.;Database=IqlSampleApp;Integrated Security=True;TrustServerCertificate=True` (`ApplicationDbContext.cs:104`) + `EnsureCreated()`. This machine (hub) has **no SQL Server/LocalDB** — see R11.
- ~30 commented-out `[TestMethod]`s (12 in `UnitTest1.cs`); zero `[Ignore]`.
- `JetBrains.DotMemoryUnit` referenced by test projects (needs its own runner; almost certainly dead weight — verify usage and remove, R17).

### 2.6 Distribution: nothing public exists

Nothing on nuget.org (no `Iql.*`), nothing on npm (`iql` is squatted by an unrelated project; the planned `@brandless/iql` from the `Iql.Npm` repo was never published). The repo has **no LICENSE** (public repo = all-rights-reserved today). `version.xml` says 0.0.28-preview20631.

---

## 3. Ship definition (scope ruling)

**Ship = the C# suite**, meaning all of:

1. A clean `git clone` + `dotnet build Code/Iql.sln` + `dotnet test` succeeds on a machine with only a .NET SDK — no private feeds, no env vars, no absolute paths.
2. CI (GitHub Actions) proves it on every push/PR, badge in README.
3. `dotnet pack` produces a coherent, fully-metadata'd NuGet package set; a tag-driven release workflow can push them to nuget.org (the actual publish is a manual gate).
4. LICENSE, real README, architecture docs, contributor/build docs exist.
5. The repo contains no dead private-infrastructure references and no orphaned/broken projects.

**Explicit non-goals of this program** (record in README/docs, revisit later):
- Reactivating the TypeScript pipeline / publishing `@brandless/iql` (blocked on the sibling TypeSharp shippability program; IQL preserves all transpiler-facing machinery — `#if TypeScript` configs, `[DoNotConvert]`, DelayedInitialized idiom — and documents it; see task 00007).
- The Dart port (2021 spike — archived).
- Repo-wide nullable annotations (only `Iql.Entities` has it; full enablement is a separate program).
- iql.rocks refresh (private `Iql.Web` repo).
- Performance work; OData server rearchitecture.

---

## 4. Standing rulings (decided — do not relitigate)

- **R1 — Feeds:** `Code/NuGet.config` becomes `<clear/>` + nuget.org + a repo-relative local folder source `nuget-local/`. The `%BrandlessNuGet%` source is deleted.
- **R2 — TypeSharp.Extensions:** vendor a locally-built nupkg into `nuget-local/`, **same version string** `0.0.6-preview0128-1-Debug` so `Iql.csproj` is untouched in PR1. Recipe: temp checkout of TypeSharp @ `dc67c8b` → add `netstandard2.0` to `TypeSharp.Extensions` TargetFrameworks (required: the historical package has no netstandard2.0 asset, and core retargets to netstandard2.0 — without this, NU1202) → `dotnet pack -c Debug` with version props matching → copy nupkg → discard checkout. Document the recipe in `nuget-local/README.md`. Do **not** inline the attribute into IQL (TypeSharp's converter matches `DoNotConvertAttribute` — the type must keep coming from TypeSharp.Extensions). Swap to the public TypeSharp.Extensions package when TypeSharp ships (task 00007).
- **R3 — Brandless excision:** per package: (a) refs with no code usage → delete ref + dead usings/commented remnants (`Iql.Tests`, `Iql.Tests.DotNet`, `Iql.Tests.ConsoleApp` is deleted anyway); (b) shallow usage → **minimal-port** the used types into the consuming project under an `Internal/` namespace with a file-header attribution ("ported from joshcomley/<repo> @ <sha>"); (c) if a minimal port drags **>~15 files** or fights the type system, vendor that package's **full source** as a project under `Code/Vendor/<Name>/`. **Josh approved porting freely from the private repos (Q2 answered 2026-07-07)** — port/vendor from `Brandless.Data` and `Brandless.AspNetCore.OData.Extensions` without further gating; keep the attribution headers.
- **R4 — Signing:** remove `SignAssembly`/`AssemblyOriginatorKeyFile`/`PublicSign` from `Code/Build/Build.props`. The key never existed in-repo; there are no published consumers, so there is no assembly identity to preserve.
- **R5 — Target frameworks:** core libraries (all ten under `Code/Core` + `Iql.Forms`) → `netstandard2.0;net10.0`. Server/ASP.NET projects, ContextGenerator, tests, apps, tools → `net10.0`. Package bumps that follow: EF Core → 10.x, `Microsoft.AspNetCore.OData` → latest 9.x (port the routing/model deltas properly), `Microsoft.Extensions.Identity.Stores` → 10.x, MSTest → 3.x + `Microsoft.NET.Test.Sdk` → current, `Newtonsoft.Json` → 13.0.4, `Azure.Storage.Blobs` → current, `Microsoft.CodeAnalysis` → current major compatible with net10 SDK. netstandard2.0 gotchas: default LangVersion is 7.3 — set `LangVersion` explicitly where modern syntax is used; missing-API stragglers get `System.Memory`-style compat packages or `#if NETSTANDARD2_0` shims (expect few — the code already compiled for net471).
- **R6 — Versioning:** delete `version.xml` + `BuildNumber` suffix machinery + `pack.bat`/`push.bat`. Single `VersionPrefix` (start `0.1.0`) in the root build props; releases are tag-driven (`v*`).
- **R7 — License:** MIT — **confirmed by Josh 2026-07-07 (Q1)**. The LICENSE file (MIT, Josh Comley copyright) lands in PR2 unconditionally; `PackageLicenseExpression=MIT` in PR5.
- **R8 — History preservation:** before any pruning, tag the pre-cleanup tree `pre-release-cleanup-2026-07`. All deletions are `git rm` (history keeps everything); README gains a one-line "historical samples live at tag X" note.
- **R9 — Behavior preservation:** PRs 1, 3, 4 must not change what tests assert (except tests deleted *with* their dead feature — none expected). Defeated conditionals `#if TypeScript || CustomEvaluate || true` are unwrapped to **unconditional** code (that is today's behavior); never "restore" the condition.
- **R10 — CI:** GitHub Actions replaces `azure-pipelines.yml`. `windows-latest` is the primary lane (build Debug+Release, run all tests incl. server tests on LocalDB); `ubuntu-latest` secondary (build + non-SQL test projects) proving cross-platform. Add dependabot for nuget + actions ecosystems (weekly).
- **R11 — Server-test SQL:** parameterize the connection string — env var `IQL_TESTS_SQL_CONNECTION`, falling back to the current `Server=.;...` literal. CI (windows-latest) sets it to LocalDB (`Server=(localdb)\MSSQLLocalDB;...`). For local verification on the hub (no SQL installed): install SQL Server Express LocalDB via the **admin gate** (check `D:\Servers\Cmd-Admin\admin-gate\HEARTBEAT` freshness; fall back to `request-admin-action` if closed). Do not weaken the tests to in-memory — they exist to prove real-SQL behavior.
- **R12 — Branching:** `master` stays the default branch (no rename churn).
- **R13 — Keep/delete:** keep `Iql.Queryable` (load-bearing). Delete (from disk; they are orphaned or dead): `TestBed/` ×3, `Tests/Iql.Tests.ConsoleApp`, `Tests/Server/Iql.Tests.Server.Old`, `Apps/EntityFramework.ReferenceApp.ConsoleApp`, `Extensions/Iql.Client.ContextGenerator.ConsoleApp` (stub), `Extensions/Iql.Server.ConsoleApp` (empty stub + junk csproj; also removes a TypeSharp.Core ref), `Apps/IqlSampleApp/IqlSampleApp.Migrations`, `Code/SampleApps/` (all three), `Code/Dart/`, the 22 root stub `.cs` files, `Code/Core/Iql/Test.txt`, `Generated/Class1.cs`, `pack.bat`, `push.bat`, `azure-pipelines.yml`. Keep + modernize: `IqlSampleApp` + `IqlSampleApp.Data` (the canonical server example; regenerate EF migrations fresh under EF 10 to replace the deleted Migrations project's role), both code generators (R15).
- **R14 — Namespace↔assembly realignment:** do it (breaking changes are free — nothing was ever published). Method: rename namespaces to match the owning assembly's root namespace (files stay put; the dependency graph already works). Pure-mechanical commits, one assembly per commit, tests green after each.
- **R15 — Generator harness:** `Apps/Iql.Client.ContextGenerator.ConsoleApp` is the real codegen harness — keep, but replace the hardcoded `switch("safesite")` + absolute personal paths with CLI args + an `appsettings.json`/`--config` file; document usage. `Iql.ExpressionMethodGenerator.ConsoleApp` + `Iql.CloneMethodGenerator.ConsoleApp`: keep, retarget, document what they generate and when to re-run them.
- **R16 — ContextGenerator namespace:** the assembly `Iql.Client.ContextGenerator` uses root namespace `Iql.OData.TypeScript.Generator` — realign to `Iql.Client.ContextGenerator` as part of R14.
- **R17 — DotMemoryUnit:** remove the package refs (requires a proprietary runner; the benchmark harness that motivated it is deleted). If any test body uses its API, rewrite the assertion or delete that memory test with a decision-log note.
- **R18 — Known small bugs to fix while there** (each with a red→green test where observable): `AzureMediaManager.SetMediaUriAsync` ignores its `TimeSpan? lifetime` parameter (overwrites with `FromDays(1)`, `AzureMediaManager.cs:62`); `AzureSafe(name)` is a no-op — implement sanitization or inline-remove it; `OutputType.TypeScript;n` typo; misspelled `JsonSerializableExensions.cs` → `JsonSerializableExtensions.cs` (file + class); `Sptial*Tests` → `Spatial*Tests`.
- **R19 — Decision log:** create `decisions/` (fleet convention: `decisions/NNNNN-slug/title.md + decision.md`). Backfill entries for: the Brandless excision, the TFM/package-bump choice, signing removal, namespace realignment, sample-app archival, TypeSharp vendoring. Add new entries for anything non-obvious discovered during execution.

---

## 5. The PR program

Each PR: branch off up-to-date `master` in this worktree → conventional commits → build+test gate → push → `gh pr create` → merge → immediately start the next. Update `todos/TODO-00001.md` (mark `DONE`, move sidecar to `done/`) as part of each PR.

### PR 1 — Restore buildability (`fix/restore-buildability`) — sidecar 00001
The keystone. Commit-ordered so the build gets progressively further:
1. `NuGet.config` rewrite (R1) + create `nuget-local/` with the vendored TypeSharp.Extensions nupkg + provenance README (R2).
2. Delete vestigial Brandless refs + dead usings/commented remnants in `Iql.Tests`, `Iql.Tests.DotNet` (R3a).
3. Remove signing from `Code/Build/Build.props` (R4).
4. Retarget TFMs + package bumps (R5): edit the four `Build/Build.props` files; delete net46/net471 conditional ItemGroups (e.g. `System.Net.Http` in Iql.DotNet), the netcoreapp3.1 HintPath block (OData.Net), the netcoreapp2.1 block (Iql.Server.Azure), fix `Iql.OData` DefineConstants (canonical per-config sets, drop `ABC`/`TS`/dupes), drop stray `LangVersion 7.1` pins.
5. Brandless excision (R3b/c): `Iql.Server` (Brandless.Data, 1 file), `Iql.Server.OData.Net` (CrudManager/CrudBase/Mptt surface), `Iql.Client.ContextGenerator` (ObjectSerializer, 2 hits), `ServerTestsBase` (OData.Extensions binding). Q2 is answered — port freely with attribution.
6. Fix whatever the compiler then finds (MSTest 3 deltas, OData 9 port in `IqlODataController`/`Startup`, netstandard2.0 stragglers).
**Gate:** `dotnet build Code/Iql.sln -c Debug` and `-c Release` clean; `dotnet test` green for Iql.Tests + Iql.Tests.DotNet; server tests compile and pass locally against LocalDB (install via admin gate, R11). Also verify the two remaining solution configs (`TypeScript Debug`, `Debug TypeScript CSharp`) still evaluate (build of `Iql` project in `TypeScript Debug` is allowed to fail only on pre-existing TypeScript-mode code paths — record what it does).

### PR 2 — CI, license, README (`ci/github-actions`) — sidecar 00002
GitHub Actions workflow (R10): windows lane = restore/build Debug+Release + full `dotnet test` with `IQL_TESTS_SQL_CONNECTION` → LocalDB; ubuntu lane = build + `dotnet test` excluding Iql.Tests.Server. Dependabot config. Delete `azure-pipelines.yml`. LICENSE = MIT (R7, confirmed). README rewrite: what IQL is (from §1), quickstart (clone/build/test), package matrix, CI badge, iql.rocks link, historical-samples tag note (R8), TypeScript-pipeline status note. Set the GitHub repo description + topics via `gh repo edit`.

### PR 3 — Prune + hygiene (`chore/prune-and-hygiene`) — sidecar 00003
Tag `pre-release-cleanup-2026-07` first (R8). Execute the R13 delete list; purge the deleted projects from `Iql.sln` and prune orphaned sln config rows. Refactor the generator harness (R15). Add `.editorconfig` (match existing style: 4-space C#, CRLF tolerated) + one `dotnet format whitespace` sweep as its own commit. File/typo renames (R18 non-behavioral ones). Delete the ~30 commented-out `[TestMethod]`s and the ~590 commented-out code lines (keep genuinely explanatory comments; history preserves the rest). `.gitignore` tidy (drop yarn/npm sections if samples went; keep `nuget-local/` un-ignored).

### PR 4 — Core code health (`refactor/core-health`) — sidecar 00004
Unwrap the 10 defeated conditionals (R9). Delete the 8 SILVERLIGHT blocks and empty `#if` pairs. NotImplementedException audit: intentional-unsupported → `NotSupportedException("...")` with a message naming the supported direction; genuinely-missing → implement if small, else decision-log + keep throw with message. Triage the 20 TODOs + 2 HACK ALERTs (fix or decision-log). Fix R18 behavioral bugs red→green. Remove dead usings solution-wide. Namespace realignment (R14 + R16) as trailing mechanical commits, one assembly per commit, tests green after each.

### PR 5 — Packaging + release automation (`build/packaging`) — sidecar 00005
Consolidate the props layering: replace the `Build/Build.props` + `Directory.Build.props` indirection with standard `Code/Directory.Build.props` (shared metadata) + per-area `Directory.Build.props` (TFMs only). Per-package NuGet metadata: `Description` on each of the 14 packable libs (the 10 core libraries + Iql.Forms + Iql.Server, Iql.Server.Azure, Iql.Server.OData.Net — pack.bat's historical list, all surviving), `Authors`, `PackageLicenseExpression` MIT (R7), `PackageReadmeFile`, `RepositoryUrl`+`PublishRepositoryUrl`, SourceLink (`Microsoft.SourceLink.GitHub`), `IncludeSymbols`+snupkg, `Deterministic`+`ContinuousIntegrationBuild` in CI. Version = `VersionPrefix 0.1.0` (R6). Release workflow: on `v*` tag → build, test, pack, push to nuget.org via `NUGET_API_KEY` secret (workflow validates the secret exists and no-ops with a clear message if absent), plus a manual `workflow_dispatch` dry-run mode. `RELEASING.md` documents the ritual. Delete `version.xml`, `pack.bat`, `push.bat`.

### PR 6 — Docs + release cut (`docs/architecture-and-release`) — sidecar 00006
`docs/architecture.md` (distill §1/§2.4: expression model, backends, adapter pattern, generators, data framework layers). `docs/typescript-pipeline.md` (dual-compile design: `#if TypeScript` semantics, `[DoNotConvert]`, DelayedInitialized idiom, EvaluateContext, the solution configs, TypeSharp/Iql.Npm relationship, what reactivation requires). `CONTRIBUTING.md` (build, test, SQL setup, codegen re-run instructions). Decision-log backfill (R19). `CHANGELOG.md` seeded at 0.1.0. Final sweep: acceptance checklist below, README links verified, `gh repo edit` metadata confirmed. Cut `v0.1.0` tag → release workflow dry-run → GitHub Release notes. **Actual nuget.org publish = manual gate with Josh** (needs his API key + explicit go).

### Task 00007 — TypeSharp coordination (follow-up, not a PR here)
When the sibling TypeSharp workspace ships TypeSharp.Extensions publicly: swap `Iql.csproj` to the public version, delete `nuget-local/`, note in decision log. If IQL's TS regeneration is attempted later, it additionally needs TypeSharp.Core (blocked on public TsBeautify) and the `Iql.Npm` harness modernized — that is a new program, scoped then.

---

## 6. Questions for Josh — BOTH ANSWERED 2026-07-07

- **Q1 — License:** **MIT.** (R7 updated; no gate remains.)
- **Q2 — Private source exposure:** **Port freely** from the private Brandless repos into public IQL, with attribution headers. (R3 updated; no gate remains.)

Everything is decided (R1–R19). If Josh vetoes a ruling later, amend this doc + the todo sidecars in the same commit as the change.

---

## 7. Acceptance checklist ("shippable" = all true)

- [ ] Fresh clone on a machine with only .NET SDK 10: `dotnet build Code/Iql.sln -c Release` clean.
- [ ] `dotnet test` green locally (incl. server tests with LocalDB) and in CI (badge green on master).
- [ ] `grep -riE "BrandlessNuGet|NuGetSynchroniser|myget" --include="*" .` → 0 hits outside docs/history notes.
- [ ] `grep -rE "C:\\\\Users|D:\\\\code|D:\\\\Code" Code --include="*.csproj" --include="*.cs"` → 0 hits.
- [ ] No project targets an EOL framework; no orphaned csproj on disk.
- [ ] `dotnet pack` on the package set: every nupkg has description/license/readme/repo-URL/symbols; versions coherent.
- [ ] Release workflow dry-run green; `v0.1.0` tag + GitHub Release drafted.
- [ ] LICENSE, README, docs/architecture.md, docs/typescript-pipeline.md, CONTRIBUTING.md, RELEASING.md, CHANGELOG.md exist and are accurate.
- [ ] decisions/ log populated; todos/TODO-00001.md fully `DONE`.

---

## Appendix A — Project inventory (2026-07-07)

| Project | Area | Files | State | Fate |
|---|---|---|---|---|
| Iql | Core | 128 | expression model; TypeSharp.Extensions dep | keep (keystone) |
| Iql.Events | Core | 12 | event emitters | keep |
| Iql.Conversion | Core | 21 | converter abstractions | keep |
| Iql.Entities | Core | 234 | metadata/config; nullable enabled | keep |
| Iql.Parsing | Core | 68 | adapters/registries/reducer | keep |
| Iql.Queryable | Core | 20 | fluent query base | keep (load-bearing) |
| Iql.Data | Core | 258 | DataContext/tracking/offline | keep (largest) |
| Iql.DotNet | Core | 75 | lambda↔IQL, IQL→Expression/C# | keep |
| Iql.JavaScript | Core | 84 | JSEP port, IQL→JS | keep |
| Iql.OData | Core | 44 | IQL→OData strings | keep (fix csproj) |
| Iql.Forms | Forms | 17 | form hints/geo/sync metadata | keep, retarget |
| Iql.Server | Ext | ~55 | DI/serialization/media base | keep, excise Brandless |
| Iql.Server.OData.Net | Ext | 19 | OData controllers (800-line base) | keep, excise Brandless, OData 9 |
| Iql.Server.Azure | Ext | 4 | Azure blob media | keep, fix lifetime bug |
| Iql.Server.ConsoleApp | Ext | 1 | empty stub, junk csproj | **delete** |
| Iql.Client.ContextGenerator | Ext | 82 | $metadata→TS/C# codegen | keep, excise, realign ns |
| Iql.Client.ContextGenerator.ConsoleApp (Ext) | Ext | 1 | "Hello World" stub | **delete** |
| Iql.Client.ContextGenerator.ConsoleApp (Apps) | Apps | few | real harness, personal paths | keep, parameterize |
| Iql.ExpressionMethodGenerator.ConsoleApp | Apps | 8 | core codegen | keep, document |
| EntityFramework.ReferenceApp.ConsoleApp | Apps | 3 | EF3.1 experiment, orphan | **delete** |
| IqlSampleApp (+.Data) | Apps | 3+133 | functional net8 OData sample | keep, retarget net10 |
| IqlSampleApp.Migrations | Apps | 1 | broken orphan, alpha EFCore | **delete** (regen migrations) |
| Iql.CloneMethodGenerator.ConsoleApp | Tools | 1 | core codegen | keep, retarget |
| Iql.TestBed / .DotNet / .ConsoleApp | TestBed | 10 | 2018 orphans, no TFM | **delete** |
| Iql.Tests | Tests | 67 cls | ~664 tests, offline-capable | keep (the proof) |
| Iql.Tests.Data | Tests | big | fixtures + generated seed data | keep |
| Iql.Tests.DotNet | Tests | 2 | 1 test + 31k-line fixture | keep |
| Iql.Tests.ConsoleApp | Tests | 3 | orphan benchmark harness | **delete** |
| Iql.Tests.Server | Tests | 7 | 10 tests, needs SQL Server | keep, parameterize conn |
| Iql.Tests.Server.Old | Tests | 2 | netcoreapp2.1 orphan | **delete** |
| SampleApps ×3 + Dart | Samples | many | 2017/2021 museum pieces | **delete** (tagged) |

## Appendix B — Ecosystem map

- **TypeSharp** (`joshcomley/TypeSharp`, private): C#→TS transpiler. Local clone `D:\Servers\Cmd\Storage\clones\TypeSharp` @ `dc67c8b` = exactly the version IQL pins (0.0.6-preview0128). `TypeSharp.Extensions` = 3 files, zero deps, packs immediately. `TypeSharp.Core` additionally needs `TsBeautify` (public repo, not on nuget.org). TypeSharp has its own shippability workspace (`TypeSharp__worktrees/00001...`, no substantive work yet as of 2026-07-07). TypeSharp's Builder/Playground reverse-depend on `Iql.*` packages (the two ecosystems bootstrapped each other via the dead private feed).
- **Brandless.\*** (`joshcomley/Brandless.Data` [private], `Brandless.ObjectSerializer` [public], `Brandless.AspNetCore.OData.Extensions` [private], `Brandless.Data.Mptt` lives inside Brandless.Data): support libs consumed via the dead feed. None licensed.
- **Iql.Npm** (`joshcomley/Iql.Npm`): npm packaging of TypeSharp-generated TS (`@brandless/iql`, never published; `iql` npm name is squatted by an unrelated project). Driven by `%TypeSharp%` env-var bat scripts. Untouched by this program (task 00007 scope).
- **iql.rocks**: live Angular/Scully docs site; source = private `Iql.Web` repo.
- Old satellite repos (`IqlSample`, `iql.angular`, `Iql.OData.ContextGenerator`, `Iql.People`): historical, out of scope.
