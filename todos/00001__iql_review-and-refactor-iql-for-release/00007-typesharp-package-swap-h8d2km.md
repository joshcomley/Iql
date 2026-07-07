# 00007 [h8d2km] Follow-up — swap vendored TypeSharp.Extensions for the public package

Plan: `docs/release-plan.md` §5/task 00007 + Appendix B. Not one of the six PRs — a coordination task with the sibling TypeSharp shippability workspace (`D:\Servers\Cmd\Storage\clones\TypeSharp__worktrees\00001__typesharp_refactor-typesharp-codebase-into-shippab`, no substantive work as of 2026-07-07).

When TypeSharp publishes `TypeSharp.Extensions` to nuget.org (with a netstandard2.0 or net10 asset):
1. Update `Code/Core/Iql/Iql.csproj` PackageReference to the public version.
2. Delete `nuget-local/` + its README + the folder source from `Code/NuGet.config`.
3. Confirm the release workflow's dependency-resolvability guard now passes (see sidecar 00005 step 5 — it was designed to fail while the vendored pin existed).
4. Decision-log entry; update docs/typescript-pipeline.md status.

This task is the **release blocker** for actually publishing Iql packages to nuget.org (the packed `Iql` package would otherwise declare an unresolvable dependency). Everything else in the program can complete without it.

Context for the TypeSharp side: TypeSharp.Extensions is 3 files/zero deps and packs immediately; TypeSharp.Core additionally needs TsBeautify (public repo `joshcomley/TsBeautify`, not on nuget.org, 24 files). IQL only needs Extensions for its C# packages; Core matters only for TS regeneration + ContextGenerator's TypeSharp path (the ContextGenerator's TypeSharp.Core ref survives in-repo — if it blocks PR1's build, the generator's TypeSharp-dependent code paths compile behind the same vendoring approach: pack TypeSharp.Core locally too IF TsBeautify can be packed from its public source; otherwise isolate the TypeSharp.Core-dependent generator files behind a compile exclusion and decision-log it — check usage first: TypeSharp.Core is referenced by Iql.Client.ContextGenerator.csproj).
