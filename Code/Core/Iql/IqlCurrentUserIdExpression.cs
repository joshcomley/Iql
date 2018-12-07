namespace Iql
{
    public class IqlCurrentUserIdExpression : IqlSpecialValueExpression
    {
        public IqlCurrentUserIdExpression() : base(IqlExpressionKind.CurrentUserId)
        {
        }

        public override IqlExpression Clone()
        {
            // #CloneStart
            throw new System.NotImplementedException();
            // #CloneEnd
        }

        internal override IqlExpression ReplaceExpressions(ReplaceContext context)
        {
            // #ReplaceStart
            throw new System.NotImplementedException();
            // #ReplaceEnd
        }

        internal override void FlattenInternal(IqlFlattenContext context)
        {
            // #FlattenStart
            throw new System.NotImplementedException();
            // #FlattenEnd
        }
    }
}