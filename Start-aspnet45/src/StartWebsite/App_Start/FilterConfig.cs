using System.Web.Mvc;
using Start.Utils;

namespace Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LayoutDataAttribute());
        }
    }
}