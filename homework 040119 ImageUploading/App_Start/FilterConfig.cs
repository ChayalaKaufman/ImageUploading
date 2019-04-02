using System.Web;
using System.Web.Mvc;

namespace homework_040119_ImageUploading
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
