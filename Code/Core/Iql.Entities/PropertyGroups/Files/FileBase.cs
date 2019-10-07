﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.Entities.PropertyGroups.Files
{
    public class FileBase : SimplePropertyGroupBase<IFile>, IFile
    {
        private Guid _guid;

        public override IProperty PrimaryProperty
        {
            get { return UrlProperty; }
        }

        protected override string ResolveName()
        {
            return UrlPropertyInternal?.Name ?? "File";
        }

        public FileBase(
            Guid guid,
            IProperty urlProperty = null,
            IProperty stateProperty = null,
            IProperty nameProperty = null,
            IProperty versionProperty = null,
            IProperty kindProperty = null,
            string key = null) : base(null, key)
        {
            StateProperty = stateProperty;
            UrlProperty = urlProperty;
            NameProperty = nameProperty;
            VersionProperty = versionProperty;
            KindProperty = kindProperty;
            Guid = guid;
        }

        public IFileState TryGetFileState(object entity)
        {
            return FileState.TryGetFileState(StateProperty, entity);
        }

        public bool TrySetFileState(object entity, IFileState state)
        {
            return FileState.TrySetFileState(StateProperty, entity, state);
        }

        IMediaKey IFileUrlBase.MediaKey
        {
            get => MediaKeyInternal;
            set => MediaKeyInternal = value;
        }
        protected IMediaKey MediaKeyInternal { get; set; }
        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.File;

        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]
            {
                new PropertyGroupMetadata(UrlProperty, PropertySearchKind.Primary),
                new PropertyGroupMetadata(KindProperty, PropertySearchKind.Secondary),
                new PropertyGroupMetadata(VersionProperty, PropertySearchKind.None),
                new PropertyGroupMetadata(UrlProperty, PropertySearchKind.None),
            };
        }

        public override IEntityConfiguration EntityConfiguration =>
            EntityConfigurationInternal;

        protected IEntityConfiguration EntityConfigurationInternal =>
            (UrlProperty ?? NameProperty ?? VersionProperty ?? KindProperty)?.EntityConfiguration;

        public override PropertyKind Kind { get; set; } = PropertyKind.SimpleCollection;
        public IList<IFilePreview> Previews { get; set; } = new List<IFilePreview>();
        public IFile RootFile => RootFileInternal;
        protected IFile RootFileInternal => this;
        protected IProperty UrlPropertyInternal { get; set; }
        protected IProperty StatePropertyInternal { get; set; }

        public Guid Guid
        {
            get => _guid;
            set
            {
                EntityConfiguration?.Builder.GuidManager.Assert(value, this);
                _guid = value;
            }
        }

        public IProperty UrlProperty
        {
            get => UrlPropertyInternal;
            set => UrlPropertyInternal = value;
        }

        public IProperty StateProperty
        {
            get => StatePropertyInternal;
            set => StatePropertyInternal = value;
        }

        public IProperty NameProperty { get; set; }
        public IProperty VersionProperty { get; set; }
        public IProperty KindProperty { get; set; }

        public override IPropertyGroup[] GetGroupProperties()
        {
            var list = new List<IPropertyGroup>();
            list.AddRange(new[] { UrlProperty, NameProperty, VersionProperty, KindProperty });
            if (Previews != null)
            {
                for (var i = 0; i < Previews.Count; i++)
                {
                    var preview = Previews[i];
                    list.Add(preview.UrlProperty);
                }
            }

            return list.Where(_ => _ != null).ToArray();
        }

        public FilePropertyKind GetPropertyKind(IProperty property)
        {
            if (property == UrlProperty)
            {
                return FilePropertyKind.FileUrl;
            }

            if (Previews?.Any(p => p.UrlProperty == property) == true)
            {
                return FilePropertyKind.PreviewUrl;
            }

            if (property == VersionProperty)
            {
                return FilePropertyKind.Version;
            }

            if (property == KindProperty)
            {
                return FilePropertyKind.Kind;
            }

            if (property == StateProperty)
            {
                return FilePropertyKind.State;
            }

            return FilePropertyKind.None;
        }
    }
}