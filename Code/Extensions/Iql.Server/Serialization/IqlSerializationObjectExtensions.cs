using System;
using System.Collections;
using Iql.Extensions;

namespace Iql.Server.Serialization
{
    public static class IqlSerializationObjectExtensions
    {
        public static void SetValueAtPropertyPath(this object @object, string propertyPath, object value)
        {
            @object.ValueAtPropertyPath(propertyPath, value, true);
        }

        public static object GetValueAtPropertyPath(this object @object, string propertyPath)
        {
            return @object.ValueAtPropertyPath(propertyPath, null, false);
        }

        public static T GetValueAtPropertyPathAs<T>(this object @object, string propertyPath)
        {
            return (T)@object.GetValueAtPropertyPath(propertyPath);
        }

        private static object ValueAtPropertyPath(this object @object, string propertyPath, object value, bool set)
        {
            var path = propertyPath.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < path.Length; i++)
            {
                var part = path[i];
                var isSet = i == path.Length - 1;
                var bracketIndex = part.IndexOf('[');
                if (bracketIndex != -1)
                {
                    var member = part.Substring(0, bracketIndex);
                    var index = Convert.ToInt32(part.Substring(bracketIndex + 1, part.Length - bracketIndex - 2));
                    var obj = @object.GetPropertyValueByName(member);
                    if (obj is IList)
                    {
                        if (isSet)
                        {
                            if (set)
                            {
                                (obj as IList)[index] = value;
                            }

                            return (obj as IList)[index];
                        }
                        else
                        {
                            @object = (obj as IList)[index];
                        }
                    }
                    else
                    {
                        if (isSet)
                        {
                            throw new NotImplementedException();
                        }

                        var enumerable = (obj as IEnumerable);
                        var j = 0;
                        foreach (var item in enumerable)
                        {
                            if (j == index)
                            {
                                @object = item;
                                break;
                            }

                            j++;
                        }
                    }
                }
                else
                {
                    if (isSet)
                    {
                        if (set)
                        {
                            @object.SetPropertyValueByName(part, value);
                        }

                        return @object.GetPropertyValueByName(part);
                    }

                    @object = @object.GetPropertyValueByName(part);
                }
            }

            return null;
        }

    }
}