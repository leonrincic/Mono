using System;
using System.Collections.Generic;
using System.Configuration;
using Autofac.Integration.WebApi;
using System.Linq;
using System.Web.Http;
using Autofac;
using Example.Service;
using Example.Service.Common;

namespace ExampleWebApi.App_Start
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterType<EmployeeService>().As<IEmployeeService>();

            IContainer Container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}