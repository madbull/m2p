using System.Web;

namespace m2pService
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();
        }
    }
}