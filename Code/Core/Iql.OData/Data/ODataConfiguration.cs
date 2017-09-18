using System.Collections.Generic;
using Iql.Queryable.Data.Http;

namespace Iql.OData.Data
{
    public class ODataConfiguration
    {
        private readonly Dictionary<string, string> _entitySets = new Dictionary<string, string>();
        public IHttpProvider HttpProvider { get; set; }
        public string ApiUriBase { get; set; }

        public void RegisterEntitySet<T>(string name)
        {
            _entitySets.Add(typeof(T).Name, name);
        }

        public string GetEntitySetName<T>()
        {
            return _entitySets[typeof(T).Name];
        }
    }
}