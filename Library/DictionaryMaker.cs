namespace DictionaryLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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

            if (input is ExpandoObject)
                return (IDictionary<string, object>)input;

            var properties = input.GetType().GetProperties();
            var fields = input.GetType().GetFields();
            var members = properties.Cast<MemberInfo>().Concat(fields.Cast<MemberInfo>());
            return members.ToDictionary(m => m.Name, m => GetValue(input, m));
        }

        /// <summary>
        /// Makes a dictionary out of the properties of the given input.
        /// </summary>
        /// <param name="input">Object to make a dictionary out of</param>
        /// <returns>
        /// A dictionary with the keys being the input object property names and the values their respective types and
        /// values
        /// </returns>
        public static IDictionary<string, Tuple<Type, object>> MakeWithType(object input)
        {
            if (input is ExpandoObject)
                return ((IDictionary<string, object>)input).ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value == null ?
                        new Tuple<Type, object>(typeof(object), kvp.Value) :
                        new Tuple<Type, object>(kvp.Value.GetType(), kvp.Value)
                );

            var dict = new Dictionary<string, Tuple<Type, object>>();

            foreach (var property in input.GetType().GetProperties())
                dict.Add(property.Name, new Tuple<Type, object>(property.PropertyType, property.GetValue(input, null)));

            foreach (var field in input.GetType().GetFields())
                dict.Add(field.Name, new Tuple<Type, object>(field.FieldType, field.GetValue(input)));

            return dict;
        }

        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "It's necessary.")]
        private static object GetValue(object obj, MemberInfo member)
        {
            if (member is PropertyInfo)
                return ((PropertyInfo)member).GetValue(obj, null);

            if (member is FieldInfo)
                return ((FieldInfo)member).GetValue(obj);

            throw new ArgumentException("Passed member is neither a PropertyInfo nor a FieldInfo.");
        }
    }
}
