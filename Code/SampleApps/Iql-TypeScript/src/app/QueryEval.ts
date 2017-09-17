export const QueryFilterEval = "this.queryableExpressionEvalContext = { Evaluate: function (n) {return eval(n);}, Context: this }";

export function Qfv(fn: Function) {
    return fn(QueryFilterEval);
}