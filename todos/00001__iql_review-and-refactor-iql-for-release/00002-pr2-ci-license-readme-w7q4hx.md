# 00002 [w7q4hx] PR2 — CI, LICENSE, README

Branch `ci/github-actions`. Plan: `docs/release-plan.md` §5/PR2, rulings R7, R8, R10, R11.

Steps:
1. `.github/workflows/ci.yml`: trigger push+PR on master. **windows-latest** job: setup-dotnet (10.x), restore, build Debug+Release, `dotnet test` all four test projects with `IQL_TESTS_SQL_CONNECTION=Server=(localdb)\MSSQLLocalDB;Database=IqlSampleApp;Integrated Security=True;TrustServerCertificate=True` (verify LocalDB present on the runner — `sqllocaldb start MSSQLLocalDB` step). **ubuntu-latest** job: build + test excluding Iql.Tests.Server (`--filter` or explicit project list).
2. `.github/dependabot.yml`: nuget + github-actions ecosystems, weekly.
3. Delete `azure-pipelines.yml`.
4. **LICENSE (Q1 gate, R7):** MIT with Josh Comley copyright — ONLY if Josh has confirmed Q1 (plan §6). If unanswered, ship PR2 without it and add the moment he answers.
5. README rewrite: what IQL is (plan §1 distilled), badges (CI), quickstart (clone → `dotnet build Code/Iql.sln` → `dotnet test`), package matrix table, docs links (iql.rocks + docs/), "historical sample apps live at tag `pre-release-cleanup-2026-07`" note, TypeScript-pipeline status paragraph (preserved, reactivation pending TypeSharp — link docs/typescript-pipeline.md once PR6 lands).
6. `gh repo edit joshcomley/Iql --description "IQL — Intermediate Query Language: C#/TypeScript query abstraction over OData, JavaScript and .NET expressions" --add-topic odata,typescript,csharp,query,expression-tree` (PowerShell, full gh path).

**Acceptance:** CI green on the PR itself (both lanes); badge renders on master after merge.

**Note:** if PR3's pruning would immediately churn the README package matrix, keep the matrix to the keep-list from Appendix A (already excludes deletions).
