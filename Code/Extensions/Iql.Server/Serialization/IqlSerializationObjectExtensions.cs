using System;
using System.Collections;
using Iql.Extensions;

namespace Iql.Server.Serialization
{
    public static class IqlSerializationObjectExtensions
    {
        public static void SetValueAtPropertyPath(this object @object, string propertyPath, object value)
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
                            (obj as IList)[index] = value;
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
                        @object.SetPropertyValueByName(part, value);
                    }
                    else
                    {
                        @object = @object.GetPropertyValueByName(part);
                    }
                }
            }
        }

    }
}