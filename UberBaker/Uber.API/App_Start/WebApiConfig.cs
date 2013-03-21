using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using Uber.Core;

namespace Uber.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.Insert(0, new JsonpFormatter { });
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            formatter.SerializerSettings = new JsonSerializerSettings()
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
                //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };


            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Product>("OProducts");
            modelBuilder.EntitySet<ProductType>("ProductTypes");
            modelBuilder.EntitySet<Order>("Orders");
            modelBuilder.EntitySet<OrderItem>("OrderItems");
            
            Microsoft.Data.Edm.IEdmModel model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute("ODataRoute", "odata", model);
        }
    }
}
