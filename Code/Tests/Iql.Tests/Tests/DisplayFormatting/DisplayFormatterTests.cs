using Iql.Entities.DisplayFormatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.DisplayFormatting
{
    [TestClass]
    public class DisplayFormatterTests : TestsBase
    {
        [TestMethod]
        public void TestDisplayFormatter()
        {
            var person = new Person();
            person.Id = 7;
            person.Title = "Some guy";
            var personFormatted = Db.EntityConfigurationContext.EntityType<Person>().DisplayFormatting.Get("Report").Format(person);
            Assert.AreEqual($"{person.Title} ({person.Id})", personFormatted);
        }

        [TestMethod]
        public void TestDisplayFormatterWithInterception()
        {
            var person = new Person();
            person.Id = 7;
            person.Title = "Some guy";
            var formatter = Db.EntityConfigurationContext.EntityType<Person>().DisplayFormatting.Get("Report");
            var formatterContext = new CustomFormatterContext<Person>(person);
            var personFormatted = formatter
                .FormatAndInterceptWith(formatterContext);
            Assert.AreEqual($"{person.Title} ([{person.Id}])", personFormatted);
        }

        [TestMethod]
        public void TestDisplayFormatterWithInterceptionLong()
        {
            var person = new Person();
            person.Id = 7;
            person.Title = "Some guy";
            // Type.CreatedByUser.Client.Name
            person.Type = new PersonType();
            person.Type.CreatedByUser = new ApplicationUser();
            person.Type.CreatedByUser.Client = new Client();
            person.Type.CreatedByUser.Client.Name = "Bingo!";
            var formatter = Db.EntityConfigurationContext.EntityType<Person>().DisplayFormatting.Get("ReportLong");
            var formatterContext = new CustomFormatterContext<Person>(person);
            var personFormatted = formatter
                .FormatAndInterceptWith(formatterContext);
            Assert.AreEqual($"{person.Title} - Client:{person.Type.CreatedByUser.Client.Name} ([{person.Id}])", personFormatted);
        }

        [TestMethod]
        public void TestDisplayFormatterWithInterceptionLongInterface()
        {
            var person = new Person();
            person.Id = 7;
            person.Title = "Some guy";
            // Type.CreatedByUser.Client.Name
            person.Type = new PersonType();
            person.Type.CreatedByUser = new ApplicationUser();
            person.Type.CreatedByUser.Client = new Client();
            person.Type.CreatedByUser.Client.Name = "Bingo!";
            var formatter = (IEntityDisplayTextFormatter)Db.EntityConfigurationContext.EntityType<Person>().DisplayFormatting.Get("ReportLong");
            var formatterContext = new CustomFormatterContext<Person>(person);
            var personFormatted = formatter
                .FormatAndInterceptWith(formatterContext);
            Assert.AreEqual($"{person.Title} - Client:{person.Type.CreatedByUser.Client.Name} ([{person.Id}])", personFormatted);
        }

        [TestMethod]
        public void TestDisplayFormatterWithInterceptionLongInline()
        {
            var person = new Person();
            person.Id = 7;
            person.Title = "Some guy";
            // Type.CreatedByUser.Client.Name
            person.Type = new PersonType();
            person.Type.CreatedByUser = new ApplicationUser();
            person.Type.CreatedByUser.Client = new Client();
            person.Type.CreatedByUser.Client.Name = "Bingo!";
            var formatter = Db.EntityConfigurationContext.EntityType<Person>().DisplayFormatting.Get("ReportLong");
            var personFormatted = formatter
                .FormatAndIntercept(person, (context, expression, value) =>
                {
                    if (value is int)
                    {
                        return $"[{value}]";
                    }

                    if (expression.Kind == IqlExpressionKind.Property && expression.Parent.Kind == IqlExpressionKind.Property
                                                                      && (expression.Parent as IqlPropertyExpression)
                                                                      .PropertyName == nameof(ApplicationUser.Client))
                    {
                        return $"Client:{value}";
                    }
                    return context.Format(expression, value);
                });
            Assert.AreEqual($"{person.Title} - Client:{person.Type.CreatedByUser.Client.Name} ([{person.Id}])", personFormatted);
        }

        [TestMethod]
        public void TestDisplayFormatterWithInterceptionLongInlineInterface()
        {
            var person = new Person();
            person.Id = 7;
            person.Title = "Some guy";
            // Type.CreatedByUser.Client.Name
            person.Type = new PersonType();
            person.Type.CreatedByUser = new ApplicationUser();
            person.Type.CreatedByUser.Client = new Client();
            person.Type.CreatedByUser.Client.Name = "Bingo!";
            var formatter = (IEntityDisplayTextFormatter)Db.EntityConfigurationContext.EntityType<Person>().DisplayFormatting.Get("ReportLong");
            var personFormatted = formatter
                .FormatAndIntercept(person, (context, expression, value) =>
                {
                    if (value is int)
                    {
                        return $"[{value}]";
                    }

                    if (expression.Kind == IqlExpressionKind.Property && expression.Parent.Kind == IqlExpressionKind.Property
                                                                      && (expression.Parent as IqlPropertyExpression)
                                                                      .PropertyName == nameof(ApplicationUser.Client))
                    {
                        return $"Client:{value}";
                    }
                    return context.Format(expression, value);
                });
            Assert.AreEqual($"{person.Title} - Client:{person.Type.CreatedByUser.Client.Name} ([{person.Id}])", personFormatted);
        }
    }

    public class CustomFormatterContext<T> : FormatterContext<T>
    {
        public CustomFormatterContext(T entity) : base(entity)
        {
        }

        public override string Format(IqlExpression expression, object value)
        {
            if (value is int)
            {
                return $"[{value}]";
            }

            if (expression.Kind == IqlExpressionKind.Property && expression.Parent.Kind == IqlExpressionKind.Property
                                                              && (expression.Parent as IqlPropertyExpression)
                                                              .PropertyName == nameof(ApplicationUser.Client))
            {
                return $"Client:{value}";
            }
            return base.Format(expression, value);
        }
    }
}