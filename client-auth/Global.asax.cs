using nicepay.App_Start;
using System;
using System.Web.Http;
using System.Web.Routing;

namespace nicepay
{
    public class Global : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("index", "", "~/index.aspx", false);
            routes.MapPageRoute("response", "response", "~/response.aspx", false);
            routes.MapPageRoute("cancel", "cancel", "~/cancel.aspx", false);
            routes.MapPageRoute("cancelResponse", "cancelResponse", "~/cancelResponse.aspx", false);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}