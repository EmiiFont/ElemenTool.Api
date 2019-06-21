using System.Web.Http;
using System.Net.Http.Headers;
using ElemenTool.CacheLayer.Entities;
using System.Web.OData.Extensions;
using System.Web.OData.Builder;
using System.Web.OData.Batch;
using Microsoft.OData.Edm;
using ElementTool.WebApi.Models;

namespace ElementTool.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
           // config.SuppressDefaultHostAuthentication();
           // config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MapODataServiceRoute("odata", "odata", GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
            config.EnsureInitialized();
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "ElementTool.WebApi";
            builder.ContainerName = "DefaultContainer";
            builder.EntitySet<Issue>("Issues");
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
