using System.Web;
using System.Web.Mvc;

namespace SealPhoneAngular2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new FilterAuth());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
