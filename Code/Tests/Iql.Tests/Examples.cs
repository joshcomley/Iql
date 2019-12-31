using System.Threading.Tasks;
using Iql.Tests.Context;

namespace Iql.Tests
{
    public class Examples
    {
        public async Task GetData()
        {
var db = new AppDbContext();

// Getting a list of data
var clients = await db
    .Clients
    .Where(client => client.Name.Contains("abc"))
    .Expand(client => client.People)
    .OrderBy(client => client.Name)
    .ToListAsync();

// Getting a specific entity
var someClient = await db.Clients.GetWithKeyAsync(123);

// Making changes
someClient.Name = "New name";
await db.SaveChangesAsync();

// Getting an entity state
var clientState = db.GetEntityState(someClient);

// Check for changes
var hasChanges = clientState.HasChanges;

// Monitor property changes
clientState.PropertyLocalValueChanged.Subscribe(e =>
{
    var propertyState = e.Owner;
    var propertyName = propertyState.Property.Name;
    var oldValue = e.OldValue;
    var newValue = e.NewValue;
});
        }
    }
}