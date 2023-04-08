using System.Collections.Generic;
using System.Linq;

namespace RF.Web.Api.Extensions
{
    public static class GenericExtensions
    {
        public static T GetValue<T>(this T? value, T defaultValue = default) where T: struct
        {
            return value ?? defaultValue;
        }
        
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> value) where T: class
        {
            return value == null || !value.Any();
        }
    }
}