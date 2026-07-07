# 00006 [e4n7ws] PR6 — docs + v0.1.0 release cut

Branch `docs/architecture-and-release`. Plan: `docs/release-plan.md` §5/PR6, ruling R19; acceptance checklist §7.

Steps:
1. `docs/architecture.md`: distill plan §1 + §2.4 — expression model (73 kinds, IqlExpressionKind), serialization (Kind-discriminated JSON), parsing paths (C# lambda parser, JSEP-port JS parser), backend adapter/registry/ActionParser pattern (OData/JS/.NET/C#-string), IqlReducer, TypeResolver, Iql.Data layers (DataContext/tracking/offline), Iql.Entities metadata, server stack, ContextGenerator flow, the two code generators (what they generate, when to re-run).
2. `docs/typescript-pipeline.md`: the dual-compile design — `#if TypeScript` semantics + solution configs, `[DoNotConvert]`, DelayedInitialized idiom (WHY it exists), EvaluateContext params, TypeSharp/TsBeautify/Iql.Npm relationship, `@brandless/iql` naming (npm `iql` is squatted), exactly what reactivation requires (public TypeSharp.Core+Extensions, TsBeautify packaging, Iql.Npm harness modernization, regenerating the TS library, a modern sample).
3. `CONTRIBUTING.md`: prereqs (.NET 10 SDK, LocalDB for server tests + `IQL_TESTS_SQL_CONNECTION`), build/test commands, codegen re-run instructions, PR conventions.
4. **Decision log backfill (R19):** `decisions/00001-brandless-excision/`, `00002-tfm-net10-netstandard20/`, `00003-signing-removal/`, `00004-namespace-realignment/`, `00005-sample-archival/`, `00006-typesharp-vendoring/` — each `title.md` + `decision.md` (symptom, root cause, decision, why, watch-for). Fold in anything discovered during PRs 1–5.
5. `CHANGELOG.md` seeded at 0.1.0 (Keep-a-Changelog format).
6. Final sweep: run plan §7 acceptance checklist item by item; fix stragglers; verify README/docs links (incl. iql.rocks reachable).
7. Cut: bump nothing (VersionPrefix already 0.1.0), `git tag v0.1.0` → push tag → release workflow **dry-run** green → draft GitHub Release notes from CHANGELOG.
8. **Publish gate:** actual nuget.org push needs Josh — his NUGET_API_KEY secret + explicit go (and task 00007 resolved first; the workflow's resolvability guard enforces it). Report ready-state to him.

**Acceptance:** plan §7 checklist all ticked except the one Josh-gated item (the actual nuget.org publish).
