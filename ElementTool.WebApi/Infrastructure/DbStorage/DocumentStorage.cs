using System;
using System.Collections.Generic;
using System.Net;
using ElemenTool.CacheLayer.Entities;
using System.Threading.Tasks;
using System.Configuration;
using ElemenTool.Api.DataObjects;
using SharpRaven;
using SharpRaven.Data;
using System.Linq;

namespace ElemenTool.Api.Infrastructure.DbStorage
{
    public class DocumentStorage //: ICache
    {

        //    /// <summary>
        //    /// The Azure DocumentDB endpoint for running this GetStarted sample.
        //    /// </summary>
        //    private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];

        //    /// <summary>
        //    /// The primary key for the Azure DocumentDB account.
        //    /// </summary>
        //    private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];
        //    private static readonly string sentryDSN = ConfigurationManager.AppSettings["sentryDSN"];
        //    private static readonly string databaseName = ConfigurationManager.AppSettings["DatabaseName"];
        //    private static readonly string issuesCollection = "Issues";

        //    private DocumentClient _client;
        //    private RavenClient _sentryLog;

        //    public DocumentStorage()
        //    {
        //        // Connect to the DocumentDB Emulator running locally
        //        _client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        //        _sentryLog = new RavenClient(sentryDSN);

        //    }
        //    public Task<IssueDetails> AddIssueDetails(string cacheKey, IssueDetails item)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void AddIssueList(object p, List<Issue> issuelist)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public async void AddIssueList(string cacheKey, List<Issue> items)
        //    {
        //        foreach (var item in items)
        //        {
        //            try
        //            {
        //                await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, issuesCollection), item);
        //            }
        //            catch (Exception ex)
        //            {
        //                _sentryLog.Capture(new SentryEvent(ex));
        //            }
        //        }
        //    }

        //    public IssueDetails GetIssueDetailsFromStore(int issueNumber)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public List<Issue> GetIssueListFromStore()
        //    {
        //        // Set some common query options
        //        FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

        //        // Run a simple query via LINQ. DocumentDB indexes all properties, so queries can be completed efficiently and with low latency.
        //        // Here we find the Andersen family via its LastName
        //        IQueryable<Issue> issueQuery = _client.CreateDocumentQuery<Issue>(
        //            UriFactory.CreateDocumentCollectionUri(databaseName, issuesCollection), queryOptions);

        //        var allIssues = issueQuery.ToList();

        //        return allIssues;
        //    }

        //    private async Task CreateIssueDocumentIfNotExists(string databaseName, string collectionName, ElemenToolItem elementoolItem)
        //    {

        //    }

        //    /// <summary>
        //    /// Create a collection with the specified name if it doesn't exist.
        //    /// </summary>
        //    /// <param name="databaseName">The name/ID of the database.</param>
        //    /// <param name="collectionName">The name/ID of the collection.</param>
        //    /// <returns>The Task for asynchronous execution.</returns>
        //    public void CreateDocumentCollectionIfNotExists(string databaseName, string collectionName)
        //    {
        //        try
        //        {
        //            DocumentCollection collectionInfo = new DocumentCollection();
        //            collectionInfo.Id = collectionName;

        //            // Optionally, you can configure the indexing policy of a collection. Here we configure collections for maximum query flexibility 
        //            // including string range queries. 
        //            collectionInfo.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });


        //            var d = _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseName),
        //                    new DocumentCollection { Id = collectionName },
        //                    new RequestOptions { OfferThroughput = 400 }).Result;
        //        }
        //        catch (Exception de)
        //        {
        //            // If the document collection does not exist, create a new collection
        //            _sentryLog.Capture(new SentryEvent(de));
        //        }
        //    }

        //    /// <summary>
        //    /// Create a database with the specified name if it doesn't exist. 
        //    /// </summary>
        //    /// <param name="databaseName">The name/ID of the database.</param>
        //    /// <returns>The Task for asynchronous execution.</returns>
        //    public void CreateDatabaseIfNotExists(string databaseName)
        //    {
        //        // Check to verify a database with the id=FamilyDB does not exist
        //        try
        //        {
        //            var d =_client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName }).Result;
        //        }
        //        catch (DocumentClientException de)
        //        {
        //            // If the database does not exist, create a new database
        //            if (de.StatusCode == HttpStatusCode.NotFound)
        //            {
        //                 _client.CreateDatabaseAsync(new Database { Id = databaseName });
        //            }
        //            else
        //            {
        //                _sentryLog.Capture(new SentryEvent(de));
        //            }
        //        }
        //    }

        //    public Task<ElemenToolItem> GetAccountItem(string accountName, string userName)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void InsertElementToolEntity(ElemenToolItem item)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void UpdateAccountItem(ElemenToolItem item)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    List<Issue> ICache.GetIssueListFromStore(string accountName)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void AddToProcessTable(string userName, string accountName)
        //    {
        //        throw new NotImplementedException();
        //    }

        //}
    }
}