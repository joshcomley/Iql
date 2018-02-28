using System.Diagnostics.CodeAnalysis;

namespace Iql.OData.Json
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class ODataErrorResult
    {
        public ODataError error { get; set; }
    }
}