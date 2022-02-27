using Iql.Entities;
using IqlSampleApp.Sets;
using IqlSampleApp.ApiContext.Base;
using IqlSampleApp.Data.Entities;
using Iql.OData;
using Iql.Data.Context;
using Iql.Entities.SpecialTypes;
using Iql.Data.DataStores;
using System;
using System.Collections.Generic;
using Iql.OData.Methods;
using Iql;
using System.Linq.Expressions;
using Iql.Entities.Rules.Display;
using Iql.Entities.InferredValues;
using Iql.Entities.Metadata;
using Iql.Entities.Relationships;
using Iql.Entities.Functions;
namespace IqlSampleApp.ApiContext.Base
{
    public class IqlSampleAppDataContextBase: DataContext
    {
        public IqlSampleAppDataContextBase(IDataStore dataStore) : base(dataStore)
        {}
        protected override void InitializeProperties()
        {
            this.Users = this.AsCustomDbSet<ApplicationUser, String, ApplicationUserSet>();
            this.ApplicationLogs = this.AsCustomDbSet<ApplicationLog, Guid, ApplicationLogSet>();
            this.Clients = this.AsCustomDbSet<Client, int, ClientSet>();
            this.ClientTypes = this.AsCustomDbSet<ClientType, int, ClientTypeSet>();
            this.ClientCategories = this.AsCustomDbSet<ClientCategory, int, ClientCategorySet>();
            this.ClientCategoriesPivot = this.AsCustomDbSet<ClientCategoryPivot, CompositeKey, ClientCategoryPivotSet>();
            this.DocumentCategories = this.AsCustomDbSet<DocumentCategory, int, DocumentCategorySet>();
            this.SiteDocuments = this.AsCustomDbSet<SiteDocument, int, SiteDocumentSet>();
            this.ReportActionsTaken = this.AsCustomDbSet<ReportActionsTaken, int, ReportActionsTakenSet>();
            this.ReportCategories = this.AsCustomDbSet<ReportCategory, int, ReportCategorySet>();
            this.ReportDefaultRecommendations = this.AsCustomDbSet<ReportDefaultRecommendation, int, ReportDefaultRecommendationSet>();
            this.ReportRecommendations = this.AsCustomDbSet<ReportRecommendation, int, ReportRecommendationSet>();
            this.ReportTypes = this.AsCustomDbSet<ReportType, int, ReportTypeSet>();
            this.Projects = this.AsCustomDbSet<Project, int, ProjectSet>();
            this.ReportReceiverEmailAddresses = this.AsCustomDbSet<ReportReceiverEmailAddress, int, ReportReceiverEmailAddressSet>();
            this.RiskAssessments = this.AsCustomDbSet<RiskAssessment, int, RiskAssessmentSet>();
            this.RiskAssessmentSolutions = this.AsCustomDbSet<RiskAssessmentSolution, int, RiskAssessmentSolutionSet>();
            this.RiskAssessmentAnswers = this.AsCustomDbSet<RiskAssessmentAnswer, int, RiskAssessmentAnswerSet>();
            this.RiskAssessmentQuestions = this.AsCustomDbSet<RiskAssessmentQuestion, int, RiskAssessmentQuestionSet>();
            this.People = this.AsCustomDbSet<Person, int, PersonSet>();
            this.PersonInspections = this.AsCustomDbSet<PersonInspection, int, PersonInspectionSet>();
            this.PersonLoadings = this.AsCustomDbSet<PersonLoading, int, PersonLoadingSet>();
            this.PersonTypes = this.AsCustomDbSet<PersonType, int, PersonTypeSet>();
            this.PersonTypesMap = this.AsCustomDbSet<PersonTypeMap, CompositeKey, PersonTypeMapSet>();
            this.PersonReports = this.AsCustomDbSet<PersonReport, int, PersonReportSet>();
            this.Sites = this.AsCustomDbSet<Site, int, SiteSet>();
            this.SiteAreas = this.AsCustomDbSet<SiteArea, int, SiteAreaSet>();
            this.SiteInspections = this.AsCustomDbSet<SiteInspection, int, SiteInspectionSet>();
            this.UserSettings = this.AsCustomDbSet<UserSetting, Guid, UserSettingSet>();
            this.UserSites = this.AsCustomDbSet<UserSite, CompositeKey, UserSiteSet>();
            this.ODataConfiguration.RegisterEntitySet<ApplicationUser>(nameof(Users));
            this.ODataConfiguration.RegisterEntitySet<ApplicationLog>(nameof(ApplicationLogs));
            this.ODataConfiguration.RegisterEntitySet<Client>(nameof(Clients));
            this.ODataConfiguration.RegisterEntitySet<ClientType>(nameof(ClientTypes));
            this.ODataConfiguration.RegisterEntitySet<ClientCategory>(nameof(ClientCategories));
            this.ODataConfiguration.RegisterEntitySet<ClientCategoryPivot>(nameof(ClientCategoriesPivot));
            this.ODataConfiguration.RegisterEntitySet<DocumentCategory>(nameof(DocumentCategories));
            this.ODataConfiguration.RegisterEntitySet<SiteDocument>(nameof(SiteDocuments));
            this.ODataConfiguration.RegisterEntitySet<ReportActionsTaken>(nameof(ReportActionsTaken));
            this.ODataConfiguration.RegisterEntitySet<ReportCategory>(nameof(ReportCategories));
            this.ODataConfiguration.RegisterEntitySet<ReportDefaultRecommendation>(nameof(ReportDefaultRecommendations));
            this.ODataConfiguration.RegisterEntitySet<ReportRecommendation>(nameof(ReportRecommendations));
            this.ODataConfiguration.RegisterEntitySet<ReportType>(nameof(ReportTypes));
            this.ODataConfiguration.RegisterEntitySet<Project>(nameof(Projects));
            this.ODataConfiguration.RegisterEntitySet<ReportReceiverEmailAddress>(nameof(ReportReceiverEmailAddresses));
            this.ODataConfiguration.RegisterEntitySet<RiskAssessment>(nameof(RiskAssessments));
            this.ODataConfiguration.RegisterEntitySet<RiskAssessmentSolution>(nameof(RiskAssessmentSolutions));
            this.ODataConfiguration.RegisterEntitySet<RiskAssessmentAnswer>(nameof(RiskAssessmentAnswers));
            this.ODataConfiguration.RegisterEntitySet<RiskAssessmentQuestion>(nameof(RiskAssessmentQuestions));
            this.ODataConfiguration.RegisterEntitySet<Person>(nameof(People));
            this.ODataConfiguration.RegisterEntitySet<PersonInspection>(nameof(PersonInspections));
            this.ODataConfiguration.RegisterEntitySet<PersonLoading>(nameof(PersonLoadings));
            this.ODataConfiguration.RegisterEntitySet<PersonType>(nameof(PersonTypes));
            this.ODataConfiguration.RegisterEntitySet<PersonTypeMap>(nameof(PersonTypesMap));
            this.ODataConfiguration.RegisterEntitySet<PersonReport>(nameof(PersonReports));
            this.ODataConfiguration.RegisterEntitySet<Site>(nameof(Sites));
            this.ODataConfiguration.RegisterEntitySet<SiteArea>(nameof(SiteAreas));
            this.ODataConfiguration.RegisterEntitySet<SiteInspection>(nameof(SiteInspections));
            this.ODataConfiguration.RegisterEntitySet<UserSetting>(nameof(UserSettings));
            this.ODataConfiguration.RegisterEntitySet<UserSite>(nameof(UserSites));
            this.RegisterConfiguration<ODataConfiguration>(this.ODataConfiguration);
        }
        private ODataConfiguration _oDataConfiguration;
        public ODataConfiguration ODataConfiguration => _oDataConfiguration = _oDataConfiguration ?? new ODataConfiguration(() => EntityConfigurationContext);
        public override void Configure(EntityConfigurationBuilder builder)
        {
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "ClientReadAndEdit1",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "ClientId",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Integer
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlLiteralExpression
                        {
                            Value = 10L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 2L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "ClientReadAndEdit2",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "ClientId",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Integer
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlLiteralExpression
                        {
                            Value = 10L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 2L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "SkillsPermission",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "Email",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                Value = "CannotSeeOrEdit",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.String
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlLiteralExpression
                        {
                            Value = 1L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlConditionExpression
                        {
                            Test = new IqlIsEqualToExpression
                            {
                                Left = new IqlPropertyExpression
                                {
                                    PropertyName = "Email",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "User",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlRootReferenceExpression
                                        {
                                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                            VariableName = "context",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.RootReference,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Right = new IqlLiteralExpression
                                {
                                    Value = "CanSeeCannotEdit",
                                    InferredReturnType = IqlType.String,
                                    Kind = IqlExpressionKind.Literal,
                                    ReturnType = IqlType.String
                                },
                                Kind = IqlExpressionKind.IsEqualTo,
                                ReturnType = IqlType.Boolean
                            },
                            IfTrue = new IqlLiteralExpression
                            {
                                Value = 2L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            IfFalse = new IqlLiteralExpression
                            {
                                Value = 10L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Kind = IqlExpressionKind.Condition,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "SkillsPermissionOnlyReadAttemptedOverride",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "Email",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                Value = "OnlyRead",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.String
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlLiteralExpression
                        {
                            Value = 30L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 0L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "PersonsPermission",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "Email",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                Value = "CannotSeeOrEditAnyPersons",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.String
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlLiteralExpression
                        {
                            Value = 1L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 30L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "PersonsPermissionOnlyRead",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "Email",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                Value = "OnlyRead",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.String
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlLiteralExpression
                        {
                            Value = 2L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 30L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "SuperUser",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "UserType",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlEnumLiteralExpression
                            {
                                Value = new IqlEnumValueExpression[]
                                {
                                    new IqlEnumValueExpression
                                    {
                                        Name = "",
                                        Value = 1L,
                                        InferredReturnType = IqlType.Integer,
                                        Kind = IqlExpressionKind.EnumValue,
                                        ReturnType = IqlType.EnumValue
                                    }
                                },
                                InferredReturnType = IqlType.Collection,
                                Kind = IqlExpressionKind.EnumLiteral,
                                ReturnType = IqlType.Enum
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlLiteralExpression
                        {
                            Value = 10L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 1L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "PrecedenceBase",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "UserName",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                Value = "PrecedenceTest",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.String
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlConditionExpression
                        {
                            Test = new IqlIsEqualToExpression
                            {
                                Left = new IqlPropertyExpression
                                {
                                    PropertyName = "Email",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "User",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlRootReferenceExpression
                                        {
                                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                            VariableName = "context",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.RootReference,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Right = new IqlLiteralExpression
                                {
                                    Value = "PrecedenceBase",
                                    InferredReturnType = IqlType.String,
                                    Kind = IqlExpressionKind.Literal,
                                    ReturnType = IqlType.String
                                },
                                Kind = IqlExpressionKind.IsEqualTo,
                                ReturnType = IqlType.Boolean
                            },
                            IfTrue = new IqlLiteralExpression
                            {
                                Value = 30L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            IfFalse = new IqlLiteralExpression
                            {
                                Value = 1L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Kind = IqlExpressionKind.Condition,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 0L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Up,
                Key = "PrecedenceShouldOverride",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "UserName",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                Value = "PrecedenceTest",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.String
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlConditionExpression
                        {
                            Test = new IqlIsEqualToExpression
                            {
                                Left = new IqlPropertyExpression
                                {
                                    PropertyName = "FullName",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "User",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlRootReferenceExpression
                                        {
                                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                            VariableName = "context",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.RootReference,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Right = new IqlLiteralExpression
                                {
                                    Value = "PrecedenceShouldOverride",
                                    InferredReturnType = IqlType.String,
                                    Kind = IqlExpressionKind.Literal,
                                    ReturnType = IqlType.String
                                },
                                Kind = IqlExpressionKind.IsEqualTo,
                                ReturnType = IqlType.Boolean
                            },
                            IfTrue = new IqlLiteralExpression
                            {
                                Value = 30L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            IfFalse = new IqlLiteralExpression
                            {
                                Value = 0L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Kind = IqlExpressionKind.Condition,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 0L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.PermissionRules.Add(new IqlUserPermissionRule
            {
                Precedence = IqlUserPermissionRulePrecedenceDirection.Down,
                Key = "PrecedenceShouldNotOverride",
                IqlExpression = new IqlLambdaExpression
                {
                    Body = new IqlConditionExpression
                    {
                        Test = new IqlIsEqualToExpression
                        {
                            Left = new IqlPropertyExpression
                            {
                                PropertyName = "UserName",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "User",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                        VariableName = "context",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Right = new IqlLiteralExpression
                            {
                                Value = "PrecedenceTest",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.String
                            },
                            Kind = IqlExpressionKind.IsEqualTo,
                            ReturnType = IqlType.Boolean
                        },
                        IfTrue = new IqlConditionExpression
                        {
                            Test = new IqlIsEqualToExpression
                            {
                                Left = new IqlPropertyExpression
                                {
                                    PropertyName = "FullName",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "User",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlRootReferenceExpression
                                        {
                                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                                            VariableName = "context",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.RootReference,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Right = new IqlLiteralExpression
                                {
                                    Value = "PrecedenceShouldNotOverride",
                                    InferredReturnType = IqlType.String,
                                    Kind = IqlExpressionKind.Literal,
                                    ReturnType = IqlType.String
                                },
                                Kind = IqlExpressionKind.IsEqualTo,
                                ReturnType = IqlType.Boolean
                            },
                            IfTrue = new IqlLiteralExpression
                            {
                                Value = 30L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            IfFalse = new IqlLiteralExpression
                            {
                                Value = 0L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Kind = IqlExpressionKind.Condition,
                            ReturnType = IqlType.Unknown
                        },
                        IfFalse = new IqlLiteralExpression
                        {
                            Value = 0L,
                            InferredReturnType = IqlType.Integer,
                            Kind = IqlExpressionKind.Literal,
                            ReturnType = IqlType.Unknown
                        },
                        Kind = IqlExpressionKind.Condition,
                        ReturnType = IqlType.Unknown
                    },
                    Parameters = new List<IqlRootReferenceExpression>
                    {
                        new IqlRootReferenceExpression
                        {
                            EntityTypeName = "IqlUserPermissionContext<ApplicationUser>",
                            VariableName = "context",
                            InferredReturnType = IqlType.Unknown,
                            Kind = IqlExpressionKind.RootReference,
                            ReturnType = IqlType.Unknown
                        }
                    },
                    Kind = IqlExpressionKind.Lambda,
                    ReturnType = IqlType.Unknown
                },
                UserTypeName = "ApplicationUser"
            });
            builder.DefineEntityType<ApplicationUser>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.IsLockedOut, false, IqlType.Boolean).ConfigureProperty(p => p.IsLockedOut, p => {
                p.Name = "IsLockedOut";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Is Locked Out";
                p.Name = "IsLockedOut";
                p.Title = "IsLockedOut";
                p.Permissions.UseRule("SuperUser");
            }).DefineProperty(p => p.ClientId, true, IqlType.Integer).ConfigureProperty(p => p.ClientId, p => {
                p.Name = "ClientId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Client Id";
                p.Name = "ClientId";
                p.Title = "ClientId";
            }).DefineProperty(p => p.Id, false, IqlType.String).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.Email, true, IqlType.String).ConfigureProperty(p => p.Email, p => {
                p.Name = "Email";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Email";
                p.Hints = new List<string>(new[]
                {
                    "Iql.Forms:EmailAddress"
                });
                p.Name = "Email";
                p.Title = "Email";
            }).DefineProperty(p => p.Permissions, false, IqlType.Enum).ConfigureProperty(p => p.Permissions, p => {
                p.Name = "Permissions";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Permissions";
                p.Name = "Permissions";
                p.Title = "Permissions";
            }).DefineProperty(p => p.UserType, false, IqlType.Enum).ConfigureProperty(p => p.UserType, p => {
                p.Name = "UserType";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "User Type";
                p.Name = "UserType";
                p.Title = "UserType";
            }).DefineProperty(p => p.FullName, false, IqlType.String).ConfigureProperty(p => p.FullName, p => {
                p.Name = "FullName";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Full Name";
                p.Name = "FullName";
                p.Title = "FullName";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.UserName, true, IqlType.String).ConfigureProperty(p => p.UserName, p => {
                p.Name = "UserName";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "User Name";
                p.Name = "UserName";
                p.Title = "UserName";
            }).DefineProperty(p => p.EmailConfirmed, false, IqlType.Boolean).ConfigureProperty(p => p.EmailConfirmed, p => {
                p.Name = "EmailConfirmed";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.EditKind = IqlPropertyEditKind.Display;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Email Confirmed";
                p.Name = "EmailConfirmed";
                p.Title = "EmailConfirmed";
            }).DefineProperty(p => p.PhoneNumber, true, IqlType.String).ConfigureProperty(p => p.PhoneNumber, p => {
                p.Name = "PhoneNumber";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Phone Number";
                p.Name = "PhoneNumber";
                p.Title = "PhoneNumber";
            }).DefineProperty(p => p.PhoneNumberConfirmed, false, IqlType.Boolean).ConfigureProperty(p => p.PhoneNumberConfirmed, p => {
                p.Name = "PhoneNumberConfirmed";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.EditKind = IqlPropertyEditKind.Display;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Phone Number Confirmed";
                p.Name = "PhoneNumberConfirmed";
                p.Title = "PhoneNumberConfirmed";
            }).DefineProperty(p => p.TwoFactorEnabled, false, IqlType.Boolean).ConfigureProperty(p => p.TwoFactorEnabled, p => {
                p.Name = "TwoFactorEnabled";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.EditKind = IqlPropertyEditKind.Display;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Two Factor Enabled";
                p.Name = "TwoFactorEnabled";
                p.Title = "TwoFactorEnabled";
            }).DefineProperty(p => p.LockoutEnd, true, IqlType.Date).ConfigureProperty(p => p.LockoutEnd, p => {
                p.Name = "LockoutEnd";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Lockout End";
                p.Name = "LockoutEnd";
                p.Title = "LockoutEnd";
            }).DefineProperty(p => p.LockoutEnabled, false, IqlType.Boolean).ConfigureProperty(p => p.LockoutEnabled, p => {
                p.Name = "LockoutEnabled";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Lockout Enabled";
                p.Name = "LockoutEnabled";
                p.Title = "LockoutEnabled";
            }).DefineProperty(p => p.Client, true, IqlType.Unknown).ConfigureProperty(p => p.Client, p => {
                p.Name = "Client";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Client";
                p.Name = "Client";
                p.Title = "Client";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).DefineCollectionProperty(p => p.ClientsCreated, p => p.ClientsCreatedCount).ConfigureProperty(p => p.ClientsCreated, p => {
                p.Name = "ClientsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Clients Created";
                p.Name = "ClientsCreated";
                p.Title = "ClientsCreated";
            }).DefineCollectionProperty(p => p.DocumentCategoriesCreated, p => p.DocumentCategoriesCreatedCount).ConfigureProperty(p => p.DocumentCategoriesCreated, p => {
                p.Name = "DocumentCategoriesCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Document Categories Created";
                p.Name = "DocumentCategoriesCreated";
                p.Title = "DocumentCategoriesCreated";
            }).DefineCollectionProperty(p => p.SiteDocumentsCreated, p => p.SiteDocumentsCreatedCount).ConfigureProperty(p => p.SiteDocumentsCreated, p => {
                p.Name = "SiteDocumentsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site Documents Created";
                p.Name = "SiteDocumentsCreated";
                p.Title = "SiteDocumentsCreated";
            }).DefineCollectionProperty(p => p.FaultActionsTakenCreated, p => p.FaultActionsTakenCreatedCount).ConfigureProperty(p => p.FaultActionsTakenCreated, p => {
                p.Name = "FaultActionsTakenCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Fault Actions Taken Created";
                p.Name = "FaultActionsTakenCreated";
                p.Title = "FaultActionsTakenCreated";
            }).DefineCollectionProperty(p => p.FaultCategoriesCreated, p => p.FaultCategoriesCreatedCount).ConfigureProperty(p => p.FaultCategoriesCreated, p => {
                p.Name = "FaultCategoriesCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Fault Categories Created";
                p.Name = "FaultCategoriesCreated";
                p.Title = "FaultCategoriesCreated";
            }).DefineCollectionProperty(p => p.FaultDefaultRecommendationsCreated, p => p.FaultDefaultRecommendationsCreatedCount).ConfigureProperty(p => p.FaultDefaultRecommendationsCreated, p => {
                p.Name = "FaultDefaultRecommendationsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Fault Default Recommendations Created";
                p.Name = "FaultDefaultRecommendationsCreated";
                p.Title = "FaultDefaultRecommendationsCreated";
            }).DefineCollectionProperty(p => p.FaultRecommendationsCreated, p => p.FaultRecommendationsCreatedCount).ConfigureProperty(p => p.FaultRecommendationsCreated, p => {
                p.Name = "FaultRecommendationsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Fault Recommendations Created";
                p.Name = "FaultRecommendationsCreated";
                p.Title = "FaultRecommendationsCreated";
            }).DefineCollectionProperty(p => p.FaultTypesCreated, p => p.FaultTypesCreatedCount).ConfigureProperty(p => p.FaultTypesCreated, p => {
                p.Name = "FaultTypesCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Fault Types Created";
                p.Name = "FaultTypesCreated";
                p.Title = "FaultTypesCreated";
            }).DefineCollectionProperty(p => p.ProjectCreated, p => p.ProjectCreatedCount).ConfigureProperty(p => p.ProjectCreated, p => {
                p.Name = "ProjectCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Project Created";
                p.Name = "ProjectCreated";
                p.Title = "ProjectCreated";
            }).DefineCollectionProperty(p => p.ReportReceiverEmailAddressesCreated, p => p.ReportReceiverEmailAddressesCreatedCount).ConfigureProperty(p => p.ReportReceiverEmailAddressesCreated, p => {
                p.Name = "ReportReceiverEmailAddressesCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Report Receiver Email Addresses Created";
                p.Name = "ReportReceiverEmailAddressesCreated";
                p.Title = "ReportReceiverEmailAddressesCreated";
            }).DefineCollectionProperty(p => p.RiskAssessmentsCreated, p => p.RiskAssessmentsCreatedCount).ConfigureProperty(p => p.RiskAssessmentsCreated, p => {
                p.Name = "RiskAssessmentsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Risk Assessments Created";
                p.Name = "RiskAssessmentsCreated";
                p.Title = "RiskAssessmentsCreated";
            }).DefineCollectionProperty(p => p.RiskAssessmentSolutionsCreated, p => p.RiskAssessmentSolutionsCreatedCount).ConfigureProperty(p => p.RiskAssessmentSolutionsCreated, p => {
                p.Name = "RiskAssessmentSolutionsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Risk Assessment Solutions Created";
                p.Name = "RiskAssessmentSolutionsCreated";
                p.Title = "RiskAssessmentSolutionsCreated";
            }).DefineCollectionProperty(p => p.RiskAssessmentAnswersCreated, p => p.RiskAssessmentAnswersCreatedCount).ConfigureProperty(p => p.RiskAssessmentAnswersCreated, p => {
                p.Name = "RiskAssessmentAnswersCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Risk Assessment Answers Created";
                p.Name = "RiskAssessmentAnswersCreated";
                p.Title = "RiskAssessmentAnswersCreated";
            }).DefineCollectionProperty(p => p.RiskAssessmentQuestionsCreated, p => p.RiskAssessmentQuestionsCreatedCount).ConfigureProperty(p => p.RiskAssessmentQuestionsCreated, p => {
                p.Name = "RiskAssessmentQuestionsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Risk Assessment Questions Created";
                p.Name = "RiskAssessmentQuestionsCreated";
                p.Title = "RiskAssessmentQuestionsCreated";
            }).DefineCollectionProperty(p => p.PeopleCreated, p => p.PeopleCreatedCount).ConfigureProperty(p => p.PeopleCreated, p => {
                p.Name = "PeopleCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "People Created";
                p.Name = "PeopleCreated";
                p.Title = "PeopleCreated";
            }).DefineCollectionProperty(p => p.PersonInspectionsCreated, p => p.PersonInspectionsCreatedCount).ConfigureProperty(p => p.PersonInspectionsCreated, p => {
                p.Name = "PersonInspectionsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Person Inspections Created";
                p.Name = "PersonInspectionsCreated";
                p.Title = "PersonInspectionsCreated";
            }).DefineCollectionProperty(p => p.PersonLoadingsCreated, p => p.PersonLoadingsCreatedCount).ConfigureProperty(p => p.PersonLoadingsCreated, p => {
                p.Name = "PersonLoadingsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Person Loadings Created";
                p.Name = "PersonLoadingsCreated";
                p.Title = "PersonLoadingsCreated";
            }).DefineCollectionProperty(p => p.PersonTypesCreated, p => p.PersonTypesCreatedCount).ConfigureProperty(p => p.PersonTypesCreated, p => {
                p.Name = "PersonTypesCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Person Types Created";
                p.Name = "PersonTypesCreated";
                p.Title = "PersonTypesCreated";
            }).DefineCollectionProperty(p => p.FaultReportsCreated, p => p.FaultReportsCreatedCount).ConfigureProperty(p => p.FaultReportsCreated, p => {
                p.Name = "FaultReportsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Fault Reports Created";
                p.Name = "FaultReportsCreated";
                p.Title = "FaultReportsCreated";
            }).DefineCollectionProperty(p => p.SitesCreated, p => p.SitesCreatedCount).ConfigureProperty(p => p.SitesCreated, p => {
                p.Name = "SitesCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Sites Created";
                p.Name = "SitesCreated";
                p.Title = "SitesCreated";
            }).DefineCollectionProperty(p => p.SiteAreasCreated, p => p.SiteAreasCreatedCount).ConfigureProperty(p => p.SiteAreasCreated, p => {
                p.Name = "SiteAreasCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site Areas Created";
                p.Name = "SiteAreasCreated";
                p.Title = "SiteAreasCreated";
            }).DefineCollectionProperty(p => p.SiteInspectionsCreated, p => p.SiteInspectionsCreatedCount).ConfigureProperty(p => p.SiteInspectionsCreated, p => {
                p.Name = "SiteInspectionsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site Inspections Created";
                p.Name = "SiteInspectionsCreated";
                p.Title = "SiteInspectionsCreated";
            }).DefineCollectionProperty(p => p.UserSettingsCreated, p => p.UserSettingsCreatedCount).ConfigureProperty(p => p.UserSettingsCreated, p => {
                p.Name = "UserSettingsCreated";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "User Settings Created";
                p.Name = "UserSettingsCreated";
                p.Title = "UserSettingsCreated";
            }).DefineCollectionProperty(p => p.UserSettings, p => p.UserSettingsCount).ConfigureProperty(p => p.UserSettings, p => {
                p.Name = "UserSettings";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "User Settings";
                p.Name = "UserSettings";
                p.Title = "UserSettings";
            }).DefineCollectionProperty(p => p.Sites, p => p.SitesCount).ConfigureProperty(p => p.Sites, p => {
                p.Name = "Sites";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Sites";
                p.Name = "Sites";
                p.Title = "Sites";
            });
            builder.DefineEntityType<ApplicationUser>().HasOne(p => p.Client).WithMany(p => p.Users).WithConstraint(p => p.ClientId, p => p.Id);
            builder.DefineEntityType<ApplicationUser>().Configure(p => {
                p.SetFriendlyName = "Users";
                p.SetName = "Users";
                p.FriendlyName = "Application User";
                p.Name = "ApplicationUser";
                p.Title = "ApplicationUser";
                p.Permissions.UseRule("PrecedenceBase").UseRule("PrecedenceShouldOverride").UseRule("PrecedenceShouldNotOverride");
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>(),
                        ScopeKind = IqlMethodScopeKind.EntitySet,
                        ReturnType = typeof(IEnumerable<ApplicationUser>),
                        ReturnTypeName = "Collection<ApplicationUser>",
                        Metadata = new MetadataCollection(),
                        Name = "OldUsers",
                        Title = "OldUsers",
                        FriendlyName = "Old Users",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ApplicationUser),
                                    TypeName = "ApplicationUser",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ApplicationUser),
                                    ElementTypeName = "ApplicationUser",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ApplicationUser),
                                    TypeName = "ApplicationUser",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ApplicationUser),
                                    ElementTypeName = "ApplicationUser",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "id",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Int32),
                                    TypeName = "int",
                                    Nullable = false,
                                    Kind = IqlType.Integer,
                                    ElementType = typeof(Int32),
                                    ElementTypeName = "int",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "type",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Int32),
                                    TypeName = "int",
                                    Nullable = false,
                                    Kind = IqlType.Integer,
                                    ElementType = typeof(Int32),
                                    ElementTypeName = "int",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.EntitySet,
                        ReturnType = typeof(IEnumerable<ApplicationUser>),
                        ReturnTypeName = "Collection<ApplicationUser>",
                        Metadata = new MetadataCollection(),
                        Name = "ForClient",
                        Title = "ForClient",
                        FriendlyName = "For Client",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ApplicationUser),
                                    TypeName = "ApplicationUser",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ApplicationUser),
                                    ElementTypeName = "ApplicationUser",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GeneratePasswordResetLink",
                        Title = "GeneratePasswordResetLink",
                        FriendlyName = "Generate Password Reset Link",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ApplicationUser),
                                    TypeName = "ApplicationUser",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ApplicationUser),
                                    ElementTypeName = "ApplicationUser",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "AccountConfirm",
                        Title = "AccountConfirm",
                        FriendlyName = "Account Confirm",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ApplicationUser),
                                    TypeName = "ApplicationUser",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ApplicationUser),
                                    ElementTypeName = "ApplicationUser",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "SendPasswordResetEmail",
                        Title = "SendPasswordResetEmail",
                        FriendlyName = "Send Password Reset Email",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ApplicationUser),
                                    TypeName = "ApplicationUser",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ApplicationUser),
                                    ElementTypeName = "ApplicationUser",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "ReinstateUser",
                        Title = "ReinstateUser",
                        FriendlyName = "Reinstate User",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>(),
                        ScopeKind = IqlMethodScopeKind.EntitySet,
                        ReturnType = typeof(ApplicationUser),
                        ReturnTypeName = "ApplicationUser",
                        Metadata = new MetadataCollection(),
                        Name = "Me",
                        Title = "Me",
                        FriendlyName = "Me",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ApplicationLog>().HasKey(p => p.Id, IqlType.Unknown, false).DefineConvertedProperty(p => p.Id, "Guid", false, IqlType.String).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.Module, true, IqlType.String).ConfigureProperty(p => p.Module, p => {
                p.Name = "Module";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Module";
                p.Name = "Module";
                p.Title = "Module";
            }).DefineProperty(p => p.Message, true, IqlType.String).ConfigureProperty(p => p.Message, p => {
                p.Name = "Message";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Message";
                p.Name = "Message";
                p.Title = "Message";
            }).DefineProperty(p => p.Kind, true, IqlType.String).ConfigureProperty(p => p.Kind, p => {
                p.Name = "Kind";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Kind";
                p.Name = "Kind";
                p.Title = "Kind";
            });
            builder.DefineEntityType<ApplicationLog>().Configure(p => {
                p.SetFriendlyName = "Application Logs";
                p.SetName = "ApplicationLogs";
                p.FriendlyName = "Application Log";
                p.Name = "ApplicationLog";
                p.Title = "ApplicationLog";
            });
            builder.DefineEntityType<Client>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.TypeId, false, IqlType.Integer).ConfigureProperty(p => p.TypeId, p => {
                p.Name = "TypeId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Type Id";
                p.Name = "TypeId";
                p.Title = "TypeId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Client>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.AverageSales, false, IqlType.Decimal).ConfigureProperty(p => p.AverageSales, p => {
                p.Name = "AverageSales";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Average Sales";
                p.Name = "AverageSales";
                p.Title = "AverageSales";
            }).DefineProperty(p => p.AverageIncome, false, IqlType.Decimal).ConfigureProperty(p => p.AverageIncome, p => {
                p.Name = "AverageIncome";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Average Income";
                p.Name = "AverageIncome";
                p.Title = "AverageIncome";
                p.Permissions.UseRule("PropertyRule1").UseRule("PropertyRule2");
            }).DefineProperty(p => p.Category, false, IqlType.Integer).ConfigureProperty(p => p.Category, p => {
                p.Name = "Category";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Category";
                p.Name = "Category";
                p.Title = "Category";
            }).DefineProperty(p => p.Description, true, IqlType.String).ConfigureProperty(p => p.Description, p => {
                p.Name = "Description";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Description";
                p.Name = "Description";
                p.Title = "Description";
            }).DefineProperty(p => p.Discount, false, IqlType.Decimal).ConfigureProperty(p => p.Discount, p => {
                p.Name = "Discount";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Discount";
                p.Name = "Discount";
                p.Title = "Discount";
            }).DefineProperty(p => p.Name, false, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Client>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Client>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Client>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineCollectionProperty(p => p.Users, p => p.UsersCount).ConfigureProperty(p => p.Users, p => {
                p.Name = "Users";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Users";
                p.Name = "Users";
                p.Title = "Users";
            }).DefineProperty(p => p.Type, false, IqlType.Unknown).ConfigureProperty(p => p.Type, p => {
                p.Name = "Type";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Type";
                p.Name = "Type";
                p.Title = "Type";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineCollectionProperty(p => p.Categories, p => p.CategoriesCount).ConfigureProperty(p => p.Categories, p => {
                p.Name = "Categories";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Categories";
                p.Name = "Categories";
                p.Title = "Categories";
            }).DefineCollectionProperty(p => p.People, p => p.PeopleCount).ConfigureProperty(p => p.People, p => {
                p.Name = "People";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "People";
                p.Name = "People";
                p.Title = "People";
            }).DefineCollectionProperty(p => p.InferredPeople, p => p.InferredPeopleCount).ConfigureProperty(p => p.InferredPeople, p => {
                p.Name = "InferredPeople";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Inferred People";
                p.Name = "InferredPeople";
                p.Title = "InferredPeople";
            }).DefineCollectionProperty(p => p.Sites, p => p.SitesCount).ConfigureProperty(p => p.Sites, p => {
                p.Name = "Sites";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Sites";
                p.Name = "Sites";
                p.Title = "Sites";
            });
            builder.DefineEntityType<Client>().HasOne(p => p.Type).WithMany(p => p.Clients).WithConstraint(p => p.TypeId, p => p.Id);
            builder.DefineEntityType<Client>().HasOne(p => p.CreatedByUser).WithMany(p => p.ClientsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<Client>().Configure(p => {
                p.SetFriendlyName = "Clients";
                p.SetName = "Clients";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Client";
                p.Name = "Client";
                p.Title = "Client";
                p.Permissions.UseRule("BlipBlop").UseRule("BooBoo");
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        NameSpace = "Abc",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Client),
                                    TypeName = "Client",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(Client),
                                    ElementTypeName = "Client",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>
                            {
                                "FlipFlop"
                            }
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Client),
                                    TypeName = "Client",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(Client),
                                    ElementTypeName = "Client",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>(),
                        ScopeKind = IqlMethodScopeKind.EntitySet,
                        ReturnType = typeof(IEnumerable<Client>),
                        ReturnTypeName = "Collection<Client>",
                        Metadata = new MetadataCollection(),
                        Name = "All",
                        Title = "All",
                        FriendlyName = "All",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ClientType>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineCollectionProperty(p => p.Clients, p => p.ClientsCount).ConfigureProperty(p => p.Clients, p => {
                p.Name = "Clients";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Clients";
                p.Name = "Clients";
                p.Title = "Clients";
            });
            builder.DefineEntityType<ClientType>().Configure(p => {
                p.SetFriendlyName = "Client Types";
                p.SetName = "ClientTypes";
                p.FriendlyName = "Client Type";
                p.Name = "ClientType";
                p.Title = "ClientType";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ClientType),
                                    TypeName = "ClientType",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ClientType),
                                    ElementTypeName = "ClientType",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "SayHi",
                        Title = "SayHi",
                        FriendlyName = "Say Hi",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ClientType),
                                    TypeName = "ClientType",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ClientType),
                                    ElementTypeName = "ClientType",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ClientType),
                                    TypeName = "ClientType",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ClientType),
                                    ElementTypeName = "ClientType",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ClientCategory>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineCollectionProperty(p => p.Clients, p => p.ClientsCount).ConfigureProperty(p => p.Clients, p => {
                p.Name = "Clients";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Clients";
                p.Name = "Clients";
                p.Title = "Clients";
            });
            builder.DefineEntityType<ClientCategory>().Configure(p => {
                p.SetFriendlyName = "Client Categories";
                p.SetName = "ClientCategories";
                p.FriendlyName = "Client Category";
                p.Name = "ClientCategory";
                p.Title = "ClientCategory";
            });
            builder.DefineEntityType<ClientCategoryPivot>().HasCompositeKey(false, p => p.ClientId, p => p.CategoryId).DefineProperty(p => p.ClientId, false, IqlType.Integer).ConfigureProperty(p => p.ClientId, p => {
                p.Name = "ClientId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.SimpleCollection | IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Client Id";
                p.Name = "ClientId";
                p.Title = "ClientId";
            }).DefineProperty(p => p.CategoryId, false, IqlType.Integer).ConfigureProperty(p => p.CategoryId, p => {
                p.Name = "CategoryId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.SimpleCollection | IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Category Id";
                p.Name = "CategoryId";
                p.Title = "CategoryId";
            }).DefineProperty(p => p.Client, false, IqlType.Unknown).ConfigureProperty(p => p.Client, p => {
                p.Name = "Client";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Client";
                p.Name = "Client";
                p.Title = "Client";
            }).DefineProperty(p => p.Category, false, IqlType.Unknown).ConfigureProperty(p => p.Category, p => {
                p.Name = "Category";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Category";
                p.Name = "Category";
                p.Title = "Category";
            });
            builder.DefineEntityType<ClientCategoryPivot>().HasOne(p => p.Client).WithMany(p => p.Categories).WithConstraint(p => p.ClientId, p => p.Id);
            builder.DefineEntityType<ClientCategoryPivot>().HasOne(p => p.Category).WithMany(p => p.Clients).WithConstraint(p => p.CategoryId, p => p.Id);
            builder.DefineEntityType<ClientCategoryPivot>().Configure(p => {
                p.SetFriendlyName = "Client Categories Pivot";
                p.SetName = "ClientCategoriesPivot";
                p.FriendlyName = "Client Category Pivot";
                p.Name = "ClientCategoryPivot";
                p.Title = "ClientCategoryPivot";
            });
            builder.DefineEntityType<DocumentCategory>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<DocumentCategory>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<DocumentCategory>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<DocumentCategory>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<DocumentCategory>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount).ConfigureProperty(p => p.Documents, p => {
                p.Name = "Documents";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Documents";
                p.Name = "Documents";
                p.Title = "Documents";
            });
            builder.DefineEntityType<DocumentCategory>().HasOne(p => p.CreatedByUser).WithMany(p => p.DocumentCategoriesCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<DocumentCategory>().Configure(p => {
                p.SetFriendlyName = "Document Categories";
                p.SetName = "DocumentCategories";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Document Category";
                p.Name = "DocumentCategory";
                p.Title = "DocumentCategory";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(DocumentCategory),
                                    TypeName = "DocumentCategory",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(DocumentCategory),
                                    ElementTypeName = "DocumentCategory",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(DocumentCategory),
                                    TypeName = "DocumentCategory",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(DocumentCategory),
                                    ElementTypeName = "DocumentCategory",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<SiteDocument>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.CategoryId, false, IqlType.Integer).ConfigureProperty(p => p.CategoryId, p => {
                p.Name = "CategoryId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Category Id";
                p.Name = "CategoryId";
                p.Title = "CategoryId";
            }).DefineProperty(p => p.SiteId, false, IqlType.Integer).ConfigureProperty(p => p.SiteId, p => {
                p.Name = "SiteId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Id";
                p.Name = "SiteId";
                p.Title = "SiteId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteDocument>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Title, true, IqlType.String).ConfigureProperty(p => p.Title, p => {
                p.Name = "Title";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Title";
                p.Name = "Title";
                p.Title = "Title";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteDocument>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteDocument>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteDocument>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.Category, false, IqlType.Unknown).ConfigureProperty(p => p.Category, p => {
                p.Name = "Category";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Category";
                p.Name = "Category";
                p.Title = "Category";
            }).DefineProperty(p => p.Site, false, IqlType.Unknown).ConfigureProperty(p => p.Site, p => {
                p.Name = "Site";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site";
                p.Name = "Site";
                p.Title = "Site";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            });
            builder.DefineEntityType<SiteDocument>().HasOne(p => p.Category).WithMany(p => p.Documents).WithConstraint(p => p.CategoryId, p => p.Id);
            builder.DefineEntityType<SiteDocument>().HasOne(p => p.Site).WithMany(p => p.Documents).WithConstraint(p => p.SiteId, p => p.Id);
            builder.DefineEntityType<SiteDocument>().HasOne(p => p.CreatedByUser).WithMany(p => p.SiteDocumentsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<SiteDocument>().Configure(p => {
                p.SetFriendlyName = "Site Documents";
                p.SetName = "SiteDocuments";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Site Document";
                p.Name = "SiteDocument";
                p.Title = "SiteDocument";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(SiteDocument),
                                    TypeName = "SiteDocument",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(SiteDocument),
                                    ElementTypeName = "SiteDocument",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(SiteDocument),
                                    TypeName = "SiteDocument",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(SiteDocument),
                                    ElementTypeName = "SiteDocument",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ReportActionsTaken>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.FaultReportId, false, IqlType.Integer).ConfigureProperty(p => p.FaultReportId, p => {
                p.Name = "FaultReportId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Fault Report Id";
                p.Name = "FaultReportId";
                p.Title = "FaultReportId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportActionsTaken>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Notes, true, IqlType.String).ConfigureProperty(p => p.Notes, p => {
                p.Name = "Notes";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Notes";
                p.Name = "Notes";
                p.Title = "Notes";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportActionsTaken>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportActionsTaken>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportActionsTaken>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.PersonReport, false, IqlType.Unknown).ConfigureProperty(p => p.PersonReport, p => {
                p.Name = "PersonReport";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Person Report";
                p.Name = "PersonReport";
                p.Title = "PersonReport";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefinePropertyValidation(p => p.Notes, entity => (((entity.Notes == null ? null : entity.Notes.ToUpper()) != null) || ((entity.Notes == null ? null : entity.Notes.ToUpper()) != ("" == null ? null : "".ToUpper()))), "Please enter some actions taken notes", "6").DefinePropertyValidation(p => p.Notes, entity => (entity.Notes.Length > 5), "Please enter at least five characters for notes", "7");
            builder.DefineEntityType<ReportActionsTaken>().HasOne(p => p.PersonReport).WithMany(p => p.ActionsTaken).WithConstraint(p => p.FaultReportId, p => p.Id);
            builder.DefineEntityType<ReportActionsTaken>().HasOne(p => p.CreatedByUser).WithMany(p => p.FaultActionsTakenCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<ReportActionsTaken>().Configure(p => {
                p.SetFriendlyName = "Report Actions Taken";
                p.SetName = "ReportActionsTaken";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Report Actions Taken";
                p.Name = "ReportActionsTaken";
                p.Title = "ReportActionsTaken";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportActionsTaken),
                                    TypeName = "ReportActionsTaken",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportActionsTaken),
                                    ElementTypeName = "ReportActionsTaken",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportActionsTaken),
                                    TypeName = "ReportActionsTaken",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportActionsTaken),
                                    ElementTypeName = "ReportActionsTaken",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ReportCategory>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportCategory>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportCategory>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportCategory>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportCategory>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineCollectionProperty(p => p.ReportTypes, p => p.ReportTypesCount).ConfigureProperty(p => p.ReportTypes, p => {
                p.Name = "ReportTypes";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Report Types";
                p.Name = "ReportTypes";
                p.Title = "ReportTypes";
            });
            builder.DefineEntityType<ReportCategory>().HasOne(p => p.CreatedByUser).WithMany(p => p.FaultCategoriesCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<ReportCategory>().Configure(p => {
                p.SetFriendlyName = "Report Categories";
                p.SetName = "ReportCategories";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Report Category";
                p.Name = "ReportCategory";
                p.Title = "ReportCategory";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportCategory),
                                    TypeName = "ReportCategory",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportCategory),
                                    ElementTypeName = "ReportCategory",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportCategory),
                                    TypeName = "ReportCategory",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportCategory),
                                    ElementTypeName = "ReportCategory",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ReportDefaultRecommendation>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportDefaultRecommendation>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineProperty(p => p.Text, true, IqlType.String).ConfigureProperty(p => p.Text, p => {
                p.Name = "Text";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Text";
                p.Name = "Text";
                p.Title = "Text";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportDefaultRecommendation>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportDefaultRecommendation>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportDefaultRecommendation>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount).ConfigureProperty(p => p.Recommendations, p => {
                p.Name = "Recommendations";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Recommendations";
                p.Name = "Recommendations";
                p.Title = "Recommendations";
            });
            builder.DefineEntityType<ReportDefaultRecommendation>().HasOne(p => p.CreatedByUser).WithMany(p => p.FaultDefaultRecommendationsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<ReportDefaultRecommendation>().Configure(p => {
                p.SetFriendlyName = "Report Default Recommendations";
                p.SetName = "ReportDefaultRecommendations";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Report Default Recommendation";
                p.Name = "ReportDefaultRecommendation";
                p.Title = "ReportDefaultRecommendation";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportDefaultRecommendation),
                                    TypeName = "ReportDefaultRecommendation",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportDefaultRecommendation),
                                    ElementTypeName = "ReportDefaultRecommendation",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportDefaultRecommendation),
                                    TypeName = "ReportDefaultRecommendation",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportDefaultRecommendation),
                                    ElementTypeName = "ReportDefaultRecommendation",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ReportRecommendation>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.ReportId, false, IqlType.Integer).ConfigureProperty(p => p.ReportId, p => {
                p.Name = "ReportId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Report Id";
                p.Name = "ReportId";
                p.Title = "ReportId";
            }).DefineProperty(p => p.RecommendationId, false, IqlType.Integer).ConfigureProperty(p => p.RecommendationId, p => {
                p.Name = "RecommendationId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Recommendation Id";
                p.Name = "RecommendationId";
                p.Title = "RecommendationId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportRecommendation>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Notes, true, IqlType.String).ConfigureProperty(p => p.Notes, p => {
                p.Name = "Notes";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Notes";
                p.Name = "Notes";
                p.Title = "Notes";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportRecommendation>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportRecommendation>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportRecommendation>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.PersonReport, false, IqlType.Unknown).ConfigureProperty(p => p.PersonReport, p => {
                p.Name = "PersonReport";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Person Report";
                p.Name = "PersonReport";
                p.Title = "PersonReport";
            }).DefineProperty(p => p.Recommendation, false, IqlType.Unknown).ConfigureProperty(p => p.Recommendation, p => {
                p.Name = "Recommendation";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Recommendation";
                p.Name = "Recommendation";
                p.Title = "Recommendation";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            });
            builder.DefineEntityType<ReportRecommendation>().HasOne(p => p.PersonReport).WithMany(p => p.Recommendations).WithConstraint(p => p.ReportId, p => p.Id);
            builder.DefineEntityType<ReportRecommendation>().HasOne(p => p.Recommendation).WithMany(p => p.Recommendations).WithConstraint(p => p.RecommendationId, p => p.Id);
            builder.DefineEntityType<ReportRecommendation>().HasOne(p => p.CreatedByUser).WithMany(p => p.FaultRecommendationsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<ReportRecommendation>().Configure(p => {
                p.SetFriendlyName = "Report Recommendations";
                p.SetName = "ReportRecommendations";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Report Recommendation";
                p.Name = "ReportRecommendation";
                p.Title = "ReportRecommendation";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportRecommendation),
                                    TypeName = "ReportRecommendation",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportRecommendation),
                                    ElementTypeName = "ReportRecommendation",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportRecommendation),
                                    TypeName = "ReportRecommendation",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportRecommendation),
                                    ElementTypeName = "ReportRecommendation",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ReportType>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CategoryId, false, IqlType.Integer).ConfigureProperty(p => p.CategoryId, p => {
                p.Name = "CategoryId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Category Id";
                p.Name = "CategoryId";
                p.Title = "CategoryId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportType>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportType>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportType>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportType>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.Category, false, IqlType.Unknown).ConfigureProperty(p => p.Category, p => {
                p.Name = "Category";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Category";
                p.Name = "Category";
                p.Title = "Category";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineCollectionProperty(p => p.FaultReports, p => p.FaultReportsCount).ConfigureProperty(p => p.FaultReports, p => {
                p.Name = "FaultReports";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Fault Reports";
                p.Name = "FaultReports";
                p.Title = "FaultReports";
            });
            builder.DefineEntityType<ReportType>().HasOne(p => p.Category).WithMany(p => p.ReportTypes).WithConstraint(p => p.CategoryId, p => p.Id);
            builder.DefineEntityType<ReportType>().HasOne(p => p.CreatedByUser).WithMany(p => p.FaultTypesCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<ReportType>().Configure(p => {
                p.SetFriendlyName = "Report Types";
                p.SetName = "ReportTypes";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Report Type";
                p.Name = "ReportType";
                p.Title = "ReportType";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportType),
                                    TypeName = "ReportType",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportType),
                                    ElementTypeName = "ReportType",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportType),
                                    TypeName = "ReportType",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportType),
                                    ElementTypeName = "ReportType",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<Project>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Project>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Title, false, IqlType.String).ConfigureProperty(p => p.Title, p => {
                p.Name = "Title";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Title";
                p.Name = "Title";
                p.Title = "Title";
            }).DefineProperty(p => p.Description, true, IqlType.String).ConfigureProperty(p => p.Description, p => {
                p.Name = "Description";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Description";
                p.Name = "Description";
                p.Title = "Description";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Project>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Project>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Project>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            });
            builder.DefineEntityType<Project>().HasOne(p => p.CreatedByUser).WithMany(p => p.ProjectCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<Project>().Configure(p => {
                p.SetFriendlyName = "Projects";
                p.SetName = "Projects";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Project";
                p.Name = "Project";
                p.Title = "Project";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Project),
                                    TypeName = "Project",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(Project),
                                    ElementTypeName = "Project",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Project),
                                    TypeName = "Project",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(Project),
                                    ElementTypeName = "Project",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ReportReceiverEmailAddress>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.SiteId, false, IqlType.Integer).ConfigureProperty(p => p.SiteId, p => {
                p.Name = "SiteId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Id";
                p.Name = "SiteId";
                p.Title = "SiteId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportReceiverEmailAddress>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.EmailAddress, true, IqlType.String).ConfigureProperty(p => p.EmailAddress, p => {
                p.Name = "EmailAddress";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Email Address";
                p.Name = "EmailAddress";
                p.Title = "EmailAddress";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportReceiverEmailAddress>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportReceiverEmailAddress>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<ReportReceiverEmailAddress>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.Site, false, IqlType.Unknown).ConfigureProperty(p => p.Site, p => {
                p.Name = "Site";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site";
                p.Name = "Site";
                p.Title = "Site";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            });
            builder.DefineEntityType<ReportReceiverEmailAddress>().HasOne(p => p.Site).WithMany(p => p.AdditionalSendReportsTo).WithConstraint(p => p.SiteId, p => p.Id);
            builder.DefineEntityType<ReportReceiverEmailAddress>().HasOne(p => p.CreatedByUser).WithMany(p => p.ReportReceiverEmailAddressesCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<ReportReceiverEmailAddress>().Configure(p => {
                p.SetFriendlyName = "Report Receiver Email Addresses";
                p.SetName = "ReportReceiverEmailAddresses";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Report Receiver Email Address";
                p.Name = "ReportReceiverEmailAddress";
                p.Title = "ReportReceiverEmailAddress";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportReceiverEmailAddress),
                                    TypeName = "ReportReceiverEmailAddress",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportReceiverEmailAddress),
                                    ElementTypeName = "ReportReceiverEmailAddress",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(ReportReceiverEmailAddress),
                                    TypeName = "ReportReceiverEmailAddress",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(ReportReceiverEmailAddress),
                                    ElementTypeName = "ReportReceiverEmailAddress",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<RiskAssessment>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.SiteInspectionId, false, IqlType.Integer).ConfigureProperty(p => p.SiteInspectionId, p => {
                p.Name = "SiteInspectionId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Inspection Id";
                p.Name = "SiteInspectionId";
                p.Title = "SiteInspectionId";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessment>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessment>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessment>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessment>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.SiteInspection, false, IqlType.Unknown).ConfigureProperty(p => p.SiteInspection, p => {
                p.Name = "SiteInspection";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site Inspection";
                p.Name = "SiteInspection";
                p.Title = "SiteInspection";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineProperty(p => p.RiskAssessmentSolution, false, IqlType.Unknown).ConfigureProperty(p => p.RiskAssessmentSolution, p => {
                p.Name = "RiskAssessmentSolution";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Risk Assessment Solution";
                p.Name = "RiskAssessmentSolution";
                p.Title = "RiskAssessmentSolution";
            });
            builder.DefineEntityType<RiskAssessment>().HasOne(p => p.SiteInspection).WithMany(p => p.RiskAssessments).WithConstraint(p => p.SiteInspectionId, p => p.Id);
            builder.DefineEntityType<RiskAssessment>().HasOne(p => p.CreatedByUser).WithMany(p => p.RiskAssessmentsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<RiskAssessment>().Configure(p => {
                p.SetFriendlyName = "Risk Assessments";
                p.SetName = "RiskAssessments";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Risk Assessment";
                p.Name = "RiskAssessment";
                p.Title = "RiskAssessment";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(RiskAssessment),
                                    TypeName = "RiskAssessment",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(RiskAssessment),
                                    ElementTypeName = "RiskAssessment",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(RiskAssessment),
                                    TypeName = "RiskAssessment",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(RiskAssessment),
                                    ElementTypeName = "RiskAssessment",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<RiskAssessmentSolution>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.RiskAssessmentId, false, IqlType.Integer).ConfigureProperty(p => p.RiskAssessmentId, p => {
                p.Name = "RiskAssessmentId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Risk Assessment Id";
                p.Name = "RiskAssessmentId";
                p.Title = "RiskAssessmentId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentSolution>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentSolution>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentSolution>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentSolution>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.RiskAssessment, false, IqlType.Unknown).ConfigureProperty(p => p.RiskAssessment, p => {
                p.Name = "RiskAssessment";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Risk Assessment";
                p.Name = "RiskAssessment";
                p.Title = "RiskAssessment";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            });
            builder.DefineEntityType<RiskAssessmentSolution>().HasOne(p => p.RiskAssessment).WithOne(p => p.RiskAssessmentSolution).WithConstraint(p => p.RiskAssessmentId, p => p.Id);
            builder.DefineEntityType<RiskAssessmentSolution>().HasOne(p => p.CreatedByUser).WithMany(p => p.RiskAssessmentSolutionsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<RiskAssessmentSolution>().Configure(p => {
                p.SetFriendlyName = "Risk Assessment Solutions";
                p.SetName = "RiskAssessmentSolutions";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Risk Assessment Solution";
                p.Name = "RiskAssessmentSolution";
                p.Title = "RiskAssessmentSolution";
            });
            builder.DefineEntityType<RiskAssessmentAnswer>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.QuestionId, false, IqlType.Integer).ConfigureProperty(p => p.QuestionId, p => {
                p.Name = "QuestionId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Question Id";
                p.Name = "QuestionId";
                p.Title = "QuestionId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentAnswer>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.SpecificHazard, true, IqlType.String).ConfigureProperty(p => p.SpecificHazard, p => {
                p.Name = "SpecificHazard";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Specific Hazard";
                p.Name = "SpecificHazard";
                p.Title = "SpecificHazard";
            }).DefineProperty(p => p.PrecautionsToControlHazard, true, IqlType.String).ConfigureProperty(p => p.PrecautionsToControlHazard, p => {
                p.Name = "PrecautionsToControlHazard";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Precautions To Control Hazard";
                p.Name = "PrecautionsToControlHazard";
                p.Title = "PrecautionsToControlHazard";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentAnswer>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentAnswer>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentAnswer>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.Question, false, IqlType.Unknown).ConfigureProperty(p => p.Question, p => {
                p.Name = "Question";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Question";
                p.Name = "Question";
                p.Title = "Question";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            });
            builder.DefineEntityType<RiskAssessmentAnswer>().HasOne(p => p.Question).WithMany(p => p.Answers).WithConstraint(p => p.QuestionId, p => p.Id);
            builder.DefineEntityType<RiskAssessmentAnswer>().HasOne(p => p.CreatedByUser).WithMany(p => p.RiskAssessmentAnswersCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<RiskAssessmentAnswer>().Configure(p => {
                p.SetFriendlyName = "Risk Assessment Answers";
                p.SetName = "RiskAssessmentAnswers";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Risk Assessment Answer";
                p.Name = "RiskAssessmentAnswer";
                p.Title = "RiskAssessmentAnswer";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(RiskAssessmentAnswer),
                                    TypeName = "RiskAssessmentAnswer",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(RiskAssessmentAnswer),
                                    ElementTypeName = "RiskAssessmentAnswer",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(RiskAssessmentAnswer),
                                    TypeName = "RiskAssessmentAnswer",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(RiskAssessmentAnswer),
                                    ElementTypeName = "RiskAssessmentAnswer",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<RiskAssessmentQuestion>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentQuestion>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentQuestion>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentQuestion>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<RiskAssessmentQuestion>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineCollectionProperty(p => p.Answers, p => p.AnswersCount).ConfigureProperty(p => p.Answers, p => {
                p.Name = "Answers";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Answers";
                p.Name = "Answers";
                p.Title = "Answers";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            });
            builder.DefineEntityType<RiskAssessmentQuestion>().HasOne(p => p.CreatedByUser).WithMany(p => p.RiskAssessmentQuestionsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<RiskAssessmentQuestion>().Configure(p => {
                p.SetFriendlyName = "Risk Assessment Questions";
                p.SetName = "RiskAssessmentQuestions";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Risk Assessment Question";
                p.Name = "RiskAssessmentQuestion";
                p.Title = "RiskAssessmentQuestion";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(RiskAssessmentQuestion),
                                    TypeName = "RiskAssessmentQuestion",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(RiskAssessmentQuestion),
                                    ElementTypeName = "RiskAssessmentQuestion",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(RiskAssessmentQuestion),
                                    TypeName = "RiskAssessmentQuestion",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(RiskAssessmentQuestion),
                                    ElementTypeName = "RiskAssessmentQuestion",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<Person>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Location, true, IqlType.GeographyPoint).ConfigureProperty(p => p.Location, p => {
                p.Name = "Location";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNull,
                        CanOverride = true,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentLocationExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentLocation,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Location";
                p.Name = "Location";
                p.Title = "Location";
            }).DefineProperty(p => p.ClientId, true, IqlType.Integer).ConfigureProperty(p => p.ClientId, p => {
                p.Name = "ClientId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Client Id";
                p.Name = "ClientId";
                p.Title = "ClientId";
            }).DefineProperty(p => p.InferredFromUserClientId, true, IqlType.Integer).ConfigureProperty(p => p.InferredFromUserClientId, p => {
                p.Name = "InferredFromUserClientId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.InitializeOnly,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlPropertyExpression
                            {
                                PropertyName = "Id",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "Client",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlCurrentUserExpression
                                    {
                                        CanFail = false,
                                        Kind = IqlExpressionKind.CurrentUser,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Inferred From User Client Id";
                p.Name = "InferredFromUserClientId";
                p.Title = "InferredFromUserClientId";
            }).DefineProperty(p => p.SiteId, true, IqlType.Integer).ConfigureProperty(p => p.SiteId, p => {
                p.Name = "SiteId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Id";
                p.Name = "SiteId";
                p.Title = "SiteId";
            }).DefineProperty(p => p.SiteAreaId, true, IqlType.Integer).ConfigureProperty(p => p.SiteAreaId, p => {
                p.Name = "SiteAreaId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Area Id";
                p.Name = "SiteAreaId";
                p.Title = "SiteAreaId";
            }).DefineProperty(p => p.TypeId, true, IqlType.Integer).ConfigureProperty(p => p.TypeId, p => {
                p.Name = "TypeId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Type Id";
                p.Name = "TypeId";
                p.Title = "TypeId";
            }).DefineProperty(p => p.LoadingId, true, IqlType.Integer).ConfigureProperty(p => p.LoadingId, p => {
                p.Name = "LoadingId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Loading Id";
                p.Name = "LoadingId";
                p.Title = "LoadingId";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.PhotoUrl, true, IqlType.String).ConfigureProperty(p => p.PhotoUrl, p => {
                p.Name = "PhotoUrl";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Photo Url";
                p.Name = "PhotoUrl";
                p.Title = "PhotoUrl";
            }).DefineProperty(p => p.PhotoState, true, IqlType.String).ConfigureProperty(p => p.PhotoState, p => {
                p.Name = "PhotoState";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Photo State";
                p.Name = "PhotoState";
                p.Title = "PhotoState";
            }).DefineProperty(p => p.PhotoRevisionKey, true, IqlType.String).ConfigureProperty(p => p.PhotoRevisionKey, p => {
                p.Name = "PhotoRevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Photo Revision Key";
                p.Name = "PhotoRevisionKey";
                p.Title = "PhotoRevisionKey";
            }).DefineProperty(p => p.Birthday, true, IqlType.Date).ConfigureProperty(p => p.Birthday, p => {
                p.Name = "Birthday";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithConditionIql = new IqlLambdaExpression
                        {
                            Body = new IqlAndExpression
                            {
                                Left = new IqlOrExpression
                                {
                                    Left = new IqlIsEqualToExpression
                                    {
                                        Left = new IqlPropertyExpression
                                        {
                                            PropertyName = "PreviousEntityState",
                                            Kind = IqlExpressionKind.Property,
                                            ReturnType = IqlType.Unknown,
                                            Parent = new IqlRootReferenceExpression
                                            {
                                                EntityTypeName = "InferredValueContext<Person>",
                                                VariableName = "_",
                                                InferredReturnType = IqlType.Unknown,
                                                Kind = IqlExpressionKind.RootReference,
                                                ReturnType = IqlType.Unknown
                                            }
                                        },
                                        Right = new IqlLiteralExpression
                                        {
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.Literal,
                                            ReturnType = IqlType.Unknown
                                        },
                                        Kind = IqlExpressionKind.IsEqualTo,
                                        ReturnType = IqlType.Boolean
                                    },
                                    Right = new IqlIsNotEqualToExpression
                                    {
                                        Left = new IqlPropertyExpression
                                        {
                                            PropertyName = "Category",
                                            Kind = IqlExpressionKind.Property,
                                            ReturnType = IqlType.Unknown,
                                            Parent = new IqlPropertyExpression
                                            {
                                                PropertyName = "PreviousEntityState",
                                                Kind = IqlExpressionKind.Property,
                                                ReturnType = IqlType.Unknown,
                                                Parent = new IqlRootReferenceExpression
                                                {
                                                    EntityTypeName = "InferredValueContext<Person>",
                                                    VariableName = "_",
                                                    InferredReturnType = IqlType.Unknown,
                                                    Kind = IqlExpressionKind.RootReference,
                                                    ReturnType = IqlType.Unknown
                                                }
                                            }
                                        },
                                        Right = new IqlLiteralExpression
                                        {
                                            Value = 1,
                                            InferredReturnType = IqlType.Integer,
                                            Kind = IqlExpressionKind.Literal,
                                            ReturnType = IqlType.Integer
                                        },
                                        Kind = IqlExpressionKind.IsNotEqualTo,
                                        ReturnType = IqlType.Boolean
                                    },
                                    Kind = IqlExpressionKind.Or,
                                    ReturnType = IqlType.Unknown
                                },
                                Right = new IqlIsEqualToExpression
                                {
                                    Left = new IqlPropertyExpression
                                    {
                                        PropertyName = "Category",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlPropertyExpression
                                        {
                                            PropertyName = "CurrentEntityState",
                                            Kind = IqlExpressionKind.Property,
                                            ReturnType = IqlType.Unknown,
                                            Parent = new IqlRootReferenceExpression
                                            {
                                                EntityTypeName = "InferredValueContext<Person>",
                                                VariableName = "_",
                                                InferredReturnType = IqlType.Unknown,
                                                Kind = IqlExpressionKind.RootReference,
                                                ReturnType = IqlType.Unknown
                                            }
                                        }
                                    },
                                    Right = new IqlEnumLiteralExpression
                                    {
                                        Value = new IqlEnumValueExpression[]
                                        {
                                            new IqlEnumValueExpression
                                            {
                                                Name = "",
                                                Value = 2L,
                                                InferredReturnType = IqlType.Integer,
                                                Kind = IqlExpressionKind.EnumValue,
                                                ReturnType = IqlType.EnumValue
                                            }
                                        },
                                        InferredReturnType = IqlType.Collection,
                                        Kind = IqlExpressionKind.EnumLiteral,
                                        ReturnType = IqlType.Enum
                                    },
                                    Kind = IqlExpressionKind.IsEqualTo,
                                    ReturnType = IqlType.Boolean
                                },
                                Kind = IqlExpressionKind.And,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        }
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Birthday";
                p.Name = "Birthday";
                p.Title = "Birthday";
            }).DefineProperty(p => p.Key, true, IqlType.String).ConfigureProperty(p => p.Key, p => {
                p.Name = "Key";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Key";
                p.Name = "Key";
                p.Title = "Key";
            }).DefineProperty(p => p.InferredWhenKeyChanges, true, IqlType.String).ConfigureProperty(p => p.InferredWhenKeyChanges, p => {
                p.Name = "InferredWhenKeyChanges";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlConditionExpression
                            {
                                Test = new IqlAndExpression
                                {
                                    Left = new IqlOrExpression
                                    {
                                        Left = new IqlIsEqualToExpression
                                        {
                                            Left = new IqlPropertyExpression
                                            {
                                                PropertyName = "PreviousEntityState",
                                                Kind = IqlExpressionKind.Property,
                                                ReturnType = IqlType.Unknown,
                                                Parent = new IqlRootReferenceExpression
                                                {
                                                    EntityTypeName = "InferredValueContext<Person>",
                                                    VariableName = "_",
                                                    InferredReturnType = IqlType.Unknown,
                                                    Kind = IqlExpressionKind.RootReference,
                                                    ReturnType = IqlType.Unknown
                                                }
                                            },
                                            Right = new IqlLiteralExpression
                                            {
                                                InferredReturnType = IqlType.Unknown,
                                                Kind = IqlExpressionKind.Literal,
                                                ReturnType = IqlType.Unknown
                                            },
                                            Kind = IqlExpressionKind.IsEqualTo,
                                            ReturnType = IqlType.Boolean
                                        },
                                        Right = new IqlIsEqualToExpression
                                        {
                                            Left = new IqlPropertyExpression
                                            {
                                                PropertyName = "Key",
                                                Kind = IqlExpressionKind.Property,
                                                ReturnType = IqlType.Unknown,
                                                Parent = new IqlPropertyExpression
                                                {
                                                    PropertyName = "PreviousEntityState",
                                                    Kind = IqlExpressionKind.Property,
                                                    ReturnType = IqlType.Unknown,
                                                    Parent = new IqlRootReferenceExpression
                                                    {
                                                        EntityTypeName = "InferredValueContext<Person>",
                                                        VariableName = "_",
                                                        InferredReturnType = IqlType.Unknown,
                                                        Kind = IqlExpressionKind.RootReference,
                                                        ReturnType = IqlType.Unknown
                                                    }
                                                }
                                            },
                                            Right = new IqlLiteralExpression
                                            {
                                                Value = "ABC",
                                                InferredReturnType = IqlType.String,
                                                Kind = IqlExpressionKind.Literal,
                                                ReturnType = IqlType.String
                                            },
                                            Kind = IqlExpressionKind.IsEqualTo,
                                            ReturnType = IqlType.Boolean
                                        },
                                        Kind = IqlExpressionKind.Or,
                                        ReturnType = IqlType.Unknown
                                    },
                                    Right = new IqlIsEqualToExpression
                                    {
                                        Left = new IqlPropertyExpression
                                        {
                                            PropertyName = "Key",
                                            Kind = IqlExpressionKind.Property,
                                            ReturnType = IqlType.Unknown,
                                            Parent = new IqlPropertyExpression
                                            {
                                                PropertyName = "CurrentEntityState",
                                                Kind = IqlExpressionKind.Property,
                                                ReturnType = IqlType.Unknown,
                                                Parent = new IqlRootReferenceExpression
                                                {
                                                    EntityTypeName = "InferredValueContext<Person>",
                                                    VariableName = "_",
                                                    InferredReturnType = IqlType.Unknown,
                                                    Kind = IqlExpressionKind.RootReference,
                                                    ReturnType = IqlType.Unknown
                                                }
                                            }
                                        },
                                        Right = new IqlLiteralExpression
                                        {
                                            Value = "DEF",
                                            InferredReturnType = IqlType.String,
                                            Kind = IqlExpressionKind.Literal,
                                            ReturnType = IqlType.String
                                        },
                                        Kind = IqlExpressionKind.IsEqualTo,
                                        ReturnType = IqlType.Boolean
                                    },
                                    Kind = IqlExpressionKind.And,
                                    ReturnType = IqlType.Unknown
                                },
                                IfTrue = new IqlLiteralExpression
                                {
                                    Value = "alphabet!",
                                    InferredReturnType = IqlType.String,
                                    Kind = IqlExpressionKind.Literal,
                                    ReturnType = IqlType.String
                                },
                                IfFalse = new IqlPropertyExpression
                                {
                                    PropertyName = "InferredWhenKeyChanges",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "CurrentEntityState",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlRootReferenceExpression
                                        {
                                            EntityTypeName = "InferredValueContext<Person>",
                                            VariableName = "_",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.RootReference,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Kind = IqlExpressionKind.Condition,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {
                            "Key"
                        }
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Inferred When Key Changes";
                p.Name = "InferredWhenKeyChanges";
                p.Title = "InferredWhenKeyChanges";
            }).DefineProperty(p => p.IsComplete, false, IqlType.Boolean).ConfigureProperty(p => p.IsComplete, p => {
                p.Name = "IsComplete";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.ForceDecision = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Is Complete";
                p.Name = "IsComplete";
                p.Title = "IsComplete";
            }).DefineProperty(p => p.HasPaid, true, IqlType.Boolean).ConfigureProperty(p => p.HasPaid, p => {
                p.Name = "HasPaid";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Has Paid";
                p.Name = "HasPaid";
                p.Title = "HasPaid";
            }).DefineProperty(p => p.Title, true, IqlType.String).ConfigureProperty(p => p.Title, p => {
                p.Name = "Title";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Title";
                p.Name = "Title";
                p.Title = "Title";
            }).DefineProperty(p => p.Description, true, IqlType.String).ConfigureProperty(p => p.Description, p => {
                p.Name = "Description";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithConditionIql = new IqlLambdaExpression
                        {
                            Body = new IqlIsEqualToExpression
                            {
                                Left = new IqlPropertyExpression
                                {
                                    PropertyName = "Category",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "CurrentEntityState",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlRootReferenceExpression
                                        {
                                            EntityTypeName = "InferredValueContext<Person>",
                                            VariableName = "_",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.RootReference,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Right = new IqlEnumLiteralExpression
                                {
                                    Value = new IqlEnumValueExpression[]
                                    {
                                        new IqlEnumValueExpression
                                        {
                                            Name = "",
                                            Value = 2L,
                                            InferredReturnType = IqlType.Integer,
                                            Kind = IqlExpressionKind.EnumValue,
                                            ReturnType = IqlType.EnumValue
                                        }
                                    },
                                    InferredReturnType = IqlType.Collection,
                                    Kind = IqlExpressionKind.EnumLiteral,
                                    ReturnType = IqlType.Enum
                                },
                                Kind = IqlExpressionKind.IsEqualTo,
                                ReturnType = IqlType.Boolean
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Boolean
                        },
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = "I'm \\ \"auto\"",
                                InferredReturnType = IqlType.String,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.String
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.String
                        }
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Description";
                p.Name = "Description";
                p.Title = "Description";
            }).DefineProperty(p => p.Skills, false, IqlType.Enum).ConfigureProperty(p => p.Skills, p => {
                p.Name = "Skills";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = true,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = 2L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Skills";
                p.Name = "Skills";
                p.Title = "Skills";
                p.Permissions.UseRule("SkillsPermission").UseRule("SkillsPermissionOnlyReadAttemptedOverride");
            }).DefineProperty(p => p.Category, false, IqlType.Enum).ConfigureProperty(p => p.Category, p => {
                p.Name = "Category";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.InitializeOnly,
                        CanOverride = true,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = 1L,
                                InferredReturnType = IqlType.Integer,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Category";
                p.Name = "Category";
                p.Title = "Category";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.Client, true, IqlType.Unknown).ConfigureProperty(p => p.Client, p => {
                p.Name = "Client";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlPropertyExpression
                            {
                                PropertyName = "Client",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "Site",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "CurrentEntityState",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlRootReferenceExpression
                                        {
                                            EntityTypeName = "InferredValueContext<Person>",
                                            VariableName = "_",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.RootReference,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                }
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Person>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Client";
                p.Name = "Client";
                p.Title = "Client";
            }).DefineProperty(p => p.InferredFromUserClient, true, IqlType.Unknown).ConfigureProperty(p => p.InferredFromUserClient, p => {
                p.Name = "InferredFromUserClient";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Inferred From User Client";
                p.Name = "InferredFromUserClient";
                p.Title = "InferredFromUserClient";
            }).DefineProperty(p => p.Site, true, IqlType.Unknown).ConfigureProperty(p => p.Site, p => {
                p.Name = "Site";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site";
                p.Name = "Site";
                p.Title = "Site";
            }).DefineProperty(p => p.SiteArea, true, IqlType.Unknown).ConfigureProperty(p => p.SiteArea, p => {
                p.Name = "SiteArea";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site Area";
                p.Name = "SiteArea";
                p.Title = "SiteArea";
            }).DefineProperty(p => p.Type, true, IqlType.Unknown).ConfigureProperty(p => p.Type, p => {
                p.Name = "Type";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Type";
                p.Name = "Type";
                p.Title = "Type";
            }).DefineProperty(p => p.Loading, true, IqlType.Unknown).ConfigureProperty(p => p.Loading, p => {
                p.Name = "Loading";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Loading";
                p.Name = "Loading";
                p.Title = "Loading";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineCollectionProperty(p => p.Types, p => p.TypesCount).ConfigureProperty(p => p.Types, p => {
                p.Name = "Types";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Types";
                p.Name = "Types";
                p.Title = "Types";
            }).DefineCollectionProperty(p => p.Reports, p => p.ReportsCount).ConfigureProperty(p => p.Reports, p => {
                p.Name = "Reports";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Reports";
                p.Name = "Reports";
                p.Title = "Reports";
            }).DefineEntityValidation(entity => ((((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()))) && (((entity.Description == null ? null : entity.Description.ToUpper()) == null) || ((entity.Description.Trim() == null ? null : entity.Description.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())))), "Please enter either a title or a description", "NoTitleOrDescription").DefineEntityValidation(entity => (((entity.Title == null ? null : entity.Title.ToUpper()) == ("Josh" == null ? null : "Josh".ToUpper())) && ((entity.Description == null ? null : entity.Description.ToUpper()) != ("Josh" == null ? null : "Josh".ToUpper()))), "If the name is 'Josh' please match it in the description", "JoshCheck").DefineDisplayFormatter(entity => entity.Title, "Default").DefineDisplayFormatter(entity => (((entity.Title + " (") + entity.Id) + ")"), "Report").DefineRelationshipFilterRule(p => p.Site, entity => ((entity.Owner.ClientId == 0) ? (Expression<Func<Site, bool>>)(entity2 => true) : entity2 => (entity2.ClientId == entity.Owner.ClientId)), "1", "").DefineRelationshipFilterRule(p => p.Loading, entity => entity2 => ((entity2.Name == null ? null : entity2.Name.ToUpper()) == ("some constant" == null ? null : "some constant".ToUpper())), "2", "").DefinePropertyValidation(p => p.Title, entity => (((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()))), "Please enter a person title", "EmptyTitle").DefinePropertyValidation(p => p.Title, entity => (!((((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())))) && (entity.Title.Trim().Length > 50)), "Please enter less than fifty characters", "TitleMaxLength").DefinePropertyValidation(p => p.Title, entity => (!((((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())))) && (entity.Title.Trim().Length < 3)), "Please enter at least three characters for the person's title", "TitleMinLength").DefinePropertyValidation(p => p.Description, entity => (((entity.Description == null ? null : entity.Description.ToUpper()) == null) || ((entity.Description.Trim() == null ? null : entity.Description.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()))), "Please enter a person description", "EmptyDescription");
            builder.DefineEntityType<Person>().HasOne(p => p.Client).WithMany(p => p.People).WithConstraint(p => p.ClientId, p => p.Id);
            builder.DefineEntityType<Person>().HasOne(p => p.InferredFromUserClient).WithMany(p => p.InferredPeople).WithConstraint(p => p.InferredFromUserClientId, p => p.Id);
            builder.DefineEntityType<Person>().HasOne(p => p.Site).WithMany(p => p.People).WithConstraint(p => p.SiteId, p => p.Id);
            builder.DefineEntityType<Person>().HasOne(p => p.SiteArea).WithMany(p => p.People).WithConstraint(p => p.SiteAreaId, p => p.Id);
            builder.DefineEntityType<Person>().HasOne(p => p.Type).WithMany(p => p.People).WithConstraint(p => p.TypeId, p => p.Id);
            builder.DefineEntityType<Person>().HasOne(p => p.Loading).WithMany(p => p.People).WithConstraint(p => p.LoadingId, p => p.Id);
            builder.DefineEntityType<Person>().HasOne(p => p.CreatedByUser).WithMany(p => p.PeopleCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<Person>().Configure(p => {
                p.PreviewPropertyName = "Photo";
                p.SetFriendlyName = "People";
                p.SetName = "People";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.HasFile(p_f => p_f.PhotoUrl, new Guid("2ac84af2-2090-4bfd-a27e-ea68ad0faccb"), p_f => {
                    p_f.VersionProperty = p.FindProperty("PhotoRevisionKey");
                    p_f.CanWrite = true;
                    p_f.FriendlyName = "Photo";
                    p_f.Name = "Photo";
                    p_f.Title = "Photo";
                    p_f.UrlProperty = p.FindProperty("PhotoUrl");
                    p_f.StateProperty = p.FindProperty("PhotoState");
                    // p_f.Guid = ???;
                });
                p.FriendlyName = "Person";
                p.Name = "Person";
                p.Title = "Person";
                p.Permissions.UseRule("PersonsPermission").UseRule("PersonsPermissionOnlyRead");
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Person),
                                    TypeName = "Person",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(Person),
                                    ElementTypeName = "Person",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Person),
                                    TypeName = "Person",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(Person),
                                    ElementTypeName = "Person",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<PersonInspection>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.SiteInspectionId, false, IqlType.Integer).ConfigureProperty(p => p.SiteInspectionId, p => {
                p.Name = "SiteInspectionId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Inspection Id";
                p.Name = "SiteInspectionId";
                p.Title = "SiteInspectionId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonInspection>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.PersonId, false, IqlType.Integer).ConfigureProperty(p => p.PersonId, p => {
                p.Name = "PersonId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Person Id";
                p.Name = "PersonId";
                p.Title = "PersonId";
            }).DefineProperty(p => p.InspectionStatus, false, IqlType.Enum).ConfigureProperty(p => p.InspectionStatus, p => {
                p.Name = "InspectionStatus";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Inspection Status";
                p.Name = "InspectionStatus";
                p.Title = "InspectionStatus";
            }).DefineProperty(p => p.StartTime, false, IqlType.Date).ConfigureProperty(p => p.StartTime, p => {
                p.Name = "StartTime";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Start Time";
                p.Name = "StartTime";
                p.Title = "StartTime";
            }).DefineProperty(p => p.EndTime, false, IqlType.Date).ConfigureProperty(p => p.EndTime, p => {
                p.Name = "EndTime";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "End Time";
                p.Name = "EndTime";
                p.Title = "EndTime";
            }).DefineProperty(p => p.ReasonForFailure, false, IqlType.Enum).ConfigureProperty(p => p.ReasonForFailure, p => {
                p.Name = "ReasonForFailure";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Reason For Failure";
                p.Name = "ReasonForFailure";
                p.Title = "ReasonForFailure";
            }).DefineProperty(p => p.IsDesignRequired, false, IqlType.Boolean).ConfigureProperty(p => p.IsDesignRequired, p => {
                p.Name = "IsDesignRequired";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Is Design Required";
                p.Name = "IsDesignRequired";
                p.Title = "IsDesignRequired";
            }).DefineProperty(p => p.DrawingNumber, true, IqlType.String).ConfigureProperty(p => p.DrawingNumber, p => {
                p.Name = "DrawingNumber";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Drawing Number";
                p.Name = "DrawingNumber";
                p.Title = "DrawingNumber";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonInspection>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonInspection>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonInspection>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.SiteInspection, false, IqlType.Unknown).ConfigureProperty(p => p.SiteInspection, p => {
                p.Name = "SiteInspection";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site Inspection";
                p.Name = "SiteInspection";
                p.Title = "SiteInspection";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            });
            builder.DefineEntityType<PersonInspection>().HasOne(p => p.SiteInspection).WithMany(p => p.PersonInspections).WithConstraint(p => p.SiteInspectionId, p => p.Id);
            builder.DefineEntityType<PersonInspection>().HasOne(p => p.CreatedByUser).WithMany(p => p.PersonInspectionsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<PersonInspection>().Configure(p => {
                p.SetFriendlyName = "Person Inspections";
                p.SetName = "PersonInspections";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Person Inspection";
                p.Name = "PersonInspection";
                p.Title = "PersonInspection";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonInspection),
                                    TypeName = "PersonInspection",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonInspection),
                                    ElementTypeName = "PersonInspection",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonInspection),
                                    TypeName = "PersonInspection",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonInspection),
                                    ElementTypeName = "PersonInspection",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<PersonLoading>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonLoading>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonLoading>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonLoading>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonLoading>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineCollectionProperty(p => p.People, p => p.PeopleCount).ConfigureProperty(p => p.People, p => {
                p.Name = "People";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "People";
                p.Name = "People";
                p.Title = "People";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefinePropertyValidation(p => p.Name, entity => (((entity.Name == null ? null : entity.Name.ToUpper()) != null) && ((entity.Name == null ? null : entity.Name.ToUpper()) != ("" == null ? null : "".ToUpper()))), "Please enter a loading name", "3");
            builder.DefineEntityType<PersonLoading>().HasOne(p => p.CreatedByUser).WithMany(p => p.PersonLoadingsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<PersonLoading>().Configure(p => {
                p.SetFriendlyName = "Person Loadings";
                p.SetName = "PersonLoadings";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Person Loading";
                p.Name = "PersonLoading";
                p.Title = "PersonLoading";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonLoading),
                                    TypeName = "PersonLoading",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonLoading),
                                    ElementTypeName = "PersonLoading",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonLoading),
                                    TypeName = "PersonLoading",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonLoading),
                                    ElementTypeName = "PersonLoading",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<PersonType>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonType>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Title, false, IqlType.String).ConfigureProperty(p => p.Title, p => {
                p.Name = "Title";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Title";
                p.Name = "Title";
                p.Title = "Title";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonType>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonType>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonType>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineCollectionProperty(p => p.People, p => p.PeopleCount).ConfigureProperty(p => p.People, p => {
                p.Name = "People";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "People";
                p.Name = "People";
                p.Title = "People";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineCollectionProperty(p => p.PeopleMap, p => p.PeopleMapCount).ConfigureProperty(p => p.PeopleMap, p => {
                p.Name = "PeopleMap";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "People Map";
                p.Name = "PeopleMap";
                p.Title = "PeopleMap";
            });
            builder.DefineEntityType<PersonType>().HasOne(p => p.CreatedByUser).WithMany(p => p.PersonTypesCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<PersonType>().Configure(p => {
                p.SetFriendlyName = "Person Types";
                p.SetName = "PersonTypes";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Person Type";
                p.Name = "PersonType";
                p.Title = "PersonType";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonType),
                                    TypeName = "PersonType",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonType),
                                    ElementTypeName = "PersonType",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonType),
                                    TypeName = "PersonType",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonType),
                                    ElementTypeName = "PersonType",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<PersonTypeMap>().HasCompositeKey(true, p => p.PersonId, p => p.TypeId).DefineProperty(p => p.PersonId, false, IqlType.Integer).ConfigureProperty(p => p.PersonId, p => {
                p.Name = "PersonId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.SimpleCollection | IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Person Id";
                p.Name = "PersonId";
                p.Title = "PersonId";
            }).DefineProperty(p => p.TypeId, false, IqlType.Integer).ConfigureProperty(p => p.TypeId, p => {
                p.Name = "TypeId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.SimpleCollection | IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Type Id";
                p.Name = "TypeId";
                p.Title = "TypeId";
            }).DefineProperty(p => p.Notes, true, IqlType.String).ConfigureProperty(p => p.Notes, p => {
                p.Name = "Notes";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Notes";
                p.Name = "Notes";
                p.Title = "Notes";
            }).DefineProperty(p => p.Description, true, IqlType.String).ConfigureProperty(p => p.Description, p => {
                p.Name = "Description";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Description";
                p.Name = "Description";
                p.Title = "Description";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonTypeMap>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonTypeMap>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.Person, false, IqlType.Unknown).ConfigureProperty(p => p.Person, p => {
                p.Name = "Person";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Person";
                p.Name = "Person";
                p.Title = "Person";
            }).DefineProperty(p => p.Type, false, IqlType.Unknown).ConfigureProperty(p => p.Type, p => {
                p.Name = "Type";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Type";
                p.Name = "Type";
                p.Title = "Type";
            });
            builder.DefineEntityType<PersonTypeMap>().HasOne(p => p.Person).WithMany(p => p.Types).WithConstraint(p => p.PersonId, p => p.Id);
            builder.DefineEntityType<PersonTypeMap>().HasOne(p => p.Type).WithMany(p => p.PeopleMap).WithConstraint(p => p.TypeId, p => p.Id);
            builder.DefineEntityType<PersonTypeMap>().Configure(p => {
                p.SetFriendlyName = "Person Types Map";
                p.SetName = "PersonTypesMap";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.FriendlyName = "Person Type Map";
                p.Name = "PersonTypeMap";
                p.Title = "PersonTypeMap";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonTypeMap),
                                    TypeName = "PersonTypeMap",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonTypeMap),
                                    ElementTypeName = "PersonTypeMap",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonTypeMap),
                                    TypeName = "PersonTypeMap",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonTypeMap),
                                    ElementTypeName = "PersonTypeMap",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<PersonReport>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.PersonId, false, IqlType.Integer).ConfigureProperty(p => p.PersonId, p => {
                p.Name = "PersonId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Person Id";
                p.Name = "PersonId";
                p.Title = "PersonId";
            }).DefineProperty(p => p.TypeId, false, IqlType.Integer).ConfigureProperty(p => p.TypeId, p => {
                p.Name = "TypeId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Type Id";
                p.Name = "TypeId";
                p.Title = "TypeId";
            }).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonReport>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Title, true, IqlType.String).ConfigureProperty(p => p.Title, p => {
                p.Name = "Title";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Title";
                p.Name = "Title";
                p.Title = "Title";
            }).DefineProperty(p => p.Status, false, IqlType.Enum).ConfigureProperty(p => p.Status, p => {
                p.Name = "Status";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Status";
                p.Name = "Status";
                p.Title = "Status";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonReport>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonReport>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<PersonReport>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineCollectionProperty(p => p.ActionsTaken, p => p.ActionsTakenCount).ConfigureProperty(p => p.ActionsTaken, p => {
                p.Name = "ActionsTaken";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Actions Taken";
                p.Name = "ActionsTaken";
                p.Title = "ActionsTaken";
            }).DefineCollectionProperty(p => p.Recommendations, p => p.RecommendationsCount).ConfigureProperty(p => p.Recommendations, p => {
                p.Name = "Recommendations";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Recommendations";
                p.Name = "Recommendations";
                p.Title = "Recommendations";
            }).DefineProperty(p => p.Person, false, IqlType.Unknown).ConfigureProperty(p => p.Person, p => {
                p.Name = "Person";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Person";
                p.Name = "Person";
                p.Title = "Person";
            }).DefineProperty(p => p.Type, false, IqlType.Unknown).ConfigureProperty(p => p.Type, p => {
                p.Name = "Type";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Type";
                p.Name = "Type";
                p.Title = "Type";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefinePropertyValidation(p => p.Title, entity => (((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper()))), "Please enter a valid report title", "4").DefinePropertyValidation(p => p.Title, entity => (!((((entity.Title == null ? null : entity.Title.ToUpper()) == null) || ((entity.Title.Trim() == null ? null : entity.Title.Trim().ToUpper()) == ("" == null ? null : "".ToUpper())))) && (entity.Title.Trim().Length > 5)), "Please enter less than five characters", "5");
            builder.DefineEntityType<PersonReport>().HasOne(p => p.Person).WithMany(p => p.Reports).WithConstraint(p => p.PersonId, p => p.Id);
            builder.DefineEntityType<PersonReport>().HasOne(p => p.Type).WithMany(p => p.FaultReports).WithConstraint(p => p.TypeId, p => p.Id);
            builder.DefineEntityType<PersonReport>().HasOne(p => p.CreatedByUser).WithMany(p => p.FaultReportsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<PersonReport>().Configure(p => {
                p.SetFriendlyName = "Person Reports";
                p.SetName = "PersonReports";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Person Report";
                p.Name = "PersonReport";
                p.Title = "PersonReport";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonReport),
                                    TypeName = "PersonReport",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonReport),
                                    ElementTypeName = "PersonReport",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(PersonReport),
                                    TypeName = "PersonReport",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(PersonReport),
                                    ElementTypeName = "PersonReport",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<Site>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.Location, true, IqlType.GeographyPoint).ConfigureProperty(p => p.Location, p => {
                p.Name = "Location";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Location";
                p.Name = "Location";
                p.Title = "Location";
            }).DefineProperty(p => p.Area, true, IqlType.GeographyPolygon).ConfigureProperty(p => p.Area, p => {
                p.Name = "Area";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Area";
                p.Name = "Area";
                p.Title = "Area";
            }).DefineProperty(p => p.Line, true, IqlType.GeographyLine).ConfigureProperty(p => p.Line, p => {
                p.Name = "Line";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Line";
                p.Name = "Line";
                p.Title = "Line";
            }).DefineProperty(p => p.ParentId, true, IqlType.Integer).ConfigureProperty(p => p.ParentId, p => {
                p.Name = "ParentId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Parent Id";
                p.Name = "ParentId";
                p.Title = "ParentId";
            }).DefineProperty(p => p.ClientId, true, IqlType.Integer).ConfigureProperty(p => p.ClientId, p => {
                p.Name = "ClientId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Client Id";
                p.Name = "ClientId";
                p.Title = "ClientId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Site>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.InferredChainFromSelf, true, IqlType.String).ConfigureProperty(p => p.InferredChainFromSelf, p => {
                p.Name = "InferredChainFromSelf";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlPropertyExpression
                            {
                                PropertyName = "InferredChainFromUserName",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "CurrentEntityState",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "InferredValueContext<Site>",
                                        VariableName = "site",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Site>",
                                    VariableName = "site",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Inferred Chain From Self";
                p.Name = "InferredChainFromSelf";
                p.Title = "InferredChainFromSelf";
            }).DefineProperty(p => p.InferredChainFromUserName, true, IqlType.String).ConfigureProperty(p => p.InferredChainFromUserName, p => {
                p.Name = "InferredChainFromUserName";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlPropertyExpression
                            {
                                PropertyName = "UserName",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlCurrentUserExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUser,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Site>",
                                    VariableName = "site",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Inferred Chain From User Name";
                p.Name = "InferredChainFromUserName";
                p.Title = "InferredChainFromUserName";
            }).DefineProperty(p => p.FullAddress, true, IqlType.String).ConfigureProperty(p => p.FullAddress, p => {
                p.Name = "FullAddress";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlAddExpression
                            {
                                Left = new IqlAddExpression
                                {
                                    Left = new IqlPropertyExpression
                                    {
                                        PropertyName = "Address",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlPropertyExpression
                                        {
                                            PropertyName = "CurrentEntityState",
                                            Kind = IqlExpressionKind.Property,
                                            ReturnType = IqlType.Unknown,
                                            Parent = new IqlRootReferenceExpression
                                            {
                                                EntityTypeName = "InferredValueContext<Site>",
                                                VariableName = "site",
                                                InferredReturnType = IqlType.Unknown,
                                                Kind = IqlExpressionKind.RootReference,
                                                ReturnType = IqlType.Unknown
                                            }
                                        }
                                    },
                                    Right = new IqlLiteralExpression
                                    {
                                        Value = "\n",
                                        InferredReturnType = IqlType.String,
                                        Kind = IqlExpressionKind.Literal,
                                        ReturnType = IqlType.String
                                    },
                                    Kind = IqlExpressionKind.Add,
                                    ReturnType = IqlType.Unknown
                                },
                                Right = new IqlPropertyExpression
                                {
                                    PropertyName = "PostCode",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "CurrentEntityState",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlRootReferenceExpression
                                        {
                                            EntityTypeName = "InferredValueContext<Site>",
                                            VariableName = "site",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.RootReference,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Kind = IqlExpressionKind.Add,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Site>",
                                    VariableName = "site",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Full Address";
                p.Name = "FullAddress";
                p.Title = "FullAddress";
            }).DefineProperty(p => p.Address, true, IqlType.String).ConfigureProperty(p => p.Address, p => {
                p.Name = "Address";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Address";
                p.Name = "Address";
                p.Title = "Address";
            }).DefineProperty(p => p.PostCode, true, IqlType.String).ConfigureProperty(p => p.PostCode, p => {
                p.Name = "PostCode";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Post Code";
                p.Name = "PostCode";
                p.Title = "PostCode";
            }).DefineProperty(p => p.Key, true, IqlType.String).ConfigureProperty(p => p.Key, p => {
                p.Name = "Key";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlPropertyExpression
                            {
                                PropertyName = "ClientId",
                                Kind = IqlExpressionKind.Property,
                                ReturnType = IqlType.Unknown,
                                Parent = new IqlPropertyExpression
                                {
                                    PropertyName = "CurrentEntityState",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "InferredValueContext<Site>",
                                        VariableName = "site",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                }
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Site>",
                                    VariableName = "site",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Key";
                p.Name = "Key";
                p.Title = "Key";
            }).DefineProperty(p => p.Name, true, IqlType.String).ConfigureProperty(p => p.Name, p => {
                p.Name = "Name";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Name";
                p.Name = "Name";
                p.Title = "Name";
            }).DefineProperty(p => p.LeftOf, true, IqlType.Integer).ConfigureProperty(p => p.LeftOf, p => {
                p.Name = "LeftOf";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Left Of";
                p.Name = "LeftOf";
                p.Title = "LeftOf";
            }).DefineProperty(p => p.RightOf, true, IqlType.Integer).ConfigureProperty(p => p.RightOf, p => {
                p.Name = "RightOf";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Right Of";
                p.Name = "RightOf";
                p.Title = "RightOf";
            }).DefineProperty(p => p.Level, false, IqlType.Integer).ConfigureProperty(p => p.Level, p => {
                p.Name = "Level";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Level";
                p.Name = "Level";
                p.Title = "Level";
            }).DefineProperty(p => p.Left, false, IqlType.Integer).ConfigureProperty(p => p.Left, p => {
                p.Name = "Left";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Left";
                p.Name = "Left";
                p.Title = "Left";
            }).DefineProperty(p => p.Right, false, IqlType.Integer).ConfigureProperty(p => p.Right, p => {
                p.Name = "Right";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Right";
                p.Name = "Right";
                p.Title = "Right";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Site>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Site>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<Site>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineCollectionProperty(p => p.Documents, p => p.DocumentsCount).ConfigureProperty(p => p.Documents, p => {
                p.Name = "Documents";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Documents";
                p.Name = "Documents";
                p.Title = "Documents";
            }).DefineCollectionProperty(p => p.AdditionalSendReportsTo, p => p.AdditionalSendReportsToCount).ConfigureProperty(p => p.AdditionalSendReportsTo, p => {
                p.Name = "AdditionalSendReportsTo";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Additional Send Reports To";
                p.Name = "AdditionalSendReportsTo";
                p.Title = "AdditionalSendReportsTo";
            }).DefineCollectionProperty(p => p.People, p => p.PeopleCount).ConfigureProperty(p => p.People, p => {
                p.Name = "People";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "People";
                p.Name = "People";
                p.Title = "People";
            }).DefineProperty(p => p.Parent, true, IqlType.Unknown).ConfigureProperty(p => p.Parent, p => {
                p.Name = "Parent";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Parent";
                p.Name = "Parent";
                p.Title = "Parent";
            }).DefineCollectionProperty(p => p.Children, p => p.ChildrenCount).ConfigureProperty(p => p.Children, p => {
                p.Name = "Children";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Children";
                p.Name = "Children";
                p.Title = "Children";
            }).DefineProperty(p => p.Client, true, IqlType.Unknown).ConfigureProperty(p => p.Client, p => {
                p.Name = "Client";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Client";
                p.Name = "Client";
                p.Title = "Client";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineCollectionProperty(p => p.Areas, p => p.AreasCount).ConfigureProperty(p => p.Areas, p => {
                p.Name = "Areas";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Areas";
                p.Name = "Areas";
                p.Title = "Areas";
            }).DefineCollectionProperty(p => p.SiteInspections, p => p.SiteInspectionsCount).ConfigureProperty(p => p.SiteInspections, p => {
                p.Name = "SiteInspections";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site Inspections";
                p.Name = "SiteInspections";
                p.Title = "SiteInspections";
            }).DefineCollectionProperty(p => p.Users, p => p.UsersCount).ConfigureProperty(p => p.Users, p => {
                p.Name = "Users";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Users";
                p.Name = "Users";
                p.Title = "Users";
            }).DefinePropertyDisplayRuleByExpression(p => p.Id, entity => (entity.ClientId != 0), "8", "", DisplayRuleKind.DisplayIf, DisplayRuleAppliesToKind.NewAndEdit).DefinePropertyValidation(p => p.FullAddress, entity => (((entity.FullAddress == null ? null : entity.FullAddress.ToUpper()) == null) || ((entity.FullAddress == null ? null : entity.FullAddress.ToUpper()) == ("" == null ? null : "".ToUpper()))), "", "9");
            builder.DefineEntityType<Site>().HasOne(p => p.Parent).WithMany(p => p.Children).WithConstraint(p => p.ParentId, p => p.Id);
            builder.DefineEntityType<Site>().HasOne(p => p.Client).WithMany(p => p.Sites).WithConstraint(p => p.ClientId, p => p.Id);
            builder.DefineEntityType<Site>().HasOne(p => p.CreatedByUser).WithMany(p => p.SitesCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<Site>().Configure(p => {
                p.HasNestedSet(p_ns => p_ns.Left, p_ns => p_ns.Right, p_ns => p_ns.LeftOf, p_ns => p_ns.RightOf, p_ns => p_ns.Key, p_ns => p_ns.Level, p_ns => p_ns.ParentId, p_ns => p_ns.Parent, p_ns => p_ns.Id, "", "", p_ns => {
                    p_ns.CanWrite = true;
                    p_ns.Kind = IqlPropertyKind.SimpleCollection;
                    p_ns.FriendlyName = "Hierarchy";
                    p_ns.Name = "Hierarchy";
                    p_ns.Title = "Hierarchy";
                });
                p.SetFriendlyName = "Sites";
                p.SetName = "Sites";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Site";
                p.Name = "Site";
                p.Title = "Site";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Site),
                                    TypeName = "Site",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(Site),
                                    ElementTypeName = "Site",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(Site),
                                    TypeName = "Site",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(Site),
                                    ElementTypeName = "Site",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<SiteArea>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.SiteId, false, IqlType.Integer).ConfigureProperty(p => p.SiteId, p => {
                p.Name = "SiteId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Id";
                p.Name = "SiteId";
                p.Title = "SiteId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteArea>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteArea>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteArea>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteArea>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineCollectionProperty(p => p.People, p => p.PeopleCount).ConfigureProperty(p => p.People, p => {
                p.Name = "People";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "People";
                p.Name = "People";
                p.Title = "People";
            }).DefineProperty(p => p.Site, false, IqlType.Unknown).ConfigureProperty(p => p.Site, p => {
                p.Name = "Site";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site";
                p.Name = "Site";
                p.Title = "Site";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineDisplayFormatter(entity => (((entity.Site == null) ? "no site" : entity.Site.Name) + ((entity.CreatedByUser == null) ? "" : (" - " + entity.CreatedByUser.FullName))), "Default");
            builder.DefineEntityType<SiteArea>().HasOne(p => p.Site).WithMany(p => p.Areas).WithConstraint(p => p.SiteId, p => p.Id);
            builder.DefineEntityType<SiteArea>().HasOne(p => p.CreatedByUser).WithMany(p => p.SiteAreasCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<SiteArea>().Configure(p => {
                p.SetFriendlyName = "Site Areas";
                p.SetName = "SiteAreas";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Site Area";
                p.Name = "SiteArea";
                p.Title = "SiteArea";
            });
            builder.DefineEntityType<SiteInspection>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.Id, false, IqlType.Integer).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.SiteId, false, IqlType.Integer).ConfigureProperty(p => p.SiteId, p => {
                p.Name = "SiteId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Id";
                p.Name = "SiteId";
                p.Title = "SiteId";
            }).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteInspection>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.StartTime, false, IqlType.Date).ConfigureProperty(p => p.StartTime, p => {
                p.Name = "StartTime";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Start Time";
                p.Name = "StartTime";
                p.Title = "StartTime";
            }).DefineProperty(p => p.EndTime, false, IqlType.Date).ConfigureProperty(p => p.EndTime, p => {
                p.Name = "EndTime";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "End Time";
                p.Name = "EndTime";
                p.Title = "EndTime";
            }).DefineConvertedProperty(p => p.Guid, "Guid", false, IqlType.String).ConfigureProperty(p => p.Guid, p => {
                p.Name = "Guid";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteInspection>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Guid";
                p.Name = "Guid";
                p.Title = "Guid";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteInspection>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<SiteInspection>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineCollectionProperty(p => p.RiskAssessments, p => p.RiskAssessmentsCount).ConfigureProperty(p => p.RiskAssessments, p => {
                p.Name = "RiskAssessments";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Risk Assessments";
                p.Name = "RiskAssessments";
                p.Title = "RiskAssessments";
            }).DefineCollectionProperty(p => p.PersonInspections, p => p.PersonInspectionsCount).ConfigureProperty(p => p.PersonInspections, p => {
                p.Name = "PersonInspections";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Person Inspections";
                p.Name = "PersonInspections";
                p.Title = "PersonInspections";
            }).DefineProperty(p => p.Site, false, IqlType.Unknown).ConfigureProperty(p => p.Site, p => {
                p.Name = "Site";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site";
                p.Name = "Site";
                p.Title = "Site";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineDisplayFormatter(entity => (((((entity.Site.Name + " - ") + entity.EndTime) + " (") + ((entity.CreatedByUser == null) ? "no creator" : entity.CreatedByUser.FullName)) + ")"), "Default");
            builder.DefineEntityType<SiteInspection>().HasOne(p => p.Site).WithMany(p => p.SiteInspections).WithConstraint(p => p.SiteId, p => p.Id);
            builder.DefineEntityType<SiteInspection>().HasOne(p => p.CreatedByUser).WithMany(p => p.SiteInspectionsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<SiteInspection>().Configure(p => {
                p.SetFriendlyName = "Site Inspections";
                p.SetName = "SiteInspections";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "Site Inspection";
                p.Name = "SiteInspection";
                p.Title = "SiteInspection";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(SiteInspection),
                                    TypeName = "SiteInspection",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(SiteInspection),
                                    ElementTypeName = "SiteInspection",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(SiteInspection),
                                    TypeName = "SiteInspection",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(SiteInspection),
                                    ElementTypeName = "SiteInspection",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<UserSetting>().HasKey(p => p.Id, IqlType.Unknown, false).DefineProperty(p => p.CreatedByUserId, true, IqlType.String).ConfigureProperty(p => p.CreatedByUserId, p => {
                p.Name = "CreatedByUserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<UserSetting>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Created By User Id";
                p.Name = "CreatedByUserId";
                p.Title = "CreatedByUserId";
            }).DefineProperty(p => p.Key1, false, IqlType.String).ConfigureProperty(p => p.Key1, p => {
                p.Name = "Key1";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Key 1";
                p.Name = "Key1";
                p.Title = "Key1";
            }).DefineConvertedProperty(p => p.Id, "Guid", false, IqlType.String).ConfigureProperty(p => p.Id, p => {
                p.Name = "Id";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Key;
                p.FriendlyName = "Id";
                p.Name = "Id";
                p.Title = "Id";
            }).DefineProperty(p => p.UserId, true, IqlType.String).ConfigureProperty(p => p.UserId, p => {
                p.Name = "UserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.Always,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlCurrentUserIdExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.CurrentUserId,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<UserSetting>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "User Id";
                p.Name = "UserId";
                p.Title = "UserId";
            }).DefineProperty(p => p.Key2, true, IqlType.String).ConfigureProperty(p => p.Key2, p => {
                p.Name = "Key2";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Key 2";
                p.Name = "Key2";
                p.Title = "Key2";
            }).DefineProperty(p => p.Key3, true, IqlType.String).ConfigureProperty(p => p.Key3, p => {
                p.Name = "Key3";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Key 3";
                p.Name = "Key3";
                p.Title = "Key3";
            }).DefineProperty(p => p.Key4, true, IqlType.String).ConfigureProperty(p => p.Key4, p => {
                p.Name = "Key4";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Key 4";
                p.Name = "Key4";
                p.Title = "Key4";
            }).DefineProperty(p => p.Value, true, IqlType.String).ConfigureProperty(p => p.Value, p => {
                p.Name = "Value";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Value";
                p.Name = "Value";
                p.Title = "Value";
            }).DefineProperty(p => p.CreatedDate, false, IqlType.Date).ConfigureProperty(p => p.CreatedDate, p => {
                p.Name = "CreatedDate";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = true,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNowExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.Now,
                                    ReturnType = IqlType.Date
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<UserSetting>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Created Date";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedDate"
                });
                p.Name = "CreatedDate";
                p.Title = "CreatedDate";
            }).DefineProperty(p => p.RevisionKey, true, IqlType.String).ConfigureProperty(p => p.RevisionKey, p => {
                p.Name = "RevisionKey";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Revision Key";
                p.Hints = new List<string>(new[]
                {
                    "Iql:Version"
                });
                p.Name = "RevisionKey";
                p.Title = "RevisionKey";
            }).DefineConvertedProperty(p => p.PersistenceKey, "Guid", false, IqlType.String).ConfigureProperty(p => p.PersistenceKey, p => {
                p.Name = "PersistenceKey";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>
                {
                    new InferredValueConfiguration
                    {
                        Kind = InferredValueKind.IfNullOrEmpty,
                        CanOverride = false,
                        ForNewOnly = false,
                        InferredWithIql = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = new IqlNewGuidExpression
                                {
                                    CanFail = false,
                                    Kind = IqlExpressionKind.NewGuid,
                                    ReturnType = IqlType.Unknown
                                },
                                InferredReturnType = IqlType.Unknown,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "InferredValueContext<UserSetting>",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        OnPropertyChanges = new string[]
                        {}
                    }
                };
                p.CanWrite = false;
                p.ReadKind = IqlPropertyReadKind.Hidden;
                p.EditKind = IqlPropertyEditKind.Hidden;
                p.Kind = IqlPropertyKind.Primitive;
                p.FriendlyName = "Persistence Key";
                p.Name = "PersistenceKey";
                p.Title = "PersistenceKey";
            }).DefineProperty(p => p.CreatedByUser, true, IqlType.Unknown).ConfigureProperty(p => p.CreatedByUser, p => {
                p.Name = "CreatedByUser";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = false;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Created By User";
                p.Hints = new List<string>(new[]
                {
                    "IsKnownProperty:CreatedByUser"
                });
                p.Name = "CreatedByUser";
                p.Title = "CreatedByUser";
            }).DefineProperty(p => p.User, false, IqlType.Unknown).ConfigureProperty(p => p.User, p => {
                p.Name = "User";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "User";
                p.Name = "User";
                p.Title = "User";
            });
            builder.DefineEntityType<UserSetting>().HasOne(p => p.CreatedByUser).WithMany(p => p.UserSettingsCreated).WithConstraint(p => p.CreatedByUserId, p => p.Id);
            builder.DefineEntityType<UserSetting>().HasOne(p => p.User).WithMany(p => p.UserSettings).WithConstraint(p => p.UserId, p => p.Id);
            builder.DefineEntityType<UserSetting>().Configure(p => {
                p.SetFriendlyName = "User Settings";
                p.SetName = "UserSettings";
                p.DefaultBrowseSortExpression = "CreatedDate";
                p.DefaultBrowseSortDescending = true;
                p.PersistenceKeyProperty = p.FindProperty("PersistenceKey");
                p.FriendlyName = "User Setting";
                p.Name = "UserSetting";
                p.Title = "UserSetting";
            });
            builder.DefineEntityType<UserSite>().HasCompositeKey(false, p => p.SiteId, p => p.UserId).DefineProperty(p => p.SiteId, false, IqlType.Integer).ConfigureProperty(p => p.SiteId, p => {
                p.Name = "SiteId";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.SimpleCollection | IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "Site Id";
                p.Name = "SiteId";
                p.Title = "SiteId";
            }).DefineProperty(p => p.UserId, true, IqlType.String).ConfigureProperty(p => p.UserId, p => {
                p.Name = "UserId";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.SimpleCollection | IqlPropertyKind.RelationshipKey;
                p.FriendlyName = "User Id";
                p.Name = "UserId";
                p.Title = "UserId";
            }).DefineProperty(p => p.User, false, IqlType.Unknown).ConfigureProperty(p => p.User, p => {
                p.Name = "User";
                p.Nullable = true;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "User";
                p.Name = "User";
                p.Title = "User";
            }).DefineProperty(p => p.Site, false, IqlType.Unknown).ConfigureProperty(p => p.Site, p => {
                p.Name = "Site";
                p.Nullable = false;
                p.InferredValueConfigurations = new List<IInferredValueConfiguration>();
                p.CanWrite = true;
                p.Kind = IqlPropertyKind.Relationship;
                p.FriendlyName = "Site";
                p.Name = "Site";
                p.Title = "Site";
            });
            builder.DefineEntityType<UserSite>().HasOne(p => p.User).WithMany(p => p.Sites).WithConstraint(p => p.UserId, p => p.Id);
            builder.DefineEntityType<UserSite>().HasOne(p => p.Site).WithMany(p => p.Users).WithConstraint(p => p.SiteId, p => p.Id);
            builder.DefineEntityType<UserSite>().Configure(p => {
                p.SetFriendlyName = "User Sites";
                p.SetName = "UserSites";
                p.FriendlyName = "User Site";
                p.Name = "UserSite";
                p.Title = "UserSite";
                p.Methods = new List<IqlMethod>
                {
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(UserSite),
                                    TypeName = "UserSite",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(UserSite),
                                    ElementTypeName = "UserSite",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "Name",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        Metadata = new MetadataCollection(),
                        Name = "IncrementVersion",
                        Title = "IncrementVersion",
                        FriendlyName = "Increment Version",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    },
                    new IqlMethod
                    {
                        Ordering = 0,
                        SupportsOffline = false,
                        DataStoreRequired = "ODataDataStore",
                        IsPublic = false,
                        Parameters = new List<IqlMethodParameter>
                        {
                            new IqlMethodParameter
                            {
                                Name = "bindingParameter",
                                IsBindingParameter = true,
                                Type = new TypeDetail
                                {
                                    Type = typeof(UserSite),
                                    TypeName = "UserSite",
                                    Nullable = true,
                                    Kind = IqlType.Unknown,
                                    ElementType = typeof(UserSite),
                                    ElementTypeName = "UserSite",
                                    IsCollection = false
                                }
                            },
                            new IqlMethodParameter
                            {
                                Name = "property",
                                IsBindingParameter = false,
                                Type = new TypeDetail
                                {
                                    Type = typeof(String),
                                    TypeName = "string",
                                    Nullable = true,
                                    Kind = IqlType.String,
                                    ElementType = typeof(String),
                                    ElementTypeName = "string",
                                    IsCollection = false
                                }
                            }
                        },
                        ScopeKind = IqlMethodScopeKind.Entity,
                        ReturnType = typeof(String),
                        ReturnTypeName = "string",
                        Metadata = new MetadataCollection(),
                        Name = "GetMediaUploadUrl",
                        Title = "GetMediaUploadUrl",
                        FriendlyName = "Get Media Upload Url",
                        GroupOrder = 0F,
                        Hints = new List<String>(),
                        HelpTexts = new List<HelpText>(),
                        Permissions = new UserPermissionsCollection
                        {
                            Keys = new List<String>()
                        }
                    }
                };
            });
            builder.DefineEntityType<ApplicationUser>("ApplicationUser").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.Client).Configure(rel_cnf => {
                    rel_cnf.ValueMappings.Add(new ValueMapping(rel_cnf)
                    {
                        Property = rel_cnf.OtherSide.EntityConfiguration.FindProperty("AverageIncome"),
                        Expression = new IqlLambdaExpression
                        {
                            Body = new IqlLiteralExpression
                            {
                                Value = 12F,
                                InferredReturnType = IqlType.Decimal,
                                Kind = IqlExpressionKind.Literal,
                                ReturnType = IqlType.Decimal
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "ApplicationUser",
                                    VariableName = "_",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Decimal
                        },
                        UseForFiltering = true
                    });
                    rel_cnf.RelationshipMappings.Add(new RelationshipMapping(rel_cnf)
                    {
                        Property = rel_cnf.OtherSide.EntityConfiguration.FindProperty("CreatedByUser").Relationship.ThisEnd,
                        Expression = new IqlLambdaExpression
                        {
                            Body = new IqlLambdaExpression
                            {
                                Body = new IqlPropertyExpression
                                {
                                    PropertyName = "CreatedByUser",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "Owner",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlVariableExpression
                                        {
                                            EntityTypeName = "RelationshipFilterContext<ApplicationUser>",
                                            VariableName = "ctx",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.Variable,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Parameters = new List<IqlRootReferenceExpression>
                                {
                                    new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "Client",
                                        VariableName = "_",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                },
                                Kind = IqlExpressionKind.Lambda,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "RelationshipFilterContext<ApplicationUser>",
                                    VariableName = "ctx",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        UseForFiltering = true
                    });
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Client";
                    rel_cnf.Name = "Client";
                    rel_cnf.Title = "Client";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.ClientsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Clients Created";
                    rel_cnf.Name = "ClientsCreated";
                    rel_cnf.Title = "Clients Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.DocumentCategoriesCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Document Categories Created";
                    rel_cnf.Name = "DocumentCategoriesCreated";
                    rel_cnf.Title = "Document Categories Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.SiteDocumentsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Site Documents Created";
                    rel_cnf.Name = "SiteDocumentsCreated";
                    rel_cnf.Title = "Site Documents Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.FaultActionsTakenCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Fault Actions Taken Created";
                    rel_cnf.Name = "FaultActionsTakenCreated";
                    rel_cnf.Title = "Fault Actions Taken Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.FaultCategoriesCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Fault Categories Created";
                    rel_cnf.Name = "FaultCategoriesCreated";
                    rel_cnf.Title = "Fault Categories Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.FaultDefaultRecommendationsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Fault Default Recommendations Created";
                    rel_cnf.Name = "FaultDefaultRecommendationsCreated";
                    rel_cnf.Title = "Fault Default Recommendations Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.FaultRecommendationsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Fault Recommendations Created";
                    rel_cnf.Name = "FaultRecommendationsCreated";
                    rel_cnf.Title = "Fault Recommendations Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.FaultTypesCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Fault Types Created";
                    rel_cnf.Name = "FaultTypesCreated";
                    rel_cnf.Title = "Fault Types Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.ProjectCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Project Created";
                    rel_cnf.Name = "ProjectCreated";
                    rel_cnf.Title = "Project Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.ReportReceiverEmailAddressesCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Report Receiver Email Addresses Created";
                    rel_cnf.Name = "ReportReceiverEmailAddressesCreated";
                    rel_cnf.Title = "Report Receiver Email Addresses Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.RiskAssessmentsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Risk Assessments Created";
                    rel_cnf.Name = "RiskAssessmentsCreated";
                    rel_cnf.Title = "Risk Assessments Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.RiskAssessmentSolutionsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Risk Assessment Solutions Created";
                    rel_cnf.Name = "RiskAssessmentSolutionsCreated";
                    rel_cnf.Title = "Risk Assessment Solutions Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.RiskAssessmentAnswersCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Risk Assessment Answers Created";
                    rel_cnf.Name = "RiskAssessmentAnswersCreated";
                    rel_cnf.Title = "Risk Assessment Answers Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.RiskAssessmentQuestionsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Risk Assessment Questions Created";
                    rel_cnf.Name = "RiskAssessmentQuestionsCreated";
                    rel_cnf.Title = "Risk Assessment Questions Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.PeopleCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "People Created";
                    rel_cnf.Name = "PeopleCreated";
                    rel_cnf.Title = "People Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.PersonInspectionsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Person Inspections Created";
                    rel_cnf.Name = "PersonInspectionsCreated";
                    rel_cnf.Title = "Person Inspections Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.PersonLoadingsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Person Loadings Created";
                    rel_cnf.Name = "PersonLoadingsCreated";
                    rel_cnf.Title = "Person Loadings Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.PersonTypesCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Person Types Created";
                    rel_cnf.Name = "PersonTypesCreated";
                    rel_cnf.Title = "Person Types Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.FaultReportsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Fault Reports Created";
                    rel_cnf.Name = "FaultReportsCreated";
                    rel_cnf.Title = "Fault Reports Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.SitesCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Sites Created";
                    rel_cnf.Name = "SitesCreated";
                    rel_cnf.Title = "Sites Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.SiteAreasCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Site Areas Created";
                    rel_cnf.Name = "SiteAreasCreated";
                    rel_cnf.Title = "Site Areas Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.SiteInspectionsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Site Inspections Created";
                    rel_cnf.Name = "SiteInspectionsCreated";
                    rel_cnf.Title = "Site Inspections Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.UserSettingsCreated).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "User Settings Created";
                    rel_cnf.Name = "UserSettingsCreated";
                    rel_cnf.Title = "User Settings Created";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.UserSettings).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "User Settings";
                    rel_cnf.Name = "UserSettings";
                    rel_cnf.Title = "User Settings";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Sites).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Sites";
                    rel_cnf.Name = "Sites";
                    rel_cnf.Title = "Sites";
                });
            });
            builder.DefineEntityType<Client>("Client").Configure(rel => {
                rel.FindCollectionRelationship(rel_p => rel_p.Users).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Users";
                    rel_cnf.Name = "Users";
                    rel_cnf.Title = "Users";
                });
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.Type).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Type";
                    rel_cnf.Name = "Type";
                    rel_cnf.Title = "Type";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Categories).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Categories";
                    rel_cnf.Name = "Categories";
                    rel_cnf.Title = "Categories";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.People).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "People";
                    rel_cnf.Name = "People";
                    rel_cnf.Title = "People";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.InferredPeople).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Inferred People";
                    rel_cnf.Name = "InferredPeople";
                    rel_cnf.Title = "Inferred People";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Sites).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Sites";
                    rel_cnf.Name = "Sites";
                    rel_cnf.Title = "Sites";
                });
            });
            builder.DefineEntityType<ClientType>("ClientType").Configure(rel => {
                rel.FindCollectionRelationship(rel_p => rel_p.Clients).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Clients";
                    rel_cnf.Name = "Clients";
                    rel_cnf.Title = "Clients";
                });
            });
            builder.DefineEntityType<ClientCategory>("ClientCategory").Configure(rel => {
                rel.FindCollectionRelationship(rel_p => rel_p.Clients).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Clients";
                    rel_cnf.Name = "Clients";
                    rel_cnf.Title = "Clients";
                });
            });
            builder.DefineEntityType<ClientCategoryPivot>("ClientCategoryPivot").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.Client).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Client";
                    rel_cnf.Name = "Client";
                    rel_cnf.Title = "Client";
                });
                rel.FindRelationship(rel_p => rel_p.Category).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Category";
                    rel_cnf.Name = "Category";
                    rel_cnf.Title = "Category";
                });
            });
            builder.DefineEntityType<DocumentCategory>("DocumentCategory").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Documents).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Documents";
                    rel_cnf.Name = "Documents";
                    rel_cnf.Title = "Documents";
                });
            });
            builder.DefineEntityType<SiteDocument>("SiteDocument").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.Category).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Category";
                    rel_cnf.Name = "Category";
                    rel_cnf.Title = "Category";
                });
                rel.FindRelationship(rel_p => rel_p.Site).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site";
                    rel_cnf.Name = "Site";
                    rel_cnf.Title = "Site";
                });
            });
            builder.DefineEntityType<Site>("Site").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.Client).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Client";
                    rel_cnf.Name = "Client";
                    rel_cnf.Title = "Client";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Documents).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Documents";
                    rel_cnf.Name = "Documents";
                    rel_cnf.Title = "Documents";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.AdditionalSendReportsTo).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Additional Send Reports To";
                    rel_cnf.Name = "AdditionalSendReportsTo";
                    rel_cnf.Title = "Additional Send Reports To";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.People).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "People";
                    rel_cnf.Name = "People";
                    rel_cnf.Title = "People";
                });
                rel.FindRelationship(rel_p => rel_p.Parent).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Parent";
                    rel_cnf.Name = "Parent";
                    rel_cnf.Title = "Parent";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Children).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Children";
                    rel_cnf.Name = "Children";
                    rel_cnf.Title = "Children";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Areas).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Areas";
                    rel_cnf.Name = "Areas";
                    rel_cnf.Title = "Areas";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.SiteInspections).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Site Inspections";
                    rel_cnf.Name = "SiteInspections";
                    rel_cnf.Title = "Site Inspections";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Users).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Users";
                    rel_cnf.Name = "Users";
                    rel_cnf.Title = "Users";
                });
            });
            builder.DefineEntityType<ReportActionsTaken>("ReportActionsTaken").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.PersonReport).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Person Report";
                    rel_cnf.Name = "PersonReport";
                    rel_cnf.Title = "Person Report";
                });
            });
            builder.DefineEntityType<PersonReport>("PersonReport").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.ActionsTaken).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Actions Taken";
                    rel_cnf.Name = "ActionsTaken";
                    rel_cnf.Title = "Actions Taken";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Recommendations).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Recommendations";
                    rel_cnf.Name = "Recommendations";
                    rel_cnf.Title = "Recommendations";
                });
                rel.FindRelationship(rel_p => rel_p.Person).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Person";
                    rel_cnf.Name = "Person";
                    rel_cnf.Title = "Person";
                });
                rel.FindRelationship(rel_p => rel_p.Type).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Type";
                    rel_cnf.Name = "Type";
                    rel_cnf.Title = "Type";
                });
            });
            builder.DefineEntityType<ReportCategory>("ReportCategory").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.ReportTypes).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Report Types";
                    rel_cnf.Name = "ReportTypes";
                    rel_cnf.Title = "Report Types";
                });
            });
            builder.DefineEntityType<ReportDefaultRecommendation>("ReportDefaultRecommendation").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Recommendations).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Recommendations";
                    rel_cnf.Name = "Recommendations";
                    rel_cnf.Title = "Recommendations";
                });
            });
            builder.DefineEntityType<ReportRecommendation>("ReportRecommendation").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.PersonReport).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Person Report";
                    rel_cnf.Name = "PersonReport";
                    rel_cnf.Title = "Person Report";
                });
                rel.FindRelationship(rel_p => rel_p.Recommendation).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Recommendation";
                    rel_cnf.Name = "Recommendation";
                    rel_cnf.Title = "Recommendation";
                });
            });
            builder.DefineEntityType<ReportType>("ReportType").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.FaultReports).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Fault Reports";
                    rel_cnf.Name = "FaultReports";
                    rel_cnf.Title = "Fault Reports";
                });
                rel.FindRelationship(rel_p => rel_p.Category).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Category";
                    rel_cnf.Name = "Category";
                    rel_cnf.Title = "Category";
                });
            });
            builder.DefineEntityType<Project>("Project").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
            });
            builder.DefineEntityType<ReportReceiverEmailAddress>("ReportReceiverEmailAddress").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.Site).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site";
                    rel_cnf.Name = "Site";
                    rel_cnf.Title = "Site";
                });
            });
            builder.DefineEntityType<RiskAssessment>("RiskAssessment").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.SiteInspection).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site Inspection";
                    rel_cnf.Name = "SiteInspection";
                    rel_cnf.Title = "Site Inspection";
                });
            });
            builder.DefineEntityType<SiteInspection>("SiteInspection").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.Site).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site";
                    rel_cnf.Name = "Site";
                    rel_cnf.Title = "Site";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.RiskAssessments).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Risk Assessments";
                    rel_cnf.Name = "RiskAssessments";
                    rel_cnf.Title = "Risk Assessments";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.PersonInspections).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Person Inspections";
                    rel_cnf.Name = "PersonInspections";
                    rel_cnf.Title = "Person Inspections";
                });
            });
            builder.DefineEntityType<RiskAssessmentSolution>("RiskAssessmentSolution").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
            });
            builder.DefineEntityType<RiskAssessmentAnswer>("RiskAssessmentAnswer").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.Question).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Question";
                    rel_cnf.Name = "Question";
                    rel_cnf.Title = "Question";
                });
            });
            builder.DefineEntityType<RiskAssessmentQuestion>("RiskAssessmentQuestion").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Answers).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Answers";
                    rel_cnf.Name = "Answers";
                    rel_cnf.Title = "Answers";
                });
            });
            builder.DefineEntityType<Person>("Person").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.Client).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Client";
                    rel_cnf.Name = "Client";
                    rel_cnf.Title = "Client";
                });
                rel.FindRelationship(rel_p => rel_p.InferredFromUserClient).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Inferred From User Client";
                    rel_cnf.Name = "InferredFromUserClient";
                    rel_cnf.Title = "Inferred From User Client";
                });
                rel.FindRelationship(rel_p => rel_p.Site).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site";
                    rel_cnf.Name = "Site";
                    rel_cnf.Title = "Site";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Reports).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Reports";
                    rel_cnf.Name = "Reports";
                    rel_cnf.Title = "Reports";
                });
                rel.FindRelationship(rel_p => rel_p.SiteArea).Configure(rel_cnf => {
                    rel_cnf.RelationshipMappings.Add(new RelationshipMapping(rel_cnf)
                    {
                        Property = rel_cnf.OtherSide.EntityConfiguration.FindProperty("Site").Relationship.ThisEnd,
                        Expression = new IqlLambdaExpression
                        {
                            Body = new IqlLambdaExpression
                            {
                                Body = new IqlPropertyExpression
                                {
                                    PropertyName = "Site",
                                    Kind = IqlExpressionKind.Property,
                                    ReturnType = IqlType.Unknown,
                                    Parent = new IqlPropertyExpression
                                    {
                                        PropertyName = "Owner",
                                        Kind = IqlExpressionKind.Property,
                                        ReturnType = IqlType.Unknown,
                                        Parent = new IqlVariableExpression
                                        {
                                            EntityTypeName = "RelationshipFilterContext<Person>",
                                            VariableName = "ctx",
                                            InferredReturnType = IqlType.Unknown,
                                            Kind = IqlExpressionKind.Variable,
                                            ReturnType = IqlType.Unknown
                                        }
                                    }
                                },
                                Parameters = new List<IqlRootReferenceExpression>
                                {
                                    new IqlRootReferenceExpression
                                    {
                                        EntityTypeName = "SiteArea",
                                        VariableName = "_",
                                        InferredReturnType = IqlType.Unknown,
                                        Kind = IqlExpressionKind.RootReference,
                                        ReturnType = IqlType.Unknown
                                    }
                                },
                                Kind = IqlExpressionKind.Lambda,
                                ReturnType = IqlType.Unknown
                            },
                            Parameters = new List<IqlRootReferenceExpression>
                            {
                                new IqlRootReferenceExpression
                                {
                                    EntityTypeName = "RelationshipFilterContext<Person>",
                                    VariableName = "ctx",
                                    InferredReturnType = IqlType.Unknown,
                                    Kind = IqlExpressionKind.RootReference,
                                    ReturnType = IqlType.Unknown
                                }
                            },
                            Kind = IqlExpressionKind.Lambda,
                            ReturnType = IqlType.Unknown
                        },
                        UseForFiltering = true
                    });
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site Area";
                    rel_cnf.Name = "SiteArea";
                    rel_cnf.Title = "Site Area";
                });
                rel.FindRelationship(rel_p => rel_p.Type).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Type";
                    rel_cnf.Name = "Type";
                    rel_cnf.Title = "Type";
                });
                rel.FindRelationship(rel_p => rel_p.Loading).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Loading";
                    rel_cnf.Name = "Loading";
                    rel_cnf.Title = "Loading";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.Types).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "Types";
                    rel_cnf.Name = "Types";
                    rel_cnf.Title = "Types";
                });
            });
            builder.DefineEntityType<SiteArea>("SiteArea").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.Site).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site";
                    rel_cnf.Name = "Site";
                    rel_cnf.Title = "Site";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.People).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "People";
                    rel_cnf.Name = "People";
                    rel_cnf.Title = "People";
                });
            });
            builder.DefineEntityType<PersonType>("PersonType").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.People).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "People";
                    rel_cnf.Name = "People";
                    rel_cnf.Title = "People";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.PeopleMap).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "People Map";
                    rel_cnf.Name = "PeopleMap";
                    rel_cnf.Title = "People Map";
                });
            });
            builder.DefineEntityType<PersonLoading>("PersonLoading").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindCollectionRelationship(rel_p => rel_p.People).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.ReadOnlyEditDisplayKind = ReadOnlyEditDisplayKind.Hide;
                    rel_cnf.EditKind = IqlPropertyEditKind.Hidden;
                    rel_cnf.FriendlyName = "People";
                    rel_cnf.Name = "People";
                    rel_cnf.Title = "People";
                });
            });
            builder.DefineEntityType<PersonInspection>("PersonInspection").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.SiteInspection).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site Inspection";
                    rel_cnf.Name = "SiteInspection";
                    rel_cnf.Title = "Site Inspection";
                });
            });
            builder.DefineEntityType<PersonTypeMap>("PersonTypeMap").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.Person).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Person";
                    rel_cnf.Name = "Person";
                    rel_cnf.Title = "Person";
                });
                rel.FindRelationship(rel_p => rel_p.Type).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Type";
                    rel_cnf.Name = "Type";
                    rel_cnf.Title = "Type";
                });
            });
            builder.DefineEntityType<UserSetting>("UserSetting").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.CreatedByUser).Configure(rel_cnf => {
                    rel_cnf.CanWrite = false;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Created By User";
                    rel_cnf.Name = "CreatedByUser";
                    rel_cnf.Title = "Created By User";
                });
                rel.FindRelationship(rel_p => rel_p.User).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "User";
                    rel_cnf.Name = "User";
                    rel_cnf.Title = "User";
                });
            });
            builder.DefineEntityType<UserSite>("UserSite").Configure(rel => {
                rel.FindRelationship(rel_p => rel_p.User).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "User";
                    rel_cnf.Name = "User";
                    rel_cnf.Title = "User";
                });
                rel.FindRelationship(rel_p => rel_p.Site).Configure(rel_cnf => {
                    rel_cnf.CanWrite = true;
                    rel_cnf.EditKind = IqlPropertyEditKind.Edit;
                    rel_cnf.FriendlyName = "Site";
                    rel_cnf.Name = "Site";
                    rel_cnf.Title = "Site";
                });
            });
            builder.DefineEntityType<Site>("Site").SetDisplay("", DisplayConfigurationKind.Edit, (ec, displayConfiguration) => {
                displayConfiguration.SetProperties(ec, _ => _.FindRelationship(_1 => _1.Client), _ => _.FindPropertyByExpression(_1 => _1.Name), _ => _.FindRelationship(_1 => _1.Parent), _ => _.PropertyCollection(_1 => _1.FindPropertyByExpression(_2 => _2.Address), _2 => _2.FindPropertyByExpression(_3 => _3.PostCode)).Configure(coll3 => {
                    coll3.ContentAlignment = ContentAlignment.Horizontal;
                    coll3.CanWrite = true;
                    coll3.FriendlyName = "Site Address";
                    coll3.Hints = new List<string>(new[]
                    {
                        "Iql.Forms:HelpText:Top"
                    });
                    coll3.Name = "Site Address";
                    coll3.Title = "Site Address";
                }), _ => _.FindPropertyByExpression(_1 => _1.Parent), _ => _.FindPropertyByExpression(_1 => _1.Key), _ => _.FindPropertyByExpression(_1 => _1.Location));
            });
            builder.UserSettingsDefinition = UserSettingsDefinition.Define(builder.EntityType<UserSetting>(), _ => _.Id, _ => _.UserId, _ => _.Key1, _ => _.Key2, _ => _.Key3, _ => _.Key4, _ => _.Value);
            builder.UsersDefinition = UsersDefinition.Define(builder.EntityType<ApplicationUser>(), _ => _.Id, _ => _.FullName);
        }
        public ApplicationUserSet Users
        {
            get;
            set;
        }
        public ApplicationLogSet ApplicationLogs
        {
            get;
            set;
        }
        public ClientSet Clients
        {
            get;
            set;
        }
        public ClientTypeSet ClientTypes
        {
            get;
            set;
        }
        public ClientCategorySet ClientCategories
        {
            get;
            set;
        }
        public ClientCategoryPivotSet ClientCategoriesPivot
        {
            get;
            set;
        }
        public DocumentCategorySet DocumentCategories
        {
            get;
            set;
        }
        public SiteDocumentSet SiteDocuments
        {
            get;
            set;
        }
        public ReportActionsTakenSet ReportActionsTaken
        {
            get;
            set;
        }
        public ReportCategorySet ReportCategories
        {
            get;
            set;
        }
        public ReportDefaultRecommendationSet ReportDefaultRecommendations
        {
            get;
            set;
        }
        public ReportRecommendationSet ReportRecommendations
        {
            get;
            set;
        }
        public ReportTypeSet ReportTypes
        {
            get;
            set;
        }
        public ProjectSet Projects
        {
            get;
            set;
        }
        public ReportReceiverEmailAddressSet ReportReceiverEmailAddresses
        {
            get;
            set;
        }
        public RiskAssessmentSet RiskAssessments
        {
            get;
            set;
        }
        public RiskAssessmentSolutionSet RiskAssessmentSolutions
        {
            get;
            set;
        }
        public RiskAssessmentAnswerSet RiskAssessmentAnswers
        {
            get;
            set;
        }
        public RiskAssessmentQuestionSet RiskAssessmentQuestions
        {
            get;
            set;
        }
        public PersonSet People
        {
            get;
            set;
        }
        public PersonInspectionSet PersonInspections
        {
            get;
            set;
        }
        public PersonLoadingSet PersonLoadings
        {
            get;
            set;
        }
        public PersonTypeSet PersonTypes
        {
            get;
            set;
        }
        public PersonTypeMapSet PersonTypesMap
        {
            get;
            set;
        }
        public PersonReportSet PersonReports
        {
            get;
            set;
        }
        public SiteSet Sites
        {
            get;
            set;
        }
        public SiteAreaSet SiteAreas
        {
            get;
            set;
        }
        public SiteInspectionSet SiteInspections
        {
            get;
            set;
        }
        public UserSettingSet UserSettings
        {
            get;
            set;
        }
        public UserSiteSet UserSites
        {
            get;
            set;
        }
        public virtual ODataDataMethodRequest<string>SendHi(string name)
        {
            var parameters = new List<ODataParameter>();
            parameters.Add(new ODataParameter(name, typeof(string), "name", false));
            return ((ODataDataStore) this.DataStore).MethodWithResponse<string>(parameters, ODataMethodType.Action, ODataMethodScopeKind.Global, "IqlSampleApp", "SendHi", null, typeof(String));
        }
    }
}