using System.Diagnostics.CodeAnalysis;

namespace Iql.OData.Json
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class ODataError
    {
        public string code { get; set; }
        public string message { get; set; }
        public string target { get; set; }
        public ODataError[] details { get; set; }
    }
}