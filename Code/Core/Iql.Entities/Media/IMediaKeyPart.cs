namespace Iql.Entities
{
    public interface IMediaKeyPart
    {
        IMediaKey MediaKey { get; set; }
        bool IsPropertyPath { get; set; }
        string Key { get; set; }
        IqlPropertyPath GetRelationshipPath();
    }
}