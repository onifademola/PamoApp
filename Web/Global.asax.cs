using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.App_Start;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Auto Mapper config
            MappingConfig.RegisterMaps();

            //Start SqlDependency with application initialization
            //SqlDependency.Start(connString);

            //ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();

        }

        //protected void Application_End()
        //{
        //    //Stop SQL dependency
        //    SqlDependency.Stop(connString);
        //}

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //DevExpress.Web.Mvc.DevExpressHelper.Theme = "DevEx";
        }
    }
}
