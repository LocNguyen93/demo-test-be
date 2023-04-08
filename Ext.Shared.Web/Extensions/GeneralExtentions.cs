namespace Ext.Shared.Web.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class GeneralExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> items, Func<T, string> keySelector, Func<T, string> valueSelector)
        {
            return items.Select(x => new SelectListItem(keySelector(x), valueSelector(x)));
        }
    }
}
