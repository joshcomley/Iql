# 00004 [r6y3zn] PR4 — core code health

Branch `refactor/core-health`. Plan: `docs/release-plan.md` §5/PR4, rulings R9, R14, R16, R17, R18, R19.

Steps:
1. **Defeated conditionals (R9):** the 10 `#if TypeScript || CustomEvaluate || true` sites (`JavaScriptExpressionConverter.cs:22,119`, `JavaScriptExpressionNodeParseContext.cs`, `IqlReducer.cs`, `IqlVariableExpressionReducer.cs`, +5 more — grep `\|\| true`) → unwrap to UNCONDITIONAL code (today's behavior). Delete empty `#if TypeScript`/`#endif` pairs (e.g. `JavaScriptExpressionConverter.cs:316-317`).
2. Delete the 8 `#if SILVERLIGHT` blocks (`ExpressionCSharpStringBuilder.cs`).
3. **NotImplementedException audit (49 sites):** intentional-unsupported (e.g. `ODataExpressionConverter` lambda→IQL direction) → `NotSupportedException` with a message naming what IS supported; genuinely-missing-small → implement with a test; genuinely-missing-large → decision-log entry + keep throw with an explanatory message.
4. **TODO/HACK triage:** 20 TODOs + `DataContextGenerator.cs:1323,1454` HACK ALERTs — fix trivially-fixable; otherwise decisions/ entry per plan R19 (never silently delete the marker).
5. **Behavioral bug fixes (R18), red→green each:** `AzureMediaManager.SetMediaUriAsync` must honor its `lifetime` param (default `FromDays(1)` only when null); `AzureSafe(name)` — implement real sanitization (Azure blob/container naming rules) or remove the indirection.
6. Dead `using` sweep solution-wide (analyzer-driven, own commit).
7. **Namespace realignment (R14+R16), trailing mechanical commits, one assembly per commit, tests green after each:** namespaces become the owning assembly's root (files stay put). Known mismatches: `Iql` asm carries `Iql.Entities.{InferredValues,Permissions,Rules.Relationship}`; `Iql.Entities` carries `Iql.Data.{Evaluation,Queryable,Types}`; `Iql.Conversion` carries `Iql.Entities`/`Iql.Parsing`/`Iql.Queryable.Extensions`; `Iql.Queryable` carries `Iql.Data.Context`; ContextGenerator asm root ns is `Iql.OData.TypeScript.Generator` → `Iql.Client.ContextGenerator`. Update the `#if TypeScript` regions too — they must keep compiling as text even when the symbol is off (they do compile under TypeScript Debug config).
8. MSTest 3 leftovers from PR1 (if any deferred), analyzer warnings triage — do NOT enable TreatWarningsAsErrors in this PR (note for later if desired).

**Acceptance:** all tests green; `grep -rn "|| true" Code --include="*.cs"` → 0 in `#if` lines; `grep -rn "SILVERLIGHT" Code` → 0; decision-log entries exist for every non-trivial call.

**Caution:** the DelayedInitialized lazy-static idiom (190 sites) and `EvaluateContext` conditional params (259 sites) are LOAD-BEARING for the TypeScript build — do NOT "clean" them (plan §1).
