using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Context;
using Iql.Data.DataStores;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.OData.Json;
using Iql.OData.TypeScript.Generator.DataContext;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Extensions;
using Iql.OData.TypeScript.Generator.Models;
using Iql.OData.TypeScript.Generator.Parsers;
using Iql.Parsing;

namespace Iql.OData.TypeScript.Generator.ClassGenerators
{
    public class DbSetsGenerator : ClassGenerator
    {
        private readonly IEnumerable<EntitySetDefinition> _entitySetDefinitions;
        public DbSetsGenerator(
            ODataSchema schema,
            string fileName,
            string @namespace,
            IEnumerable<EntitySetDefinition> entitySetDefinitions,
            OutputType outputType,
            GeneratorSettings settings

            ) : base(
            fileName,
            @namespace,
            schema,
            outputType,
            settings)
        {
            _entitySetDefinitions = entitySetDefinitions;
        }

        public GeneratedFile Generate()
        {
            foreach (var entitySetDefinition in _entitySetDefinitions)
            {
                var dbSetName = entitySetDefinition.GetDbSetName(NameMapper);
                Class(
                    dbSetName,
                    Namespace,
                    "",
                    () =>
                    {
                        //EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore> dataStoreGetter,
                        //EvaluateContext evaluateContext = null, IDataContext dataContext = null
                        var constructorParameters = new IVariable[]
                        {
                            new EntityFunctionParameterDefinition("entityConfigurationBuilder", TypeResolver.TranslateType(typeof(EntityConfigurationBuilder))),
                            new EntityFunctionParameterDefinition("dataStoreGetter", TypeResolver.TranslateType(typeof(Func<IDataStore>))),
                            new EntityFunctionParameterDefinition("evaluateContext", TypeResolver.TranslateType(typeof(EvaluateContext)), hasDefaultValue: true, defaultValue: null),
                            new EntityFunctionParameterDefinition("dataContext", TypeResolver.TranslateType(typeof(IDataContext)), hasDefaultValue: true, defaultValue: null),
                        };
                        var baseParameters = constructorParameters.ToList();
                        if (OutputType == OutputType.TypeScript)
                        {
                            baseParameters.Add(new EntityFunctionParameterDefinition(entitySetDefinition.Type.Name, new TypeInfo(entitySetDefinition.Type.Name)));
                        }
                        Constructor(
                            constructorParameters,
                            () => { },
                            baseParameters
                        );
                        //const string validateEntityMethodName = nameof(IEntity.ValidateEntity);
                        if (entitySetDefinition.Functions.Any())
                        {
                            AppendLine();
                            CommentLine("Collection methods");
                            foreach (var method in entitySetDefinition.Functions)
                            {
                                PrintODataMethod(method);
                            }
                        }

                        if (entitySetDefinition.Type.Functions.Any())
                        {
                            CommentLine("Entity methods");
                            foreach (var method in entitySetDefinition.Type.Functions)
                            {
                                PrintODataMethod(method);
                            }
                        }
                    },
                    $"{nameof(DbSet<object, object>)}<{string.Join(", ", entitySetDefinition.Type.Name, ResolveKeyType(entitySetDefinition))}>");
            }
            foreach (var set in _entitySetDefinitions)
            {
                File.References.Add(set.Type);
            }
            File.Contents = Contents();
            return File;
        }
    }
}