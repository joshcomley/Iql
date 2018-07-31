namespace Iql.Entities.PropertyGroups.Files
{
    public interface IFileUrl<T> : IFileUrlBase
        where T : class
    {
        File<T> RootFile { get; }
        IEntityProperty<T> UrlProperty { get; set; }
        MediaKey<T> MediaKey { get; set; }
    }
}