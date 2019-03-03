using Brandless.Data.EntityFramework.Crud;
using IqlSampleApp.Data;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {            
            var crudBase = new CrudBase<ApplicationDbContext, ApplicationDbContext, UserSetting>(null);
            //IqlServerEvaluator serverEvaluator = new IqlServerEvaluator(new CrudManager(), false);
            //T clone = (T)dbObject.Clone(this.Builder, this.EntityConfiguration.Type, RelationshipCloneMode.DoNotClone, (Dictionary<object, object>)null, (Dictionary<object, object>)null);
            //await base.OnPatchAsync(patch, dbObject);
            //InferredValuesResult inferredValuesResult = await EntityConfigurationExtensions.TrySetInferredValuesAsync((IEntityConfiguration)this.EntityConfiguration, (object)clone, (object)dbObject, (IIqlCustomEvaluator)serverEvaluator, this.ResolveServiceProviderProvider());
            //this.ClearNestedEntities(dbObject);
        }
    }
}
