using System.Linq;
using System.Threading.Tasks;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OrderByTests
{
    [TestClass]
    public class OrderByTests : TestsBase
    {
        [TestMethod]
        public async Task TestOrderByDefault()
        {
            var array = Db.EntityConfigurationContext.AllEntityTypes().ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                var type = array[i];
                var set = Db.GetDbSetByEntityType(type.Type);
                if (set != null)
                {
                    var list = await set.OrderByDefault().ToListAsync();
                    Assert.IsNotNull(list);
                }
            }
        }

        [TestMethod]
        public async Task TestOrderByProperty()
        {
            var query = Db.Clients.OrderByProperty($"{nameof(Client.CreatedByUser)}/{nameof(ApplicationUser.FullName)}");
            var list = await query.ToListAsync();
            Assert.IsNotNull(list);
        }
    }
}