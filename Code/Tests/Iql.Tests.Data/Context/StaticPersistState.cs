using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Data.Tracking;

namespace Iql.Tests.Context
{
    public class StaticPersistState : IPersistState
    {
        private static Dictionary<string, string> _state = new Dictionary<string, string>();
        public static bool UseDummyState { get; set; }
        public Task<bool> DeleteStateAsync(string key)
        {
            if (_state.ContainsKey(key))
            {
                _state.Remove(key);
            }
            return Task.FromResult(true);
        }

        public Task<bool> SaveStateAsync(string key, string state)
        {
            if (!_state.ContainsKey(key))
            {
                _state.Add(key, state);
            }
            else
            {
                _state[key] = state;
            }

            return Task.FromResult(true);
        }

        public Task<string> FetchStateAsync(string key)
        {
            if (UseDummyState)
            {
                switch (key)
                {
                    case "DataStore-OfflineData":
                        return Task.FromResult(@"[{""Type"":""Client"",""Entities"":[{""Id"":1,""TypeId"":2,""Name"":""New Client 123"",""AverageSales"":0.0,""AverageIncome"":0.0,""Category"":0,""Discount"":0.0,""Guid"":""00000000-0000-0000-0000-000000000000"",""CreatedDate"":""0001-01-01T00:00:00.0+00:00"",""PersistenceKey"":""23a792ac-cf55-4cae-bec7-9808cfad8f6a""}]}]");
                    case "DataTracker-Offline-Offline":
                        return
                            Task.FromResult(@"{""Sets"":[{""Type"":""Client"",""EntityStates"":[{""CurrentKey"":{""Keys"":[{""Name"":""Id"",""Value"":1}]},""IsNew"":true,""MarkedForDeletion"":false,""MarkedForCascadeDeletion"":false,""PropertyStates"":[{""RemoteValue"":1,""LocalValue"":1,""Property"":""Id""},{""RemoteValue"":2,""LocalValue"":2,""Property"":""TypeId""},{""Property"":""CreatedByUserId""},{""RemoteValue"":""New Client 123"",""LocalValue"":""New Client 123"",""Property"":""Name""},{""RemoteValue"":0.0,""LocalValue"":0.0,""Property"":""AverageSales""},{""RemoteValue"":0.0,""LocalValue"":0.0,""Property"":""AverageIncome""},{""RemoteValue"":0,""LocalValue"":0,""Property"":""Category""},{""Property"":""Description""},{""RemoteValue"":0.0,""LocalValue"":0.0,""Property"":""Discount""},{""RemoteValue"":""00000000-0000-0000-0000-000000000000"",""LocalValue"":""00000000-0000-0000-0000-000000000000"",""Property"":""Guid""},{""RemoteValue"":""0001-01-01T00:00:00+00:00"",""LocalValue"":""0001-01-01T00:00:00+00:00"",""Property"":""CreatedDate""},{""Property"":""RevisionKey""},{""RemoteValue"":""f17c722f-4aee-4fea-9b03-1bd4e03ada01"",""LocalValue"":""f17c722f-4aee-4fea-9b03-1bd4e03ada01"",""Property"":""PersistenceKey""}]}]}]}");
                }
            }
            if (!_state.ContainsKey(key))
            {
                return Task.FromResult<string>(null);
            }
            return Task.FromResult(_state[key]);
        }
    }
}