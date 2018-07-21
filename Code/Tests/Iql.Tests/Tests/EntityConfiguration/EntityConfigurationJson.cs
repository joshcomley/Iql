#if !TypeScript
namespace Iql.Tests.Tests.EntityConfiguration
{
    public class EntityConfigurationJson
    {
        public const string Json = @"{  
   ""EnumTypes"":[  
      {  
         ""Name"":""UserType"",
         ""IsFlags"":false,
         ""Values"":[  
            {  
               ""Name"":""Super"",
               ""Value"":1
            },
            {  
               ""Name"":""Client"",
               ""Value"":2
            },
            {  
               ""Name"":""Candidate"",
               ""Value"":3
            }
         ]
      },
      {  
         ""Name"":""FaultReportStatus"",
         ""IsFlags"":false,
         ""Values"":[  
            {  
               ""Name"":""Fail"",
               ""Value"":0
            },
            {  
               ""Name"":""ImmediateActionRequired"",
               ""Value"":1
            }
         ]
      },
      {  
         ""Name"":""NewProjectAlertKind"",
         ""IsFlags"":false,
         ""Values"":[  
            {  
               ""Name"":""Critical"",
               ""Value"":1
            },
            {  
               ""Name"":""All"",
               ""Value"":2
            }
         ]
      },
      {  
         ""Name"":""ScaffoldCategoryType"",
         ""IsFlags"":false,
         ""Values"":[  
            {  
               ""Name"":""System"",
               ""Value"":0
            },
            {  
               ""Name"":""Conventional"",
               ""Value"":1
            }
         ]
      },
      {  
         ""Name"":""ScaffoldStatus"",
         ""IsFlags"":false,
         ""Values"":[  
            {  
               ""Name"":""Safe"",
               ""Value"":0
            },
            {  
               ""Name"":""SafeWithImmediateActions"",
               ""Value"":1
            },
            {  
               ""Name"":""Unsafe"",
               ""Value"":2
            }
         ]
      },
      {  
         ""Name"":""ScaffoldInspectionStatus"",
         ""IsFlags"":false,
         ""Values"":[  
            {  
               ""Name"":""Pass"",
               ""Value"":0
            },
            {  
               ""Name"":""Fail"",
               ""Value"":1
            },
            {  
               ""Name"":""ImmediateActionRequired"",
               ""Value"":2
            }
         ]
      },
      {  
         ""Name"":""InspectionFailReason"",
         ""IsFlags"":false,
         ""Values"":[  
            {  
               ""Name"":""None"",
               ""Value"":0
            },
            {  
               ""Name"":""UnableToAccess"",
               ""Value"":1
            },
            {  
               ""Name"":""PersistentFaults"",
               ""Value"":2
            },
            {  
               ""Name"":""FailuresInFaultReports"",
               ""Value"":3
            },
            {  
               ""Name"":""TooManyMinorObservations"",
               ""Value"":4
            },
            {  
               ""Name"":""NoDesignSupplied"",
               ""Value"":5
            }
         ]
      }
   ],
   ""EntityTypes"":[  
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""ApplicationUser:Id""
            ]
         },
         ""TitlePropertyName"":""UserName"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Users"",
         ""SetName"":""Users"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":null,
         ""DefaultSortDescending"":false,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Client Id"",
               ""Hints"":[  

               ],
               ""Name"":""ClientId"",
               ""Title"":""ClientId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Email"",
               ""Hints"":[  
                  ""Iql:EmailAddress""
               ],
               ""Name"":""Email"",
               ""Title"":""Email"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Full Name"",
               ""Hints"":[  

               ],
               ""Name"":""FullName"",
               ""Title"":""FullName"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":7
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Email Confirmed"",
               ""Hints"":[  

               ],
               ""Name"":""EmailConfirmed"",
               ""Title"":""EmailConfirmed"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""User Type"",
               ""Hints"":[  

               ],
               ""Name"":""UserType"",
               ""Title"":""UserType"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":7
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Is Locked Out"",
               ""Hints"":[  

               ],
               ""Name"":""IsLockedOut"",
               ""Title"":""IsLockedOut"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Client"",
               ""Hints"":[  

               ],
               ""Name"":""Client"",
               ""Title"":""Client"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Clients Created"",
               ""Hints"":[  

               ],
               ""Name"":""ClientsCreated"",
               ""Title"":""ClientsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Custom Reports"",
               ""Hints"":[  

               ],
               ""Name"":""CustomReports"",
               ""Title"":""CustomReports"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Custom Reports Created"",
               ""Hints"":[  

               ],
               ""Name"":""CustomReportsCreated"",
               ""Title"":""CustomReportsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Document Categories Created"",
               ""Hints"":[  

               ],
               ""Name"":""DocumentCategoriesCreated"",
               ""Title"":""DocumentCategoriesCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Actions Taken Created"",
               ""Hints"":[  

               ],
               ""Name"":""FaultActionsTakenCreated"",
               ""Title"":""FaultActionsTakenCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Categories Created"",
               ""Hints"":[  

               ],
               ""Name"":""FaultCategoriesCreated"",
               ""Title"":""FaultCategoriesCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Default Recommendations Created"",
               ""Hints"":[  

               ],
               ""Name"":""FaultDefaultRecommendationsCreated"",
               ""Title"":""FaultDefaultRecommendationsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Recommendations Created"",
               ""Hints"":[  

               ],
               ""Name"":""FaultRecommendationsCreated"",
               ""Title"":""FaultRecommendationsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Reports Created"",
               ""Hints"":[  

               ],
               ""Name"":""FaultReportsCreated"",
               ""Title"":""FaultReportsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Types Created"",
               ""Hints"":[  

               ],
               ""Name"":""FaultTypesCreated"",
               ""Title"":""FaultTypesCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Project Created"",
               ""Hints"":[  

               ],
               ""Name"":""ProjectCreated"",
               ""Title"":""ProjectCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Report Receiver Email Addresses Created"",
               ""Hints"":[  

               ],
               ""Name"":""ReportReceiverEmailAddressesCreated"",
               ""Title"":""ReportReceiverEmailAddressesCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Risk Assessments Created"",
               ""Hints"":[  

               ],
               ""Name"":""RiskAssessmentsCreated"",
               ""Title"":""RiskAssessmentsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Risk Assessment Answers Created"",
               ""Hints"":[  

               ],
               ""Name"":""RiskAssessmentAnswersCreated"",
               ""Title"":""RiskAssessmentAnswersCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Risk Assessment Questions Created"",
               ""Hints"":[  

               ],
               ""Name"":""RiskAssessmentQuestionsCreated"",
               ""Title"":""RiskAssessmentQuestionsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffolds Created"",
               ""Hints"":[  

               ],
               ""Name"":""ScaffoldsCreated"",
               ""Title"":""ScaffoldsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffold Inspections Created"",
               ""Hints"":[  

               ],
               ""Name"":""ScaffoldInspectionsCreated"",
               ""Title"":""ScaffoldInspectionsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffold Loadings Created"",
               ""Hints"":[  

               ],
               ""Name"":""ScaffoldLoadingsCreated"",
               ""Title"":""ScaffoldLoadingsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffold Types Created"",
               ""Hints"":[  

               ],
               ""Name"":""ScaffoldTypesCreated"",
               ""Title"":""ScaffoldTypesCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffold Systems"",
               ""Hints"":[  

               ],
               ""Name"":""ScaffoldSystems"",
               ""Title"":""ScaffoldSystems"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Sites Created"",
               ""Hints"":[  

               ],
               ""Name"":""SitesCreated"",
               ""Title"":""SitesCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Documents Created"",
               ""Hints"":[  

               ],
               ""Name"":""SiteDocumentsCreated"",
               ""Title"":""SiteDocumentsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Inspections Created"",
               ""Hints"":[  

               ],
               ""Name"":""SiteInspectionsCreated"",
               ""Title"":""SiteInspectionsCreated"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Sites"",
               ""Hints"":[  

               ],
               ""Name"":""Sites"",
               ""Title"":""Sites"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ApplicationUser:ClientId"",
                     ""TargetKeyProperty"":""Client:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ApplicationUser:Client"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Client:Users"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Client:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Client:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Client:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ClientsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""CustomReport:UserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""CustomReport:User"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:CustomReports"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""CustomReport:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""CustomReport:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:CustomReportsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""DocumentCategory:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""DocumentCategory:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:DocumentCategoriesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultActionsTaken:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultActionsTaken:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultActionsTakenCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultCategory:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultCategory:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultCategoriesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultDefaultRecommendation:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultDefaultRecommendation:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultDefaultRecommendationsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultRecommendation:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultRecommendation:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultRecommendationsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultReport:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultReport:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultReportsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultType:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultType:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultTypesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Project:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Project:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ProjectCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ReportReceiverEmailAddress:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ReportReceiverEmailAddress:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ReportReceiverEmailAddressesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""RiskAssessment:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""RiskAssessment:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:RiskAssessmentsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""RiskAssessmentAnswer:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""RiskAssessmentAnswer:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:RiskAssessmentAnswersCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""RiskAssessmentQuestion:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""RiskAssessmentQuestion:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:RiskAssessmentQuestionsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldInspection:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldInspection:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldInspectionsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldLoading:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldLoading:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldLoadingsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldType:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldType:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldTypesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldSystem:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldSystem:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldSystems"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Site:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Site:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:SitesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteDocument:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteDocument:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:SiteDocumentsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteInspection:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteInspection:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:SiteInspectionsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""UserSite:UserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""UserSite:User"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:Sites"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":null,
         ""Description"":null,
         ""FriendlyName"":""Application User"",
         ""Hints"":[  

         ],
         ""Name"":""ApplicationUser"",
         ""Title"":""ApplicationUser"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""Client:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Clients"",
         ""SetName"":""Clients"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Users"",
               ""Hints"":[  

               ],
               ""Name"":""Users"",
               ""Title"":""Users"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Type"",
               ""Hints"":[  

               ],
               ""Name"":""Type"",
               ""Title"":""Type"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Type Id"",
               ""Hints"":[  

               ],
               ""Name"":""TypeId"",
               ""Title"":""TypeId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Description"",
               ""Hints"":[  
                  ""Iql:BigText""
               ],
               ""Name"":""Description"",
               ""Title"":""Description"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffolds"",
               ""Hints"":[  

               ],
               ""Name"":""Scaffolds"",
               ""Title"":""Scaffolds"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Sites"",
               ""Hints"":[  

               ],
               ""Name"":""Sites"",
               ""Title"":""Sites"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ApplicationUser:ClientId"",
                     ""TargetKeyProperty"":""Client:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ApplicationUser:Client"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Client:Users"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Client:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Client:TypeId"",
                     ""TargetKeyProperty"":""ClientType:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Client:Type"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ClientType:Clients"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ClientType:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Client:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Client:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ClientsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:ClientId"",
                     ""TargetKeyProperty"":""Client:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:Client"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Client:Scaffolds"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Client:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Site:ClientId"",
                     ""TargetKeyProperty"":""Client:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Site:Client"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Client:Sites"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Client:Id""
            }
         ],
         ""GroupPath"":null,
         ""Description"":null,
         ""FriendlyName"":""Client"",
         ""Hints"":[  

         ],
         ""Name"":""Client"",
         ""Title"":""Client"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""ClientType:Id""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":2,
         ""SetFriendlyName"":""Client Types"",
         ""SetName"":""ClientTypes"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":null,
         ""DefaultSortDescending"":false,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Clients"",
               ""Hints"":[  

               ],
               ""Name"":""Clients"",
               ""Title"":""Clients"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Client:TypeId"",
                     ""TargetKeyProperty"":""ClientType:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Client:Type"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ClientType:Clients"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ClientType:Id""
            }
         ],
         ""GroupPath"":null,
         ""Description"":null,
         ""FriendlyName"":""Client Type"",
         ""Hints"":[  

         ],
         ""Name"":""ClientType"",
         ""Title"":""ClientType"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""CustomReport:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":26,
         ""SetFriendlyName"":""Custom report"",
         ""SetName"":""CustomReports"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""User"",
               ""Hints"":[  

               ],
               ""Name"":""User"",
               ""Title"":""User"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""User Id"",
               ""Hints"":[  

               ],
               ""Name"":""UserId"",
               ""Title"":""UserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Report"",
               ""Hints"":[  

               ],
               ""Name"":""Report"",
               ""Title"":""Report"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Path"",
               ""Hints"":[  

               ],
               ""Name"":""Path"",
               ""Title"":""Path"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Query"",
               ""Hints"":[  

               ],
               ""Name"":""Query"",
               ""Title"":""Query"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Iql"",
               ""Hints"":[  

               ],
               ""Name"":""Iql"",
               ""Title"":""Iql"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fields"",
               ""Hints"":[  

               ],
               ""Name"":""Fields"",
               ""Title"":""Fields"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  
                     {  
                        ""Key"":""Please enter a report name"",
                        ""Message"":null,
                        ""Expression"":""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>CustomReport</EntityTypeName>\r\n      <VariableName>report</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlOrExpression\"">\r\n    <Kind>Or</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <EntityTypeName>CustomReport</EntityTypeName>\r\n          <VariableName>report</VariableName>\r\n        </Parent>\r\n        <PropertyName>Name</PropertyName>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Left>\r\n    <Right xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlStringTrimExpression\"">\r\n        <Kind>StringTrim</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Parent xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n            <Kind>RootReference</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <EntityTypeName>CustomReport</EntityTypeName>\r\n            <VariableName>report</VariableName>\r\n          </Parent>\r\n          <PropertyName>Name</PropertyName>\r\n        </Parent>\r\n      </Left>\r\n      <Right xsi:type=\""IqlLiteralExpression\"">\r\n        <Kind>Literal</Kind>\r\n        <ReturnType>String</ReturnType>\r\n        <Value xsi:type=\""xsd:string\"" />\r\n        <InferredReturnType>String</InferredReturnType>\r\n      </Right>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
                     }
                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":""Save current filters as..."",
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""CustomReport:UserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""CustomReport:User"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:CustomReports"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""CustomReport:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""CustomReport:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:CustomReportsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":null,
         ""Description"":null,
         ""FriendlyName"":""Custom Report"",
         ""Hints"":[  

         ],
         ""Name"":""CustomReport"",
         ""Title"":""CustomReport"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""DocumentCategory:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Document Categories"",
         ""SetName"":""DocumentCategories"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Documents"",
               ""Hints"":[  

               ],
               ""Name"":""Documents"",
               ""Title"":""Documents"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""DocumentCategory:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""DocumentCategory:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:DocumentCategoriesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteDocument:CategoryId"",
                     ""TargetKeyProperty"":""DocumentCategory:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteDocument:Category"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""DocumentCategory:Documents"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""DocumentCategory:Id""
            }
         ],
         ""GroupPath"":""Sites"",
         ""Description"":null,
         ""FriendlyName"":""Document Category"",
         ""Hints"":[  

         ],
         ""Name"":""DocumentCategory"",
         ""Title"":""DocumentCategory"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""FaultActionsTaken:Id""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Fault Actions Taken"",
         ""SetName"":""FaultActionsTaken"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Report"",
               ""Hints"":[  

               ],
               ""Name"":""FaultReport"",
               ""Title"":""FaultReport"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Report Id"",
               ""Hints"":[  

               ],
               ""Name"":""FaultReportId"",
               ""Title"":""FaultReportId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Notes"",
               ""Hints"":[  

               ],
               ""Name"":""Notes"",
               ""Title"":""Notes"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultActionsTaken:FaultReportId"",
                     ""TargetKeyProperty"":""FaultReport:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultActionsTaken:FaultReport"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultReport:ActionsTaken"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultReport:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultActionsTaken:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultActionsTaken:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultActionsTakenCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Faults"",
         ""Description"":null,
         ""FriendlyName"":""Fault Actions Taken"",
         ""Hints"":[  

         ],
         ""Name"":""FaultActionsTaken"",
         ""Title"":""FaultActionsTaken"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""FaultReport:Id""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Fault Reports"",
         ""SetName"":""FaultReports"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Actions Taken"",
               ""Hints"":[  

               ],
               ""Name"":""ActionsTaken"",
               ""Title"":""ActionsTaken"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Recommendations"",
               ""Hints"":[  

               ],
               ""Name"":""Recommendations"",
               ""Title"":""Recommendations"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffold"",
               ""Hints"":[  

               ],
               ""Name"":""Scaffold"",
               ""Title"":""Scaffold"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffold Id"",
               ""Hints"":[  

               ],
               ""Name"":""ScaffoldId"",
               ""Title"":""ScaffoldId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Type"",
               ""Hints"":[  

               ],
               ""Name"":""Type"",
               ""Title"":""Type"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Type Id"",
               ""Hints"":[  

               ],
               ""Name"":""TypeId"",
               ""Title"":""TypeId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":7
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Rectified"",
               ""Hints"":[  

               ],
               ""Name"":""Rectified"",
               ""Title"":""Rectified"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Status"",
               ""Hints"":[  

               ],
               ""Name"":""Status"",
               ""Title"":""Status"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultActionsTaken:FaultReportId"",
                     ""TargetKeyProperty"":""FaultReport:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultActionsTaken:FaultReport"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultReport:ActionsTaken"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultReport:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultRecommendation:FaultReportId"",
                     ""TargetKeyProperty"":""FaultReport:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultRecommendation:FaultReport"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultReport:Recommendations"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultReport:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultReport:ScaffoldId"",
                     ""TargetKeyProperty"":""Scaffold:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultReport:Scaffold"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Scaffold:FaultReports"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Scaffold:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultReport:TypeId"",
                     ""TargetKeyProperty"":""FaultType:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultReport:Type"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultType:FaultReports"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultType:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultReport:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultReport:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultReportsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Faults"",
         ""Description"":null,
         ""FriendlyName"":""Fault Report"",
         ""Hints"":[  

         ],
         ""Name"":""FaultReport"",
         ""Title"":""FaultReport"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""FaultCategory:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Fault Categories"",
         ""SetName"":""FaultCategories"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Types"",
               ""Hints"":[  

               ],
               ""Name"":""FaultTypes"",
               ""Title"":""FaultTypes"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultCategory:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultCategory:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultCategoriesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultType:CategoryId"",
                     ""TargetKeyProperty"":""FaultCategory:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultType:Category"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultCategory:FaultTypes"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultCategory:Id""
            }
         ],
         ""GroupPath"":""Faults"",
         ""Description"":null,
         ""FriendlyName"":""Fault Category"",
         ""Hints"":[  

         ],
         ""Name"":""FaultCategory"",
         ""Title"":""FaultCategory"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""FaultDefaultRecommendation:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Fault Default Recommendations"",
         ""SetName"":""FaultDefaultRecommendations"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Text"",
               ""Hints"":[  
                  ""Iql:BigText""
               ],
               ""Name"":""Text"",
               ""Title"":""Text"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Recommendations"",
               ""Hints"":[  

               ],
               ""Name"":""Recommendations"",
               ""Title"":""Recommendations"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultDefaultRecommendation:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultDefaultRecommendation:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultDefaultRecommendationsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultRecommendation:FaultReportId"",
                     ""TargetKeyProperty"":""FaultDefaultRecommendation:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultRecommendation:Recommendation"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultDefaultRecommendation:Recommendations"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultDefaultRecommendation:Id""
            }
         ],
         ""GroupPath"":""Faults"",
         ""Description"":null,
         ""FriendlyName"":""Fault Default Recommendation"",
         ""Hints"":[  

         ],
         ""Name"":""FaultDefaultRecommendation"",
         ""Title"":""FaultDefaultRecommendation"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""FaultRecommendation:RecommendationId""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Fault Recommendations"",
         ""SetName"":""FaultRecommendations"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":13,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Report Id"",
               ""Hints"":[  

               ],
               ""Name"":""FaultReportId"",
               ""Title"":""FaultReportId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Recommendation Id"",
               ""Hints"":[  

               ],
               ""Name"":""RecommendationId"",
               ""Title"":""RecommendationId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Report"",
               ""Hints"":[  

               ],
               ""Name"":""FaultReport"",
               ""Title"":""FaultReport"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Recommendation"",
               ""Hints"":[  

               ],
               ""Name"":""Recommendation"",
               ""Title"":""Recommendation"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Notes"",
               ""Hints"":[  

               ],
               ""Name"":""Notes"",
               ""Title"":""Notes"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultRecommendation:FaultReportId"",
                     ""TargetKeyProperty"":""FaultReport:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultRecommendation:FaultReport"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultReport:Recommendations"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultReport:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultRecommendation:FaultReportId"",
                     ""TargetKeyProperty"":""FaultDefaultRecommendation:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultRecommendation:Recommendation"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultDefaultRecommendation:Recommendations"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultDefaultRecommendation:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultRecommendation:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultRecommendation:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultRecommendationsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Faults"",
         ""Description"":null,
         ""FriendlyName"":""Fault Recommendation"",
         ""Hints"":[  

         ],
         ""Name"":""FaultRecommendation"",
         ""Title"":""FaultRecommendation"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""Scaffold:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Scaffolds"",
         ""SetName"":""Scaffolds"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Reports"",
               ""Hints"":[  

               ],
               ""Name"":""FaultReports"",
               ""Title"":""FaultReports"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Client"",
               ""Hints"":[  

               ],
               ""Name"":""Client"",
               ""Title"":""Client"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Type"",
               ""Hints"":[  

               ],
               ""Name"":""Type"",
               ""Title"":""Type"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Type Id"",
               ""Hints"":[  

               ],
               ""Name"":""TypeId"",
               ""Title"":""TypeId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  
                     {  
                        ""Kind"":1,
                        ""AppliesToKind"":1,
                        ""Key"":""1"",
                        ""Message"":null,
                        ""Expression"":""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>Scaffold</EntityTypeName>\r\n      <VariableName>scaffold</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlIsEqualToExpression\"">\r\n    <Kind>IsEqualTo</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlPropertyExpression\"">\r\n      <Kind>Property</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <EntityTypeName>Scaffold</EntityTypeName>\r\n        <VariableName>scaffold</VariableName>\r\n      </Parent>\r\n      <PropertyName>TypeId</PropertyName>\r\n    </Left>\r\n    <Right xsi:type=\""IqlLiteralExpression\"">\r\n      <Kind>Literal</Kind>\r\n      <ReturnType>Integer</ReturnType>\r\n      <Value xsi:type=\""xsd:int\"">1</Value>\r\n      <InferredReturnType>Integer</InferredReturnType>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
                     }
                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""System"",
               ""Hints"":[  

               ],
               ""Name"":""System"",
               ""Title"":""System"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""System Id"",
               ""Hints"":[  

               ],
               ""Name"":""SystemId"",
               ""Title"":""SystemId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Loading"",
               ""Hints"":[  

               ],
               ""Name"":""Loading"",
               ""Title"":""Loading"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Loading Id"",
               ""Hints"":[  

               ],
               ""Name"":""LoadingId"",
               ""Title"":""LoadingId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Key"",
               ""Hints"":[  

               ],
               ""Name"":""Key"",
               ""Title"":""Key"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Description"",
               ""Hints"":[  
                  ""Iql:BigText""
               ],
               ""Name"":""Description"",
               ""Title"":""Description"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Category"",
               ""Hints"":[  

               ],
               ""Name"":""Category"",
               ""Title"":""Category"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Status"",
               ""Hints"":[  

               ],
               ""Name"":""Status"",
               ""Title"":""Status"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Client Id"",
               ""Hints"":[  

               ],
               ""Name"":""ClientId"",
               ""Title"":""ClientId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Client Guid"",
               ""Hints"":[  

               ],
               ""Name"":""ClientGuid"",
               ""Title"":""ClientGuid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  
                     {  
                        ""Separator"":""-"",
                        ""Parts"":[  
                           {  
                              ""IsPropertyPath"":true,
                              ""Key"":""Client/Guid""
                           }
                        ]
                     },
                     {  
                        ""Separator"":""-"",
                        ""Parts"":[  
                           {  
                              ""IsPropertyPath"":true,
                              ""Key"":""Guid""
                           },
                           {  
                              ""IsPropertyPath"":false,
                              ""Key"":""main-image""
                           }
                        ]
                     }
                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Photo"",
               ""Hints"":[  
                  ""Iql:File"",
                  ""Iql:Image""
               ],
               ""Name"":""ImageUrl"",
               ""Title"":""Photo"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultReport:ScaffoldId"",
                     ""TargetKeyProperty"":""Scaffold:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultReport:Scaffold"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Scaffold:FaultReports"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Scaffold:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:ClientId"",
                     ""TargetKeyProperty"":""Client:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:Client"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Client:Scaffolds"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Client:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:TypeId"",
                     ""TargetKeyProperty"":""ScaffoldType:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:Type"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ScaffoldType:Scaffolds"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ScaffoldType:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:SystemId"",
                     ""TargetKeyProperty"":""ScaffoldSystem:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:System"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ScaffoldSystem:Scaffolds"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ScaffoldSystem:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:LoadingId"",
                     ""TargetKeyProperty"":""ScaffoldLoading:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:Loading"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ScaffoldLoading:Scaffolds"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ScaffoldLoading:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Scaffolds"",
         ""Description"":null,
         ""FriendlyName"":""Scaffold"",
         ""Hints"":[  

         ],
         ""Name"":""Scaffold"",
         ""Title"":""Scaffold"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""FaultType:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Fault Types"",
         ""SetName"":""FaultTypes"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Fault Reports"",
               ""Hints"":[  

               ],
               ""Name"":""FaultReports"",
               ""Title"":""FaultReports"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Category"",
               ""Hints"":[  

               ],
               ""Name"":""Category"",
               ""Title"":""Category"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Category Id"",
               ""Hints"":[  

               ],
               ""Name"":""CategoryId"",
               ""Title"":""CategoryId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultReport:TypeId"",
                     ""TargetKeyProperty"":""FaultType:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultReport:Type"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultType:FaultReports"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultType:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultType:CategoryId"",
                     ""TargetKeyProperty"":""FaultCategory:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultType:Category"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""FaultCategory:FaultTypes"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""FaultCategory:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""FaultType:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""FaultType:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:FaultTypesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Faults"",
         ""Description"":null,
         ""FriendlyName"":""Fault Type"",
         ""Hints"":[  

         ],
         ""Name"":""FaultType"",
         ""Title"":""FaultType"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""NewProjectContact:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""New Project Contacts"",
         ""SetName"":""NewProjectContacts"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Notification"",
               ""Hints"":[  

               ],
               ""Name"":""Notification"",
               ""Title"":""Notification"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Notification Id"",
               ""Hints"":[  

               ],
               ""Name"":""NotificationId"",
               ""Title"":""NotificationId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Kind"",
               ""Hints"":[  

               ],
               ""Name"":""Kind"",
               ""Title"":""Kind"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Kind Id"",
               ""Hints"":[  

               ],
               ""Name"":""KindId"",
               ""Title"":""KindId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Alerts"",
               ""Hints"":[  

               ],
               ""Name"":""Alerts"",
               ""Title"":""Alerts"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Email Address"",
               ""Hints"":[  
                  ""Iql:EmailAddress""
               ],
               ""Name"":""EmailAddress"",
               ""Title"":""EmailAddress"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Phone Number"",
               ""Hints"":[  
                  ""Iql:PhoneNumber""
               ],
               ""Name"":""PhoneNumber"",
               ""Title"":""PhoneNumber"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""NewProjectContact:NotificationId"",
                     ""TargetKeyProperty"":""NewProjectNotification:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""NewProjectContact:Notification"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""NewProjectNotification:Contacts"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""NewProjectNotification:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""NewProjectContact:KindId"",
                     ""TargetKeyProperty"":""NewProjectContactKind:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""NewProjectContact:Kind"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""NewProjectContactKind:Contacts"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""NewProjectContactKind:Id""
            }
         ],
         ""GroupPath"":""New Projects"",
         ""Description"":null,
         ""FriendlyName"":""New Project Contact"",
         ""Hints"":[  

         ],
         ""Name"":""NewProjectContact"",
         ""Title"":""NewProjectContact"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""NewProjectNotification:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""New Project Notifications"",
         ""SetName"":""NewProjectNotifications"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Contacts"",
               ""Hints"":[  

               ],
               ""Name"":""Contacts"",
               ""Title"":""Contacts"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Start Date"",
               ""Hints"":[  

               ],
               ""Name"":""StartDate"",
               ""Title"":""StartDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Anticipated Finish Date"",
               ""Hints"":[  

               ],
               ""Name"":""AnticipatedEndDate"",
               ""Title"":""Anticipated Finish Date"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Address"",
               ""Hints"":[  
                  ""Iql:BigText""
               ],
               ""Name"":""SiteAddress"",
               ""Title"":""SiteAddress"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Contract Number"",
               ""Hints"":[  
                  ""Iql:PhoneNumber""
               ],
               ""Name"":""ContractNumber"",
               ""Title"":""ContractNumber"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""First Inspection Required"",
               ""Hints"":[  

               ],
               ""Name"":""FirstInspectionRequired"",
               ""Title"":""FirstInspectionRequired"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""On Site Contact"",
               ""Hints"":[  

               ],
               ""Name"":""OnSiteContact"",
               ""Title"":""OnSiteContact"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":7
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Have designs been supplied with this notice?"",
               ""Hints"":[  

               ],
               ""Name"":""DesignsSupplied"",
               ""Title"":""Have designs been supplied with this notice?"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":7
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Has construction programme been supplied with this notice?"",
               ""Hints"":[  

               ],
               ""Name"":""ConstructionProgrammeSupplied"",
               ""Title"":""Has construction programme been supplied with this notice?"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""NewProjectContact:NotificationId"",
                     ""TargetKeyProperty"":""NewProjectNotification:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""NewProjectContact:Notification"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""NewProjectNotification:Contacts"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""NewProjectNotification:Id""
            }
         ],
         ""GroupPath"":""New Projects"",
         ""Description"":null,
         ""FriendlyName"":""New Project Notification"",
         ""Hints"":[  

         ],
         ""Name"":""NewProjectNotification"",
         ""Title"":""NewProjectNotification"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""NewProjectContactKind:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""New Project Contact Kinds"",
         ""SetName"":""NewProjectContactKinds"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Contacts"",
               ""Hints"":[  

               ],
               ""Name"":""Contacts"",
               ""Title"":""Contacts"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""NewProjectContact:KindId"",
                     ""TargetKeyProperty"":""NewProjectContactKind:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""NewProjectContact:Kind"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""NewProjectContactKind:Contacts"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""NewProjectContactKind:Id""
            }
         ],
         ""GroupPath"":""New Projects"",
         ""Description"":null,
         ""FriendlyName"":""New Project Contact Kind"",
         ""Hints"":[  

         ],
         ""Name"":""NewProjectContactKind"",
         ""Title"":""NewProjectContactKind"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""Project:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Projects"",
         ""SetName"":""Projects"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Description"",
               ""Hints"":[  
                  ""Iql:BigText""
               ],
               ""Name"":""Description"",
               ""Title"":""Description"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Project:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Project:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ProjectCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":null,
         ""Description"":null,
         ""FriendlyName"":""Project"",
         ""Hints"":[  

         ],
         ""Name"":""Project"",
         ""Title"":""Project"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""ReportReceiverEmailAddress:Id""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Report Receiver Email Addresses"",
         ""SetName"":""ReportReceiverEmailAddresses"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site"",
               ""Hints"":[  

               ],
               ""Name"":""Site"",
               ""Title"":""Site"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Id"",
               ""Hints"":[  

               ],
               ""Name"":""SiteId"",
               ""Title"":""SiteId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Email Address"",
               ""Hints"":[  

               ],
               ""Name"":""EmailAddress"",
               ""Title"":""EmailAddress"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ReportReceiverEmailAddress:SiteId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ReportReceiverEmailAddress:Site"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:AdditionalSendReportsTo"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ReportReceiverEmailAddress:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ReportReceiverEmailAddress:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ReportReceiverEmailAddressesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Faults"",
         ""Description"":null,
         ""FriendlyName"":""Report Receiver Email Address"",
         ""Hints"":[  

         ],
         ""Name"":""ReportReceiverEmailAddress"",
         ""Title"":""ReportReceiverEmailAddress"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""Site:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Sites"",
         ""SetName"":""Sites"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  
            ""Client"",
            ""Parent"",
            ""Name"",
            ""Address"",
            ""PostCode"",
            ""WeeklyCharge"",
            ""Key"",
            ""DrawingNumber""
         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Additional Send Reports To"",
               ""Hints"":[  

               ],
               ""Name"":""AdditionalSendReportsTo"",
               ""Title"":""AdditionalSendReportsTo"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  
                  ""Iql:Geographic:Longitude"",
                  ""Iql:NestedSets:Id""
               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  
                     {  
                        ""Kind"":1,
                        ""AppliesToKind"":1,
                        ""Key"":""2"",
                        ""Message"":null,
                        ""Expression"":""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>Site</EntityTypeName>\r\n      <VariableName>site</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlIsGreaterThanExpression\"">\r\n    <Kind>IsGreaterThan</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Left xsi:type=\""IqlPropertyExpression\"">\r\n      <Kind>Property</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <EntityTypeName>Site</EntityTypeName>\r\n        <VariableName>site</VariableName>\r\n      </Parent>\r\n      <PropertyName>ClientId</PropertyName>\r\n    </Left>\r\n    <Right xsi:type=\""IqlLiteralExpression\"">\r\n      <Kind>Literal</Kind>\r\n      <ReturnType>Integer</ReturnType>\r\n      <Value xsi:type=\""xsd:int\"">0</Value>\r\n      <InferredReturnType>Integer</InferredReturnType>\r\n    </Right>\r\n  </Body>\r\n</IqlLambdaExpression>""
                     }
                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  
                     {  
                        ""Key"":""3"",
                        ""Message"":null,
                        ""Expression"":""<?xml version=\""1.0\"" encoding=\""utf-16\""?>\r\n<IqlLambdaExpression xmlns:xsi=\""http://www.w3.org/2001/XMLSchema-instance\"" xmlns:xsd=\""http://www.w3.org/2001/XMLSchema\"">\r\n  <Kind>Lambda</Kind>\r\n  <ReturnType>Unknown</ReturnType>\r\n  <Parameters>\r\n    <IqlRootReferenceExpression>\r\n      <Kind>RootReference</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <EntityTypeName>RelationshipFilterContext&lt;Site&gt;</EntityTypeName>\r\n      <VariableName>context</VariableName>\r\n    </IqlRootReferenceExpression>\r\n  </Parameters>\r\n  <Body xsi:type=\""IqlLambdaExpression\"">\r\n    <Kind>Lambda</Kind>\r\n    <ReturnType>Unknown</ReturnType>\r\n    <Parameters>\r\n      <IqlRootReferenceExpression>\r\n        <Kind>RootReference</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <EntityTypeName>Site</EntityTypeName>\r\n        <VariableName>site</VariableName>\r\n      </IqlRootReferenceExpression>\r\n    </Parameters>\r\n    <Body xsi:type=\""IqlIsEqualToExpression\"">\r\n      <Kind>IsEqualTo</Kind>\r\n      <ReturnType>Unknown</ReturnType>\r\n      <Left xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlRootReferenceExpression\"">\r\n          <Kind>RootReference</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <EntityTypeName>Site</EntityTypeName>\r\n          <VariableName>site</VariableName>\r\n        </Parent>\r\n        <PropertyName>ClientId</PropertyName>\r\n      </Left>\r\n      <Right xsi:type=\""IqlPropertyExpression\"">\r\n        <Kind>Property</Kind>\r\n        <ReturnType>Unknown</ReturnType>\r\n        <Parent xsi:type=\""IqlPropertyExpression\"">\r\n          <Kind>Property</Kind>\r\n          <ReturnType>Unknown</ReturnType>\r\n          <Parent xsi:type=\""IqlVariableExpression\"">\r\n            <Kind>Variable</Kind>\r\n            <ReturnType>Unknown</ReturnType>\r\n            <EntityTypeName>RelationshipFilterContext&lt;Site&gt;</EntityTypeName>\r\n            <VariableName>context</VariableName>\r\n          </Parent>\r\n          <PropertyName>Owner</PropertyName>\r\n        </Parent>\r\n        <PropertyName>ClientId</PropertyName>\r\n      </Right>\r\n    </Body>\r\n  </Body>\r\n</IqlLambdaExpression>""
                     }
                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Parent"",
               ""Hints"":[  
                  ""Iql:NestedSets:Parent""
               ],
               ""Name"":""Parent"",
               ""Title"":""Parent"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Parent Id"",
               ""Hints"":[  
                  ""Iql:NestedSets:ParentId""
               ],
               ""Name"":""ParentId"",
               ""Title"":""ParentId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Client"",
               ""Hints"":[  

               ],
               ""Name"":""Client"",
               ""Title"":""Client"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Client Id"",
               ""Hints"":[  

               ],
               ""Name"":""ClientId"",
               ""Title"":""ClientId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":6
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Weekly Charge"",
               ""Hints"":[  
                  ""Iql:Currency""
               ],
               ""Name"":""WeeklyCharge"",
               ""Title"":""WeeklyCharge"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Address"",
               ""Hints"":[  
                  ""Iql:BigText""
               ],
               ""Name"":""Address"",
               ""Title"":""Address"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Post Code"",
               ""Hints"":[  

               ],
               ""Name"":""PostCode"",
               ""Title"":""PostCode"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Key"",
               ""Hints"":[  
                  ""Iql:NestedSets:Key""
               ],
               ""Name"":""Key"",
               ""Title"":""Key"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":6
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Latitude"",
               ""Hints"":[  
                  ""Iql:Geographic:Latitude""
               ],
               ""Name"":""Latitude"",
               ""Title"":""Latitude"",
               ""HelpTexts"":[  
                  {  
                     ""Text"":""Select the location of the site on the map.\n\nYou can search for the address or postcode, and drag the red pin to the precise location."",
                     ""Kind"":1
                  }
               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":6
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Longitude"",
               ""Hints"":[  
                  ""Iql:Geographic:Longitude""
               ],
               ""Name"":""Longitude"",
               ""Title"":""Longitude"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Left Of"",
               ""Hints"":[  
                  ""Iql:NestedSets:LeftOf""
               ],
               ""Name"":""LeftOf"",
               ""Title"":""LeftOf"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Right Of"",
               ""Hints"":[  
                  ""Iql:NestedSets:RightOf""
               ],
               ""Name"":""RightOf"",
               ""Title"":""RightOf"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Level"",
               ""Hints"":[  
                  ""Iql:NestedSets:Level""
               ],
               ""Name"":""Level"",
               ""Title"":""Level"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Left"",
               ""Hints"":[  
                  ""Iql:NestedSets:Left""
               ],
               ""Name"":""Left"",
               ""Title"":""Left"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Right"",
               ""Hints"":[  
                  ""Iql:NestedSets:Right""
               ],
               ""Name"":""Right"",
               ""Title"":""Right"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Drawing Number"",
               ""Hints"":[  

               ],
               ""Name"":""DrawingNumber"",
               ""Title"":""DrawingNumber"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Children"",
               ""Hints"":[  

               ],
               ""Name"":""Children"",
               ""Title"":""Children"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Documents"",
               ""Hints"":[  

               ],
               ""Name"":""Documents"",
               ""Title"":""Documents"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Inspections"",
               ""Hints"":[  

               ],
               ""Name"":""SiteInspections"",
               ""Title"":""SiteInspections"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Users"",
               ""Hints"":[  

               ],
               ""Name"":""Users"",
               ""Title"":""Users"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ReportReceiverEmailAddress:SiteId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ReportReceiverEmailAddress:Site"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:AdditionalSendReportsTo"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Site:ParentId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Site:Parent"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:Children"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Site:ClientId"",
                     ""TargetKeyProperty"":""Client:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Site:Client"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Client:Sites"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Client:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Site:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Site:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:SitesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteDocument:SiteId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteDocument:Site"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:Documents"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteInspection:SiteId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteInspection:Site"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:SiteInspections"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""UserSite:SiteId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""UserSite:Site"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:Users"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            }
         ],
         ""GroupPath"":""Sites"",
         ""Description"":null,
         ""FriendlyName"":""Site"",
         ""Hints"":[  
            ""Iql:Mptt:ClientId"",
            ""Iql:Geographic"",
            ""Iql:NestedSets""
         ],
         ""Name"":""Site"",
         ""Title"":""Site"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""RiskAssessment:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Risk Assessments"",
         ""SetName"":""RiskAssessments"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Inspection Id"",
               ""Hints"":[  

               ],
               ""Name"":""SiteInspectionId"",
               ""Title"":""SiteInspectionId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Inspection"",
               ""Hints"":[  

               ],
               ""Name"":""SiteInspection"",
               ""Title"":""SiteInspection"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""RiskAssessment:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""RiskAssessment:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:RiskAssessmentsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Risk Assessments"",
         ""Description"":null,
         ""FriendlyName"":""Risk Assessment"",
         ""Hints"":[  

         ],
         ""Name"":""RiskAssessment"",
         ""Title"":""RiskAssessment"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""RiskAssessmentAnswer:Id""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Risk Assessment Answers"",
         ""SetName"":""RiskAssessmentAnswers"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Question"",
               ""Hints"":[  

               ],
               ""Name"":""Question"",
               ""Title"":""Question"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Question Id"",
               ""Hints"":[  

               ],
               ""Name"":""QuestionId"",
               ""Title"":""QuestionId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Specific Hazard"",
               ""Hints"":[  

               ],
               ""Name"":""SpecificHazard"",
               ""Title"":""SpecificHazard"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Precautions To Control Hazard"",
               ""Hints"":[  

               ],
               ""Name"":""PrecautionsToControlHazard"",
               ""Title"":""PrecautionsToControlHazard"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""RiskAssessmentAnswer:QuestionId"",
                     ""TargetKeyProperty"":""RiskAssessmentQuestion:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""RiskAssessmentAnswer:Question"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""RiskAssessmentQuestion:Answers"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""RiskAssessmentQuestion:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""RiskAssessmentAnswer:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""RiskAssessmentAnswer:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:RiskAssessmentAnswersCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Risk Assessments"",
         ""Description"":null,
         ""FriendlyName"":""Risk Assessment Answer"",
         ""Hints"":[  

         ],
         ""Name"":""RiskAssessmentAnswer"",
         ""Title"":""RiskAssessmentAnswer"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""RiskAssessmentQuestion:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Risk Assessment Questions"",
         ""SetName"":""RiskAssessmentQuestions"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Answers"",
               ""Hints"":[  

               ],
               ""Name"":""Answers"",
               ""Title"":""Answers"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""RiskAssessmentAnswer:QuestionId"",
                     ""TargetKeyProperty"":""RiskAssessmentQuestion:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""RiskAssessmentAnswer:Question"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""RiskAssessmentQuestion:Answers"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""RiskAssessmentQuestion:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""RiskAssessmentQuestion:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""RiskAssessmentQuestion:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:RiskAssessmentQuestionsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Risk Assessments"",
         ""Description"":null,
         ""FriendlyName"":""Risk Assessment Question"",
         ""Hints"":[  

         ],
         ""Name"":""RiskAssessmentQuestion"",
         ""Title"":""RiskAssessmentQuestion"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""ScaffoldType:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Scaffold Types"",
         ""SetName"":""ScaffoldTypes"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffolds"",
               ""Hints"":[  

               ],
               ""Name"":""Scaffolds"",
               ""Title"":""Scaffolds"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Description"",
               ""Hints"":[  
                  ""Iql:BigText""
               ],
               ""Name"":""Description"",
               ""Title"":""Description"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:TypeId"",
                     ""TargetKeyProperty"":""ScaffoldType:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:Type"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ScaffoldType:Scaffolds"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ScaffoldType:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldType:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldType:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldTypesCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Scaffolds"",
         ""Description"":null,
         ""FriendlyName"":""Scaffold Type"",
         ""Hints"":[  

         ],
         ""Name"":""ScaffoldType"",
         ""Title"":""ScaffoldType"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""ScaffoldSystem:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Scaffold Systems"",
         ""SetName"":""ScaffoldSystems"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffolds"",
               ""Hints"":[  

               ],
               ""Name"":""Scaffolds"",
               ""Title"":""Scaffolds"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:SystemId"",
                     ""TargetKeyProperty"":""ScaffoldSystem:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:System"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ScaffoldSystem:Scaffolds"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ScaffoldSystem:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldSystem:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldSystem:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldSystems"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Scaffolds"",
         ""Description"":null,
         ""FriendlyName"":""Scaffold System"",
         ""Hints"":[  

         ],
         ""Name"":""ScaffoldSystem"",
         ""Title"":""ScaffoldSystem"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""ScaffoldLoading:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Scaffold Loadings"",
         ""SetName"":""ScaffoldLoadings"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffolds"",
               ""Hints"":[  

               ],
               ""Name"":""Scaffolds"",
               ""Title"":""Scaffolds"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  

               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""Scaffold:LoadingId"",
                     ""TargetKeyProperty"":""ScaffoldLoading:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""Scaffold:Loading"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ScaffoldLoading:Scaffolds"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ScaffoldLoading:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldLoading:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldLoading:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldLoadingsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Scaffolds"",
         ""Description"":null,
         ""FriendlyName"":""Scaffold Loading"",
         ""Hints"":[  

         ],
         ""Name"":""ScaffoldLoading"",
         ""Title"":""ScaffoldLoading"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""ScaffoldInspection:Id""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Scaffold Inspections"",
         ""SetName"":""ScaffoldInspections"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Inspection"",
               ""Hints"":[  

               ],
               ""Name"":""SiteInspection"",
               ""Title"":""SiteInspection"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Inspection Id"",
               ""Hints"":[  

               ],
               ""Name"":""SiteInspectionId"",
               ""Title"":""SiteInspectionId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffold Id"",
               ""Hints"":[  

               ],
               ""Name"":""ScaffoldId"",
               ""Title"":""ScaffoldId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Inspection Status"",
               ""Hints"":[  

               ],
               ""Name"":""InspectionStatus"",
               ""Title"":""InspectionStatus"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Start Time"",
               ""Hints"":[  

               ],
               ""Name"":""StartTime"",
               ""Title"":""StartTime"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""End Time"",
               ""Hints"":[  

               ],
               ""Name"":""EndTime"",
               ""Title"":""EndTime"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Reason For Failure"",
               ""Hints"":[  

               ],
               ""Name"":""ReasonForFailure"",
               ""Title"":""ReasonForFailure"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":7
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Is Design Required"",
               ""Hints"":[  

               ],
               ""Name"":""IsDesignRequired"",
               ""Title"":""IsDesignRequired"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldInspection:SiteInspectionId"",
                     ""TargetKeyProperty"":""SiteInspection:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldInspection:SiteInspection"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""SiteInspection:ScaffoldInspections"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""SiteInspection:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldInspection:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldInspection:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:ScaffoldInspectionsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Scaffolds"",
         ""Description"":null,
         ""FriendlyName"":""Scaffold Inspection"",
         ""Hints"":[  

         ],
         ""Name"":""ScaffoldInspection"",
         ""Title"":""ScaffoldInspection"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""SiteInspection:Id""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":62,
         ""SetFriendlyName"":""Site Inspections"",
         ""SetName"":""SiteInspections"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":true,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":2
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Scaffold Inspections"",
               ""Hints"":[  

               ],
               ""Name"":""ScaffoldInspections"",
               ""Title"":""ScaffoldInspections"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Risk Assessment"",
               ""Hints"":[  

               ],
               ""Name"":""RiskAssessment"",
               ""Title"":""RiskAssessment"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site"",
               ""Hints"":[  

               ],
               ""Name"":""Site"",
               ""Title"":""Site"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Id"",
               ""Hints"":[  

               ],
               ""Name"":""SiteId"",
               ""Title"":""SiteId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Start Time"",
               ""Hints"":[  

               ],
               ""Name"":""StartTime"",
               ""Title"":""StartTime"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""End Time"",
               ""Hints"":[  

               ],
               ""Name"":""EndTime"",
               ""Title"":""EndTime"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""ScaffoldInspection:SiteInspectionId"",
                     ""TargetKeyProperty"":""SiteInspection:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""ScaffoldInspection:SiteInspection"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""SiteInspection:ScaffoldInspections"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""SiteInspection:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteInspection:SiteId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteInspection:Site"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:SiteInspections"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteInspection:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteInspection:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:SiteInspectionsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Sites"",
         ""Description"":null,
         ""FriendlyName"":""Site Inspection"",
         ""Hints"":[  

         ],
         ""Name"":""SiteInspection"",
         ""Title"":""SiteInspection"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""SiteDocument:Id""
            ]
         },
         ""TitlePropertyName"":""Name"",
         ""PreviewPropertyName"":""PreviewUrl"",
         ""ManageKind"":62,
         ""SetFriendlyName"":""Site Documents"",
         ""SetName"":""SiteDocuments"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Category"",
               ""Hints"":[  

               ],
               ""Name"":""Category"",
               ""Title"":""Category"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Category Id"",
               ""Hints"":[  

               ],
               ""Name"":""CategoryId"",
               ""Title"":""CategoryId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site"",
               ""Hints"":[  

               ],
               ""Name"":""Site"",
               ""Title"":""Site"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Id"",
               ""Hints"":[  

               ],
               ""Name"":""SiteId"",
               ""Title"":""SiteId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUser"",
               ""Title"":""CreatedByUser"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":9,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created By User Id"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedByUserId"",
               ""Title"":""CreatedByUserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Guid"",
               ""Hints"":[  

               ],
               ""Name"":""SiteGuid"",
               ""Title"":""SiteGuid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":false,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Document"",
               ""Hints"":[  
                  ""Iql:File""
               ],
               ""Name"":""DocumentUrl"",
               ""Title"":""Document"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":false,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Preview"",
               ""Hints"":[  
                  ""Iql:PreviewFor:DocumentUrl"",
                  ""Iql:Image""
               ],
               ""Name"":""PreviewUrl"",
               ""Title"":""Preview"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Document Type"",
               ""Hints"":[  
                  ""Iql:FileTypeFor:DocumentUrl""
               ],
               ""Name"":""DocumentType"",
               ""Title"":""DocumentType"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":false,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Document Revision"",
               ""Hints"":[  
                  ""Iql:FileRevisionFor:DocumentUrl""
               ],
               ""Name"":""DocumentRevision"",
               ""Title"":""DocumentRevision"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Name"",
               ""Hints"":[  
                  ""Iql:FileNameFor:DocumentUrl""
               ],
               ""Name"":""Name"",
               ""Title"":""Name"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Guid"",
               ""Hints"":[  

               ],
               ""Name"":""Guid"",
               ""Title"":""Guid"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Revision Key"",
               ""Hints"":[  
                  ""Iql:Version""
               ],
               ""Name"":""RevisionKey"",
               ""Title"":""RevisionKey"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":true,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Persistence Key"",
               ""Hints"":[  

               ],
               ""Name"":""PersistenceKey"",
               ""Title"":""PersistenceKey"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteDocument:CategoryId"",
                     ""TargetKeyProperty"":""DocumentCategory:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteDocument:Category"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""DocumentCategory:Documents"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""DocumentCategory:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteDocument:SiteId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteDocument:Site"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:Documents"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""SiteDocument:CreatedByUserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""SiteDocument:CreatedByUser"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:SiteDocumentsCreated"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            }
         ],
         ""GroupPath"":""Sites"",
         ""Description"":null,
         ""FriendlyName"":""Site Document"",
         ""Hints"":[  

         ],
         ""Name"":""SiteDocument"",
         ""Title"":""SiteDocument"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":true,
            ""Properties"":[  
               ""UserSite:UserId""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":2,
         ""SetFriendlyName"":""User Sites"",
         ""SetName"":""UserSites"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":null,
         ""DefaultSortDescending"":false,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":5
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":13,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site Id"",
               ""Hints"":[  

               ],
               ""Name"":""SiteId"",
               ""Title"":""SiteId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":13,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""User Id"",
               ""Hints"":[  

               ],
               ""Name"":""UserId"",
               ""Title"":""UserId"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""User"",
               ""Hints"":[  

               ],
               ""Name"":""User"",
               ""Title"":""User"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":1
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":2,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Site"",
               ""Hints"":[  

               ],
               ""Name"":""Site"",
               ""Title"":""Site"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""UserSite:UserId"",
                     ""TargetKeyProperty"":""ApplicationUser:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""UserSite:User"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""ApplicationUser:Sites"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""ApplicationUser:Id""
            },
            {  
               ""Constraints"":[  
                  {  
                     ""SourceKeyProperty"":""UserSite:SiteId"",
                     ""TargetKeyProperty"":""Site:Id""
                  }
               ],
               ""Kind"":1,
               ""Source"":{  
                  ""RelationshipSide"":0,
                  ""Type"":""Unknown"",
                  ""IsCollection"":false,
                  ""Property"":""UserSite:Site"",
                  ""CountProperty"":null
               },
               ""Target"":{  
                  ""RelationshipSide"":1,
                  ""Type"":""Unknown"",
                  ""IsCollection"":true,
                  ""Property"":""Site:Users"",
                  ""CountProperty"":null
               },
               ""ConstraintKey"":""Id"",
               ""QualifiedConstraintKey"":""Site:Id""
            }
         ],
         ""GroupPath"":null,
         ""Description"":null,
         ""FriendlyName"":""User Site"",
         ""Hints"":[  

         ],
         ""Name"":""UserSite"",
         ""Title"":""UserSite"",
         ""HelpTexts"":[  

         ]
      },
      {  
         ""DisplayFormatting"":{  
            ""All"":[  

            ],
            ""Default"":null
         },
         ""EntityValidation"":{  
            ""All"":[  

            ]
         },
         ""Key"":{  
            ""HasRelationshipKeys"":false,
            ""Properties"":[  
               ""ApplicationLog:Id""
            ]
         },
         ""TitlePropertyName"":null,
         ""PreviewPropertyName"":null,
         ""ManageKind"":1,
         ""SetFriendlyName"":""Application Logs"",
         ""SetName"":""ApplicationLogs"",
         ""SetNameSet"":true,
         ""DefaultSortExpression"":""CreatedDate"",
         ""DefaultSortDescending"":true,
         ""PropertyOrder"":[  

         ],
         ""Properties"":[  
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":12
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":5,
               ""SearchKind"":1,
               ""ReadOnly"":true,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Id"",
               ""Hints"":[  

               ],
               ""Name"":""Id"",
               ""Title"":""Id"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":false,
                  ""Kind"":8
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":1,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":false,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Created Date"",
               ""Hints"":[  

               ],
               ""Name"":""CreatedDate"",
               ""Title"":""CreatedDate"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Module"",
               ""Hints"":[  

               ],
               ""Name"":""Module"",
               ""Title"":""Module"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Message"",
               ""Hints"":[  

               ],
               ""Name"":""Message"",
               ""Title"":""Message"",
               ""HelpTexts"":[  

               ]
            },
            {  
               ""ValidationRules"":{  
                  ""All"":[  

                  ]
               },
               ""DisplayRules"":{  
                  ""All"":[  

                  ]
               },
               ""RelationshipFilterRules"":{  
                  ""All"":[  

                  ]
               },
               ""Relationships"":[  

               ],
               ""TypeDefinition"":{  
                  ""IsCollection"":false,
                  ""ConvertedFromType"":null,
                  ""Nullable"":true,
                  ""Kind"":4
               },
               ""MediaKey"":{  
                  ""Separator"":""/"",
                  ""Groups"":[  

                  ]
               },
               ""Placeholder"":null,
               ""Kind"":1,
               ""SearchKind"":3,
               ""ReadOnly"":false,
               ""Hidden"":false,
               ""Sortable"":true,
               ""Searchable"":true,
               ""Nullable"":true,
               ""GroupPath"":null,
               ""Description"":null,
               ""FriendlyName"":""Kind"",
               ""Hints"":[  

               ],
               ""Name"":""Kind"",
               ""Title"":""Kind"",
               ""HelpTexts"":[  

               ]
            }
         ],
         ""Relationships"":[  

         ],
         ""GroupPath"":null,
         ""Description"":null,
         ""FriendlyName"":""Application Log"",
         ""Hints"":[  

         ],
         ""Name"":""ApplicationLog"",
         ""Title"":""ApplicationLog"",
         ""HelpTexts"":[  

         ]
      }
   ]
}";
    }
}
#endif