using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // add login required to use the site
            filters.Add(new AuthorizeAttribute());
            //add https required - remove access to old port without ssl
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
