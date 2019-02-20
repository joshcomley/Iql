using System.Threading.Tasks;

namespace Iql.Data.Tracking
{
    public interface IPersistState
    {
        Task<bool> DeleteStateAsync(string key);
        Task<bool> SaveStateAsync(string key, string state);
        Task<string> FetchStateAsync(string key);
    }
}