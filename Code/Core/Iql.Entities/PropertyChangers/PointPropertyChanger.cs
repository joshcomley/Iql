namespace Iql.Entities.PropertyChangers
{
    public class PointPropertyChanger : ComplexPropertyChanger<IqlPointExpression>
    {
        public static PointPropertyChanger Instance { get; } = new PointPropertyChanger();
        protected override bool CheckEquivalence(IqlPointExpression newValue, IqlPointExpression oldValue)
        {
            return oldValue.X == newValue.X && oldValue.Y == newValue.Y && oldValue.Srid == newValue.Srid;
        }
    }
}