namespace Iql.Entities.PropertyChangers
{
    public class PointPropertyChanger : ComplexPropertyChanger<IqlPointExpression>
    {
        public PointPropertyChanger()
        {
            CanSilentlyChange = true;
        }
        private static PointPropertyChanger _instance;

        public static PointPropertyChanger Instance => _instance = _instance ?? new PointPropertyChanger();
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