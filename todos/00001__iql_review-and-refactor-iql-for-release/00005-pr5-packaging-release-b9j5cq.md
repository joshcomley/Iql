# 00005 [b9j5cq] PR5 — packaging + release automation

Branch `build/packaging`. Plan: `docs/release-plan.md` §5/PR5, rulings R2, R6.

Steps:
1. **Props consolidation:** replace the `Directory.Build.props → Build/Build.props` indirection with standard MSBuild layering: `Code/Directory.Build.props` (Product, Authors, RepositoryUrl, VersionPrefix 0.1.0, license/readme/SourceLink metadata, TreatWarningsAsErrors=false) + per-area `Code/<Area>/Directory.Build.props` (TFMs only, importing parent via `<Import Project="$([MSBuild]::GetPathOfFileAbove(...))"/>`). Delete the four `Build/Build.props` files + `Code/Apps/IqlSampleApp/Build/`. Delete `version.xml` (R6).
2. **Packable set (14, = pack.bat's historical list, all of which survive):** Iql, Iql.Conversion, Iql.Data, Iql.DotNet, Iql.Entities, Iql.Events, Iql.JavaScript, Iql.OData, Iql.Parsing, Iql.Queryable, Iql.Forms, Iql.Server, Iql.Server.Azure, Iql.Server.OData.Net. Everything else `<IsPackable>false</IsPackable>`.
3. Per-package `Description`; shared: `PackageLicenseExpression` (post-Q1), `PackageReadmeFile` (per-package or root README), `PublishRepositoryUrl`, `Microsoft.SourceLink.GitHub`, `IncludeSymbols`+`SymbolPackageFormat snupkg`, `EmbedUntrackedSources`, `Deterministic`, `ContinuousIntegrationBuild` (CI-only via env condition).
4. **Release workflow** `.github/workflows/release.yml`: on `v*` tag + `workflow_dispatch` (dry-run input). Build, test, `dotnet pack -c Release -o packages`, then push all nupkg+snupkg to nuget.org with `${{ secrets.NUGET_API_KEY }}` — validate the secret exists first and fail with a clear message if absent; dry-run mode skips push and uploads artifacts.
5. `RELEASING.md`: version bump → tag → workflow → verify on nuget.org; note the vendored TypeSharp.Extensions caveat (task 00007) — the PACKED Iql package declares a dependency on `TypeSharp.Extensions 0.0.6-preview0128-1-Debug` which is NOT on nuget.org, so **publishing Iql before task 00007 resolves would ship a broken dependency chain**. The release workflow must hard-fail the push step if any packed dependency is not publicly resolvable (script check) — this makes task 00007 a release blocker by construction, not by memory.
6. Local verification: `dotnet pack` full set; inspect one nupkg (nuspec metadata complete, dll TFMs correct: netstandard2.0+net10.0 for core).

**Acceptance:** pack clean; dry-run workflow green end-to-end; the dependency-resolvability check demonstrably fails while TypeSharp.Extensions is vendored-only (that's correct behavior — it proves the guard).
