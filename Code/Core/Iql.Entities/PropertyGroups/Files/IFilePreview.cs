using System;

namespace Iql.Entities.PropertyGroups.Files
{
    public interface IFilePreview : IFileUrlBase
    {
        Guid Guid { get; set; }
        IFile File { get; set; }
        int? MaxWidth { get; set; }
        int? MaxHeight { get; set; }
        string Key { get; set; }
        IqlPreviewKind Kind { get; set; }
    }
}