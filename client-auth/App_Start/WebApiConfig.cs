using System.Web.Http;

namespace nicepay.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "hookApi",
                routeTemplate: "hook",
                defaults: new { controller = "hook"}
            );
        }
    }
}
