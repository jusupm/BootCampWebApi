using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Example.Model;
using Example.Model.Common;
using Example.Repository;
using Example.Repository.Common;
using Example.Service;
using Example.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Example.WebApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PhoneStoreRepository>().As<IPhoneStoreRepository>();
            builder.RegisterType<PhoneStoreService>().As<IPhoneStoreService>();
            builder.RegisterType<PhoneStore>().As<IPhoneStore>();

            IContainer container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
