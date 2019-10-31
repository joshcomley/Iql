namespace Iql.Entities.PropertyChangers
{
    public class PointPropertyChanger : ComplexPropertyChanger<IqlPointExpression>
    {
        public PointPropertyChanger()
        {
            CanSilentlyChange = true;
        }

        public static PointPropertyChanger Instance { get; } = new PointPropertyChanger();
        protected override bool CheckEquivalence(IqlPointExpression newValue, IqlPointExpression oldValue)
        {
            return oldValue.X == newValue.X && oldValue.Y == newValue.Y && oldValue.Srid == newValue.Srid;
        }

        protected override IqlPointExpression CloneValueInternal(IqlPointExpression value)
        {
            return new IqlPointExpression(value.X, value.Y, value.ReturnType, value.Srid);
        }

        //protected override void ApplyToInternal(IqlPointExpression value, IqlPointExpression applyTo)
        //{
        //    applyTo.Y = value.Y;
        //    applyTo.X = value.X;
        //    applyTo.Srid = value.Srid;
        //    applyTo.ReturnType = value.ReturnType;
        //}
    }
}