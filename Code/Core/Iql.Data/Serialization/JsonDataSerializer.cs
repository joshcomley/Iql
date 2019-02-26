using System;
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
                        (SerializedEntitySet[]) JsonConvert.DeserializeObject(json, typeof(SerializedEntitySet[]));
                    foreach (var set in deserialized)
                    {
                        var entityConfiguration = builder.GetEntityByTypeName(set.Type);
                        var genericList = ListExtensions.NewGenericList(entityConfiguration.Type);
                        foreach (var entity in set.Entities)
                        {
                            var parsed = ParseEntityInternal(entity, entityConfiguration, false);
                            var entityResult = parsed.ToObject(entityConfiguration.Type);
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
            params IPropertyState[] properties)
        {
            var all = new List<JObject>();
            foreach (var entity in entities)
            {
                all.Add(SerializeInternal(entityConfiguration, entity, properties));
            }
            return all;
        }

        public static JObject PrepareEntityForSerialization(object entity,
            IEntityConfiguration entityConfiguration,
            params IPropertyState[] properties)
        {
            var obj = SerializeInternal(entityConfiguration, entity, properties);
            return obj;
        }

        public static string SerializeEntityToJson(object entity,
            IEntityConfiguration entityConfigurationBuilder,
            params IPropertyState[] properties)
        {
            return PrepareEntityForSerialization(entity, entityConfigurationBuilder, properties).ToString();
        }

        public static DeserializeCollectionResult<T> DeserializeCollection<T>(string json,
            IEntityConfiguration entityConfiguration)
        {
            var odataResultRoot = JObject.Parse(json);
            ParseEntityInternal(odataResultRoot, entityConfiguration, true);
            return new DeserializeCollectionResult<T>(entityConfiguration, odataResultRoot);
        }

        public static DeserializeSingleResult<T> DeserializeEntity<T>(string json,
            IEntityConfiguration entityConfiguration)
        {
            var odataResultRoot = JObject.Parse(json);
            ParseEntityInternal(odataResultRoot, entityConfiguration, false);
            return new DeserializeSingleResult<T>(entityConfiguration, odataResultRoot);
        }

        public static void ParseSerializedValue(JToken jvalue, IEntityConfiguration entityType)
        {
            ParseEntityInternal(jvalue, entityType, false);
        }

        private static JToken ParseEntityInternal(JToken jvalue, IEntityConfiguration entityType, bool isCollectionRoot, IProperty property = null)
        {
            if (property != null)
            {
                if (property.TypeDefinition.Kind.IsGeographic())
                {
                    return jvalue as JObject == null ? null : JObject.FromObject(JsonDataSerializer.ConvertODataGeographyToIqlGeography(jvalue as JObject, property.TypeDefinition.Kind));
                }
            }
            if (jvalue is JArray)
            {
                foreach (var child in (JArray)jvalue)
                {
                    ParseEntityInternal(child, entityType, isCollectionRoot);
                }
            }
            else if (jvalue is JObject)
            {
                var jobj = (JObject)jvalue;
                foreach (var prop in jobj.Properties().ToArray())
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
                        jobj[before + odataName] = value;
                        jobj.Remove(prop.Name);
                    }

                    if (!isCollectionRoot && entityType != null)
                    {
                        var entityProperty = entityType.Properties.SingleOrDefault(p => p.PropertyName == prop.Name);
                        if (entityProperty != null)
                        {
                            if (entityProperty.Kind == PropertyKind.Relationship)
                            {
                                jobj[prop.Name] = ParseEntityInternal(value, entityProperty.Relationship.OtherEnd.EntityConfiguration, false, entityProperty);
                            }
                            else
                            {
                                jobj[prop.Name] = ParseEntityInternal(value, entityType, false, entityProperty);
                            }
                        }
                    }
                }

                if (isCollectionRoot)
                {
                    var collection = jobj["value"] as JArray;
                    foreach (var entity in collection)
                    {
                        ParseEntityInternal(entity, entityType, false);
                    }
                }
            }

            return jvalue;
        }

        private static JObject SerializeInternal(
            IEntityConfiguration entityConfiguration, 
            object entity, 
            IEnumerable<IPropertyState> properties)
        {
            var obj = new JObject();
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
            foreach (var property in propertyChanges)
            {
                var propertyValue = entity.GetPropertyValue(property.Property);
                if (property.Property.Kind.HasFlag(PropertyKind.Count) || property.Property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    continue;
                }

                if (propertyValue != null)
                {
                    if (property.Property.TypeDefinition.Kind.IsGeographic())
                    {
                        SerializeGeography(obj, property, propertyValue);
                        continue;
                    }
                }

                if (property.Property.TypeDefinition.IsCollection)
                {
                    obj[property.Property.Name] = new JArray(propertyValue);
                }
                else
                {
                    if (property.Property.TypeDefinition.Type == typeof(DateTimeOffset))
                    {
                        if (propertyValue == null && !property.Property.TypeDefinition.Nullable)
                        {
                            obj[property.Property.Name] = "0001-01-01T00:00:00.0+00:00";
                        }
                        else
                        {
                            obj[property.Property.Name] = new JValue(((DateTimeOffset)propertyValue).NormalizeDate());
                        }
                    }
                    else if (property.Property.TypeDefinition.ConvertedFromType == KnownPrimitiveTypes.Guid && !property.Property.TypeDefinition.Nullable && propertyValue == null)
                    {
                        obj[property.Property.Name] = "00000000-0000-0000-0000-000000000000";
                    }
                    else if (property.Property.TypeDefinition.Kind == IqlType.Enum)
                    {
                        if (propertyValue == null && property.Property.Nullable == false)
                        {
                            propertyValue = 0;
                        }

                        var value = propertyValue;
                        if (value != null)
                        {
#if !TypeScript
                            var enumUnderlyingType = property.Property.TypeDefinition.Type.GetEnumUnderlyingType();
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
                        obj[property.Property.Name] = new JValue(value);
                    }
                    else
                    {
                        obj[property.Property.Name] = new JValue(propertyValue);
                    }
                }
            }
            foreach (var key in entityConfiguration.Key.Properties)
            {
                if (propertyChanges.Any(p => p.Property.Name == key.Name))
                {
                    continue;
                }
                obj[key.Name] = new JValue(entity.GetPropertyValueByName(key.Name));
            }
            return obj;
        }

        private static void SerializeGeography(
            JObject obj,
            IPropertyState property,
            object propertyValue)
        {
            if (propertyValue == null)
            {
                return;
            }
            var container = new JObject();
            obj[property.Property.Name] = container;
            string typeName = "";
            object coordinates = null;
            switch (property.Property.TypeDefinition.Kind)
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
}