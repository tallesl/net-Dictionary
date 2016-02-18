namespace DictionaryLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Makes a dictionary out of a given object.
    /// </summary>
    public static class DictionaryMaker
    {
        /// <summary>
        /// Makes a dictionary out of the properties of the given input.
        /// </summary>
        /// <param name="input">Object to make a dictionary out of</param>
        /// <returns>
        /// A dictionary with the keys being the input object property names and the values their respective values
        /// </returns>
        public static IDictionary<string, object> Make(object input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            else if (input is ExpandoObject)
                return (IDictionary<string, object>)input;

            else
            {
                var properties = input.GetType().GetProperties();
                var fields = input.GetType().GetFields();
                var members = properties.Cast<MemberInfo>().Concat(fields.Cast<MemberInfo>());
                return members.ToDictionary(m => m.Name, m => GetValue(input, m));
            }
        }

        private static object GetValue(object obj, MemberInfo member)
        {
            if (member is PropertyInfo)
                return ((PropertyInfo)member).GetValue(obj, null);

            else if (member is FieldInfo)
                return ((FieldInfo)member).GetValue(obj);

            else
                throw new ArgumentException("Passed member is neither a PropertyInfo nor a FieldInfo.");
        }
    }
}
