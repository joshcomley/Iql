﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Extensions;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Iql.Entities.Extensions;
using Iql.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Data.Serialization
{
    public abstract class DeserializeResult<T>
    {
        public abstract T Deserialize(bool ensureType = false);

        public IEntityConfiguration EntityConfiguration { get; }

        protected DeserializeResult(IEntityConfiguration entityConfiguration)
        {
            EntityConfiguration = entityConfiguration;
        }
    }
    public class DeserializeCollectionResult<T> : DeserializeResult<IEnumerable<T>>
    {
        public JObject Root { get; set; }

        public DeserializeCollectionResult(IEntityConfiguration entityConfiguration, JObject root) : base(entityConfiguration)
        {
            Root = root;
        }

        public override IEnumerable<T> Deserialize(bool ensureType = false)
        {
            return Root.ToObject<T[]>();
        }
    }

    public class DeserializeSingleResult<T> : DeserializeResult<T>
    {
        public JObject Root { get; set; }

        public DeserializeSingleResult(IEntityConfiguration entityConfiguration, JObject root) : base(entityConfiguration)
        {
            Root = root;
        }

        public override T Deserialize(bool ensureType = false)
        {
            return Root.ToObject<T>();
        }
    }

    public static class JsonDataSerializer
    {
        public static string NormalizeDate(this DateTimeOffset offset)
        {
            var timezoneOffsetInHours = -offset.Offset.TotalHours; //UTC minus local time
            var sign = timezoneOffsetInHours >= 0 ? '+' : '-';
            var iso =
                $"{offset.Year.PadLeft('0', 4)}-{offset.Month.PadLeft('0', 2)}-{offset.Day.PadLeft('0', 2)}T{offset.Hour.PadLeft('0', 2)}:{offset.Minute.PadLeft('0', 2)}:{offset.Second.PadLeft('0', 2)}.{offset.Millisecond}{sign}00:00";
            return iso;
        }

        //public static List<JObject> PrepareListForSerialization(IList entities,
        //    IEntityConfiguration entityConfiguration,
        //    params IPropertyState[] properties)
        //{
        //    var all = new List<JObject>();
        //    var arr = entities as object[] ?? entities.ToArray();
        //    for (var i = 0; i < arr.Length; i++)
        //    {
        //        var entity = arr[i];
        //        all.Add(SerializeInternal(entityConfiguration, entity, properties));
        //    }

        //    return all;
        //}

        class SerializedEntitySet
        {
            public string Type { get; set; }
            public JArray Entities { get; set; }
        }

        public static DeserializedEntitySets DeserializeEntitySets(IEntityConfigurationBuilder builder, string json)
        {
            var dictionary = new Dictionary<IEntityConfiguration, IList>();
            if (!string.IsNullOrWhiteSpace(json))
            {
                try
                {
                    var deserialized =
                        (SerializedEntitySet[])JsonConvert.DeserializeObject(json, typeof(SerializedEntitySet[]));
                    foreach (var set in deserialized)
                    {
                        var entityConfiguration = builder.GetEntityByTypeName(set.Type);
                        var genericList = ListExtensions.NewGenericList(entityConfiguration.Type);
                        foreach (var entity in set.Entities)
                        {
                            var parsed = ParseEntityInternal(entity, entityConfiguration, null, null, false);
                            var entityResult = parsed.Token.ToObject(entityConfiguration.Type);
                            entityResult =
                                builder.EnsureTypedEntityByType(entityResult, entityConfiguration.Type, false);
                            genericList.Add(entityResult);
                        }

                        dictionary.Add(entityConfiguration, genericList);
                    }
                }
                catch
                {

                }
            }
            return new DeserializedEntitySets(dictionary);
        }

        public static List<JObject> PrepareCollectionForSerialization(IEnumerable entities,
            IEntityConfiguration entityConfiguration,
            bool areNew,
            bool allowAllKeys,
            params IPropertyState[] properties)
        {
            var all = new List<JObject>();
            foreach (var entity in entities)
            {
                all.Add(SerializePropertyChangesInternal(entityConfiguration, entity, areNew, allowAllKeys, properties));
            }
            return all;
        }

        public static JObject PrepareEntityForSerialization(object entity,
            IEntityConfiguration entityConfiguration,
            bool isNew,
            bool allowAllKeys,
            params IPropertyState[] properties)
        {
            var obj = SerializePropertyChangesInternal(entityConfiguration, entity, isNew, allowAllKeys, properties);
            return obj;
        }

        public static string SerializeEntityToJson(object entity,
            IEntityConfiguration entityConfiguration)
        {
            return SerializePropertiesInternal(entityConfiguration, entity, true, true, entityConfiguration.Properties.ToArray()).ToString();
        }

        public static string SerializeEntityPropertiesToJson(object entity,
            IEntityConfiguration entityConfiguration,
            bool isNew,
            bool allowAllKeys,
            params IPropertyState[] properties)
        {
            return PrepareEntityForSerialization(entity, entityConfiguration, isNew, allowAllKeys, properties).ToString();
        }

        public static DeserializeCollectionResult<T> DeserializeCollection<T>(string json,
            IEntityConfiguration entityConfiguration)
        {
            var odataResultRoot = JObject.Parse(json);
            ParseEntityInternal(odataResultRoot, entityConfiguration, null, null, true);
            return new DeserializeCollectionResult<T>(entityConfiguration, odataResultRoot);
        }

        public static DeserializeSingleResult<T> DeserializeEntity<T>(string json,
            IEntityConfiguration entityConfiguration)
        {
            var odataResultRoot = JObject.Parse(json);
            ParseEntityInternal(odataResultRoot, entityConfiguration, null, null, false);
            return new DeserializeSingleResult<T>(entityConfiguration, odataResultRoot);
        }

        public static void ParseSerializedValue(JToken jvalue, IEntityConfiguration entityType)
        {
            ParseEntityInternal(jvalue, entityType, null, null, false);
        }

        private static ParseTokenResult ParseEntityInternal(
            JToken jvalue,
            IEntityConfiguration entityType,
            JToken parent,
            IEntityConfiguration parentEntityType,
            bool isCollectionRoot,
            IProperty property = null)
        {
            var isEmptyObject = false;
            if (jvalue is JArray)
            {
                var isCountArray = true;
                foreach (var child in (JArray)jvalue)
                {
                    var result = ParseEntityInternal(child, entityType, jvalue, parentEntityType, isCollectionRoot);
                    if (!result.IsEmptyObject)
                    {
                        isCountArray = false;
                    }
                }

                if (isCountArray && parentEntityType != null)
                {
                    var countProperty =
                        parentEntityType
                            .FindProperty($"{property.Name}Count");
                    if (countProperty != null)
                    {
                        parent[countProperty.Name] = ((JArray)jvalue).Count;
                    }
                }
            }
            else if (jvalue is JObject)
            {
                var jobj = (JObject)jvalue;
                var jProperties = jobj.Properties().ToArray();
                var remainingProperties = jProperties.Length;
                foreach (var prop in jProperties)
                {
                    var value = jobj[prop.Name];
                    const string odataKey = "@odata.";
                    if (prop.Name.Contains(odataKey))
                    {
                        var index = prop.Name.IndexOf(odataKey);
                        var odataName = prop.Name.Substring(index + odataKey.Length);
                        var before = prop.Name.Substring(0, index);
                        switch (odataName)
                        {
                            case "count":
                                odataName = "Count";
                                break;
                        }

                        // Remove @odata.id values
                        if (odataName != "id")
                        {
                            jobj[before + odataName] = value;
                        }
                        else
                        {
                            remainingProperties--;
                        }
                        jobj.Remove(prop.Name);
                    }

                    if (!isCollectionRoot && entityType != null)
                    {
                        var entityProperty = entityType.Properties.SingleOrDefault(p => p.Name == prop.Name);
                        if (entityProperty != null)
                        {
                            if (entityProperty.Kind == IqlPropertyKind.Relationship)
                            {
                                jobj[prop.Name] = ParseEntityInternal(value, entityProperty.Relationship.OtherEnd.EntityConfiguration, jvalue, entityType, false, entityProperty)
                                    .Token;
                            }
                            else if (entityProperty.TypeDefinition.Kind.IsGeographic())
                            {
                                jobj[prop.Name] = value as JObject == null ? null : JObject.FromObject(JsonDataSerializer.ConvertODataGeographyToIqlGeography(value as JObject, entityProperty.TypeDefinition.Kind));
                            }
                            else
                            {
                                jobj[prop.Name] = value;
                            }
                        }
                    }
                }

                if (isCollectionRoot)
                {
                    var collection = jobj["value"] as JArray;
                    foreach (var entity in collection)
                    {
                        ParseEntityInternal(entity, entityType, collection, null, false);
                    }
                }

                if (remainingProperties == 0)
                {
                    isEmptyObject = true;
                }
            }

            return new ParseTokenResult(jvalue, isEmptyObject);
        }

        private static JObject SerializePropertyChangesInternal(
            IEntityConfiguration entityConfiguration,
            object entity,
            bool isNew,
            bool allowAllKeys,
            IEnumerable<IPropertyState> properties)
        {
            if (properties == null)
            {
                properties = new PropertyState[] { };
            }

            var propertyChanges = properties as PropertyState[] ?? properties.ToArray();
            if (!propertyChanges.Any())
            {
                var state = DataContext.FindEntityState(entity);
                if (state?.IsNew != false)
                {
                    propertyChanges = entityConfiguration.Builder.EntityNonNullProperties(entity).ToArray();
                }
            }
            return SerializePropertiesInternal(entityConfiguration, entity, isNew, allowAllKeys, propertyChanges.Select(_ => _.Property).ToArray());
        }

        private static JObject SerializePropertiesInternal(
            IEntityConfiguration entityConfiguration,
            object entity,
            bool isNew,
            bool allowAllKeys,
            IProperty[] propertiesToSerialize)
        {
            var obj = new JObject();

            bool CanSendKey(IProperty keyProperty)
            {
                if (allowAllKeys)
                {
                    return true;
                }

                var invalid = (isNew && !keyProperty.EntityConfiguration.Key.CanWrite) ||
                              keyProperty.GetValue(entity) == null;
                return !invalid;
            }

            foreach (var key in entityConfiguration.Key.Properties)
            {
                if (!CanSendKey(key))
                {
                    continue;
                }

                obj[((IMetadata)key).Name] = new JValue(entity.GetPropertyValueByName(((IMetadata)key).Name));
            }

            foreach (var property in propertiesToSerialize)
            {
                //if (property.EntityConfiguration.Key.Properties.Any(p => p == property))
                //{
                //    // Main keys are dealt with above
                //    continue;
                //}

                if (property.Kind.HasFlag(IqlPropertyKind.Key) && !property.Kind.HasFlag(IqlPropertyKind.RelationshipKey) && !CanSendKey(property))
                {
                    continue;
                }

                var propertyValue = property.GetValue(entity);
                if (property.Kind.HasFlag(IqlPropertyKind.Count) || property.Kind.HasFlag(IqlPropertyKind.Relationship))
                {
                    continue;
                }

                if (propertyValue != null)
                {
                    if (property.TypeDefinition.Kind.IsGeographic())
                    {
                        SerializeGeography(obj, property, propertyValue);
                        continue;
                    }
                }

                if (property.TypeDefinition.IsCollection)
                {
                    obj[((IMetadata)property).Name] = new JArray(propertyValue);
                }
                else
                {
                    if (property.TypeDefinition.ToIqlType() == IqlType.Date)
                    {
                        if (propertyValue == null && !property.TypeDefinition.Nullable)
                        {
                            obj[((IMetadata)property).Name] = "0001-01-01T00:00:00.0+00:00";
                        }
                        else
                        {
                            var normalizedDate = propertyValue == null ? null : ((DateTimeOffset)propertyValue).NormalizeDate();
                            obj[((IMetadata)property).Name] = new JValue(normalizedDate);
                        }
                    }
                    else if (property.TypeDefinition.ConvertedFromType == KnownPrimitiveTypes.Guid &&
                             !property.TypeDefinition.Nullable && propertyValue == null)
                    {
                        obj[((IMetadata)property).Name] = "00000000-0000-0000-0000-000000000000";
                    }
                    else if (property.TypeDefinition.Kind == IqlType.Enum)
                    {
                        if (propertyValue == null && property.Nullable == false)
                        {
                            propertyValue = 0;
                        }

                        var value = propertyValue;
                        if (value != null)
                        {
#if !TypeScript
                            var enumUnderlyingType = property.TypeDefinition.Type.GetEnumUnderlyingType();
                            if (enumUnderlyingType == typeof(long))
                            {
                                try
                                {
                                    value = ((long)propertyValue).ToString();
                                }
                                catch
                                {
                                    value = ((int)propertyValue).ToString();
                                }
                            }
                            else if (enumUnderlyingType == typeof(short))
                            {
                                try
                                {
                                    value = ((short)propertyValue).ToString();
                                }
                                catch
                                {
                                    value = ((int)propertyValue).ToString();
                                }
                            }
                            else
                            {
                                value = ((int)propertyValue).ToString();
                            }
#else
                            value = propertyValue.ToString();
#endif
                        }

                        obj[((IMetadata)property).Name] = new JValue(value);
                    }
                    else
                    {
                        obj[((IMetadata)property).Name] = new JValue(propertyValue);
                    }
                }
            }

            return obj;
        }

        private static void SerializeGeography(
            JObject obj,
            IProperty propertyProperty,
            object propertyValue)
        {
            if (propertyValue == null)
            {
                return;
            }
            var container = new JObject();
            obj[((IMetadata)propertyProperty).Name] = container;
            string typeName = "";
            object coordinates = null;
            switch (propertyProperty.TypeDefinition.Kind)
            {
                case IqlType.GeometryLine:
                case IqlType.GeographyLine:
                    typeName = "LineString";
                    coordinates = SerializePoints(propertyValue as IqlLineExpression);
                    break;
                case IqlType.GeometryMultiLine:
                case IqlType.GeographyMultiLine:
                    typeName = "MultiLineString";
                    coordinates = (propertyValue as IqlMultiLineExpression).Lines.Select(line => SerializePoints(line)).ToArray();
                    break;
                case IqlType.GeometryPolygon:
                case IqlType.GeographyPolygon:
                    typeName = "Polygon";
                    coordinates = SerializePolygon(propertyValue as IqlPolygonExpression);
                    break;
                case IqlType.GeometryMultiPolygon:
                case IqlType.GeographyMultiPolygon:
                    typeName = "MultiPolygon";
                    coordinates = (propertyValue as IqlMultiPolygonExpression).Polygons.Select(_ => SerializePolygon(_)).ToArray();
                    break;
                case IqlType.GeometryPoint:
                case IqlType.GeographyPoint:
                    typeName = "Point";
                    var point = propertyValue as IqlPointExpression;
                    coordinates = new double[]
                    {
                        point.X,
                        point.Y
                    };
                    break;
                case IqlType.GeometryMultiPoint:
                case IqlType.GeographyMultiPoint:
                    typeName = "MultiPoint";
                    coordinates = SerializePoints(propertyValue as IqlMultiPointExpression);
                    break;
            }

            container["type"] = typeName;
            var coordinatesValue = JArray.FromObject(coordinates);
            container["coordinates"] = coordinatesValue;
        }

        private static double[][][] SerializePolygon(IqlPolygonExpression polygon)
        {
            var outerRing = polygon.OuterRing;
            var all = new List<double[][]>();
            all.Add(SerializePoints(outerRing, true));
            if (polygon.InnerRings != null)
            {
                all.AddRange(polygon.InnerRings.Select(_ => SerializePoints(_, true)));
            }

            var result = all.ToArray();
            return result;
        }

        private static double[][] SerializePoints(IPointsExpression line, bool isRing = false)
        {
            var points = line.Points.Select(point => new double[]
            {
                point.X,
                point.Y
            }).ToList();
            if (isRing && points.Count > 0)
            {
                var first = points[0];
                var last = points[points.Count - 1];
                if (points.Count == 1 || (first[0] != last[0] || first[1] != last[1]))
                {
                    points.Add(new double[] { first[0], first[1] });
                }
            }
            return points.ToArray();
        }

        public static IqlExpression ConvertODataGeographyToIqlGeography(JObject jObject, IqlType geographyType)
        {
            if (jObject == null)
            {
                return null;
            }

            if (jObject["coordinates"] == null)
            {
                return null;
            }

            var coordinates = jObject["coordinates"].ToString();

            double[][][] getPolygonPoints()
            {
                return (double[][][])jObject["coordinates"].ToObject(typeof(double[][][]));
            }
            switch (geographyType)
            {
                case IqlType.GeometryLine:
                    return new IqlLineExpression(FromODataArray(IqlType.GeometryPoint,
                        (double[][])JsonConvert.DeserializeObject(coordinates, typeof(double[][]))));
                case IqlType.GeographyLine:
                    return new IqlLineExpression(FromODataArray(IqlType.GeographyPoint,
                        (double[][])JsonConvert.DeserializeObject(coordinates, typeof(double[][]))));
                case IqlType.GeometryMultiLine:
                    throw new NotImplementedException();
                case IqlType.GeographyMultiLine:
                    throw new NotImplementedException();
                case IqlType.GeometryPolygon:
                    var arr1 = getPolygonPoints();
                    return new IqlPolygonExpression(IqlRingExpression.From(arr1[0]), arr1.Skip(1).Select(_ => IqlRingExpression.From(_)));
                case IqlType.GeographyPolygon:
                    var arr2 = getPolygonPoints();
                    return new IqlPolygonExpression(IqlRingExpression.From(arr2[0]), arr2.Skip(1).Select(_ => IqlRingExpression.From(_)));
                case IqlType.GeometryMultiPolygon:
                    throw new NotImplementedException();
                case IqlType.GeographyMultiPolygon:
                    throw new NotImplementedException();
                case IqlType.GeometryPoint:
                    var geometryPoint = (double[])JsonConvert.DeserializeObject(coordinates, typeof(double[]));
                    return new IqlPointExpression(geometryPoint[0], geometryPoint[1]);
                case IqlType.GeographyPoint:
                    var geographyPoint = (double[])JsonConvert.DeserializeObject(coordinates, typeof(double[]));
                    return new IqlPointExpression(geographyPoint[0], geographyPoint[1]);
                case IqlType.GeometryMultiPoint:
                    throw new NotImplementedException();
                case IqlType.GeographyMultiPoint:
                    throw new NotImplementedException();
            }

            return null;
        }

        private static IEnumerable<IqlPointExpression> FromODataArray(IqlType type, params double[][] coordinates)
        {
            return coordinates.Select(c => type == IqlType.GeometryPoint
                ? (IqlPointExpression)new IqlPointExpression(c[0], c[1])
                : new IqlPointExpression(c[0], c[1]));
        }
    }

    public class DeserializedEntitySets
    {
        private Dictionary<IEntityConfiguration, IList> Dictionary { get; }

        public int Count => Dictionary.Keys.Count;

        public DeserializedEntitySets(Dictionary<IEntityConfiguration, IList> dictionary)
        {
            Dictionary = dictionary;
        }

        public IList<T> Set<T>()
        {
            return Dictionary.Single(_ => _.Key.Type == typeof(T)).Value as IList<T>;
        }

        public IList<object> SetByType(Type type)
        {
            var list = Dictionary.Single(_ => _.Key.Type == type).Value;
            var newList = new List<object>();
            foreach (var item in list)
            {
                newList.Add(item);
            }
            return newList;
        }

        public Type[] Types => Dictionary.Keys.Select(_ => _.Type).ToArray();
    }

    class ParseTokenResult
    {
        public JToken Token { get; }
        public bool IsEmptyObject { get; }

        public ParseTokenResult(JToken token, bool isEmptyObject)
        {
            Token = token;
            IsEmptyObject = isEmptyObject;
        }
    }
}