# 00003 [m2t8fp] PR3 — prune + hygiene

Branch `chore/prune-and-hygiene`. Plan: `docs/release-plan.md` §5/PR3, rulings R8, R13, R15, R18 (non-behavioral renames).

Steps:
1. **Tag first (R8):** `git tag pre-release-cleanup-2026-07 origin/master && git push origin pre-release-cleanup-2026-07` (from current master tip, BEFORE the deletions merge).
2. **Delete (R13):** `Code/TestBed/` (3 projects), `Code/Tests/Iql.Tests.ConsoleApp/`, `Code/Tests/Server/Iql.Tests.Server.Old/`, `Code/Apps/EntityFramework.ReferenceApp.ConsoleApp/`, `Code/Extensions/Iql.Client.ContextGenerator.ConsoleApp/` (stub), `Code/Extensions/Iql.Server.ConsoleApp/` (also drops a TypeSharp.Core ref), `Code/Apps/IqlSampleApp/IqlSampleApp.Migrations/`, `Code/SampleApps/` (all three), `Code/Dart/`, the 22 empty stub `.cs` files in `Code/` root (each is 3 bytes/BOM — verify size before rm), `Code/Core/Iql/Test.txt`, `Code/Extensions/Iql.Client.ContextGenerator/EntityConfiguration/Generated/Class1.cs`, `pack.bat`, `push.bat`. Remove deleted projects + stale config rows from `Code/Iql.sln`; also remove them from `Code/Apps/IqlSampleApp/IqlSampleApp.sln` if affected.
3. **Regenerate sample migrations:** fresh EF Core 10 initial migration inside `IqlSampleApp.Data` (replaces the deleted Migrations project's role; `Startup.cs` calls `Database.Migrate()`).
4. **Generator harness (R15):** `Code/Apps/Iql.Client.ContextGenerator.ConsoleApp/Program.cs` — replace `switch("safesite")` + absolute personal paths with CLI args (`--metadata-url`, `--output`, `--kind ts|cs`, `--config <json>`); fix the `OutputType.TypeScript;n` typo; delete the ~60% commented-out cases.
5. `.editorconfig` (root): match dominant existing style; include `[*.cs]` indent 4, charset utf-8. One `dotnet format whitespace --verify-no-changes`-clean sweep as its OWN commit.
6. Delete the ~30 commented-out `[TestMethod]` blocks (list in plan §2.5) and the ~590 commented-out code lines (Iql.Data 326, Iql.Entities 114, rest scattered) — keep genuinely explanatory comments; history preserves the rest. Non-behavioral renames from R18: `JsonSerializableExensions.cs`→`JsonSerializableExtensions.cs` (+class), `Sptial*Tests`→`Spatial*Tests`.
7. `.gitignore` tidy: drop yarn/npm/Angular sections that served the deleted samples; ensure `nuget-local/` stays tracked; keep standard VS ignores.

**Acceptance:** solution builds + all tests green after every commit; `find Code -name "*.csproj"` count matches Iql.sln membership exactly (zero orphans); repo root has no bat scripts.
