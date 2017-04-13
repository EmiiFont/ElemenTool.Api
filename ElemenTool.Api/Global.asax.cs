using ElemenTool.Api.Infrastructure.DbStorage;
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Routing;

namespace ElemenTool.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register();

        }

        private void InitializeDatabase()
        {
            string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];
            string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
            string databaseName = ConfigurationManager.AppSettings["DatabaseName"];

            var documentDb = new DocumentStorage();

            documentDb.CreateDatabaseIfNotExists(databaseName);
            documentDb.CreateDocumentCollectionIfNotExists(databaseName, "ElementAccount");
            documentDb.CreateDocumentCollectionIfNotExists(databaseName, "Issues");
        }
    }
}