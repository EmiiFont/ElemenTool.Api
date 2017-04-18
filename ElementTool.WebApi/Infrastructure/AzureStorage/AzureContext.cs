using System;
using System.Linq;
using ElemenTool.Api.DataObjects;
using ElemenTool.Api.Infrastructure.DbStorage;
using System.Configuration;
using SharpRaven;
using SharpRaven.Data;

namespace ElemenTool.Api.Infrastructure.AzureStorage
{
    public class AzureContext
    {
      
        private DocumentStorage _documentDb;
        /// <summary>
        /// The Azure DocumentDB endpoint for running this GetStarted sample.
        /// </summary>
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];
        /// <summary>
        /// The primary key for the Azure DocumentDB account.
        /// </summary>
        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
        private static readonly string databaseName = ConfigurationManager.AppSettings["DatabaseName"];
        private static readonly string sentryDSN = ConfigurationManager.AppSettings["sentryDSN"];

        private static readonly string accountCollection = "ElementAccount";
        private static readonly string issuesCollection = "Issues";

        //private DocumentClient _client;
        private RavenClient _sentryLog;

        public AzureContext()
        {
            //// Create the table client.
            //_client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
            //_documentDb = new DocumentStorage();

            ////TODO: Run this on application start
            //_documentDb.CreateDatabaseIfNotExists(databaseName);
            //_documentDb.CreateDocumentCollectionIfNotExists(databaseName, accountCollection);
            //_documentDb.CreateDocumentCollectionIfNotExists(databaseName, issuesCollection);
            //_sentryLog = new RavenClient(sentryDSN);
        }

        //public void InsertElementToolEntity(ElemenToolItem item)
        //{
        //    try
        //    {
        //        // Set some common query options
        //        FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

        //        // Run a simple query via LINQ. DocumentDB indexes all properties, so queries can be completed efficiently and with low latency.
        //        IQueryable<ElemenToolItem> elementToolQuery = _client.CreateDocumentQuery<ElemenToolItem>(
        //            UriFactory.CreateDocumentCollectionUri(databaseName, accountCollection), queryOptions)
        //            .Where(f => f.FullAccount == item.FullAccount);

        //        var existList = elementToolQuery.ToList();

        //        if (existList.Count == 0)
        //        {
        //            var d = _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, accountCollection), item).Result;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _sentryLog.Capture(new SentryEvent(ex));
        //    }
        //}

        //public void UpdateAccountItem(ElemenToolItem item)
        //{

        //    try
        //    {
        //       var d = _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseName, accountCollection, item.FullAccount), item).Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _sentryLog.Capture(new SentryEvent(ex));
        //    }
        //}

        //public ElemenToolItem GetAccountItem(string accountName, string userName)
        //{

        //    try
        //    {
        //        var id = userName + "@" + accountName;
        //        // Set some common query options
        //        FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

        //        // Run a simple query via LINQ. DocumentDB indexes all properties, so queries can be completed efficiently and with low latency.
        //        IQueryable<ElemenToolItem> elementToolQuery = _client.CreateDocumentQuery<ElemenToolItem>(
        //            UriFactory.CreateDocumentCollectionUri(databaseName, accountCollection), queryOptions)
        //            .Where(f => f.FullAccount == id);

        //        var existList = elementToolQuery.ToList();

        //        return existList.FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        _sentryLog.Capture(new SentryEvent(ex));
        //    }

        //    return null;
        //}
    }
}