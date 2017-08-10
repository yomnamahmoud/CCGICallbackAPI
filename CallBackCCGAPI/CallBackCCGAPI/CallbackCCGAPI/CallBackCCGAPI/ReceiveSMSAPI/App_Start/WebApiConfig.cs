using CallBackCCGAPILibrary.Concretes.Repositories;
using CallBackCCGAPILibrary.Concretes.Services;
using CallBackCCGAPILibrary.Contracts.Repositories;
using CallBackCCGAPILibrary.Contracts.Services;
using Microsoft.Practices.Unity;
using CallBackCCGAPI.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace CallBackCCGAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new UnityContainer();
            container.RegisterType<ILogRepository, LogRepository>(new InjectionConstructor(new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.MainSMSConnectionString].ConnectionString)));
            container.RegisterType<ILogService, LogService>();
            container.RegisterType<ICallBackCCGService, CallBackCCGService>();
            container.RegisterType<ICallBackCCGRepository, CallBackCCGRepository>(new InjectionConstructor(new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.MainSMSConnectionString].ConnectionString), container.Resolve<ILogService>()));

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
