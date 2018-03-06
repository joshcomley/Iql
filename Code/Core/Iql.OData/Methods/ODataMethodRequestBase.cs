namespace Iql.OData.Methods
{
    public class ODataMethodRequestBase
    {
        public ODataDataStore DataStore { get; }
        public string Uri { get; set; }

        public ODataMethodRequestBase(ODataDataStore dataStore, string uri)
        {
            DataStore = dataStore;
            Uri = uri;
        }
    }
}