using System;
using System.Collections.Generic;

namespace Iql.Entities.PropertyGroups.Files
{
    public interface IFile : ISimpleProperty, IConfigurable<IFile>, IFileUrlBase
    {
        IList<IFilePreview> Previews { get; set; }
        IProperty NameProperty { get; set; }
        IProperty VersionProperty { get; set; }
        IProperty KindProperty { get; set; }
        IqlFilePropertyKind GetPropertyKind(IProperty property);
    }
}