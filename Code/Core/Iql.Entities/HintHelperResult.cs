namespace Iql.Entities
{
    public class HintHelperResult
    {
        public MetadataHint? Hint { get; set; }
        public IMetadata? Metadata { get; set; }
        public HintHelperResult(MetadataHint hint, IMetadata? metadata)
        {
            Hint = hint;
            Metadata = metadata;
        }
    }
}