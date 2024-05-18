using MySqlAccessAuthen.MySqlAccessAuthen;
using MySqlAccessAuthen.MySqlServicesAuthen;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace MySqlWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IMySqlServicesAuthen, MySqlServicesAuthen>(new HierarchicalLifetimeManager());
            container.RegisterType<IMySqlAccessAuthen, MySqlAccessAuthen.MySqlAccessAuthen.MySqlAccessAuthen>(
                new InjectionConstructor(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}
