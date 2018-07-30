namespace Iql.Entities.Dates
{
    public interface IFile : IPropertyGroup, IConfigurable<IFile>
    {
        IProperty FileUrlProperty { get; set; }
        IProperty PreviewUrlProperty { get; set; }
        IProperty NameProperty { get; set; }
        IProperty VersionProperty { get; set; }
        IProperty KindProperty { get; set; }
        FilePropertyKind GetPropertyKind(IProperty property);
        IMediaKey MediaKey { get; set; }
    }
}