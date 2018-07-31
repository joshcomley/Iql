namespace Iql.Entities.PropertyGroups.Files
{
    public interface IFilePreview : IFileUrlBase
    {
        IFile File { get; set; }
        int? MaxWidth { get; set; }
        int? MaxHeight { get; set; }
        string Key { get; set; }
    }
}