#if !TypeScript
namespace Iql.Tests.Tests.EntityConfiguration
{
    public class EntityConfigurationJson
    {
        public const string Json = @"
{
  ""EnumTypes"": [
    {
      ""Name"": ""UserType"",
      ""IsFlags"": false,
      ""Values"": [
        {
          ""Name"": ""Super"",
          ""Value"": 1
        },
        {
          ""Name"": ""Client"",
          ""Value"": 2
        },
        {
          ""Name"": ""Candidate"",
          ""Value"": 3
        }
      ]
    },
    {
      ""Name"": ""FaultReportStatus"",
      ""IsFlags"": false,
      ""Values"": [
        {
          ""Name"": ""Fail"",
          ""Value"": 0
        },
        {
          ""Name"": ""ImmediateActionRequired"",
          ""Value"": 1
        }
      ]
    },
    {
      ""Name"": ""NewProjectAlertKind"",
      ""IsFlags"": false,
      ""Values"": [
        {
          ""Name"": ""Critical"",
          ""Value"": 1
        },
        {
          ""Name"": ""All"",
          ""Value"": 2
        }
      ]
    },
    {
      ""Name"": ""ScaffoldCategoryType"",
      ""IsFlags"": false,
      ""Values"": [
        {
          ""Name"": ""System"",
          ""Value"": 0
        },
        {
          ""Name"": ""Conventional"",
          ""Value"": 1
        }
      ]
    },
    {
      ""Name"": ""ScaffoldStatus"",
      ""IsFlags"": false,
      ""Values"": [
        {
          ""Name"": ""Safe"",
          ""Value"": 0
        },
        {
          ""Name"": ""SafeWithImmediateActions"",
          ""Value"": 1
        },
        {
          ""Name"": ""Unsafe"",
          ""Value"": 2
        }
      ]
    },
    {
      ""Name"": ""ScaffoldInspectionStatus"",
      ""IsFlags"": false,
      ""Values"": [
        {
          ""Name"": ""Pass"",
          ""Value"": 0
        },
        {
          ""Name"": ""Fail"",
          ""Value"": 1
        },
        {
          ""Name"": ""ImmediateActionRequired"",
          ""Value"": 2
        }
      ]
    },
    {
      ""Name"": ""InspectionFailReason"",
      ""IsFlags"": false,
      ""Values"": [
        {
          ""Name"": ""None"",
          ""Value"": 0
        },
        {
          ""Name"": ""UnableToAccess"",
          ""Value"": 1
        },
        {
          ""Name"": ""PersistentFaults"",
          ""Value"": 2
        },
        {
          ""Name"": ""FailuresInFaultReports"",
          ""Value"": 3
        },
        {
          ""Name"": ""TooManyMinorObservations"",
          ""Value"": 4
        },
        {
          ""Name"": ""NoDesignSupplied"",
          ""Value"": 5
        }
      ]
    }
  ],
  ""EntityTypes"": [
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""UserName"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Users"",
      ""SetName"": ""Users"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Client Id"",
  ""Hints"": [],
  ""Name"": ""ClientId"",
  ""Title"": ""ClientId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Email"",
  ""Hints"": [
    ""Iql:EmailAddress""
  ],
  ""Name"": ""Email"",
  ""Title"": ""Email"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Full Name"",
  ""Hints"": [],
  ""Name"": ""FullName"",
  ""Title"": ""FullName"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Email Confirmed"",
  ""Hints"": [],
  ""Name"": ""EmailConfirmed"",
  ""Title"": ""EmailConfirmed"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""User Type"",
  ""Hints"": [],
  ""Name"": ""UserType"",
  ""Title"": ""UserType"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Is Locked Out"",
  ""Hints"": [],
  ""Name"": ""IsLockedOut"",
  ""Title"": ""IsLockedOut"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Client"",
  ""Hints"": [],
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Clients Created"",
  ""Hints"": [],
  ""Name"": ""ClientsCreated"",
  ""Title"": ""ClientsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Custom Reports"",
  ""Hints"": [],
  ""Name"": ""CustomReports"",
  ""Title"": ""CustomReports"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Custom Reports Created"",
  ""Hints"": [],
  ""Name"": ""CustomReportsCreated"",
  ""Title"": ""CustomReportsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Document Categories Created"",
  ""Hints"": [],
  ""Name"": ""DocumentCategoriesCreated"",
  ""Title"": ""DocumentCategoriesCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Actions Taken Created"",
  ""Hints"": [],
  ""Name"": ""FaultActionsTakenCreated"",
  ""Title"": ""FaultActionsTakenCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Categories Created"",
  ""Hints"": [],
  ""Name"": ""FaultCategoriesCreated"",
  ""Title"": ""FaultCategoriesCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Default Recommendations Created"",
  ""Hints"": [],
  ""Name"": ""FaultDefaultRecommendationsCreated"",
  ""Title"": ""FaultDefaultRecommendationsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Recommendations Created"",
  ""Hints"": [],
  ""Name"": ""FaultRecommendationsCreated"",
  ""Title"": ""FaultRecommendationsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Reports Created"",
  ""Hints"": [],
  ""Name"": ""FaultReportsCreated"",
  ""Title"": ""FaultReportsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Types Created"",
  ""Hints"": [],
  ""Name"": ""FaultTypesCreated"",
  ""Title"": ""FaultTypesCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Project Created"",
  ""Hints"": [],
  ""Name"": ""ProjectCreated"",
  ""Title"": ""ProjectCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Report Receiver Email Addresses Created"",
  ""Hints"": [],
  ""Name"": ""ReportReceiverEmailAddressesCreated"",
  ""Title"": ""ReportReceiverEmailAddressesCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Risk Assessments Created"",
  ""Hints"": [],
  ""Name"": ""RiskAssessmentsCreated"",
  ""Title"": ""RiskAssessmentsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Risk Assessment Answers Created"",
  ""Hints"": [],
  ""Name"": ""RiskAssessmentAnswersCreated"",
  ""Title"": ""RiskAssessmentAnswersCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Risk Assessment Questions Created"",
  ""Hints"": [],
  ""Name"": ""RiskAssessmentQuestionsCreated"",
  ""Title"": ""RiskAssessmentQuestionsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffolds Created"",
  ""Hints"": [],
  ""Name"": ""ScaffoldsCreated"",
  ""Title"": ""ScaffoldsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffold Inspections Created"",
  ""Hints"": [],
  ""Name"": ""ScaffoldInspectionsCreated"",
  ""Title"": ""ScaffoldInspectionsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffold Loadings Created"",
  ""Hints"": [],
  ""Name"": ""ScaffoldLoadingsCreated"",
  ""Title"": ""ScaffoldLoadingsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffold Types Created"",
  ""Hints"": [],
  ""Name"": ""ScaffoldTypesCreated"",
  ""Title"": ""ScaffoldTypesCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffold Systems"",
  ""Hints"": [],
  ""Name"": ""ScaffoldSystems"",
  ""Title"": ""ScaffoldSystems"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Sites Created"",
  ""Hints"": [],
  ""Name"": ""SitesCreated"",
  ""Title"": ""SitesCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Documents Created"",
  ""Hints"": [],
  ""Name"": ""SiteDocumentsCreated"",
  ""Title"": ""SiteDocumentsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Inspections Created"",
  ""Hints"": [],
  ""Name"": ""SiteInspectionsCreated"",
  ""Title"": ""SiteInspectionsCreated"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Sites"",
  ""Hints"": [],
  ""Name"": ""Sites"",
  ""Title"": ""Sites"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Client\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Users\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""UserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""User\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""CustomReports\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""CustomReportsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""DocumentCategoriesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultActionsTakenCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultCategoriesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultDefaultRecommendationsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultRecommendationsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultReportsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultTypesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Project\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Project\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ProjectCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ReportReceiverEmailAddressesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentAnswersCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentQuestionsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldInspectionsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldLoadingsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldTypesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldSystems\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteDocumentsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteInspectionsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""UserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""User\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Sites\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": null,
      ""Description"": null,
      ""FriendlyName"": ""Application User"",
      ""Hints"": [],
      ""Name"": ""ApplicationUser"",
      ""Title"": ""ApplicationUser"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
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
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Users"",
  ""Hints"": [],
  ""Name"": ""Users"",
  ""Title"": ""Users"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Type"",
  ""Hints"": [],
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Type Id"",
  ""Hints"": [],
  ""Name"": ""TypeId"",
  ""Title"": ""TypeId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Description"",
  ""Hints"": [
    ""Iql:BigText""
  ],
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffolds"",
  ""Hints"": [],
  ""Name"": ""Scaffolds"",
  ""Title"": ""Scaffolds"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Sites"",
  ""Hints"": [],
  ""Name"": ""Sites"",
  ""Title"": ""Sites"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Client\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Users\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""TypeId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Type\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Clients\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ClientType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ClientsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""ClientId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Client\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Scaffolds\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""ClientId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Client\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Sites\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        }
      ],
      ""GroupPath"": null,
      ""Description"": null,
      ""FriendlyName"": ""Client"",
      ""Hints"": [],
      ""Name"": ""Client"",
      ""Title"": ""Client"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 2,
      ""SetFriendlyName"": ""Client Types"",
      ""SetName"": ""ClientTypes"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Clients"",
  ""Hints"": [],
  ""Name"": ""Clients"",
  ""Title"": ""Clients"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""TypeId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Type\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ClientType\"",\""Paths\"":\""Clients\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ClientType:Id""
        }
      ],
      ""GroupPath"": null,
      ""Description"": null,
      ""FriendlyName"": ""Client Type"",
      ""Hints"": [],
      ""Name"": ""ClientType"",
      ""Title"": ""ClientType"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 26,
      ""SetFriendlyName"": ""Custom report"",
      ""SetName"": ""CustomReports"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""User"",
  ""Hints"": [],
  ""Name"": ""User"",
  ""Title"": ""User"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""User Id"",
  ""Hints"": [],
  ""Name"": ""UserId"",
  ""Title"": ""UserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Report"",
  ""Hints"": [],
  ""Name"": ""Report"",
  ""Title"": ""Report"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Path"",
  ""Hints"": [],
  ""Name"": ""Path"",
  ""Title"": ""Path"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Query"",
  ""Hints"": [],
  ""Name"": ""Query"",
  ""Title"": ""Query"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Iql"",
  ""Hints"": [],
  ""Name"": ""Iql"",
  ""Title"": ""Iql"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fields"",
  ""Hints"": [],
  ""Name"": ""Fields"",
  ""Title"": ""Fields"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": [
      {
        ""Key"": ""1"",
        ""Message"": ""Please enter a report name"",
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>CustomReport</EntityTypeName>\r\n      <VariableName>report</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlOrExpression\"">\r\n    <Kind>Or</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <EntityTypeName>CustomReport</EntityTypeName>\r\n          <VariableName>report</VariableName>\r\n        </Parent>\r\n        <Name>Name</Name>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlStringTrimExpression\"">\r\n        <Kind>StringTrim</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Parent xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n            <Kind>RootReference</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <EntityTypeName>CustomReport</EntityTypeName>\r\n            <VariableName>report</VariableName>\r\n          </Parent>\r\n          <Name>Name</Name>\r\n        </Parent>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"" />\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": ""Save current filters as..."",
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""UserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""User\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""CustomReports\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""CustomReport\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""CustomReportsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": null,
      ""Description"": null,
      ""FriendlyName"": ""Custom Report"",
      ""Hints"": [],
      ""Name"": ""CustomReport"",
      ""Title"": ""CustomReport"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
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
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Documents"",
  ""Hints"": [],
  ""Name"": ""Documents"",
  ""Title"": ""Documents"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""DocumentCategoriesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CategoryId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Category\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Documents\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""DocumentCategory:Id""
        }
      ],
      ""GroupPath"": ""Sites"",
      ""Description"": null,
      ""FriendlyName"": ""Document Category"",
      ""Hints"": [],
      ""Name"": ""DocumentCategory"",
      ""Title"": ""DocumentCategory"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Fault Actions Taken"",
      ""SetName"": ""FaultActionsTaken"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Report"",
  ""Hints"": [],
  ""Name"": ""FaultReport"",
  ""Title"": ""FaultReport"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Report Id"",
  ""Hints"": [],
  ""Name"": ""FaultReportId"",
  ""Title"": ""FaultReportId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Notes"",
  ""Hints"": [],
  ""Name"": ""Notes"",
  ""Title"": ""Notes"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""FaultReportId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""FaultReport\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""ActionsTaken\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultReport:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultActionsTakenCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Faults"",
      ""Description"": null,
      ""FriendlyName"": ""Fault Actions Taken"",
      ""Hints"": [],
      ""Name"": ""FaultActionsTaken"",
      ""Title"": ""FaultActionsTaken"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Fault Reports"",
      ""SetName"": ""FaultReports"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Actions Taken"",
  ""Hints"": [],
  ""Name"": ""ActionsTaken"",
  ""Title"": ""ActionsTaken"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Recommendations"",
  ""Hints"": [],
  ""Name"": ""Recommendations"",
  ""Title"": ""Recommendations"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffold"",
  ""Hints"": [],
  ""Name"": ""Scaffold"",
  ""Title"": ""Scaffold"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffold Id"",
  ""Hints"": [],
  ""Name"": ""ScaffoldId"",
  ""Title"": ""ScaffoldId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Type"",
  ""Hints"": [],
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Type Id"",
  ""Hints"": [],
  ""Name"": ""TypeId"",
  ""Title"": ""TypeId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Rectified"",
  ""Hints"": [],
  ""Name"": ""Rectified"",
  ""Title"": ""Rectified"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Status"",
  ""Hints"": [],
  ""Name"": ""Status"",
  ""Title"": ""Status"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""FaultReportId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultActionsTaken\"",\""Paths\"":\""FaultReport\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""ActionsTaken\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultReport:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""FaultReportId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""FaultReport\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Recommendations\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultReport:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""ScaffoldId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Scaffold\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""FaultReports\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Scaffold:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""TypeId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Type\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""FaultReports\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultReportsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Faults"",
      ""Description"": null,
      ""FriendlyName"": ""Fault Report"",
      ""Hints"": [],
      ""Name"": ""FaultReport"",
      ""Title"": ""FaultReport"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Fault Categories"",
      ""SetName"": ""FaultCategories"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Types"",
  ""Hints"": [],
  ""Name"": ""FaultTypes"",
  ""Title"": ""FaultTypes"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultCategoriesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""CategoryId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""Category\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""FaultTypes\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultCategory:Id""
        }
      ],
      ""GroupPath"": ""Faults"",
      ""Description"": null,
      ""FriendlyName"": ""Fault Category"",
      ""Hints"": [],
      ""Name"": ""FaultCategory"",
      ""Title"": ""FaultCategory"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Fault Default Recommendations"",
      ""SetName"": ""FaultDefaultRecommendations"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Text"",
  ""Hints"": [
    ""Iql:BigText""
  ],
  ""Name"": ""Text"",
  ""Title"": ""Text"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Recommendations"",
  ""Hints"": [],
  ""Name"": ""Recommendations"",
  ""Title"": ""Recommendations"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultDefaultRecommendationsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""FaultReportId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""Recommendation\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""Recommendations\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultDefaultRecommendation:Id""
        }
      ],
      ""GroupPath"": ""Faults"",
      ""Description"": null,
      ""FriendlyName"": ""Fault Default Recommendation"",
      ""Hints"": [],
      ""Name"": ""FaultDefaultRecommendation"",
      ""Title"": ""FaultDefaultRecommendation"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""RecommendationId\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Fault Recommendations"",
      ""SetName"": ""FaultRecommendations"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 13,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Report Id"",
  ""Hints"": [],
  ""Name"": ""FaultReportId"",
  ""Title"": ""FaultReportId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Recommendation Id"",
  ""Hints"": [],
  ""Name"": ""RecommendationId"",
  ""Title"": ""RecommendationId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Report"",
  ""Hints"": [],
  ""Name"": ""FaultReport"",
  ""Title"": ""FaultReport"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Recommendation"",
  ""Hints"": [],
  ""Name"": ""Recommendation"",
  ""Title"": ""Recommendation"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Notes"",
  ""Hints"": [],
  ""Name"": ""Notes"",
  ""Title"": ""Notes"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""FaultReportId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""FaultReport\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Recommendations\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultReport:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""FaultReportId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""Recommendation\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultDefaultRecommendation\"",\""Paths\"":\""Recommendations\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultDefaultRecommendation:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultRecommendation\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultRecommendationsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Faults"",
      ""Description"": null,
      ""FriendlyName"": ""Fault Recommendation"",
      ""Hints"": [],
      ""Name"": ""FaultRecommendation"",
      ""Title"": ""FaultRecommendation"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Scaffolds"",
      ""SetName"": ""Scaffolds"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Reports"",
  ""Hints"": [],
  ""Name"": ""FaultReports"",
  ""Title"": ""FaultReports"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Client"",
  ""Hints"": [],
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Type"",
  ""Hints"": [],
  ""Name"": ""Type"",
  ""Title"": ""Type"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Type Id"",
  ""Hints"": [],
  ""Name"": ""TypeId"",
  ""Title"": ""TypeId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": [
      {
        ""Kind"": 1,
        ""AppliesToKind"": 1,
        ""Key"": ""2"",
        ""Message"": null,
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>Scaffold</EntityTypeName>\r\n      <VariableName>scaffold</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlIsEqualToExpression\"">\r\n    <Kind>IsEqualTo</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlPropertyExpression\"">\r\n      <Kind>Property</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <EntityTypeName>Scaffold</EntityTypeName>\r\n        <VariableName>scaffold</VariableName>\r\n      </Parent>\r\n      <Name>TypeId</Name>\r\n    </Left>\r\n    <Right xsi:type=\""IqlLiteralExpression\"">\r\n      <Kind>Literal</Kind>\r\n      <ReturnType>Integer</ReturnType>\r\n      <Value xsi:type=\""xsd:int\"">1</Value>\r\n      <InferredReturnType>Integer</InferredReturnType>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""System"",
  ""Hints"": [],
  ""Name"": ""System"",
  ""Title"": ""System"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""System Id"",
  ""Hints"": [],
  ""Name"": ""SystemId"",
  ""Title"": ""SystemId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Loading"",
  ""Hints"": [],
  ""Name"": ""Loading"",
  ""Title"": ""Loading"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Loading Id"",
  ""Hints"": [],
  ""Name"": ""LoadingId"",
  ""Title"": ""LoadingId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Key"",
  ""Hints"": [],
  ""Name"": ""Key"",
  ""Title"": ""Key"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Description"",
  ""Hints"": [
    ""Iql:BigText""
  ],
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Category"",
  ""Hints"": [],
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Status"",
  ""Hints"": [],
  ""Name"": ""Status"",
  ""Title"": ""Status"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Client Id"",
  ""Hints"": [],
  ""Name"": ""ClientId"",
  ""Title"": ""ClientId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Client Guid"",
  ""Hints"": [],
  ""Name"": ""ClientGuid"",
  ""Title"": ""ClientGuid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": [
      {
        ""Separator"": ""-"",
        ""Parts"": [
          {
            ""IsPropertyPath"": true,
            ""Key"": ""Client/Guid""
          }
        ]
      },
      {
        ""Separator"": ""-"",
        ""Parts"": [
          {
            ""IsPropertyPath"": true,
            ""Key"": ""Guid""
          },
          {
            ""IsPropertyPath"": false,
            ""Key"": ""ImageUrl""
          }
        ]
      }
    ]
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Photo"",
  ""Hints"": [
    ""Iql:File"",
    ""Iql:Image""
  ],
  ""Name"": ""ImageUrl"",
  ""Title"": ""Photo"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""ScaffoldId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Scaffold\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""FaultReports\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Scaffold:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""ClientId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Client\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Scaffolds\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""TypeId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Type\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""Scaffolds\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ScaffoldType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""SystemId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""System\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""Scaffolds\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ScaffoldSystem:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""LoadingId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Loading\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""Scaffolds\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ScaffoldLoading:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Scaffolds"",
      ""Description"": null,
      ""FriendlyName"": ""Scaffold"",
      ""Hints"": [],
      ""Name"": ""Scaffold"",
      ""Title"": ""Scaffold"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""FaultType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Fault Types"",
      ""SetName"": ""FaultTypes"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Fault Reports"",
  ""Hints"": [],
  ""Name"": ""FaultReports"",
  ""Title"": ""FaultReports"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Category"",
  ""Hints"": [],
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Category Id"",
  ""Hints"": [],
  ""Name"": ""CategoryId"",
  ""Title"": ""CategoryId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""TypeId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultReport\"",\""Paths\"":\""Type\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""FaultReports\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""CategoryId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""Category\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""FaultCategory\"",\""Paths\"":\""FaultTypes\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""FaultCategory:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""FaultType\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""FaultTypesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Faults"",
      ""Description"": null,
      ""FriendlyName"": ""Fault Type"",
      ""Hints"": [],
      ""Name"": ""FaultType"",
      ""Title"": ""FaultType"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""New Project Contacts"",
      ""SetName"": ""NewProjectContacts"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Notification"",
  ""Hints"": [],
  ""Name"": ""Notification"",
  ""Title"": ""Notification"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Notification Id"",
  ""Hints"": [],
  ""Name"": ""NotificationId"",
  ""Title"": ""NotificationId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Kind"",
  ""Hints"": [],
  ""Name"": ""Kind"",
  ""Title"": ""Kind"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Kind Id"",
  ""Hints"": [],
  ""Name"": ""KindId"",
  ""Title"": ""KindId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Alerts"",
  ""Hints"": [],
  ""Name"": ""Alerts"",
  ""Title"": ""Alerts"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Email Address"",
  ""Hints"": [
    ""Iql:EmailAddress""
  ],
  ""Name"": ""EmailAddress"",
  ""Title"": ""EmailAddress"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Phone Number"",
  ""Hints"": [
    ""Iql:PhoneNumber""
  ],
  ""Name"": ""PhoneNumber"",
  ""Title"": ""PhoneNumber"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""NotificationId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""NewProjectNotification\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""Notification\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""NewProjectNotification\"",\""Paths\"":\""Contacts\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""NewProjectNotification:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""KindId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""NewProjectContactKind\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""Kind\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""NewProjectContactKind\"",\""Paths\"":\""Contacts\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""NewProjectContactKind:Id""
        }
      ],
      ""GroupPath"": ""New Projects"",
      ""Description"": null,
      ""FriendlyName"": ""New Project Contact"",
      ""Hints"": [],
      ""Name"": ""NewProjectContact"",
      ""Title"": ""NewProjectContact"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""NewProjectNotification\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""New Project Notifications"",
      ""SetName"": ""NewProjectNotifications"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Contacts"",
  ""Hints"": [],
  ""Name"": ""Contacts"",
  ""Title"": ""Contacts"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Start Date"",
  ""Hints"": [],
  ""Name"": ""StartDate"",
  ""Title"": ""StartDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Anticipated Finish Date"",
  ""Hints"": [],
  ""Name"": ""AnticipatedEndDate"",
  ""Title"": ""Anticipated Finish Date"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Address"",
  ""Hints"": [
    ""Iql:BigText""
  ],
  ""Name"": ""SiteAddress"",
  ""Title"": ""SiteAddress"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Contract Number"",
  ""Hints"": [
    ""Iql:PhoneNumber""
  ],
  ""Name"": ""ContractNumber"",
  ""Title"": ""ContractNumber"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""First Inspection Required"",
  ""Hints"": [],
  ""Name"": ""FirstInspectionRequired"",
  ""Title"": ""FirstInspectionRequired"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""On Site Contact"",
  ""Hints"": [],
  ""Name"": ""OnSiteContact"",
  ""Title"": ""OnSiteContact"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Have designs been supplied with this notice?"",
  ""Hints"": [],
  ""Name"": ""DesignsSupplied"",
  ""Title"": ""Have designs been supplied with this notice?"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Has construction programme been supplied with this notice?"",
  ""Hints"": [],
  ""Name"": ""ConstructionProgrammeSupplied"",
  ""Title"": ""Has construction programme been supplied with this notice?"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""NotificationId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""NewProjectNotification\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""Notification\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""NewProjectNotification\"",\""Paths\"":\""Contacts\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""NewProjectNotification:Id""
        }
      ],
      ""GroupPath"": ""New Projects"",
      ""Description"": null,
      ""FriendlyName"": ""New Project Notification"",
      ""Hints"": [],
      ""Name"": ""NewProjectNotification"",
      ""Title"": ""NewProjectNotification"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""NewProjectContactKind\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""New Project Contact Kinds"",
      ""SetName"": ""NewProjectContactKinds"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Contacts"",
  ""Hints"": [],
  ""Name"": ""Contacts"",
  ""Title"": ""Contacts"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""KindId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""NewProjectContactKind\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""NewProjectContact\"",\""Paths\"":\""Kind\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""NewProjectContactKind\"",\""Paths\"":\""Contacts\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""NewProjectContactKind:Id""
        }
      ],
      ""GroupPath"": ""New Projects"",
      ""Description"": null,
      ""FriendlyName"": ""New Project Contact Kind"",
      ""Hints"": [],
      ""Name"": ""NewProjectContactKind"",
      ""Title"": ""NewProjectContactKind"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""Project\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Projects"",
      ""SetName"": ""Projects"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Description"",
  ""Hints"": [
    ""Iql:BigText""
  ],
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Project\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Project\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ProjectCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": null,
      ""Description"": null,
      ""FriendlyName"": ""Project"",
      ""Hints"": [],
      ""Name"": ""Project"",
      ""Title"": ""Project"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Report Receiver Email Addresses"",
      ""SetName"": ""ReportReceiverEmailAddresses"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site"",
  ""Hints"": [],
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Id"",
  ""Hints"": [],
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Email Address"",
  ""Hints"": [],
  ""Name"": ""EmailAddress"",
  ""Title"": ""EmailAddress"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""SiteId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""Site\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AdditionalSendReportsTo\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ReportReceiverEmailAddressesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Faults"",
      ""Description"": null,
      ""FriendlyName"": ""Report Receiver Email Address"",
      ""Hints"": [],
      ""Name"": ""ReportReceiverEmailAddress"",
      ""Title"": ""ReportReceiverEmailAddress"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [
        {
  ""Key"": null,
  ""LongitudeProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": true,
      ""Kind"": 6
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 1,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": true,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Longitude"",
    ""Hints"": [],
    ""Name"": ""Longitude"",
    ""Title"": ""Longitude"",
    ""HelpTexts"": []
  },
  ""LatitudeProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": true,
      ""Kind"": 6
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 1,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": true,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Latitude"",
    ""Hints"": [],
    ""Name"": ""Latitude"",
    ""Title"": ""Latitude"",
    ""HelpTexts"": [
      {
        ""Text"": ""Select the location of the site on the map.\n\nYou can search for the address or postcode, and drag the red pin to the precise location."",
        ""Kind"": 1
      }
    ]
  }
}
      ],
      ""NestedSets"": [
        {
  ""SetKey"": null,
  ""LeftProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": false,
      ""Kind"": 5
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 1,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": false,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Left"",
    ""Hints"": [],
    ""Name"": ""Left"",
    ""Title"": ""Left"",
    ""HelpTexts"": []
  },
  ""RightProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": false,
      ""Kind"": 5
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 1,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": false,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Right"",
    ""Hints"": [],
    ""Name"": ""Right"",
    ""Title"": ""Right"",
    ""HelpTexts"": []
  },
  ""LeftOfProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": true,
      ""Kind"": 5
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 1,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": true,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Left Of"",
    ""Hints"": [],
    ""Name"": ""LeftOf"",
    ""Title"": ""LeftOf"",
    ""HelpTexts"": []
  },
  ""RightOfProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": true,
      ""Kind"": 5
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 1,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": true,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Right Of"",
    ""Hints"": [],
    ""Name"": ""RightOf"",
    ""Title"": ""RightOf"",
    ""HelpTexts"": []
  },
  ""KeyProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": true,
      ""Kind"": 4
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 1,
    ""SearchKind"": 3,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": true,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Key"",
    ""Hints"": [],
    ""Name"": ""Key"",
    ""Title"": ""Key"",
    ""HelpTexts"": []
  },
  ""LevelProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": false,
      ""Kind"": 5
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 1,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": false,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Level"",
    ""Hints"": [],
    ""Name"": ""Level"",
    ""Title"": ""Level"",
    ""HelpTexts"": []
  },
  ""ParentIdProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": true,
      ""Kind"": 5
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 9,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": true,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Parent Id"",
    ""Hints"": [],
    ""Name"": ""ParentId"",
    ""Title"": ""ParentId"",
    ""HelpTexts"": []
  },
  ""ParentProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": [
        {
          ""Kind"": 1,
          ""AppliesToKind"": 1,
          ""Key"": ""3"",
          ""Message"": null,
          ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>Site</EntityTypeName>\r\n      <VariableName>site</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlIsGreaterThanExpression\"">\r\n    <Kind>IsGreaterThan</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlPropertyExpression\"">\r\n      <Kind>Property</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <EntityTypeName>Site</EntityTypeName>\r\n        <VariableName>site</VariableName>\r\n      </Parent>\r\n      <Name>ClientId</Name>\r\n    </Left>\r\n    <Right xsi:type=\""IqlLiteralExpression\"">\r\n      <Kind>Literal</Kind>\r\n      <ReturnType>Integer</ReturnType>\r\n      <Value xsi:type=\""xsd:int\"">0</Value>\r\n      <InferredReturnType>Integer</InferredReturnType>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
        }
      ]
    },
    ""RelationshipFilterRules"": {
      ""All"": [
        {
          ""Key"": ""4"",
          ""Message"": null,
          ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>RelationshipFilterContext&lt;Site&gt;</EntityTypeName>\r\n      <VariableName>context</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlLambdaExpression\"">\r\n    <Kind>Lambda</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Parameters>\r\n      <IqlRootReferenceExpression>\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <EntityTypeName>Site</EntityTypeName>\r\n        <VariableName>site</VariableName>\r\n      </IqlRootReferenceExpression>\r\n    </Parameters>\r\n    <Body xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <EntityTypeName>Site</EntityTypeName>\r\n          <VariableName>site</VariableName>\r\n        </Parent>\r\n        <Name>ClientId</Name>\r\n      </Left>\r\n      <Right xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlVariableExpression\"">\r\n            <Kind>Variable</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <EntityTypeName>RelationshipFilterContext&lt;Site&gt;</EntityTypeName>\r\n            <VariableName>context</VariableName>\r\n          </Parent>\r\n          <Name>Owner</Name>\r\n        </Parent>\r\n        <Name>ClientId</Name>\r\n      </Right>\r\n    </Body>\r\n  </Body>\r\n</IqlLambdaExpression>""
        }
      ]
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": true,
      ""Kind"": 1
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 2,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": true,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Parent"",
    ""Hints"": [],
    ""Name"": ""Parent"",
    ""Title"": ""Parent"",
    ""HelpTexts"": []
  },
  ""IdProperty"": {
    ""ValidationRules"": {
      ""All"": []
    },
    ""DisplayRules"": {
      ""All"": []
    },
    ""RelationshipFilterRules"": {
      ""All"": []
    },
    ""Relationships"": [],
    ""TypeDefinition"": {
      ""IsCollection"": false,
      ""ConvertedFromType"": null,
      ""Nullable"": false,
      ""Kind"": 5
    },
    ""MediaKey"": {
      ""Separator"": ""/"",
      ""Groups"": []
    },
    ""Placeholder"": null,
    ""Kind"": 5,
    ""SearchKind"": 1,
    ""ReadOnly"": true,
    ""Hidden"": false,
    ""Sortable"": true,
    ""Searchable"": true,
    ""Nullable"": false,
    ""GroupPath"": null,
    ""Description"": null,
    ""FriendlyName"": ""Id"",
    ""Hints"": [],
    ""Name"": ""Id"",
    ""Title"": ""Id"",
    ""HelpTexts"": []
  }
}
      ],
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
          ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Sites"",
      ""SetName"": ""Sites"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [
        ""{\""Type\"":\""Site\"",\""Paths\"":\""Client\"",\""Kind\"":1}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""Parent\"",\""Kind\"":1}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""Name\"",\""Kind\"":1}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""Address\"",\""Kind\"":1}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""PostCode\"",\""Kind\"":1}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""WeeklyCharge\"",\""Kind\"":1}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""0\"",\""Kind\"":3}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""0\"",\""Kind\"":4}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""Key\"",\""Kind\"":1}"",
        ""{\""Type\"":\""Site\"",\""Paths\"":\""DrawingNumber\"",\""Kind\"":1}""
      ],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Additional Send Reports To"",
  ""Hints"": [],
  ""Name"": ""AdditionalSendReportsTo"",
  ""Title"": ""AdditionalSendReportsTo"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": [
      {
        ""Kind"": 1,
        ""AppliesToKind"": 1,
        ""Key"": ""3"",
        ""Message"": null,
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>Site</EntityTypeName>\r\n      <VariableName>site</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlIsGreaterThanExpression\"">\r\n    <Kind>IsGreaterThan</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlPropertyExpression\"">\r\n      <Kind>Property</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <EntityTypeName>Site</EntityTypeName>\r\n        <VariableName>site</VariableName>\r\n      </Parent>\r\n      <Name>ClientId</Name>\r\n    </Left>\r\n    <Right xsi:type=\""IqlLiteralExpression\"">\r\n      <Kind>Literal</Kind>\r\n      <ReturnType>Integer</ReturnType>\r\n      <Value xsi:type=\""xsd:int\"">0</Value>\r\n      <InferredReturnType>Integer</InferredReturnType>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""RelationshipFilterRules"": {
    ""All"": [
      {
        ""Key"": ""4"",
        ""Message"": null,
        ""Expression"": ""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>RelationshipFilterContext&lt;Site&gt;</EntityTypeName>\r\n      <VariableName>context</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlLambdaExpression\"">\r\n    <Kind>Lambda</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Parameters>\r\n      <IqlRootReferenceExpression>\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <EntityTypeName>Site</EntityTypeName>\r\n        <VariableName>site</VariableName>\r\n      </IqlRootReferenceExpression>\r\n    </Parameters>\r\n    <Body xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <EntityTypeName>Site</EntityTypeName>\r\n          <VariableName>site</VariableName>\r\n        </Parent>\r\n        <Name>ClientId</Name>\r\n      </Left>\r\n      <Right xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlVariableExpression\"">\r\n            <Kind>Variable</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <EntityTypeName>RelationshipFilterContext&lt;Site&gt;</EntityTypeName>\r\n            <VariableName>context</VariableName>\r\n          </Parent>\r\n          <Name>Owner</Name>\r\n        </Parent>\r\n        <Name>ClientId</Name>\r\n      </Right>\r\n    </Body>\r\n  </Body>\r\n</IqlLambdaExpression>""
      }
    ]
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Parent"",
  ""Hints"": [],
  ""Name"": ""Parent"",
  ""Title"": ""Parent"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Parent Id"",
  ""Hints"": [],
  ""Name"": ""ParentId"",
  ""Title"": ""ParentId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Client"",
  ""Hints"": [],
  ""Name"": ""Client"",
  ""Title"": ""Client"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Client Id"",
  ""Hints"": [],
  ""Name"": ""ClientId"",
  ""Title"": ""ClientId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 6
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Weekly Charge"",
  ""Hints"": [
    ""Iql:Currency""
  ],
  ""Name"": ""WeeklyCharge"",
  ""Title"": ""WeeklyCharge"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Address"",
  ""Hints"": [
    ""Iql:BigText""
  ],
  ""Name"": ""Address"",
  ""Title"": ""Address"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Post Code"",
  ""Hints"": [],
  ""Name"": ""PostCode"",
  ""Title"": ""PostCode"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Key"",
  ""Hints"": [],
  ""Name"": ""Key"",
  ""Title"": ""Key"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 6
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Latitude"",
  ""Hints"": [],
  ""Name"": ""Latitude"",
  ""Title"": ""Latitude"",
  ""HelpTexts"": [
    {
      ""Text"": ""Select the location of the site on the map.\n\nYou can search for the address or postcode, and drag the red pin to the precise location."",
      ""Kind"": 1
    }
  ]
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 6
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Longitude"",
  ""Hints"": [],
  ""Name"": ""Longitude"",
  ""Title"": ""Longitude"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Left Of"",
  ""Hints"": [],
  ""Name"": ""LeftOf"",
  ""Title"": ""LeftOf"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Right Of"",
  ""Hints"": [],
  ""Name"": ""RightOf"",
  ""Title"": ""RightOf"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Level"",
  ""Hints"": [],
  ""Name"": ""Level"",
  ""Title"": ""Level"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Left"",
  ""Hints"": [],
  ""Name"": ""Left"",
  ""Title"": ""Left"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Right"",
  ""Hints"": [],
  ""Name"": ""Right"",
  ""Title"": ""Right"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Drawing Number"",
  ""Hints"": [],
  ""Name"": ""DrawingNumber"",
  ""Title"": ""DrawingNumber"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Children"",
  ""Hints"": [],
  ""Name"": ""Children"",
  ""Title"": ""Children"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Documents"",
  ""Hints"": [],
  ""Name"": ""Documents"",
  ""Title"": ""Documents"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Inspections"",
  ""Hints"": [],
  ""Name"": ""SiteInspections"",
  ""Title"": ""SiteInspections"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Users"",
  ""Hints"": [],
  ""Name"": ""Users"",
  ""Title"": ""Users"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""SiteId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ReportReceiverEmailAddress\"",\""Paths\"":\""Site\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""AdditionalSendReportsTo\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""ParentId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Parent\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Children\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""ClientId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Client\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Client\"",\""Paths\"":\""Sites\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Client:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SitesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""SiteId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Site\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Documents\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""SiteId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Site\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""SiteInspections\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""SiteId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""Site\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Users\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        }
      ],
      ""GroupPath"": ""Sites"",
      ""Description"": null,
      ""FriendlyName"": ""Site"",
      ""Hints"": [],
      ""Name"": ""Site"",
      ""Title"": ""Site"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Risk Assessments"",
      ""SetName"": ""RiskAssessments"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Inspection Id"",
  ""Hints"": [],
  ""Name"": ""SiteInspectionId"",
  ""Title"": ""SiteInspectionId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Inspection"",
  ""Hints"": [],
  ""Name"": ""SiteInspection"",
  ""Title"": ""SiteInspection"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""RiskAssessment\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Risk Assessments"",
      ""Description"": null,
      ""FriendlyName"": ""Risk Assessment"",
      ""Hints"": [],
      ""Name"": ""RiskAssessment"",
      ""Title"": ""RiskAssessment"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Risk Assessment Answers"",
      ""SetName"": ""RiskAssessmentAnswers"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Question"",
  ""Hints"": [],
  ""Name"": ""Question"",
  ""Title"": ""Question"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Question Id"",
  ""Hints"": [],
  ""Name"": ""QuestionId"",
  ""Title"": ""QuestionId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Specific Hazard"",
  ""Hints"": [],
  ""Name"": ""SpecificHazard"",
  ""Title"": ""SpecificHazard"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Precautions To Control Hazard"",
  ""Hints"": [],
  ""Name"": ""PrecautionsToControlHazard"",
  ""Title"": ""PrecautionsToControlHazard"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""QuestionId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""Question\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Answers\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""RiskAssessmentQuestion:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentAnswersCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Risk Assessments"",
      ""Description"": null,
      ""FriendlyName"": ""Risk Assessment Answer"",
      ""Hints"": [],
      ""Name"": ""RiskAssessmentAnswer"",
      ""Title"": ""RiskAssessmentAnswer"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
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
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Answers"",
  ""Hints"": [],
  ""Name"": ""Answers"",
  ""Title"": ""Answers"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""QuestionId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""RiskAssessmentAnswer\"",\""Paths\"":\""Question\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""Answers\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""RiskAssessmentQuestion:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""RiskAssessmentQuestion\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""RiskAssessmentQuestionsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Risk Assessments"",
      ""Description"": null,
      ""FriendlyName"": ""Risk Assessment Question"",
      ""Hints"": [],
      ""Name"": ""RiskAssessmentQuestion"",
      ""Title"": ""RiskAssessmentQuestion"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Scaffold Types"",
      ""SetName"": ""ScaffoldTypes"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffolds"",
  ""Hints"": [],
  ""Name"": ""Scaffolds"",
  ""Title"": ""Scaffolds"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Description"",
  ""Hints"": [
    ""Iql:BigText""
  ],
  ""Name"": ""Description"",
  ""Title"": ""Description"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""TypeId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Type\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""Scaffolds\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ScaffoldType:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldType\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldTypesCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Scaffolds"",
      ""Description"": null,
      ""FriendlyName"": ""Scaffold Type"",
      ""Hints"": [],
      ""Name"": ""ScaffoldType"",
      ""Title"": ""ScaffoldType"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Scaffold Systems"",
      ""SetName"": ""ScaffoldSystems"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffolds"",
  ""Hints"": [],
  ""Name"": ""Scaffolds"",
  ""Title"": ""Scaffolds"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""SystemId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""System\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""Scaffolds\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ScaffoldSystem:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldSystem\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldSystems\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Scaffolds"",
      ""Description"": null,
      ""FriendlyName"": ""Scaffold System"",
      ""Hints"": [],
      ""Name"": ""ScaffoldSystem"",
      ""Title"": ""ScaffoldSystem"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Scaffold Loadings"",
      ""SetName"": ""ScaffoldLoadings"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffolds"",
  ""Hints"": [],
  ""Name"": ""Scaffolds"",
  ""Title"": ""Scaffolds"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""LoadingId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""Scaffold\"",\""Paths\"":\""Loading\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""Scaffolds\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ScaffoldLoading:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldLoading\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldLoadingsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Scaffolds"",
      ""Description"": null,
      ""FriendlyName"": ""Scaffold Loading"",
      ""Hints"": [],
      ""Name"": ""ScaffoldLoading"",
      ""Title"": ""ScaffoldLoading"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Scaffold Inspections"",
      ""SetName"": ""ScaffoldInspections"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Inspection"",
  ""Hints"": [],
  ""Name"": ""SiteInspection"",
  ""Title"": ""SiteInspection"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Inspection Id"",
  ""Hints"": [],
  ""Name"": ""SiteInspectionId"",
  ""Title"": ""SiteInspectionId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffold Id"",
  ""Hints"": [],
  ""Name"": ""ScaffoldId"",
  ""Title"": ""ScaffoldId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Inspection Status"",
  ""Hints"": [],
  ""Name"": ""InspectionStatus"",
  ""Title"": ""InspectionStatus"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Start Time"",
  ""Hints"": [],
  ""Name"": ""StartTime"",
  ""Title"": ""StartTime"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""End Time"",
  ""Hints"": [],
  ""Name"": ""EndTime"",
  ""Title"": ""EndTime"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Reason For Failure"",
  ""Hints"": [],
  ""Name"": ""ReasonForFailure"",
  ""Title"": ""ReasonForFailure"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 7
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Is Design Required"",
  ""Hints"": [],
  ""Name"": ""IsDesignRequired"",
  ""Title"": ""IsDesignRequired"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""SiteInspectionId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""SiteInspection\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""ScaffoldInspections\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""SiteInspection:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""ScaffoldInspectionsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Scaffolds"",
      ""Description"": null,
      ""FriendlyName"": ""Scaffold Inspection"",
      ""Hints"": [],
      ""Name"": ""ScaffoldInspection"",
      ""Title"": ""ScaffoldInspection"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Site Inspections"",
      ""SetName"": ""SiteInspections"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": true,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 2
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Scaffold Inspections"",
  ""Hints"": [],
  ""Name"": ""ScaffoldInspections"",
  ""Title"": ""ScaffoldInspections"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Risk Assessment"",
  ""Hints"": [],
  ""Name"": ""RiskAssessment"",
  ""Title"": ""RiskAssessment"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site"",
  ""Hints"": [],
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Id"",
  ""Hints"": [],
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Start Time"",
  ""Hints"": [],
  ""Name"": ""StartTime"",
  ""Title"": ""StartTime"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""End Time"",
  ""Hints"": [],
  ""Name"": ""EndTime"",
  ""Title"": ""EndTime"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""SiteInspectionId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""ScaffoldInspection\"",\""Paths\"":\""SiteInspection\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""ScaffoldInspections\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""SiteInspection:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""SiteId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""Site\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""SiteInspections\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteInspection\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteInspectionsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Sites"",
      ""Description"": null,
      ""FriendlyName"": ""Site Inspection"",
      ""Hints"": [],
      ""Name"": ""SiteInspection"",
      ""Title"": ""SiteInspection"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": ""Name"",
      ""PreviewPropertyName"": ""PreviewUrl"",
      ""ManageKind"": 62,
      ""SetFriendlyName"": ""Site Documents"",
      ""SetName"": ""SiteDocuments"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Category"",
  ""Hints"": [],
  ""Name"": ""Category"",
  ""Title"": ""Category"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Category Id"",
  ""Hints"": [],
  ""Name"": ""CategoryId"",
  ""Title"": ""CategoryId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site"",
  ""Hints"": [],
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Id"",
  ""Hints"": [],
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User"",
  ""Hints"": [],
  ""Name"": ""CreatedByUser"",
  ""Title"": ""CreatedByUser"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 9,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created By User Id"",
  ""Hints"": [],
  ""Name"": ""CreatedByUserId"",
  ""Title"": ""CreatedByUserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Guid"",
  ""Hints"": [],
  ""Name"": ""SiteGuid"",
  ""Title"": ""SiteGuid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": [
      {
        ""Separator"": ""-"",
        ""Parts"": [
          {
            ""IsPropertyPath"": true,
            ""Key"": ""Site/Client/Guid""
          }
        ]
      },
      {
        ""Separator"": ""-"",
        ""Parts"": [
          {
            ""IsPropertyPath"": true,
            ""Key"": ""Guid""
          },
          {
            ""IsPropertyPath"": false,
            ""Key"": ""DocumentUrl""
          }
        ]
      }
    ]
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": false,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Document"",
  ""Hints"": [
    ""Iql:File""
  ],
  ""Name"": ""DocumentUrl"",
  ""Title"": ""Document"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": [
      {
        ""Separator"": ""-"",
        ""Parts"": [
          {
            ""IsPropertyPath"": true,
            ""Key"": ""Site/Client/Guid""
          }
        ]
      },
      {
        ""Separator"": ""-"",
        ""Parts"": [
          {
            ""IsPropertyPath"": true,
            ""Key"": ""Guid""
          },
          {
            ""IsPropertyPath"": false,
            ""Key"": ""PreviewUrl""
          }
        ]
      }
    ]
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": false,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Preview"",
  ""Hints"": [
    ""Iql:PreviewFor:DocumentUrl"",
    ""Iql:Image""
  ],
  ""Name"": ""PreviewUrl"",
  ""Title"": ""Preview"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Document Type"",
  ""Hints"": [
    ""Iql:FileTypeFor:DocumentUrl""
  ],
  ""Name"": ""DocumentType"",
  ""Title"": ""DocumentType"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": false,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Document Revision"",
  ""Hints"": [
    ""Iql:FileRevisionFor:DocumentUrl""
  ],
  ""Name"": ""DocumentRevision"",
  ""Title"": ""DocumentRevision"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Name"",
  ""Hints"": [
    ""Iql:FileNameFor:DocumentUrl""
  ],
  ""Name"": ""Name"",
  ""Title"": ""Name"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Guid"",
  ""Hints"": [],
  ""Name"": ""Guid"",
  ""Title"": ""Guid"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Revision Key"",
  ""Hints"": [
    ""Iql:Version""
  ],
  ""Name"": ""RevisionKey"",
  ""Title"": ""RevisionKey"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": true,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Persistence Key"",
  ""Hints"": [],
  ""Name"": ""PersistenceKey"",
  ""Title"": ""PersistenceKey"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CategoryId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Category\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""DocumentCategory\"",\""Paths\"":\""Documents\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""DocumentCategory:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""SiteId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""Site\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Documents\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CreatedByUserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""SiteDocument\"",\""Paths\"":\""CreatedByUser\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""SiteDocumentsCreated\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        }
      ],
      ""GroupPath"": ""Sites"",
      ""Description"": null,
      ""FriendlyName"": ""Site Document"",
      ""Hints"": [],
      ""Name"": ""SiteDocument"",
      ""Title"": ""SiteDocument"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""UserSite\"",\""Paths\"":\""UserId\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 2,
      ""SetFriendlyName"": ""User Sites"",
      ""SetName"": ""UserSites"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": null,
      ""DefaultSortDescending"": false,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 5
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 13,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site Id"",
  ""Hints"": [],
  ""Name"": ""SiteId"",
  ""Title"": ""SiteId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 13,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""User Id"",
  ""Hints"": [],
  ""Name"": ""UserId"",
  ""Title"": ""UserId"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""User"",
  ""Hints"": [],
  ""Name"": ""User"",
  ""Title"": ""User"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 1
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 2,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Site"",
  ""Hints"": [],
  ""Name"": ""Site"",
  ""Title"": ""Site"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""UserId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""User\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""ApplicationUser\"",\""Paths\"":\""Sites\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""ApplicationUser:Id""
        },
        {
          ""Constraints"": [
            {
              ""SourceKeyProperty"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""SiteId\"",\""Kind\"":1}"",
              ""TargetKeyProperty"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
            }
          ],
          ""Kind"": 1,
          ""Source"": {
            ""RelationshipSide"": 0,
            ""Type"": ""Unknown"",
            ""IsCollection"": false,
            ""Property"": ""{\""Type\"":\""UserSite\"",\""Paths\"":\""Site\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""Target"": {
            ""RelationshipSide"": 1,
            ""Type"": ""Unknown"",
            ""IsCollection"": true,
            ""Property"": ""{\""Type\"":\""Site\"",\""Paths\"":\""Users\"",\""Kind\"":1}"",
            ""CountProperty"": null
          },
          ""ConstraintKey"": ""Id"",
          ""QualifiedConstraintKey"": ""Site:Id""
        }
      ],
      ""GroupPath"": null,
      ""Description"": null,
      ""FriendlyName"": ""User Site"",
      ""Hints"": [],
      ""Name"": ""UserSite"",
      ""Title"": ""UserSite"",
      ""HelpTexts"": []
    },
    {
      ""Geographics"": [],
      ""NestedSets"": [],
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
          ""{\""Type\"":\""ApplicationLog\"",\""Paths\"":\""Id\"",\""Kind\"":1}""
        ]
      },
      ""TitlePropertyName"": null,
      ""PreviewPropertyName"": null,
      ""ManageKind"": 1,
      ""SetFriendlyName"": ""Application Logs"",
      ""SetName"": ""ApplicationLogs"",
      ""SetNameSet"": true,
      ""DefaultSortExpression"": ""CreatedDate"",
      ""DefaultSortDescending"": true,
      ""PropertyOrder"": [],
      ""Properties"": [
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 12
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 5,
  ""SearchKind"": 1,
  ""ReadOnly"": true,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Id"",
  ""Hints"": [],
  ""Name"": ""Id"",
  ""Title"": ""Id"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": false,
    ""Kind"": 8
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 1,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": false,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Created Date"",
  ""Hints"": [],
  ""Name"": ""CreatedDate"",
  ""Title"": ""CreatedDate"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Module"",
  ""Hints"": [],
  ""Name"": ""Module"",
  ""Title"": ""Module"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Message"",
  ""Hints"": [],
  ""Name"": ""Message"",
  ""Title"": ""Message"",
  ""HelpTexts"": []
},
        {
  ""ValidationRules"": {
    ""All"": []
  },
  ""DisplayRules"": {
    ""All"": []
  },
  ""RelationshipFilterRules"": {
    ""All"": []
  },
  ""Relationships"": [],
  ""TypeDefinition"": {
    ""IsCollection"": false,
    ""ConvertedFromType"": null,
    ""Nullable"": true,
    ""Kind"": 4
  },
  ""MediaKey"": {
    ""Separator"": ""/"",
    ""Groups"": []
  },
  ""Placeholder"": null,
  ""Kind"": 1,
  ""SearchKind"": 3,
  ""ReadOnly"": false,
  ""Hidden"": false,
  ""Sortable"": true,
  ""Searchable"": true,
  ""Nullable"": true,
  ""GroupPath"": null,
  ""Description"": null,
  ""FriendlyName"": ""Kind"",
  ""Hints"": [],
  ""Name"": ""Kind"",
  ""Title"": ""Kind"",
  ""HelpTexts"": []
}
      ],
      ""Relationships"": [],
      ""GroupPath"": null,
      ""Description"": null,
      ""FriendlyName"": ""Application Log"",
      ""Hints"": [],
      ""Name"": ""ApplicationLog"",
      ""Title"": ""ApplicationLog"",
      ""HelpTexts"": []
    }
  ]
}";
    }
}
#endif