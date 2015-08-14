namespace PropertiesHash
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

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

            return toHash is ExpandoObject ?
                (IDictionary<string, object>)toHash :
                toHash.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(toHash, null));
        }
    }
}
