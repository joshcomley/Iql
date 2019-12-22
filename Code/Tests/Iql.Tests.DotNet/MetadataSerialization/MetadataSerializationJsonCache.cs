namespace Iql.Tests.Tests.MetadataSerialization
{
    public class MetadataSerializationJsonCache
    {
        public const string Json = @"{
  ""UsersDefinition"": null,
  ""CustomReportsDefinition"": {
    ""UserIdProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MyUserId\"",\""Kind\"":1,\""Children\"":null}"",
    ""NameProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MyName\"",\""Kind\"":1,\""Children\"":null}"",
    ""EntityTypeProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MyEntityType\"",\""Kind\"":1,\""Children\"":null}"",
    ""IqlProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MyIql\"",\""Kind\"":1,\""Children\"":null}"",
    ""FieldsProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MyFields\"",\""Kind\"":1,\""Children\"":null}"",
    ""SortProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MySort\"",\""Kind\"":1,\""Children\"":null}"",
    ""SortDescendingProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MySortDescending\"",\""Kind\"":1,\""Children\"":null}"",
    ""SearchProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MySearch\"",\""Kind\"":1,\""Children\"":null}"",
    ""IdProperty"": ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MyId\"",\""Kind\"":1,\""Children\"":null}"",
    ""Metadata"": {
      ""All"": []
    },
    ""Name"": null,
    ""Title"": null,
    ""FriendlyName"": null,
    ""GroupPath"": null,
    ""EditDisplay"": [],
    ""ReadDisplay"": [],
    ""Description"": null,
    ""Hints"": [],
    ""HelpTexts"": []
  },
  ""UserSettingsDefinition"": null,
  ""EnumTypes"": [],
  ""EntityTypes"": [
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""FullName"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Users"",
      ""SetName"": ""Users"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""SearchKind"": 1,
  ""Name"": ""IsLockedOut"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__IsLockedOut__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""IsLockedOut"",
  ""Title"": ""IsLockedOut"",
  ""FriendlyName"": ""Is Locked Out"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ClientId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ClientId"",
  ""Title"": ""ClientId"",
  ""FriendlyName"": ""Client Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Email"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Email__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Email"",
  ""Title"": ""Email"",
  ""FriendlyName"": ""Email"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:EmailAddress""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 9
  },
  ""SearchKind"": 1,
  ""Name"": ""Permissions"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Permissions__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Permissions"",
  ""Title"": ""Permissions"",
  ""FriendlyName"": ""Permissions"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 9
  },
  ""SearchKind"": 1,
  ""Name"": ""UserType"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__UserType__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""UserType"",
  ""Title"": ""UserType"",
  ""FriendlyName"": ""User Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""FullName"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FullName__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FullName"",
  ""Title"": ""FullName"",
  ""FriendlyName"": ""Full Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""UserName"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__UserName__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""UserName"",
  ""Title"": ""UserName"",
  ""FriendlyName"": ""User Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""SearchKind"": 1,
  ""Name"": ""EmailConfirmed"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__EmailConfirmed__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""EmailConfirmed"",
  ""Title"": ""EmailConfirmed"",
  ""FriendlyName"": ""Email Confirmed"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""PhoneNumber"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PhoneNumber__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PhoneNumber"",
  ""Title"": ""PhoneNumber"",
  ""FriendlyName"": ""Phone Number"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""SearchKind"": 1,
  ""Name"": ""PhoneNumberConfirmed"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PhoneNumberConfirmed__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PhoneNumberConfirmed"",
  ""Title"": ""PhoneNumberConfirmed"",
  ""FriendlyName"": ""Phone Number Confirmed"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""SearchKind"": 1,
  ""Name"": ""TwoFactorEnabled"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TwoFactorEnabled__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""TwoFactorEnabled"",
  ""Title"": ""TwoFactorEnabled"",
  ""FriendlyName"": ""Two Factor Enabled"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""LockoutEnd"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__LockoutEnd__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""LockoutEnd"",
  ""Title"": ""LockoutEnd"",
  ""FriendlyName"": ""Lockout End"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""SearchKind"": 1,
  ""Name"": ""LockoutEnabled"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__LockoutEnabled__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""LockoutEnabled"",
  ""Title"": ""LockoutEnabled"",
  ""FriendlyName"": ""Lockout Enabled"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Client"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Client__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""ClientsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ClientsCreated"",
  ""Title"": ""ClientsCreated"",
  ""FriendlyName"": ""Clients Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ClientsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ClientsCreatedCount"",
  ""Title"": ""ClientsCreatedCount"",
  ""FriendlyName"": ""Clients Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""DocumentCategoriesCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__DocumentCategoriesCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""DocumentCategoriesCreated"",
  ""Title"": ""DocumentCategoriesCreated"",
  ""FriendlyName"": ""Document Categories Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""DocumentCategoriesCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__DocumentCategoriesCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""DocumentCategoriesCreatedCount"",
  ""Title"": ""DocumentCategoriesCreatedCount"",
  ""FriendlyName"": ""Document Categories Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteDocumentsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteDocumentsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteDocumentsCreated"",
  ""Title"": ""SiteDocumentsCreated"",
  ""FriendlyName"": ""Site Documents Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteDocumentsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteDocumentsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteDocumentsCreatedCount"",
  ""Title"": ""SiteDocumentsCreatedCount"",
  ""FriendlyName"": ""Site Documents Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultActionsTakenCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultActionsTakenCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultActionsTakenCreated"",
  ""Title"": ""FaultActionsTakenCreated"",
  ""FriendlyName"": ""Fault Actions Taken Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultActionsTakenCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultActionsTakenCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultActionsTakenCreatedCount"",
  ""Title"": ""FaultActionsTakenCreatedCount"",
  ""FriendlyName"": ""Fault Actions Taken Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultCategoriesCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultCategoriesCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultCategoriesCreated"",
  ""Title"": ""FaultCategoriesCreated"",
  ""FriendlyName"": ""Fault Categories Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultCategoriesCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultCategoriesCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultCategoriesCreatedCount"",
  ""Title"": ""FaultCategoriesCreatedCount"",
  ""FriendlyName"": ""Fault Categories Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultDefaultRecommendationsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultDefaultRecommendationsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultDefaultRecommendationsCreated"",
  ""Title"": ""FaultDefaultRecommendationsCreated"",
  ""FriendlyName"": ""Fault Default Recommendations Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultDefaultRecommendationsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultDefaultRecommendationsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultDefaultRecommendationsCreatedCount"",
  ""Title"": ""FaultDefaultRecommendationsCreatedCount"",
  ""FriendlyName"": ""Fault Default Recommendations Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultRecommendationsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultRecommendationsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultRecommendationsCreated"",
  ""Title"": ""FaultRecommendationsCreated"",
  ""FriendlyName"": ""Fault Recommendations Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultRecommendationsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultRecommendationsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultRecommendationsCreatedCount"",
  ""Title"": ""FaultRecommendationsCreatedCount"",
  ""FriendlyName"": ""Fault Recommendations Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultTypesCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultTypesCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultTypesCreated"",
  ""Title"": ""FaultTypesCreated"",
  ""FriendlyName"": ""Fault Types Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultTypesCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultTypesCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultTypesCreatedCount"",
  ""Title"": ""FaultTypesCreatedCount"",
  ""FriendlyName"": ""Fault Types Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""ProjectCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ProjectCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ProjectCreated"",
  ""Title"": ""ProjectCreated"",
  ""FriendlyName"": ""Project Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ProjectCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ProjectCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ProjectCreatedCount"",
  ""Title"": ""ProjectCreatedCount"",
  ""FriendlyName"": ""Project Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""ReportReceiverEmailAddressesCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReportReceiverEmailAddressesCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportReceiverEmailAddressesCreated"",
  ""Title"": ""ReportReceiverEmailAddressesCreated"",
  ""FriendlyName"": ""Report Receiver Email Addresses Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ReportReceiverEmailAddressesCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReportReceiverEmailAddressesCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportReceiverEmailAddressesCreatedCount"",
  ""Title"": ""ReportReceiverEmailAddressesCreatedCount"",
  ""FriendlyName"": ""Report Receiver Email Addresses Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentsCreated"",
  ""Title"": ""RiskAssessmentsCreated"",
  ""FriendlyName"": ""Risk Assessments Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentsCreatedCount"",
  ""Title"": ""RiskAssessmentsCreatedCount"",
  ""FriendlyName"": ""Risk Assessments Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentSolutionsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentSolutionsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentSolutionsCreated"",
  ""Title"": ""RiskAssessmentSolutionsCreated"",
  ""FriendlyName"": ""Risk Assessment Solutions Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentSolutionsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentSolutionsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentSolutionsCreatedCount"",
  ""Title"": ""RiskAssessmentSolutionsCreatedCount"",
  ""FriendlyName"": ""Risk Assessment Solutions Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentAnswersCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentAnswersCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentAnswersCreated"",
  ""Title"": ""RiskAssessmentAnswersCreated"",
  ""FriendlyName"": ""Risk Assessment Answers Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentAnswersCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentAnswersCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentAnswersCreatedCount"",
  ""Title"": ""RiskAssessmentAnswersCreatedCount"",
  ""FriendlyName"": ""Risk Assessment Answers Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentQuestionsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentQuestionsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentQuestionsCreated"",
  ""Title"": ""RiskAssessmentQuestionsCreated"",
  ""FriendlyName"": ""Risk Assessment Questions Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentQuestionsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentQuestionsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentQuestionsCreatedCount"",
  ""Title"": ""RiskAssessmentQuestionsCreatedCount"",
  ""FriendlyName"": ""Risk Assessment Questions Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCreated"",
  ""Title"": ""PeopleCreated"",
  ""FriendlyName"": ""People Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCreatedCount"",
  ""Title"": ""PeopleCreatedCount"",
  ""FriendlyName"": ""People Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonInspectionsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonInspectionsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonInspectionsCreated"",
  ""Title"": ""PersonInspectionsCreated"",
  ""FriendlyName"": ""Person Inspections Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonInspectionsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonInspectionsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonInspectionsCreatedCount"",
  ""Title"": ""PersonInspectionsCreatedCount"",
  ""FriendlyName"": ""Person Inspections Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonLoadingsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonLoadingsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonLoadingsCreated"",
  ""Title"": ""PersonLoadingsCreated"",
  ""FriendlyName"": ""Person Loadings Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonLoadingsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonLoadingsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonLoadingsCreatedCount"",
  ""Title"": ""PersonLoadingsCreatedCount"",
  ""FriendlyName"": ""Person Loadings Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonTypesCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonTypesCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonTypesCreated"",
  ""Title"": ""PersonTypesCreated"",
  ""FriendlyName"": ""Person Types Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonTypesCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonTypesCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonTypesCreatedCount"",
  ""Title"": ""PersonTypesCreatedCount"",
  ""FriendlyName"": ""Person Types Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultReportsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultReportsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReportsCreated"",
  ""Title"": ""FaultReportsCreated"",
  ""FriendlyName"": ""Fault Reports Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultReportsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultReportsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReportsCreatedCount"",
  ""Title"": ""FaultReportsCreatedCount"",
  ""FriendlyName"": ""Fault Reports Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""SitesCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SitesCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SitesCreated"",
  ""Title"": ""SitesCreated"",
  ""FriendlyName"": ""Sites Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SitesCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SitesCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SitesCreatedCount"",
  ""Title"": ""SitesCreatedCount"",
  ""FriendlyName"": ""Sites Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteAreasCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteAreasCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteAreasCreated"",
  ""Title"": ""SiteAreasCreated"",
  ""FriendlyName"": ""Site Areas Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteAreasCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteAreasCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteAreasCreatedCount"",
  ""Title"": ""SiteAreasCreatedCount"",
  ""FriendlyName"": ""Site Areas Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteInspectionsCreated"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionsCreated__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspectionsCreated"",
  ""Title"": ""SiteInspectionsCreated"",
  ""FriendlyName"": ""Site Inspections Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteInspectionsCreatedCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionsCreatedCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspectionsCreatedCount"",
  ""Title"": ""SiteInspectionsCreatedCount"",
  ""FriendlyName"": ""Site Inspections Created Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Sites"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Sites__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Sites"",
  ""Title"": ""Sites"",
  ""FriendlyName"": ""Sites"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SitesCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SitesCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SitesCount"",
  ""Title"": ""SitesCount"",
  ""FriendlyName"": ""Sites Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [
    {
      ""Container"": null,
      ""Expression"": {
        ""Body"": {
          ""Value"": 12.0,
          ""InferredReturnType"": 6,
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 26,
          ""ReturnType"": 6,
          ""Parent"": null
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""ApplicationUser"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""RelationshipMappings"": [
    {
      ""Container"": null,
      ""Expression"": {
        ""Body"": {
          ""Name"": ""CreatedByUser"",
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 30,
          ""ReturnType"": 1,
          ""Parent"": {
            ""EntityTypeName"": ""ApplicationUser"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""ApplicationUser"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Client\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""UsersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId_Client__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Users\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""UsersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Users__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Users"",
  ""Title"": ""Users"",
  ""FriendlyName"": ""Users"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ClientsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ClientsCreated"",
  ""Title"": ""Clients Created"",
  ""FriendlyName"": ""Clients Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""DocumentCategoriesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""DocumentCategoriesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""DocumentCategoriesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_DocumentCategoriesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""DocumentCategoriesCreated"",
  ""Title"": ""Document Categories Created"",
  ""FriendlyName"": ""Document Categories Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteDocumentsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteDocumentsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteDocumentsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SiteDocumentsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteDocumentsCreated"",
  ""Title"": ""Site Documents Created"",
  ""FriendlyName"": ""Site Documents Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultActionsTakenCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultActionsTakenCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultActionsTakenCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultActionsTakenCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultActionsTakenCreated"",
  ""Title"": ""Fault Actions Taken Created"",
  ""FriendlyName"": ""Fault Actions Taken Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultCategoriesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultCategoriesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultCategoriesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultCategoriesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultCategoriesCreated"",
  ""Title"": ""Fault Categories Created"",
  ""FriendlyName"": ""Fault Categories Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultDefaultRecommendationsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultDefaultRecommendationsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultDefaultRecommendationsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultDefaultRecommendationsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultDefaultRecommendationsCreated"",
  ""Title"": ""Fault Default Recommendations Created"",
  ""FriendlyName"": ""Fault Default Recommendations Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultRecommendationsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultRecommendationsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultRecommendationsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultRecommendationsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultRecommendationsCreated"",
  ""Title"": ""Fault Recommendations Created"",
  ""FriendlyName"": ""Fault Recommendations Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultTypesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultTypesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultTypesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultTypesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultTypesCreated"",
  ""Title"": ""Fault Types Created"",
  ""FriendlyName"": ""Fault Types Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Project\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Project\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ProjectCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ProjectCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ProjectCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ProjectCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ProjectCreated"",
  ""Title"": ""Project Created"",
  ""FriendlyName"": ""Project Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ReportReceiverEmailAddressesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ReportReceiverEmailAddressesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ReportReceiverEmailAddressesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ReportReceiverEmailAddressesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportReceiverEmailAddressesCreated"",
  ""Title"": ""Report Receiver Email Addresses Created"",
  ""FriendlyName"": ""Report Receiver Email Addresses Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentsCreated"",
  ""Title"": ""Risk Assessments Created"",
  ""FriendlyName"": ""Risk Assessments Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentSolutionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentSolutionsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentSolutionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentSolutionsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentSolutionsCreated"",
  ""Title"": ""Risk Assessment Solutions Created"",
  ""FriendlyName"": ""Risk Assessment Solutions Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentAnswersCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentAnswersCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentAnswersCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentAnswersCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentAnswersCreated"",
  ""Title"": ""Risk Assessment Answers Created"",
  ""FriendlyName"": ""Risk Assessment Answers Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentQuestionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentQuestionsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentQuestionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentQuestionsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentQuestionsCreated"",
  ""Title"": ""Risk Assessment Questions Created"",
  ""FriendlyName"": ""Risk Assessment Questions Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PeopleCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PeopleCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PeopleCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PeopleCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCreated"",
  ""Title"": ""People Created"",
  ""FriendlyName"": ""People Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonInspectionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonInspectionsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonInspectionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PersonInspectionsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonInspectionsCreated"",
  ""Title"": ""Person Inspections Created"",
  ""FriendlyName"": ""Person Inspections Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonLoadingsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonLoadingsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonLoadingsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PersonLoadingsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonLoadingsCreated"",
  ""Title"": ""Person Loadings Created"",
  ""FriendlyName"": ""Person Loadings Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonTypesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonTypesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonTypesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PersonTypesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonTypesCreated"",
  ""Title"": ""Person Types Created"",
  ""FriendlyName"": ""Person Types Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultReportsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultReportsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultReportsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultReportsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReportsCreated"",
  ""Title"": ""Fault Reports Created"",
  ""FriendlyName"": ""Fault Reports Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SitesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SitesCreated"",
  ""Title"": ""Sites Created"",
  ""FriendlyName"": ""Sites Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteAreasCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteAreasCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteAreasCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SiteAreasCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteAreasCreated"",
  ""Title"": ""Site Areas Created"",
  ""FriendlyName"": ""Site Areas Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteInspectionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteInspectionsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteInspectionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SiteInspectionsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspectionsCreated"",
  ""Title"": ""Site Inspections Created"",
  ""FriendlyName"": ""Site Inspections Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""UserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""User\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__UserId_User__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""User"",
  ""Title"": ""User"",
  ""FriendlyName"": ""User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Sites\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Sites__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Sites"",
  ""Title"": ""Sites"",
  ""FriendlyName"": ""Sites"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ApplicationUser"",
      ""Title"": ""ApplicationUser"",
      ""FriendlyName"": ""Application User"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [
        {
  ""LongitudeProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""AverageIncome\"",\""Kind\"":1,\""Children\"":null}"",
  ""LatitudeProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""AverageIncome\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__AverageIncome_AverageIncome__"",
  ""Key"": ""MyGeographic"",
  ""Kind"": 8,
  ""RelationshipFilterRules"": null,
  ""ValidationRules"": null,
  ""DisplayRules"": null,
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Location"",
  ""Title"": ""Location"",
  ""FriendlyName"": ""Location"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:BigText""
  ],
  ""HelpTexts"": []
}
      ],
      ""NestedSets"": [
        {
  ""SetKey"": ""MyNestedSet"",
  ""LeftProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""AverageIncome\"",\""Kind\"":1,\""Children\"":null}"",
  ""RightProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""AverageSales\"",\""Kind\"":1,\""Children\"":null}"",
  ""LeftOfProperty"": null,
  ""RightOfProperty"": null,
  ""KeyProperty"": null,
  ""LevelProperty"": null,
  ""ParentIdProperty"": null,
  ""ParentProperty"": null,
  ""IdProperty"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__AverageIncome_AverageSales__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": null,
  ""ValidationRules"": null,
  ""DisplayRules"": null,
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Hierarchy"",
  ""Title"": ""Hierarchy"",
  ""FriendlyName"": ""Hierarchy"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Clients"",
      ""SetName"": ""Clients"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [
        ""{\""Type\"":\""Client\"",\""Paths\"":\""Name\"",\""Kind\"":1,\""Children\"":null}"",
        {
          ""Enclose"": true,
          ""ContentAlignment"": 2,
          ""Kind"": 16,
          ""Properties"": [
            {
              ""Kind"": 8,
              ""Path"": ""Type/Name"",
              ""Internal"": false,
              ""IsHiddenOrInternal"": false,
              ""IsReadOnly"": false,
              ""IsHidden"": false,
              ""EditKindChanged"": {},
              ""ReadKindChanged"": {},
              ""ReadKind"": 1,
              ""EditKind"": 1,
              ""SupportsInlineEditing"": true,
              ""PromptBeforeEdit"": false,
              ""Placeholder"": null,
              ""Sortable"": true,
              ""Key"": null,
              ""GroupName"": ""__Name__"",
              ""RelationshipFilterRules"": null,
              ""ValidationRules"": null,
              ""DisplayRules"": null,
              ""Metadata"": {
                ""All"": []
              },
              ""Name"": null,
              ""Title"": null,
              ""FriendlyName"": null,
              ""GroupPath"": null,
              ""EditDisplay"": [],
              ""ReadDisplay"": [],
              ""Description"": null,
              ""Hints"": [],
              ""HelpTexts"": []
            },
            ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}"",
            ""{\""Type\"":\""Client\"",\""Paths\"":\""Type\"",\""Kind\"":6,\""Children\"":null}"",
            ""{\""Type\"":\""Client\"",\""Paths\"":\""0\"",\""Kind\"":4,\""Children\"":null}"",
            {
              ""Enclose"": true,
              ""ContentAlignment"": 1,
              ""Kind"": 16,
              ""Properties"": [
                ""{\""Type\"":\""Client\"",\""Paths\"":\""Description\"",\""Kind\"":1,\""Children\"":null}"",
                ""{\""Type\"":\""Client\"",\""Paths\"":\""Category\"",\""Kind\"":1,\""Children\"":null}""
              ],
              ""Key"": null,
              ""GroupName"": ""__Description_Category__"",
              ""RelationshipFilterRules"": null,
              ""ValidationRules"": null,
              ""DisplayRules"": null,
              ""Metadata"": {
                ""All"": []
              },
              ""Name"": null,
              ""Title"": null,
              ""FriendlyName"": null,
              ""GroupPath"": null,
              ""EditDisplay"": [],
              ""ReadDisplay"": [],
              ""Description"": null,
              ""Hints"": [],
              ""HelpTexts"": []
            }
          ],
          ""Key"": null,
          ""GroupName"": ""___Id_Type_Location___"",
          ""RelationshipFilterRules"": null,
          ""ValidationRules"": null,
          ""DisplayRules"": null,
          ""Metadata"": {
            ""All"": []
          },
          ""Name"": null,
          ""Title"": null,
          ""FriendlyName"": null,
          ""GroupPath"": null,
          ""EditDisplay"": [],
          ""ReadDisplay"": [],
          ""Description"": null,
          ""Hints"": [
            ""Iql:HelpText:Bottom""
          ],
          ""HelpTexts"": []
        },
        ""{\""Type\"":\""Client\"",\""Paths\"":\""0\"",\""Kind\"":5,\""Children\"":null}""
      ],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Users"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Users__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Users"",
  ""Title"": ""Users"",
  ""FriendlyName"": ""Users"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""TypeId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""TypeId"",
  ""Title"": ""TypeId"",
  ""FriendlyName"": ""Type Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 6
  },
  ""SearchKind"": 1,
  ""Name"": ""AverageSales"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__AverageSales__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""AverageSales"",
  ""Title"": ""AverageSales"",
  ""FriendlyName"": ""Average Sales"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 6
  },
  ""SearchKind"": 1,
  ""Name"": ""AverageIncome"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__AverageIncome__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""AverageIncome"",
  ""Title"": ""AverageIncome"",
  ""FriendlyName"": ""Average Income"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Category"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Category__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""FriendlyName"": ""Category"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Description"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Description__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""FriendlyName"": ""Description"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 6
  },
  ""SearchKind"": 1,
  ""Name"": ""Discount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Discount__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Discount"",
  ""Title"": ""Discount"",
  ""FriendlyName"": ""Discount"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""UsersCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__UsersCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""UsersCount"",
  ""Title"": ""UsersCount"",
  ""FriendlyName"": ""Users Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Type"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Type__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""People"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__People__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCount"",
  ""Title"": ""PeopleCount"",
  ""FriendlyName"": ""People Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Sites"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Sites__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Sites"",
  ""Title"": ""Sites"",
  ""FriendlyName"": ""Sites"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SitesCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SitesCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SitesCount"",
  ""Title"": ""SitesCount"",
  ""FriendlyName"": ""Sites Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [
    {
      ""Container"": null,
      ""Expression"": {
        ""Body"": {
          ""Value"": 12.0,
          ""InferredReturnType"": 6,
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 26,
          ""ReturnType"": 6,
          ""Parent"": null
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""ApplicationUser"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""RelationshipMappings"": [
    {
      ""Container"": null,
      ""Expression"": {
        ""Body"": {
          ""Name"": ""CreatedByUser"",
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 30,
          ""ReturnType"": 1,
          ""Parent"": {
            ""EntityTypeName"": ""ApplicationUser"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""ApplicationUser"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Client\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""UsersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId_Client__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Users\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""UsersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Users__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Users"",
  ""Title"": ""Users"",
  ""FriendlyName"": ""Users"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Type\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""ClientsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId_Type__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Clients\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""ClientsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Clients__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Clients"",
  ""Title"": ""Clients"",
  ""FriendlyName"": ""Clients"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ClientType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ClientsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ClientsCreated"",
  ""Title"": ""Clients Created"",
  ""FriendlyName"": ""Clients Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""ClientId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Client\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId_Client__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""ClientId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [
    {
      ""Container"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Name\"",\""Kind\"":1,\""Children\"":null}"",
      ""Expression"": {
        ""Body"": {
          ""Value"": ""test@123.com"",
          ""InferredReturnType"": 4,
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 26,
          ""ReturnType"": 4,
          ""Parent"": null
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""Site"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""RelationshipMappings"": [
    {
      ""Container"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Type\"",\""Kind\"":6,\""Children\"":null}"",
      ""Expression"": {
        ""Body"": {
          ""Name"": ""Type"",
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 30,
          ""ReturnType"": 1,
          ""Parent"": {
            ""Name"": ""Client"",
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 30,
            ""ReturnType"": 1,
            ""Parent"": {
              ""EntityTypeName"": ""Site"",
              ""VariableName"": ""_"",
              ""Value"": null,
              ""InferredReturnType"": 1,
              ""IsIqlExpression"": true,
              ""Key"": null,
              ""Kind"": 28,
              ""ReturnType"": 1,
              ""Parent"": null
            }
          }
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""Site"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""AllowInlineEditing"": true,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Client\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""SitesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId_Client__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": [
      {
        ""Key"": ""NumberSeven"",
        ""Value"": 7
      }
    ]
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": true,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Sites\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""SitesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Sites__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Sites"",
  ""Title"": ""Sites"",
  ""FriendlyName"": ""Sites"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        }
      ],
      ""Files"": [
        {
  ""Previews"": [
    {
      ""MaxWidth"": 200,
      ""MaxHeight"": null,
      ""Key"": null,
      ""UrlProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Name\"",\""Kind\"":1,\""Children\"":null}"",
      ""MediaKey"": {
        ""Separator"": ""/"",
        ""Groups"": [
          {
            ""Separator"": ""-"",
            ""Parts"": [
              {
                ""IsPropertyPath"": true,
                ""Key"": ""CreatedByUser/Id""
              },
              {
                ""IsPropertyPath"": false,
                ""Key"": ""sub-mk-test""
              }
            ]
          }
        ]
      },
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": null,
      ""Title"": null,
      ""FriendlyName"": null,
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    }
  ],
  ""NameProperty"": null,
  ""VersionProperty"": null,
  ""KindProperty"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Description_Name__"",
  ""Key"": ""my-file"",
  ""Kind"": 8,
  ""RelationshipFilterRules"": null,
  ""ValidationRules"": null,
  ""DisplayRules"": null,
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""FriendlyName"": ""Description"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": [],
  ""UrlProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Description\"",\""Kind\"":1,\""Children\"":null}"",
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": [
      {
        ""Separator"": ""-"",
        ""Parts"": [
          {
            ""IsPropertyPath"": true,
            ""Key"": ""CreatedByUser/Id""
          },
          {
            ""IsPropertyPath"": false,
            ""Key"": ""root-mk-test""
          }
        ]
      }
    ]
  }
}
      ],
      ""Metadata"": {
        ""All"": [
          {
            ""Key"": ""abc"",
            ""Value"": 123
          }
        ]
      },
      ""Name"": ""Client"",
      ""Title"": ""Client"",
      ""FriendlyName"": ""Client"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ApplicationLog\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Module"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Application Logs"",
      ""SetName"": ""ApplicationLogs"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Module"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Module__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Module"",
  ""Title"": ""Module"",
  ""FriendlyName"": ""Module"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Message"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Message__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Message"",
  ""Title"": ""Message"",
  ""FriendlyName"": ""Message"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Kind"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Kind__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Kind"",
  ""Title"": ""Kind"",
  ""FriendlyName"": ""Kind"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ApplicationLog"",
      ""Title"": ""ApplicationLog"",
      ""FriendlyName"": ""Application Log"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Client Types"",
      ""SetName"": ""ClientTypes"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Clients"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Clients__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Clients"",
  ""Title"": ""Clients"",
  ""FriendlyName"": ""Clients"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ClientsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ClientsCount"",
  ""Title"": ""ClientsCount"",
  ""FriendlyName"": ""Clients Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Type\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""ClientsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId_Type__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Clients\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""ClientsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Clients__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Clients"",
  ""Title"": ""Clients"",
  ""FriendlyName"": ""Clients"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ClientType:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ClientType"",
      ""Title"": ""ClientType"",
      ""FriendlyName"": ""Client Type"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Document Categories"",
      ""SetName"": ""DocumentCategories"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Documents"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Documents__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Documents"",
  ""Title"": ""Documents"",
  ""FriendlyName"": ""Documents"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""DocumentsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__DocumentsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""DocumentsCount"",
  ""Title"": ""DocumentsCount"",
  ""FriendlyName"": ""Documents Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""DocumentCategoriesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""DocumentCategoriesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""DocumentCategoriesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_DocumentCategoriesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""DocumentCategoriesCreated"",
  ""Title"": ""Document Categories Created"",
  ""FriendlyName"": ""Document Categories Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CategoryId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Category\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""DocumentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CategoryId_Category__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""FriendlyName"": ""Category"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Documents\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""DocumentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Documents__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Documents"",
  ""Title"": ""Documents"",
  ""FriendlyName"": ""Documents"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""DocumentCategory:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""DocumentCategory"",
      ""Title"": ""DocumentCategory"",
      ""FriendlyName"": ""Document Category"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Title"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Site Documents"",
      ""SetName"": ""SiteDocuments"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""CategoryId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CategoryId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CategoryId"",
  ""Title"": ""CategoryId"",
  ""FriendlyName"": ""Category Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""FriendlyName"": ""Site Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Title"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Title__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Title"",
  ""Title"": ""Title"",
  ""FriendlyName"": ""Title"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Category"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Category__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""FriendlyName"": ""Category"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Site"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Site__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CategoryId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Category\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""DocumentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CategoryId_Category__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""FriendlyName"": ""Category"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Documents\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""DocumentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Documents__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Documents"",
  ""Title"": ""Documents"",
  ""FriendlyName"": ""Documents"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""DocumentCategory:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""DocumentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Documents\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""DocumentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Documents__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Documents"",
  ""Title"": ""Documents"",
  ""FriendlyName"": ""Documents"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteDocumentsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteDocumentsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteDocumentsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SiteDocumentsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteDocumentsCreated"",
  ""Title"": ""Site Documents Created"",
  ""FriendlyName"": ""Site Documents Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""SiteDocument"",
      ""Title"": ""SiteDocument"",
      ""FriendlyName"": ""Site Document"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""FullAddress"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Sites"",
      ""SetName"": ""Sites"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Documents"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Documents__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Documents"",
  ""Title"": ""Documents"",
  ""FriendlyName"": ""Documents"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""AdditionalSendReportsTo"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__AdditionalSendReportsTo__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""AdditionalSendReportsTo"",
  ""Title"": ""AdditionalSendReportsTo"",
  ""FriendlyName"": ""Additional Send Reports To"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""People"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__People__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 20
  },
  ""SearchKind"": 1,
  ""Name"": ""Location"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Location__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Location"",
  ""Title"": ""Location"",
  ""FriendlyName"": ""Location"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 22
  },
  ""SearchKind"": 1,
  ""Name"": ""Area"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Area__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Area"",
  ""Title"": ""Area"",
  ""FriendlyName"": ""Area"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 24
  },
  ""SearchKind"": 1,
  ""Name"": ""Line"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Line__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Line"",
  ""Title"": ""Line"",
  ""FriendlyName"": ""Line"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ParentId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ParentId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ParentId"",
  ""Title"": ""ParentId"",
  ""FriendlyName"": ""Parent Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ClientId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ClientId"",
  ""Title"": ""ClientId"",
  ""FriendlyName"": ""Client Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""FullAddress"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": {
    ""Body"": {
      ""Left"": {
        ""Left"": {
          ""Name"": ""Address"",
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 30,
          ""ReturnType"": 1,
          ""Parent"": {
            ""EntityTypeName"": ""Site"",
            ""VariableName"": ""site"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        },
        ""Right"": {
          ""Value"": ""\n"",
          ""InferredReturnType"": 4,
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 26,
          ""ReturnType"": 4,
          ""Parent"": null
        },
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 15,
        ""ReturnType"": 1,
        ""Parent"": null
      },
      ""Right"": {
        ""Name"": ""PostCode"",
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 30,
        ""ReturnType"": 1,
        ""Parent"": {
          ""EntityTypeName"": ""Site"",
          ""VariableName"": ""site"",
          ""Value"": null,
          ""InferredReturnType"": 1,
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 28,
          ""ReturnType"": 1,
          ""Parent"": null
        }
      },
      ""IsIqlExpression"": true,
      ""Key"": null,
      ""Kind"": 15,
      ""ReturnType"": 1,
      ""Parent"": null
    },
    ""Parameters"": [
      {
        ""EntityTypeName"": ""Site"",
        ""VariableName"": ""site"",
        ""Value"": null,
        ""InferredReturnType"": 1,
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 28,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    ],
    ""IsIqlExpression"": true,
    ""Key"": null,
    ""Kind"": 55,
    ""ReturnType"": 1,
    ""Parent"": null
  },
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FullAddress__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": [
      {
        ""Key"": ""7"",
        ""Message"": """",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Site</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlOrExpression\"">\r\n    <Kind>Or</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Site</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>FullAddress</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Site</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>FullAddress</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Site</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>FullAddress</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Site</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>FullAddress</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"" />\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FullAddress"",
  ""Title"": ""FullAddress"",
  ""FriendlyName"": ""Full Address"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Address"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Address__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Address"",
  ""Title"": ""Address"",
  ""FriendlyName"": ""Address"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""PostCode"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PostCode__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PostCode"",
  ""Title"": ""PostCode"",
  ""FriendlyName"": ""Post Code"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Left"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Left__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Left"",
  ""Title"": ""Left"",
  ""FriendlyName"": ""Left"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Right"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Right__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Right"",
  ""Title"": ""Right"",
  ""FriendlyName"": ""Right"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""DocumentsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__DocumentsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""DocumentsCount"",
  ""Title"": ""DocumentsCount"",
  ""FriendlyName"": ""Documents Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""AdditionalSendReportsToCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__AdditionalSendReportsToCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""AdditionalSendReportsToCount"",
  ""Title"": ""AdditionalSendReportsToCount"",
  ""FriendlyName"": ""Additional Send Reports To Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCount"",
  ""Title"": ""PeopleCount"",
  ""FriendlyName"": ""People Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Parent"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Parent__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Parent"",
  ""Title"": ""Parent"",
  ""FriendlyName"": ""Parent"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Children"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Children__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Children"",
  ""Title"": ""Children"",
  ""FriendlyName"": ""Children"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ChildrenCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ChildrenCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ChildrenCount"",
  ""Title"": ""ChildrenCount"",
  ""FriendlyName"": ""Children Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Client"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": {
    ""Body"": {
      ""Name"": ""Client"",
      ""IsIqlExpression"": true,
      ""Key"": null,
      ""Kind"": 30,
      ""ReturnType"": 1,
      ""Parent"": {
        ""Name"": ""CreatedByUser"",
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 30,
        ""ReturnType"": 1,
        ""Parent"": {
          ""EntityTypeName"": ""Site"",
          ""VariableName"": ""_"",
          ""Value"": null,
          ""InferredReturnType"": 1,
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 28,
          ""ReturnType"": 1,
          ""Parent"": null
        }
      }
    },
    ""Parameters"": [
      {
        ""EntityTypeName"": ""Site"",
        ""VariableName"": ""_"",
        ""Value"": null,
        ""InferredReturnType"": 1,
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 28,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    ],
    ""IsIqlExpression"": true,
    ""Key"": null,
    ""Kind"": 55,
    ""ReturnType"": 1,
    ""Parent"": null
  },
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Client__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Areas"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Areas__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Areas"",
  ""Title"": ""Areas"",
  ""FriendlyName"": ""Areas"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""AreasCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__AreasCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""AreasCount"",
  ""Title"": ""AreasCount"",
  ""FriendlyName"": ""Areas Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteInspections"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspections__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspections"",
  ""Title"": ""SiteInspections"",
  ""FriendlyName"": ""Site Inspections"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteInspectionsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspectionsCount"",
  ""Title"": ""SiteInspectionsCount"",
  ""FriendlyName"": ""Site Inspections Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Users"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Users__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Users"",
  ""Title"": ""Users"",
  ""FriendlyName"": ""Users"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""UsersCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__UsersCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""UsersCount"",
  ""Title"": ""UsersCount"",
  ""FriendlyName"": ""Users Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""DocumentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Documents\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""DocumentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Documents__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Documents"",
  ""Title"": ""Documents"",
  ""FriendlyName"": ""Documents"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AdditionalSendReportsToCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AdditionalSendReportsTo\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AdditionalSendReportsToCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_AdditionalSendReportsTo__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""AdditionalSendReportsTo"",
  ""Title"": ""Additional Send Reports To"",
  ""FriendlyName"": ""Additional Send Reports To"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""ParentId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Parent\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""ChildrenCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ParentId_Parent__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Parent"",
  ""Title"": ""Parent"",
  ""FriendlyName"": ""Parent"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Children\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""ChildrenCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Children__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Children"",
  ""Title"": ""Children"",
  ""FriendlyName"": ""Children"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""ClientId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [
    {
      ""Container"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Name\"",\""Kind\"":1,\""Children\"":null}"",
      ""Expression"": {
        ""Body"": {
          ""Value"": ""test@123.com"",
          ""InferredReturnType"": 4,
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 26,
          ""ReturnType"": 4,
          ""Parent"": null
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""Site"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""RelationshipMappings"": [
    {
      ""Container"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Type\"",\""Kind\"":6,\""Children\"":null}"",
      ""Expression"": {
        ""Body"": {
          ""Name"": ""Type"",
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 30,
          ""ReturnType"": 1,
          ""Parent"": {
            ""Name"": ""Client"",
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 30,
            ""ReturnType"": 1,
            ""Parent"": {
              ""EntityTypeName"": ""Site"",
              ""VariableName"": ""_"",
              ""Value"": null,
              ""InferredReturnType"": 1,
              ""IsIqlExpression"": true,
              ""Key"": null,
              ""Kind"": 28,
              ""ReturnType"": 1,
              ""Parent"": null
            }
          }
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""Site"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""AllowInlineEditing"": true,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Client\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""SitesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId_Client__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": [
      {
        ""Key"": ""NumberSeven"",
        ""Value"": 7
      }
    ]
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": true,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Sites\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""SitesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Sites__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Sites"",
  ""Title"": ""Sites"",
  ""FriendlyName"": ""Sites"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SitesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SitesCreated"",
  ""Title"": ""Sites Created"",
  ""FriendlyName"": ""Sites Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AreasCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Areas\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AreasCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Areas__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Areas"",
  ""Title"": ""Areas"",
  ""FriendlyName"": ""Areas"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""SiteInspectionsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""SiteInspections\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""SiteInspectionsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SiteInspections__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspections"",
  ""Title"": ""Site Inspections"",
  ""FriendlyName"": ""Site Inspections"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""UsersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Users\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""UsersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Users__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Users"",
  ""Title"": ""Users"",
  ""FriendlyName"": ""Users"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""Site"",
      ""Title"": ""Site"",
      ""FriendlyName"": ""Site"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Notes"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Report Actions Taken"",
      ""SetName"": ""ReportActionsTaken"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultReportId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultReportId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReportId"",
  ""Title"": ""FaultReportId"",
  ""FriendlyName"": ""Fault Report Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Notes"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Notes__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": [
      {
        ""Key"": ""5"",
        ""Message"": ""Please enter some actions taken notes"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>ReportActionsTaken</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlOrExpression\"">\r\n    <Kind>Or</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsNotEqualToExpression\"">\r\n      <Kind>IsNotEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>ReportActionsTaken</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Notes</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>ReportActionsTaken</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Notes</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsNotEqualToExpression\"">\r\n      <Kind>IsNotEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>ReportActionsTaken</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Notes</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>ReportActionsTaken</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Notes</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"" />\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      },
      {
        ""Key"": ""6"",
        ""Message"": ""Please enter at least five characters for notes"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>ReportActionsTaken</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlIsGreaterThanExpression\"">\r\n    <Kind>IsGreaterThan</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlStringLengthExpression\"">\r\n      <Kind>StringLength</Kind>\r\n      <ReturnType>Integer</ReturnType>\r\n      <Parent xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <InferredReturnType>Unknown</InferredReturnType>\r\n          <EntityTypeName>ReportActionsTaken</EntityTypeName>\r\n          <VariableName>entity</VariableName>\r\n        </Parent>\r\n        <Name>Notes</Name>\r\n      </Parent>\r\n    </Left>\r\n    <Right xsi:type=\""IqlLiteralExpression\"">\r\n      <Kind>Literal</Kind>\r\n      <ReturnType>Integer</ReturnType>\r\n      <Value xsi:type=\""xsd:int\"">5</Value>\r\n      <InferredReturnType>Integer</InferredReturnType>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Notes"",
  ""Title"": ""Notes"",
  ""FriendlyName"": ""Notes"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonReport"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonReport__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonReport"",
  ""Title"": ""PersonReport"",
  ""FriendlyName"": ""Person Report"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""FaultReportId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""PersonReport\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""ActionsTakenCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultReportId_PersonReport__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonReport"",
  ""Title"": ""Person Report"",
  ""FriendlyName"": ""Person Report"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""ActionsTaken\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""ActionsTakenCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ActionsTaken__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ActionsTaken"",
  ""Title"": ""Actions Taken"",
  ""FriendlyName"": ""Actions Taken"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonReport:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultActionsTakenCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultActionsTakenCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultActionsTakenCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultActionsTakenCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultActionsTakenCreated"",
  ""Title"": ""Fault Actions Taken Created"",
  ""FriendlyName"": ""Fault Actions Taken Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ReportActionsTaken"",
      ""Title"": ""ReportActionsTaken"",
      ""FriendlyName"": ""Report Actions Taken"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Title"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Person Reports"",
      ""SetName"": ""PersonReports"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""ActionsTaken"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ActionsTaken__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ActionsTaken"",
  ""Title"": ""ActionsTaken"",
  ""FriendlyName"": ""Actions Taken"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Recommendations"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Recommendations__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendations"",
  ""Title"": ""Recommendations"",
  ""FriendlyName"": ""Recommendations"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonId"",
  ""Title"": ""PersonId"",
  ""FriendlyName"": ""Person Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""TypeId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""TypeId"",
  ""Title"": ""TypeId"",
  ""FriendlyName"": ""Type Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Title"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Title__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": [
      {
        ""Key"": ""3"",
        ""Message"": ""Please enter a valid report title"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>PersonReport</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlOrExpression\"">\r\n    <Kind>Or</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>PersonReport</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>PersonReport</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlStringTrimExpression\"">\r\n            <Kind>StringTrim</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>PersonReport</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Title</Name>\r\n            </Parent>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n            <Kind>StringTrim</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>PersonReport</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Title</Name>\r\n            </Parent>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"" />\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      },
      {
        ""Key"": ""4"",
        ""Message"": ""Please enter less than five characters"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>PersonReport</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlAndExpression\"">\r\n    <Kind>And</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlNotExpression\"">\r\n      <Kind>Not</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Expression xsi:type=\""IqlOrExpression\"">\r\n        <Kind>Or</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlConditionExpression\"">\r\n            <Kind>Condition</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n              <Kind>IsEqualTo</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Left xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>PersonReport</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Title</Name>\r\n              </Left>\r\n              <Right xsi:type=\""IqlLiteralExpression\"">\r\n                <Kind>Literal</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <InferredReturnType>String</InferredReturnType>\r\n              </Right>\r\n            </Test>\r\n            <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </IfTrue>\r\n            <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n              <Kind>StringToUpperCase</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>PersonReport</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Title</Name>\r\n              </Parent>\r\n            </IfFalse>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Left>\r\n        <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlConditionExpression\"">\r\n            <Kind>Condition</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n              <Kind>IsEqualTo</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Left xsi:type=\""IqlStringTrimExpression\"">\r\n                <Kind>StringTrim</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                  <Kind>Property</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                    <Kind>RootReference</Kind>\r\n                    <ReturnType>Unknown</ReturnType>\r\n                    <InferredReturnType>Unknown</InferredReturnType>\r\n                    <EntityTypeName>PersonReport</EntityTypeName>\r\n                    <VariableName>entity</VariableName>\r\n                  </Parent>\r\n                  <Name>Title</Name>\r\n                </Parent>\r\n              </Left>\r\n              <Right xsi:type=\""IqlLiteralExpression\"">\r\n                <Kind>Literal</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <InferredReturnType>String</InferredReturnType>\r\n              </Right>\r\n            </Test>\r\n            <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </IfTrue>\r\n            <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n              <Kind>StringToUpperCase</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n                <Kind>StringTrim</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                  <Kind>Property</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                    <Kind>RootReference</Kind>\r\n                    <ReturnType>Unknown</ReturnType>\r\n                    <InferredReturnType>Unknown</InferredReturnType>\r\n                    <EntityTypeName>PersonReport</EntityTypeName>\r\n                    <VariableName>entity</VariableName>\r\n                  </Parent>\r\n                  <Name>Title</Name>\r\n                </Parent>\r\n              </Parent>\r\n            </IfFalse>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Value xsi:type=\""xsd:string\"" />\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Right>\r\n      </Expression>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsGreaterThanExpression\"">\r\n      <Kind>IsGreaterThan</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlStringLengthExpression\"">\r\n        <Kind>StringLength</Kind>\r\n        <ReturnType>Integer</ReturnType>\r\n        <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n          <Kind>StringTrim</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>PersonReport</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Parent>\r\n        </Parent>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>Integer</ReturnType>\r\n        <Value xsi:type=\""xsd:int\"">5</Value>\r\n        <InferredReturnType>Integer</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Title"",
  ""Title"": ""Title"",
  ""FriendlyName"": ""Title"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 9
  },
  ""SearchKind"": 1,
  ""Name"": ""Status"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Status__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Status"",
  ""Title"": ""Status"",
  ""FriendlyName"": ""Status"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ActionsTakenCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ActionsTakenCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ActionsTakenCount"",
  ""Title"": ""ActionsTakenCount"",
  ""FriendlyName"": ""Actions Taken Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RecommendationsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RecommendationsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RecommendationsCount"",
  ""Title"": ""RecommendationsCount"",
  ""FriendlyName"": ""Recommendations Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Person"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Person__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Person"",
  ""Title"": ""Person"",
  ""FriendlyName"": ""Person"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Type"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Type__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""FaultReportId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportActionsTaken\"",\""Paths\"":\""PersonReport\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""ActionsTakenCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultReportId_PersonReport__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonReport"",
  ""Title"": ""Person Report"",
  ""FriendlyName"": ""Person Report"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""ActionsTaken\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""ActionsTakenCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ActionsTaken__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ActionsTaken"",
  ""Title"": ""Actions Taken"",
  ""FriendlyName"": ""Actions Taken"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonReport:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""ReportId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""PersonReport\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""RecommendationsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReportId_PersonReport__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonReport"",
  ""Title"": ""Person Report"",
  ""FriendlyName"": ""Person Report"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Recommendations\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""RecommendationsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Recommendations__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendations"",
  ""Title"": ""Recommendations"",
  ""FriendlyName"": ""Recommendations"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonReport:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""PersonId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Person\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""ReportsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonId_Person__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Person"",
  ""Title"": ""Person"",
  ""FriendlyName"": ""Person"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Reports\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""ReportsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Reports__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Reports"",
  ""Title"": ""Reports"",
  ""FriendlyName"": ""Reports"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Person:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Type\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""FaultReportsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId_Type__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""FaultReports\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""FaultReportsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultReports__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReports"",
  ""Title"": ""Fault Reports"",
  ""FriendlyName"": ""Fault Reports"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ReportType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultReportsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultReportsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultReportsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultReportsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReportsCreated"",
  ""Title"": ""Fault Reports Created"",
  ""FriendlyName"": ""Fault Reports Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""PersonReport"",
      ""Title"": ""PersonReport"",
      ""FriendlyName"": ""Person Report"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Report Categories"",
      ""SetName"": ""ReportCategories"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""ReportTypes"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReportTypes__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportTypes"",
  ""Title"": ""ReportTypes"",
  ""FriendlyName"": ""Report Types"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ReportTypesCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReportTypesCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportTypesCount"",
  ""Title"": ""ReportTypesCount"",
  ""FriendlyName"": ""Report Types Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultCategoriesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultCategoriesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultCategoriesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultCategoriesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultCategoriesCreated"",
  ""Title"": ""Fault Categories Created"",
  ""FriendlyName"": ""Fault Categories Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""CategoryId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""Category\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""ReportTypesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CategoryId_Category__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""FriendlyName"": ""Category"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""ReportTypes\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""ReportTypesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ReportTypes__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportTypes"",
  ""Title"": ""Report Types"",
  ""FriendlyName"": ""Report Types"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ReportCategory:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ReportCategory"",
      ""Title"": ""ReportCategory"",
      ""FriendlyName"": ""Report Category"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Report Default Recommendations"",
      ""SetName"": ""ReportDefaultRecommendations"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Text"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Text__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Text"",
  ""Title"": ""Text"",
  ""FriendlyName"": ""Text"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Recommendations"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Recommendations__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendations"",
  ""Title"": ""Recommendations"",
  ""FriendlyName"": ""Recommendations"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RecommendationsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RecommendationsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RecommendationsCount"",
  ""Title"": ""RecommendationsCount"",
  ""FriendlyName"": ""Recommendations Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultDefaultRecommendationsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultDefaultRecommendationsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultDefaultRecommendationsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultDefaultRecommendationsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultDefaultRecommendationsCreated"",
  ""Title"": ""Fault Default Recommendations Created"",
  ""FriendlyName"": ""Fault Default Recommendations Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""RecommendationId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""Recommendation\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""RecommendationsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RecommendationId_Recommendation__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendation"",
  ""Title"": ""Recommendation"",
  ""FriendlyName"": ""Recommendation"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""Recommendations\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""RecommendationsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Recommendations__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendations"",
  ""Title"": ""Recommendations"",
  ""FriendlyName"": ""Recommendations"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ReportDefaultRecommendation:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ReportDefaultRecommendation"",
      ""Title"": ""ReportDefaultRecommendation"",
      ""FriendlyName"": ""Report Default Recommendation"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Notes"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Report Recommendations"",
      ""SetName"": ""ReportRecommendations"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ReportId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReportId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportId"",
  ""Title"": ""ReportId"",
  ""FriendlyName"": ""Report Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RecommendationId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RecommendationId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RecommendationId"",
  ""Title"": ""RecommendationId"",
  ""FriendlyName"": ""Recommendation Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Notes"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Notes__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Notes"",
  ""Title"": ""Notes"",
  ""FriendlyName"": ""Notes"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonReport"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonReport__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonReport"",
  ""Title"": ""PersonReport"",
  ""FriendlyName"": ""Person Report"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Recommendation"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Recommendation__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendation"",
  ""Title"": ""Recommendation"",
  ""FriendlyName"": ""Recommendation"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""ReportId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""PersonReport\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""RecommendationsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReportId_PersonReport__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonReport"",
  ""Title"": ""Person Report"",
  ""FriendlyName"": ""Person Report"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Recommendations\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""RecommendationsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Recommendations__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendations"",
  ""Title"": ""Recommendations"",
  ""FriendlyName"": ""Recommendations"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonReport:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""RecommendationId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""Recommendation\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""RecommendationsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RecommendationId_Recommendation__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendation"",
  ""Title"": ""Recommendation"",
  ""FriendlyName"": ""Recommendation"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""Recommendations\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportDefaultRecommendation\"",\""Paths\"":\""RecommendationsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Recommendations__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Recommendations"",
  ""Title"": ""Recommendations"",
  ""FriendlyName"": ""Recommendations"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ReportDefaultRecommendation:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportRecommendation\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultRecommendationsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultRecommendationsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultRecommendationsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultRecommendationsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultRecommendationsCreated"",
  ""Title"": ""Fault Recommendations Created"",
  ""FriendlyName"": ""Fault Recommendations Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ReportRecommendation"",
      ""Title"": ""ReportRecommendation"",
      ""FriendlyName"": ""Report Recommendation"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ReportType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Report Types"",
      ""SetName"": ""ReportTypes"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""CategoryId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CategoryId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CategoryId"",
  ""Title"": ""CategoryId"",
  ""FriendlyName"": ""Category Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Category"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Category__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""FriendlyName"": ""Category"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultReports"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultReports__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReports"",
  ""Title"": ""FaultReports"",
  ""FriendlyName"": ""Fault Reports"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""FaultReportsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__FaultReportsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReportsCount"",
  ""Title"": ""FaultReportsCount"",
  ""FriendlyName"": ""Fault Reports Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""CategoryId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""Category\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""ReportTypesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CategoryId_Category__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""FriendlyName"": ""Category"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""ReportTypes\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportCategory\"",\""Paths\"":\""ReportTypesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ReportTypes__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportTypes"",
  ""Title"": ""Report Types"",
  ""FriendlyName"": ""Report Types"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ReportCategory:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultTypesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultTypesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultTypesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultTypesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultTypesCreated"",
  ""Title"": ""Fault Types Created"",
  ""FriendlyName"": ""Fault Types Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Type\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""FaultReportsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId_Type__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""FaultReports\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ReportType\"",\""Paths\"":\""FaultReportsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_FaultReports__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""FaultReports"",
  ""Title"": ""Fault Reports"",
  ""FriendlyName"": ""Fault Reports"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ReportType:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ReportType"",
      ""Title"": ""ReportType"",
      ""FriendlyName"": ""Report Type"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""Project\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Title"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Projects"",
      ""SetName"": ""Projects"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Title"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Title__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Title"",
  ""Title"": ""Title"",
  ""FriendlyName"": ""Title"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Description"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Description__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""FriendlyName"": ""Description"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Project\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Project\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ProjectCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ProjectCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ProjectCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ProjectCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ProjectCreated"",
  ""Title"": ""Project Created"",
  ""FriendlyName"": ""Project Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""Project"",
      ""Title"": ""Project"",
      ""FriendlyName"": ""Project"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""EmailAddress"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Report Receiver Email Addresses"",
      ""SetName"": ""ReportReceiverEmailAddresses"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""FriendlyName"": ""Site Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""EmailAddress"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__EmailAddress__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""EmailAddress"",
  ""Title"": ""EmailAddress"",
  ""FriendlyName"": ""Email Address"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Site"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Site__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AdditionalSendReportsToCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AdditionalSendReportsTo\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AdditionalSendReportsToCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_AdditionalSendReportsTo__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""AdditionalSendReportsTo"",
  ""Title"": ""Additional Send Reports To"",
  ""FriendlyName"": ""Additional Send Reports To"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ReportReceiverEmailAddressesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ReportReceiverEmailAddressesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ReportReceiverEmailAddressesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_ReportReceiverEmailAddressesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportReceiverEmailAddressesCreated"",
  ""Title"": ""Report Receiver Email Addresses Created"",
  ""FriendlyName"": ""Report Receiver Email Addresses Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""ReportReceiverEmailAddress"",
      ""Title"": ""ReportReceiverEmailAddress"",
      ""FriendlyName"": ""Report Receiver Email Address"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""RevisionKey"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Risk Assessments"",
      ""SetName"": ""RiskAssessments"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteInspectionId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspectionId"",
  ""Title"": ""SiteInspectionId"",
  ""FriendlyName"": ""Site Inspection Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteInspection"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspection__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspection"",
  ""Title"": ""SiteInspection"",
  ""FriendlyName"": ""Site Inspection"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentSolution"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentSolution__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentSolution"",
  ""Title"": ""RiskAssessmentSolution"",
  ""FriendlyName"": ""Risk Assessment Solution"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""SiteInspectionId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""SiteInspection\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""RiskAssessmentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionId_SiteInspection__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspection"",
  ""Title"": ""Site Inspection"",
  ""FriendlyName"": ""Site Inspection"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""RiskAssessments\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""RiskAssessmentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessments__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessments"",
  ""Title"": ""Risk Assessments"",
  ""FriendlyName"": ""Risk Assessments"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""SiteInspection:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentsCreated"",
  ""Title"": ""Risk Assessments Created"",
  ""FriendlyName"": ""Risk Assessments Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""RiskAssessmentId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 0,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""RiskAssessment\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentId_RiskAssessment__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessment"",
  ""Title"": ""Risk Assessment"",
  ""FriendlyName"": ""Risk Assessment"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""RiskAssessmentSolution\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentSolution__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentSolution"",
  ""Title"": ""Risk Assessment Solution"",
  ""FriendlyName"": ""Risk Assessment Solution"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""RiskAssessment:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""RiskAssessment"",
      ""Title"": ""RiskAssessment"",
      ""FriendlyName"": ""Risk Assessment"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""RevisionKey"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Site Inspections"",
      ""SetName"": ""SiteInspections"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessments"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessments__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessments"",
  ""Title"": ""RiskAssessments"",
  ""FriendlyName"": ""Risk Assessments"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonInspections"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonInspections__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonInspections"",
  ""Title"": ""PersonInspections"",
  ""FriendlyName"": ""Person Inspections"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""FriendlyName"": ""Site Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""StartTime"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__StartTime__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""StartTime"",
  ""Title"": ""StartTime"",
  ""FriendlyName"": ""Start Time"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""EndTime"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__EndTime__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""EndTime"",
  ""Title"": ""EndTime"",
  ""FriendlyName"": ""End Time"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentsCount"",
  ""Title"": ""RiskAssessmentsCount"",
  ""FriendlyName"": ""Risk Assessments Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonInspectionsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonInspectionsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonInspectionsCount"",
  ""Title"": ""PersonInspectionsCount"",
  ""FriendlyName"": ""Person Inspections Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Site"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Site__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""SiteInspectionId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""SiteInspection\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""RiskAssessmentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionId_SiteInspection__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspection"",
  ""Title"": ""Site Inspection"",
  ""FriendlyName"": ""Site Inspection"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""RiskAssessments\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""RiskAssessmentsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessments__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessments"",
  ""Title"": ""Risk Assessments"",
  ""FriendlyName"": ""Risk Assessments"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""SiteInspection:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""SiteInspectionId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""SiteInspection\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""PersonInspectionsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionId_SiteInspection__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspection"",
  ""Title"": ""Site Inspection"",
  ""FriendlyName"": ""Site Inspection"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""PersonInspections\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""PersonInspectionsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PersonInspections__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonInspections"",
  ""Title"": ""Person Inspections"",
  ""FriendlyName"": ""Person Inspections"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""SiteInspection:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""SiteInspectionsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""SiteInspections\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""SiteInspectionsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SiteInspections__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspections"",
  ""Title"": ""Site Inspections"",
  ""FriendlyName"": ""Site Inspections"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteInspectionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteInspectionsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteInspectionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SiteInspectionsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspectionsCreated"",
  ""Title"": ""Site Inspections Created"",
  ""FriendlyName"": ""Site Inspections Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""SiteInspection"",
      ""Title"": ""SiteInspection"",
      ""FriendlyName"": ""Site Inspection"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""RevisionKey"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Risk Assessment Solutions"",
      ""SetName"": ""RiskAssessmentSolutions"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessmentId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentId"",
  ""Title"": ""RiskAssessmentId"",
  ""FriendlyName"": ""Risk Assessment Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""RiskAssessment"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessment__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessment"",
  ""Title"": ""RiskAssessment"",
  ""FriendlyName"": ""Risk Assessment"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""RiskAssessmentId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 0,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""RiskAssessment\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RiskAssessmentId_RiskAssessment__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessment"",
  ""Title"": ""Risk Assessment"",
  ""FriendlyName"": ""Risk Assessment"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""RiskAssessmentSolution\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentSolution__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentSolution"",
  ""Title"": ""Risk Assessment Solution"",
  ""FriendlyName"": ""Risk Assessment Solution"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""RiskAssessment:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentSolution\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentSolutionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentSolutionsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentSolutionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentSolutionsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentSolutionsCreated"",
  ""Title"": ""Risk Assessment Solutions Created"",
  ""FriendlyName"": ""Risk Assessment Solutions Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""RiskAssessmentSolution"",
      ""Title"": ""RiskAssessmentSolution"",
      ""FriendlyName"": ""Risk Assessment Solution"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""SpecificHazard"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Risk Assessment Answers"",
      ""SetName"": ""RiskAssessmentAnswers"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""QuestionId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__QuestionId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""QuestionId"",
  ""Title"": ""QuestionId"",
  ""FriendlyName"": ""Question Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""SpecificHazard"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SpecificHazard__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SpecificHazard"",
  ""Title"": ""SpecificHazard"",
  ""FriendlyName"": ""Specific Hazard"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""PrecautionsToControlHazard"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PrecautionsToControlHazard__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PrecautionsToControlHazard"",
  ""Title"": ""PrecautionsToControlHazard"",
  ""FriendlyName"": ""Precautions To Control Hazard"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Question"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Question__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Question"",
  ""Title"": ""Question"",
  ""FriendlyName"": ""Question"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""QuestionId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""Question\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""AnswersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__QuestionId_Question__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Question"",
  ""Title"": ""Question"",
  ""FriendlyName"": ""Question"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Answers\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""AnswersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Answers__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Answers"",
  ""Title"": ""Answers"",
  ""FriendlyName"": ""Answers"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""RiskAssessmentQuestion:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentAnswersCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentAnswersCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentAnswersCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentAnswersCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentAnswersCreated"",
  ""Title"": ""Risk Assessment Answers Created"",
  ""FriendlyName"": ""Risk Assessment Answers Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""RiskAssessmentAnswer"",
      ""Title"": ""RiskAssessmentAnswer"",
      ""FriendlyName"": ""Risk Assessment Answer"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Risk Assessment Questions"",
      ""SetName"": ""RiskAssessmentQuestions"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Answers"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Answers__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Answers"",
  ""Title"": ""Answers"",
  ""FriendlyName"": ""Answers"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""AnswersCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__AnswersCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""AnswersCount"",
  ""Title"": ""AnswersCount"",
  ""FriendlyName"": ""Answers Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""QuestionId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""Question\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""AnswersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__QuestionId_Question__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Question"",
  ""Title"": ""Question"",
  ""FriendlyName"": ""Question"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Answers\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""AnswersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Answers__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Answers"",
  ""Title"": ""Answers"",
  ""FriendlyName"": ""Answers"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""RiskAssessmentQuestion:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentQuestionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentQuestionsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentQuestionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_RiskAssessmentQuestionsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RiskAssessmentQuestionsCreated"",
  ""Title"": ""Risk Assessment Questions Created"",
  ""FriendlyName"": ""Risk Assessment Questions Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""RiskAssessmentQuestion"",
      ""Title"": ""RiskAssessmentQuestion"",
      ""FriendlyName"": ""Risk Assessment Question"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [
          {
            ""Key"": ""Default"",
            ""FormatterExpression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlPropertyExpression\"">\r\n    <Kind>Property</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </Parent>\r\n    <Name>Title</Name>\r\n  </Body>\r\n</IqlLambdaExpression>""
          },
          {
            ""Key"": ""Report"",
            ""FormatterExpression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlAddExpression\"">\r\n    <Kind>Add</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlAddExpression\"">\r\n      <Kind>Add</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlAddExpression\"">\r\n        <Kind>Add</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n            <Kind>RootReference</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <InferredReturnType>Unknown</InferredReturnType>\r\n            <EntityTypeName>Person</EntityTypeName>\r\n            <VariableName>entity</VariableName>\r\n          </Parent>\r\n          <Name>Title</Name>\r\n        </Left>\r\n        <Right xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Value xsi:type=\""xsd:string\""> (</Value>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </Right>\r\n      </Left>\r\n      <Right xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <InferredReturnType>Unknown</InferredReturnType>\r\n          <EntityTypeName>Person</EntityTypeName>\r\n          <VariableName>entity</VariableName>\r\n        </Parent>\r\n        <Name>Id</Name>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlLiteralExpression\"">\r\n      <Kind>Literal</Kind>\r\n      <ReturnType>String</ReturnType>\r\n      <Value xsi:type=\""xsd:string\"">)</Value>\r\n      <InferredReturnType>String</InferredReturnType>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
          },
          {
            ""Key"": ""ReportLong"",
            ""FormatterExpression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlAddExpression\"">\r\n    <Kind>Add</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlAddExpression\"">\r\n      <Kind>Add</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlAddExpression\"">\r\n        <Kind>Add</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlAddExpression\"">\r\n          <Kind>Add</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlAddExpression\"">\r\n            <Kind>Add</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Left xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Title</Name>\r\n            </Left>\r\n            <Right xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Value xsi:type=\""xsd:string\""> - </Value>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </Right>\r\n          </Left>\r\n          <Right xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                  <Kind>Property</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                    <Kind>RootReference</Kind>\r\n                    <ReturnType>Unknown</ReturnType>\r\n                    <InferredReturnType>Unknown</InferredReturnType>\r\n                    <EntityTypeName>Person</EntityTypeName>\r\n                    <VariableName>entity</VariableName>\r\n                  </Parent>\r\n                  <Name>Type</Name>\r\n                </Parent>\r\n                <Name>CreatedByUser</Name>\r\n              </Parent>\r\n              <Name>Client</Name>\r\n            </Parent>\r\n            <Name>Name</Name>\r\n          </Right>\r\n        </Left>\r\n        <Right xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Value xsi:type=\""xsd:string\""> (</Value>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </Right>\r\n      </Left>\r\n      <Right xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <InferredReturnType>Unknown</InferredReturnType>\r\n          <EntityTypeName>Person</EntityTypeName>\r\n          <VariableName>entity</VariableName>\r\n        </Parent>\r\n        <Name>Id</Name>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlLiteralExpression\"">\r\n      <Kind>Literal</Kind>\r\n      <ReturnType>String</ReturnType>\r\n      <Value xsi:type=\""xsd:string\"">)</Value>\r\n      <InferredReturnType>String</InferredReturnType>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
          }
        ],
        ""Default"": {
          ""Key"": ""Default"",
          ""FormatterExpression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlPropertyExpression\"">\r\n    <Kind>Property</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </Parent>\r\n    <Name>Title</Name>\r\n  </Body>\r\n</IqlLambdaExpression>""
        }
      },
      ""EntityValidation"": {
        ""All"": [
          {
            ""Key"": ""NoTitleOrDescription"",
            ""Message"": ""Please enter either a title or a description"",
            ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlAndExpression\"">\r\n    <Kind>And</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlOrExpression\"">\r\n      <Kind>Or</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n        <Kind>IsEqualTo</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlConditionExpression\"">\r\n          <Kind>Condition</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n            <Kind>IsEqualTo</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Left xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Title</Name>\r\n            </Left>\r\n            <Right xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </Right>\r\n          </Test>\r\n          <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </IfTrue>\r\n          <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n            <Kind>StringToUpperCase</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Title</Name>\r\n            </Parent>\r\n          </IfFalse>\r\n        </Left>\r\n        <Right xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </Right>\r\n      </Left>\r\n      <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n        <Kind>IsEqualTo</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlConditionExpression\"">\r\n          <Kind>Condition</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n            <Kind>IsEqualTo</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Left xsi:type=\""IqlStringTrimExpression\"">\r\n              <Kind>StringTrim</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>Person</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Title</Name>\r\n              </Parent>\r\n            </Left>\r\n            <Right xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </Right>\r\n          </Test>\r\n          <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </IfTrue>\r\n          <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n            <Kind>StringToUpperCase</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n              <Kind>StringTrim</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>Person</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Title</Name>\r\n              </Parent>\r\n            </Parent>\r\n          </IfFalse>\r\n        </Left>\r\n        <Right xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Value xsi:type=\""xsd:string\"" />\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </Right>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlOrExpression\"">\r\n      <Kind>Or</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n        <Kind>IsEqualTo</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlConditionExpression\"">\r\n          <Kind>Condition</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n            <Kind>IsEqualTo</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Left xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Description</Name>\r\n            </Left>\r\n            <Right xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </Right>\r\n          </Test>\r\n          <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </IfTrue>\r\n          <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n            <Kind>StringToUpperCase</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Description</Name>\r\n            </Parent>\r\n          </IfFalse>\r\n        </Left>\r\n        <Right xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </Right>\r\n      </Left>\r\n      <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n        <Kind>IsEqualTo</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlConditionExpression\"">\r\n          <Kind>Condition</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n            <Kind>IsEqualTo</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Left xsi:type=\""IqlStringTrimExpression\"">\r\n              <Kind>StringTrim</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>Person</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Description</Name>\r\n              </Parent>\r\n            </Left>\r\n            <Right xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </Right>\r\n          </Test>\r\n          <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </IfTrue>\r\n          <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n            <Kind>StringToUpperCase</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n              <Kind>StringTrim</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>Person</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Description</Name>\r\n              </Parent>\r\n            </Parent>\r\n          </IfFalse>\r\n        </Left>\r\n        <Right xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Value xsi:type=\""xsd:string\"" />\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </Right>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
          },
          {
            ""Key"": ""JoshCheck"",
            ""Message"": ""If the name is 'Josh' please match it in the description"",
            ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlAndExpression\"">\r\n    <Kind>And</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"">JOSH</Value>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsNotEqualToExpression\"">\r\n      <Kind>IsNotEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Description</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Description</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"">JOSH</Value>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
          }
        ]
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""Person\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Key"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""People"",
      ""SetName"": ""People"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ClientId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ClientId"",
  ""Title"": ""ClientId"",
  ""FriendlyName"": ""Client Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""FriendlyName"": ""Site Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteAreaId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteAreaId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteAreaId"",
  ""Title"": ""SiteAreaId"",
  ""FriendlyName"": ""Site Area Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""TypeId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""TypeId"",
  ""Title"": ""TypeId"",
  ""FriendlyName"": ""Type Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""LoadingId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__LoadingId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""LoadingId"",
  ""Title"": ""LoadingId"",
  ""FriendlyName"": ""Loading Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Key"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Key__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Key"",
  ""Title"": ""Key"",
  ""FriendlyName"": ""Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Title"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Title__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": [
      {
        ""Key"": ""EmptyTitle"",
        ""Message"": ""Please enter a person title"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlOrExpression\"">\r\n    <Kind>Or</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlStringTrimExpression\"">\r\n            <Kind>StringTrim</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Title</Name>\r\n            </Parent>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n            <Kind>StringTrim</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Title</Name>\r\n            </Parent>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"" />\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      },
      {
        ""Key"": ""TitleMaxLength"",
        ""Message"": ""Please enter less than fifty characters"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlAndExpression\"">\r\n    <Kind>And</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlNotExpression\"">\r\n      <Kind>Not</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Expression xsi:type=\""IqlOrExpression\"">\r\n        <Kind>Or</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlConditionExpression\"">\r\n            <Kind>Condition</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n              <Kind>IsEqualTo</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Left xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>Person</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Title</Name>\r\n              </Left>\r\n              <Right xsi:type=\""IqlLiteralExpression\"">\r\n                <Kind>Literal</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <InferredReturnType>String</InferredReturnType>\r\n              </Right>\r\n            </Test>\r\n            <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </IfTrue>\r\n            <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n              <Kind>StringToUpperCase</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>Person</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Title</Name>\r\n              </Parent>\r\n            </IfFalse>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Left>\r\n        <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlConditionExpression\"">\r\n            <Kind>Condition</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n              <Kind>IsEqualTo</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Left xsi:type=\""IqlStringTrimExpression\"">\r\n                <Kind>StringTrim</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                  <Kind>Property</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                    <Kind>RootReference</Kind>\r\n                    <ReturnType>Unknown</ReturnType>\r\n                    <InferredReturnType>Unknown</InferredReturnType>\r\n                    <EntityTypeName>Person</EntityTypeName>\r\n                    <VariableName>entity</VariableName>\r\n                  </Parent>\r\n                  <Name>Title</Name>\r\n                </Parent>\r\n              </Left>\r\n              <Right xsi:type=\""IqlLiteralExpression\"">\r\n                <Kind>Literal</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <InferredReturnType>String</InferredReturnType>\r\n              </Right>\r\n            </Test>\r\n            <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </IfTrue>\r\n            <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n              <Kind>StringToUpperCase</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n                <Kind>StringTrim</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                  <Kind>Property</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                    <Kind>RootReference</Kind>\r\n                    <ReturnType>Unknown</ReturnType>\r\n                    <InferredReturnType>Unknown</InferredReturnType>\r\n                    <EntityTypeName>Person</EntityTypeName>\r\n                    <VariableName>entity</VariableName>\r\n                  </Parent>\r\n                  <Name>Title</Name>\r\n                </Parent>\r\n              </Parent>\r\n            </IfFalse>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Value xsi:type=\""xsd:string\"" />\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Right>\r\n      </Expression>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsGreaterThanExpression\"">\r\n      <Kind>IsGreaterThan</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlStringLengthExpression\"">\r\n        <Kind>StringLength</Kind>\r\n        <ReturnType>Integer</ReturnType>\r\n        <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n          <Kind>StringTrim</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Parent>\r\n        </Parent>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>Integer</ReturnType>\r\n        <Value xsi:type=\""xsd:int\"">50</Value>\r\n        <InferredReturnType>Integer</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      },
      {
        ""Key"": ""TitleMinLength"",
        ""Message"": ""Please enter at least three characters for the person's title"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlAndExpression\"">\r\n    <Kind>And</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlNotExpression\"">\r\n      <Kind>Not</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Expression xsi:type=\""IqlOrExpression\"">\r\n        <Kind>Or</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlConditionExpression\"">\r\n            <Kind>Condition</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n              <Kind>IsEqualTo</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Left xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>Person</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Title</Name>\r\n              </Left>\r\n              <Right xsi:type=\""IqlLiteralExpression\"">\r\n                <Kind>Literal</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <InferredReturnType>String</InferredReturnType>\r\n              </Right>\r\n            </Test>\r\n            <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </IfTrue>\r\n            <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n              <Kind>StringToUpperCase</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                <Kind>Property</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                  <Kind>RootReference</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <InferredReturnType>Unknown</InferredReturnType>\r\n                  <EntityTypeName>Person</EntityTypeName>\r\n                  <VariableName>entity</VariableName>\r\n                </Parent>\r\n                <Name>Title</Name>\r\n              </Parent>\r\n            </IfFalse>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Left>\r\n        <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlConditionExpression\"">\r\n            <Kind>Condition</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n              <Kind>IsEqualTo</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Left xsi:type=\""IqlStringTrimExpression\"">\r\n                <Kind>StringTrim</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                  <Kind>Property</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                    <Kind>RootReference</Kind>\r\n                    <ReturnType>Unknown</ReturnType>\r\n                    <InferredReturnType>Unknown</InferredReturnType>\r\n                    <EntityTypeName>Person</EntityTypeName>\r\n                    <VariableName>entity</VariableName>\r\n                  </Parent>\r\n                  <Name>Title</Name>\r\n                </Parent>\r\n              </Left>\r\n              <Right xsi:type=\""IqlLiteralExpression\"">\r\n                <Kind>Literal</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <InferredReturnType>String</InferredReturnType>\r\n              </Right>\r\n            </Test>\r\n            <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n              <Kind>Literal</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <InferredReturnType>String</InferredReturnType>\r\n            </IfTrue>\r\n            <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n              <Kind>StringToUpperCase</Kind>\r\n              <ReturnType>String</ReturnType>\r\n              <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n                <Kind>StringTrim</Kind>\r\n                <ReturnType>String</ReturnType>\r\n                <Parent xsi:type=\""IqlPropertyExpression\"">\r\n                  <Kind>Property</Kind>\r\n                  <ReturnType>Unknown</ReturnType>\r\n                  <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                    <Kind>RootReference</Kind>\r\n                    <ReturnType>Unknown</ReturnType>\r\n                    <InferredReturnType>Unknown</InferredReturnType>\r\n                    <EntityTypeName>Person</EntityTypeName>\r\n                    <VariableName>entity</VariableName>\r\n                  </Parent>\r\n                  <Name>Title</Name>\r\n                </Parent>\r\n              </Parent>\r\n            </IfFalse>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Value xsi:type=\""xsd:string\"" />\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Right>\r\n      </Expression>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsLessThanExpression\"">\r\n      <Kind>IsLessThan</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlStringLengthExpression\"">\r\n        <Kind>StringLength</Kind>\r\n        <ReturnType>Integer</ReturnType>\r\n        <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n          <Kind>StringTrim</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Title</Name>\r\n          </Parent>\r\n        </Parent>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>Integer</ReturnType>\r\n        <Value xsi:type=\""xsd:int\"">3</Value>\r\n        <InferredReturnType>Integer</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Title"",
  ""Title"": ""Title"",
  ""FriendlyName"": ""Title"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Description"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Description__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": [
      {
        ""Key"": ""EmptyDescription"",
        ""Message"": ""Please enter a person description"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>Person</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlOrExpression\"">\r\n    <Kind>Or</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Description</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>Person</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Description</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlStringTrimExpression\"">\r\n            <Kind>StringTrim</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Description</Name>\r\n            </Parent>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlStringTrimExpression\"">\r\n            <Kind>StringTrim</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <Parent xsi:type=\""IqlPropertyExpression\"">\r\n              <Kind>Property</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n                <Kind>RootReference</Kind>\r\n                <ReturnType>Unknown</ReturnType>\r\n                <InferredReturnType>Unknown</InferredReturnType>\r\n                <EntityTypeName>Person</EntityTypeName>\r\n                <VariableName>entity</VariableName>\r\n              </Parent>\r\n              <Name>Description</Name>\r\n            </Parent>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"" />\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""FriendlyName"": ""Description"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 9
  },
  ""SearchKind"": 1,
  ""Name"": ""Category"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Category__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""FriendlyName"": ""Category"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Client"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Client__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Site"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Site__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteArea"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteArea__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": [
      {
        ""Key"": ""1"",
        ""Message"": """",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>RelationshipFilterContext&lt;Person&gt;</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlLambdaExpression\"">\r\n    <Kind>Lambda</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Parameters>\r\n      <IqlRootReferenceExpression>\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <InferredReturnType>Unknown</InferredReturnType>\r\n        <EntityTypeName>SiteArea</EntityTypeName>\r\n        <VariableName>entity2</VariableName>\r\n      </IqlRootReferenceExpression>\r\n    </Parameters>\r\n    <Body xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <InferredReturnType>Unknown</InferredReturnType>\r\n          <EntityTypeName>SiteArea</EntityTypeName>\r\n          <VariableName>entity2</VariableName>\r\n        </Parent>\r\n        <Name>SiteId</Name>\r\n      </Left>\r\n      <Right xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlVariableExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <InferredReturnType>Unknown</InferredReturnType>\r\n            <EntityTypeName>RelationshipFilterContext&lt;Person&gt;</EntityTypeName>\r\n            <VariableName>entity</VariableName>\r\n          </Parent>\r\n          <Name>Owner</Name>\r\n        </Parent>\r\n        <Name>SiteId</Name>\r\n      </Right>\r\n    </Body>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteArea"",
  ""Title"": ""SiteArea"",
  ""FriendlyName"": ""Site Area"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Type"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Type__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Loading"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Loading__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Loading"",
  ""Title"": ""Loading"",
  ""FriendlyName"": ""Loading"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Types"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Types__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Types"",
  ""Title"": ""Types"",
  ""FriendlyName"": ""Types"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""TypesCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypesCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""TypesCount"",
  ""Title"": ""TypesCount"",
  ""FriendlyName"": ""Types Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""Reports"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Reports__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Reports"",
  ""Title"": ""Reports"",
  ""FriendlyName"": ""Reports"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""ReportsCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReportsCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReportsCount"",
  ""Title"": ""ReportsCount"",
  ""FriendlyName"": ""Reports Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""ClientId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Client\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ClientId_Client__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""FriendlyName"": ""Client"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""SiteAreaId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [
    {
      ""Container"": null,
      ""Expression"": {
        ""Body"": {
          ""Name"": ""Site"",
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 30,
          ""ReturnType"": 1,
          ""Parent"": {
            ""EntityTypeName"": ""Person"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""Person"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""SiteArea\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteAreaId_SiteArea__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": [
      {
        ""Key"": ""1"",
        ""Message"": """",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>RelationshipFilterContext&lt;Person&gt;</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlLambdaExpression\"">\r\n    <Kind>Lambda</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Parameters>\r\n      <IqlRootReferenceExpression>\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <InferredReturnType>Unknown</InferredReturnType>\r\n        <EntityTypeName>SiteArea</EntityTypeName>\r\n        <VariableName>entity2</VariableName>\r\n      </IqlRootReferenceExpression>\r\n    </Parameters>\r\n    <Body xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <InferredReturnType>Unknown</InferredReturnType>\r\n          <EntityTypeName>SiteArea</EntityTypeName>\r\n          <VariableName>entity2</VariableName>\r\n        </Parent>\r\n        <Name>SiteId</Name>\r\n      </Left>\r\n      <Right xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlVariableExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <InferredReturnType>Unknown</InferredReturnType>\r\n            <EntityTypeName>RelationshipFilterContext&lt;Person&gt;</EntityTypeName>\r\n            <VariableName>entity</VariableName>\r\n          </Parent>\r\n          <Name>Owner</Name>\r\n        </Parent>\r\n        <Name>SiteId</Name>\r\n      </Right>\r\n    </Body>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteArea"",
  ""Title"": ""Site Area"",
  ""FriendlyName"": ""Site Area"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""SiteArea:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Type\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId_Type__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""LoadingId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Loading\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__LoadingId_Loading__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Loading"",
  ""Title"": ""Loading"",
  ""FriendlyName"": ""Loading"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonLoading:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PeopleCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PeopleCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PeopleCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PeopleCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCreated"",
  ""Title"": ""People Created"",
  ""FriendlyName"": ""People Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""PersonId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""Person\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""TypesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonId_Person__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Person"",
  ""Title"": ""Person"",
  ""FriendlyName"": ""Person"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Types\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""TypesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Types__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Types"",
  ""Title"": ""Types"",
  ""FriendlyName"": ""Types"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Person:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""PersonId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonReport\"",\""Paths\"":\""Person\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""ReportsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonId_Person__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Person"",
  ""Title"": ""Person"",
  ""FriendlyName"": ""Person"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Reports\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""ReportsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Reports__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Reports"",
  ""Title"": ""Reports"",
  ""FriendlyName"": ""Reports"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Person:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""Person"",
      ""Title"": ""Person"",
      ""FriendlyName"": ""Person"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""RevisionKey"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Site Areas"",
      ""SetName"": ""SiteAreas"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""People"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__People__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""FriendlyName"": ""Site Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCount"",
  ""Title"": ""PeopleCount"",
  ""FriendlyName"": ""People Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Site"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Site__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""SiteAreaId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [
    {
      ""Container"": null,
      ""Expression"": {
        ""Body"": {
          ""Name"": ""Site"",
          ""IsIqlExpression"": true,
          ""Key"": null,
          ""Kind"": 30,
          ""ReturnType"": 1,
          ""Parent"": {
            ""EntityTypeName"": ""Person"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        },
        ""Parameters"": [
          {
            ""EntityTypeName"": ""Person"",
            ""VariableName"": ""_"",
            ""Value"": null,
            ""InferredReturnType"": 1,
            ""IsIqlExpression"": true,
            ""Key"": null,
            ""Kind"": 28,
            ""ReturnType"": 1,
            ""Parent"": null
          }
        ],
        ""IsIqlExpression"": true,
        ""Key"": null,
        ""Kind"": 55,
        ""ReturnType"": 1,
        ""Parent"": null
      }
    }
  ],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""SiteArea\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteAreaId_SiteArea__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": [
      {
        ""Key"": ""1"",
        ""Message"": """",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>RelationshipFilterContext&lt;Person&gt;</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlLambdaExpression\"">\r\n    <Kind>Lambda</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Parameters>\r\n      <IqlRootReferenceExpression>\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <InferredReturnType>Unknown</InferredReturnType>\r\n        <EntityTypeName>SiteArea</EntityTypeName>\r\n        <VariableName>entity2</VariableName>\r\n      </IqlRootReferenceExpression>\r\n    </Parameters>\r\n    <Body xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <InferredReturnType>Unknown</InferredReturnType>\r\n          <EntityTypeName>SiteArea</EntityTypeName>\r\n          <VariableName>entity2</VariableName>\r\n        </Parent>\r\n        <Name>SiteId</Name>\r\n      </Left>\r\n      <Right xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlVariableExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <InferredReturnType>Unknown</InferredReturnType>\r\n            <EntityTypeName>RelationshipFilterContext&lt;Person&gt;</EntityTypeName>\r\n            <VariableName>entity</VariableName>\r\n          </Parent>\r\n          <Name>Owner</Name>\r\n        </Parent>\r\n        <Name>SiteId</Name>\r\n      </Right>\r\n    </Body>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteArea"",
  ""Title"": ""Site Area"",
  ""FriendlyName"": ""Site Area"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""SiteArea:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AreasCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Areas\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AreasCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Areas__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Areas"",
  ""Title"": ""Areas"",
  ""FriendlyName"": ""Areas"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""SiteArea\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteAreasCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteAreasCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteAreasCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_SiteAreasCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteAreasCreated"",
  ""Title"": ""Site Areas Created"",
  ""FriendlyName"": ""Site Areas Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""SiteArea"",
      ""Title"": ""SiteArea"",
      ""FriendlyName"": ""Site Area"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""PersonType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Title"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Person Types"",
      ""SetName"": ""PersonTypes"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""People"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__People__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Title"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Title__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Title"",
  ""Title"": ""Title"",
  ""FriendlyName"": ""Title"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCount"",
  ""Title"": ""PeopleCount"",
  ""FriendlyName"": ""People Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleMap"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleMap__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleMap"",
  ""Title"": ""PeopleMap"",
  ""FriendlyName"": ""People Map"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleMapCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleMapCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleMapCount"",
  ""Title"": ""PeopleMapCount"",
  ""FriendlyName"": ""People Map Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Type\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId_Type__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonTypesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonTypesCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonTypesCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PersonTypesCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonTypesCreated"",
  ""Title"": ""Person Types Created"",
  ""FriendlyName"": ""Person Types Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""Type\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleMapCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId_Type__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleMap\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleMapCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PeopleMap__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleMap"",
  ""Title"": ""People Map"",
  ""FriendlyName"": ""People Map"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonType:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""PersonType"",
      ""Title"": ""PersonType"",
      ""FriendlyName"": ""Person Type"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Person Loadings"",
      ""SetName"": ""PersonLoadings"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""SearchKind"": 1,
  ""Name"": ""People"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__People__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": [
      {
        ""Key"": ""2"",
        ""Message"": ""Please enter a loading name"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <InferredReturnType>Unknown</InferredReturnType>\r\n      <EntityTypeName>PersonLoading</EntityTypeName>\r\n      <VariableName>entity</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlAndExpression\"">\r\n    <Kind>And</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsNotEqualToExpression\"">\r\n      <Kind>IsNotEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>PersonLoading</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Name</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>PersonLoading</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Name</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsNotEqualToExpression\"">\r\n      <Kind>IsNotEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlConditionExpression\"">\r\n        <Kind>Condition</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Test xsi:type=\""IqlIsEqualToExpression\"">\r\n          <Kind>IsEqualTo</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Left xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>PersonLoading</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Name</Name>\r\n          </Left>\r\n          <Right xsi:type=\""IqlLiteralExpression\"">\r\n            <Kind>Literal</Kind>\r\n            <ReturnType>String</ReturnType>\r\n            <InferredReturnType>String</InferredReturnType>\r\n          </Right>\r\n        </Test>\r\n        <IfTrue xsi:type=\""IqlLiteralExpression\"">\r\n          <Kind>Literal</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <InferredReturnType>String</InferredReturnType>\r\n        </IfTrue>\r\n        <IfFalse xsi:type=\""IqlStringToUpperCaseExpression\"">\r\n          <Kind>StringToUpperCase</Kind>\r\n          <ReturnType>String</ReturnType>\r\n          <Parent xsi:type=\""IqlPropertyExpression\"">\r\n            <Kind>Property</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n              <Kind>RootReference</Kind>\r\n              <ReturnType>Unknown</ReturnType>\r\n              <InferredReturnType>Unknown</InferredReturnType>\r\n              <EntityTypeName>PersonLoading</EntityTypeName>\r\n              <VariableName>entity</VariableName>\r\n            </Parent>\r\n            <Name>Name</Name>\r\n          </Parent>\r\n        </IfFalse>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"" />\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PeopleCount"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PeopleCount__"",
  ""Key"": null,
  ""Kind"": 35,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleCount"",
  ""Title"": ""PeopleCount"",
  ""FriendlyName"": ""People Count"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""LoadingId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Loading\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__LoadingId_Loading__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Loading"",
  ""Title"": ""Loading"",
  ""FriendlyName"": ""Loading"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""People\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""PeopleCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_People__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""People"",
  ""Title"": ""People"",
  ""FriendlyName"": ""People"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonLoading:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonLoading\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonLoadingsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonLoadingsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonLoadingsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PersonLoadingsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonLoadingsCreated"",
  ""Title"": ""Person Loadings Created"",
  ""FriendlyName"": ""Person Loadings Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""PersonLoading"",
      ""Title"": ""PersonLoading"",
      ""FriendlyName"": ""Person Loading"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""DrawingNumber"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Person Inspections"",
      ""SetName"": ""PersonInspections"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteInspectionId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspectionId"",
  ""Title"": ""SiteInspectionId"",
  ""FriendlyName"": ""Site Inspection Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId__"",
  ""Key"": null,
  ""Kind"": 19,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""FriendlyName"": ""Created By User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonId__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonId"",
  ""Title"": ""PersonId"",
  ""FriendlyName"": ""Person Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 9
  },
  ""SearchKind"": 1,
  ""Name"": ""InspectionStatus"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__InspectionStatus__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""InspectionStatus"",
  ""Title"": ""InspectionStatus"",
  ""FriendlyName"": ""Inspection Status"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""StartTime"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__StartTime__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""StartTime"",
  ""Title"": ""StartTime"",
  ""FriendlyName"": ""Start Time"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""EndTime"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__EndTime__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""EndTime"",
  ""Title"": ""EndTime"",
  ""FriendlyName"": ""End Time"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 9
  },
  ""SearchKind"": 1,
  ""Name"": ""ReasonForFailure"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__ReasonForFailure__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""ReasonForFailure"",
  ""Title"": ""ReasonForFailure"",
  ""FriendlyName"": ""Reason For Failure"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""SearchKind"": 1,
  ""Name"": ""IsDesignRequired"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__IsDesignRequired__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""IsDesignRequired"",
  ""Title"": ""IsDesignRequired"",
  ""FriendlyName"": ""Is Design Required"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""DrawingNumber"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__DrawingNumber__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""DrawingNumber"",
  ""Title"": ""DrawingNumber"",
  ""FriendlyName"": ""Drawing Number"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""RevisionKey"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__RevisionKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""FriendlyName"": ""Revision Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [
    ""Iql:Version""
  ],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""PersistenceKey"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersistenceKey__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""FriendlyName"": ""Persistence Key"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteInspection"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspection__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspection"",
  ""Title"": ""SiteInspection"",
  ""FriendlyName"": ""Site Inspection"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedByUser"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""SiteInspectionId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""SiteInspection\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""PersonInspectionsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteInspectionId_SiteInspection__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteInspection"",
  ""Title"": ""Site Inspection"",
  ""FriendlyName"": ""Site Inspection"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""PersonInspections\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""PersonInspectionsCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PersonInspections__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonInspections"",
  ""Title"": ""Person Inspections"",
  ""FriendlyName"": ""Person Inspections"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""SiteInspection:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonInspection\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonInspectionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedByUserId_CreatedByUser__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedByUser"",
  ""Title"": ""Created By User"",
  ""FriendlyName"": ""Created By User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonInspectionsCreated\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""PersonInspectionsCreatedCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PersonInspectionsCreated__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonInspectionsCreated"",
  ""Title"": ""Person Inspections Created"",
  ""FriendlyName"": ""Person Inspections Created"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""PersonInspection"",
      ""Title"": ""PersonInspection"",
      ""FriendlyName"": ""Person Inspection"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": true,
        ""Properties"": [
          ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""PersonId\"",\""Kind\"":1,\""Children\"":null}"",
          ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""Notes"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Person Types Map"",
      ""SetName"": ""PersonTypesMap"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""PersonId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonId__"",
  ""Key"": null,
  ""Kind"": 27,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PersonId"",
  ""Title"": ""PersonId"",
  ""FriendlyName"": ""Person Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""TypeId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId__"",
  ""Key"": null,
  ""Kind"": 27,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""TypeId"",
  ""Title"": ""TypeId"",
  ""FriendlyName"": ""Type Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Notes"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Notes__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Notes"",
  ""Title"": ""Notes"",
  ""FriendlyName"": ""Notes"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Description"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Description__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""FriendlyName"": ""Description"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": ""Guid"",
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""Guid"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 2,
  ""EditKind"": 5,
  ""IsReadOnly"": true,
  ""IsHidden"": true,
  ""Internal"": false,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Guid__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""FriendlyName"": ""Guid"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""SearchKind"": 1,
  ""Name"": ""CreatedDate"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__CreatedDate__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""FriendlyName"": ""Created Date"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Person"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Person__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Person"",
  ""Title"": ""Person"",
  ""FriendlyName"": ""Person"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Type"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Type__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""PersonId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""Person\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""TypesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__PersonId_Person__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Person"",
  ""Title"": ""Person"",
  ""FriendlyName"": ""Person"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Person\"",\""Paths\"":\""Types\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Person\"",\""Paths\"":\""TypesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Types__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Types"",
  ""Title"": ""Types"",
  ""FriendlyName"": ""Types"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Person:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""TypeId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""PersonTypeMap\"",\""Paths\"":\""Type\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleMapCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__TypeId_Type__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""FriendlyName"": ""Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleMap\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""PersonType\"",\""Paths\"":\""PeopleMapCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_PeopleMap__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""PeopleMap"",
  ""Title"": ""People Map"",
  ""FriendlyName"": ""People Map"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""PersonType:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""PersonTypeMap"",
      ""Title"": ""PersonTypeMap"",
      ""FriendlyName"": ""Person Type Map"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": true,
        ""Properties"": [
          ""{\""Type\"":\""UserSite\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
          ""{\""Type\"":\""UserSite\"",\""Paths\"":\""UserId\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""UserId"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""User Sites"",
      ""SetName"": ""UserSites"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""SearchKind"": 1,
  ""Name"": ""SiteId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId__"",
  ""Key"": null,
  ""Kind"": 27,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""FriendlyName"": ""Site Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""UserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__UserId__"",
  ""Key"": null,
  ""Kind"": 27,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""UserId"",
  ""Title"": ""UserId"",
  ""FriendlyName"": ""User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""User"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__User__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""User"",
  ""Title"": ""User"",
  ""FriendlyName"": ""User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""SearchKind"": 1,
  ""Name"": ""Site"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": true,
  ""IsHiddenOrInternal"": true,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Site__"",
  ""Key"": null,
  ""Kind"": 5,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""UserId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""User\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__UserId_User__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""User"",
  ""Title"": ""User"",
  ""FriendlyName"": ""User"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Sites\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Sites__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Sites"",
  ""Title"": ""Sites"",
  ""FriendlyName"": ""Sites"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""SiteId\"",\""Kind\"":1,\""Children\"":null}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 0,
  ""Type"": ""Unknown"",
  ""IsCollection"": false,
  ""Property"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""Site\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""UsersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SiteId_Site__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""FriendlyName"": ""Site"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""Target"": {
  ""ValueMappings"": [],
  ""RelationshipMappings"": [],
  ""AllowInlineEditing"": false,
  ""RelationshipSide"": 1,
  ""Type"": ""Unknown"",
  ""IsCollection"": true,
  ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Users\"",\""Kind\"":1,\""Children\"":null}"",
  ""CountProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""UsersCount\"",\""Kind\"":1,\""Children\"":null}"",
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id_Users__"",
  ""Key"": null,
  ""Kind"": 8,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Users"",
  ""Title"": ""Users"",
  ""FriendlyName"": ""Users"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        }
      ],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""UserSite"",
      ""Title"": ""UserSite"",
      ""FriendlyName"": ""User Site"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""MyCustomReport\"",\""Paths\"":\""MyId\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""MyUserId"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""My Custom Reports"",
      ""SetName"": ""MyCustomReports"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""SearchKind"": 1,
  ""Name"": ""MyId"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MyId__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MyId"",
  ""Title"": ""MyId"",
  ""FriendlyName"": ""My Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""MyUserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MyUserId__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MyUserId"",
  ""Title"": ""MyUserId"",
  ""FriendlyName"": ""My User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""MyName"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MyName__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MyName"",
  ""Title"": ""MyName"",
  ""FriendlyName"": ""My Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""MyEntityType"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MyEntityType__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MyEntityType"",
  ""Title"": ""MyEntityType"",
  ""FriendlyName"": ""My Entity Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""MyIql"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MyIql__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MyIql"",
  ""Title"": ""MyIql"",
  ""FriendlyName"": ""My Iql"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""MyFields"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MyFields__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MyFields"",
  ""Title"": ""MyFields"",
  ""FriendlyName"": ""My Fields"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""MySort"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MySort__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MySort"",
  ""Title"": ""MySort"",
  ""FriendlyName"": ""My Sort"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 1,
  ""Name"": ""MySortDescending"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MySortDescending__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MySortDescending"",
  ""Title"": ""MySortDescending"",
  ""FriendlyName"": ""My Sort Descending"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""MySearch"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__MySearch__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""MySearch"",
  ""Title"": ""MySearch"",
  ""FriendlyName"": ""My Search"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""MyCustomReport"",
      ""Title"": ""MyCustomReport"",
      ""FriendlyName"": ""My Custom Report"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
      ""DateRanges"": [],
      ""DisplayFormatting"": {
        ""All"": [],
        ""Default"": null
      },
      ""EntityValidation"": {
        ""All"": []
      },
      ""Key"": {
        ""HasRelationshipKeys"": false,
        ""Properties"": [
          ""{\""Type\"":\""IqlCustomReport\"",\""Paths\"":\""Id\"",\""Kind\"":1,\""Children\"":null}""
        ]
      },
      ""TitlePropertyName"": ""UserId"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 1,
      ""SetFriendlyName"": ""Iql Custom Report"",
      ""SetName"": ""IqlCustomReport"",
      ""SetNameSet"": false,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""EditDisplay"": [],
      ""ReadDisplay"": [],
      ""Properties"": [
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""SearchKind"": 1,
  ""Name"": ""Id"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": true,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Id__"",
  ""Key"": null,
  ""Kind"": 11,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""FriendlyName"": ""Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""UserId"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__UserId__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""UserId"",
  ""Title"": ""UserId"",
  ""FriendlyName"": ""User Id"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Name"",
  ""Searchable"": true,
  ""Nullable"": false,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Name__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""FriendlyName"": ""Name"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""EntityType"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__EntityType__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""EntityType"",
  ""Title"": ""EntityType"",
  ""FriendlyName"": ""Entity Type"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Iql"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Iql__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Iql"",
  ""Title"": ""Iql"",
  ""FriendlyName"": ""Iql"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Fields"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Fields__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Fields"",
  ""Title"": ""Fields"",
  ""FriendlyName"": ""Fields"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Sort"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Sort__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Sort"",
  ""Title"": ""Sort"",
  ""FriendlyName"": ""Sort"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 7
  },
  ""SearchKind"": 1,
  ""Name"": ""SortDescending"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__SortDescending__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""SortDescending"",
  ""Title"": ""SortDescending"",
  ""FriendlyName"": ""Sort Descending"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
},
        {
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""SearchKind"": 3,
  ""Name"": ""Search"",
  ""Searchable"": true,
  ""Nullable"": true,
  ""InferredWithIql"": null,
  ""EditKindChanged"": {},
  ""ReadKindChanged"": {},
  ""ReadKind"": 1,
  ""EditKind"": 1,
  ""IsReadOnly"": false,
  ""IsHidden"": false,
  ""Internal"": false,
  ""IsHiddenOrInternal"": false,
  ""SupportsInlineEditing"": true,
  ""PromptBeforeEdit"": false,
  ""Placeholder"": null,
  ""Sortable"": true,
  ""GroupName"": ""__Search__"",
  ""Key"": null,
  ""Kind"": 3,
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""Metadata"": {
    ""All"": []
  },
  ""Name"": ""Search"",
  ""Title"": ""Search"",
  ""FriendlyName"": ""Search"",
  ""GroupPath"": null,
  ""Description"": null,
  ""Hints"": [],
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [],
      ""Files"": [],
      ""Metadata"": {
        ""All"": []
      },
      ""Name"": ""IqlCustomReport"",
      ""Title"": ""IqlCustomReport"",
      ""FriendlyName"": ""Iql Custom Report"",
      ""GroupPath"": null,
      ""Description"": null,
      ""Hints"": [],
      ""HelpTexts"": []
    }
  ]
}";
    }
}