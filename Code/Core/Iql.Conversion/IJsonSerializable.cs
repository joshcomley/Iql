namespace Iql.Conversion
{
    public interface IJsonSerializable
    {
        string SerializeToJson();
        object PrepareForJson();
    }
}