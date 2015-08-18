namespace PropertiesHash
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Makes a property hash out of an object.
    /// </summary>
    public static class PropertiesHasher
    {
        /// <summary>
        /// Makes a dictionary out of the properties of the given object.
        /// </summary>
        /// <param name="toHash">Object to make a property hash out of</param>
        /// <returns>
        /// A dictionary with the keys being the given object property names and the values their respective values
        /// </returns>
        public static IDictionary<string, object> Make(object toHash)
        {
            if (toHash == null) throw new ArgumentNullException("toHash");
            else if (toHash is ExpandoObject) return (IDictionary<string, object>)toHash;
            else
            {
                var properties = toHash.GetType().GetProperties();
                var fields = toHash.GetType().GetFields();
                var members = properties.Cast<MemberInfo>().Concat(fields.Cast<MemberInfo>());
                return members.ToDictionary(m => m.Name, m => GetValue(toHash, m));
            }
        }

        private static object GetValue(object obj, MemberInfo member)
        {
            if (member is PropertyInfo) return ((PropertyInfo)member).GetValue(obj, null);
            else if (member is FieldInfo) return ((FieldInfo)member).GetValue(obj);
            else throw new ArgumentException("Passed member is neither a PropertyInfo nor a FieldInfo.");
        }
    }
}
