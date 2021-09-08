using System;
using System.Web.Http;
using System.Web.Routing;

namespace nicepay
{
    public class Global : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("index", "", "~/regist.aspx", false);
            routes.MapPageRoute("response", "response", "~/response.aspx", false);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
    }
}