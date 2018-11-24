using System;
using System.Linq.Expressions;

namespace Iql.Entities
{
    public abstract class PropertyService
    {
        public IEntityConfigurationBuilder Builder { get; }

        protected PropertyService(IEntityConfigurationBuilder builder)
        {
            Builder = builder;
        }

        public IProperty FindProperty<T>(Expression<Func<T, object>> expression)
            where T : class
        {
            var property = Builder.EntityType<T>().FindPropertyByExpression(expression);
            return property;
        }

        public IProperty FindPropertyByName<T>(string name)
            where T : class
        {
            var property = Builder.EntityType<T>().FindProperty(name);
            return property;
        }
    }
}