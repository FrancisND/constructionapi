﻿using System;
using System.Linq;
using System.Web.Http;


namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Web API configuration and services

            //Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{companyCode}",
                defaults: new { companyCode = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "EmployeeApi",
                routeTemplate: "api/{controller}/{employeeCode}",
                defaults: new { employeeCode = RouteParameter.Optional }
            );
        }
    }
}
