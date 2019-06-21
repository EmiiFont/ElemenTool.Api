using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElemenTool.CacheLayer.Entities;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using ElemenTool.Api.DataObjects;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ElementTool.WebApi.DataObjects;
using Hangfire;
using System.IO;
using System.Configuration;

namespace ElementTool.WebApi.Infrastructure.DbStorage
{
    public class FirebaseStorage : ICache
    {
        private IFirebaseClient _client;
        private readonly string firebaseUrl = ConfigurationManager.AppSettings["firebaseUrl"];
        private readonly string authSecret = ConfigurationManager.AppSettings["authSecret"];
        public FirebaseStorage()
        {

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = firebaseUrl

            };
            _client = new FirebaseClient(config);
        }
        public Task<IssueDetails> AddIssueDetails(string cacheKey, IssueDetails item)
        {
            throw new NotImplementedException();
        }

        public void AddIssueList(object p, List<Issue> issuelist)
        {
            throw new NotImplementedException();
        }

        public void AddIssueList(string cacheKey, List<Issue> item)
        {
            try
            {
                BackgroundJob.Enqueue(() => File.AppendAllText(@"C:\Test\hangfire.txt", Guid.NewGuid().ToString()));
                RecurringJob.AddOrUpdate(() => File.AppendAllText(@"C:\Test\hangfire.txt", DateTime.Now.ToLongDateString()), Cron.Minutely);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public IssueDetails GetIssueDetailsFromStore(int issueNumber)
        {
            throw new NotImplementedException();
        }

        public List<Issue> GetIssueListFromStore(string accountName)
        {
            FirebaseResponse response = _client.Get("/ProcessAccount");
            JObject accounts = response.ResultAs<JObject>();
            
            if (accounts == null) return new List<Issue>();

            foreach (var item in accounts)
            {
                var d = JsonConvert.DeserializeObject<AccountProcess>(item.Value.ToString());
                if (d.AccountName == accountName)
                {
                    if(d.Status == "Completed")
                    {
                        FirebaseResponse frresponse = _client.Get("/Issues/");
                        List<Issue> issueList = frresponse.ResultAs<List<Issue>>();

                        return issueList;
                    }
                }
            }

            return new List<Issue>();
        }

        public void InsertElementToolEntity(ElemenToolItem item)
        {
            try
            {
                PushResponse response = _client.Push("/AccountItem", item);
                item.Id = response.Result.name;
                _client.Update("/AccountItem/" + item.Id, item);
                
            }
            catch (Exception ex)
            {
                //_sentryLog.Capture(new SentryEvent(ex));
            }
        }

        public void UpdateAccountItem(ElemenToolItem item)
        {
            try
            {
                FirebaseResponse response = _client.Update("/AccountItem/" + item.Id, item);
                ElemenToolItem todo = response.ResultAs<ElemenToolItem>();
               
            }
            catch (Exception ex)
            {
                //  _sentryLog.Capture(new SentryEvent(ex));
            }
        }

        public ElemenToolItem GetAccountItem(string accountName, string userName)
        {

            try
            {
                ElemenToolItem account = null;
                var fullAcc = userName + "@" + accountName;
                FirebaseResponse response = _client.Get("/AccountItem/", QueryBuilder.New().OrderBy("FullAccount").StartAt(fullAcc).EndAt(fullAcc));
                JObject todo = response.ResultAs<JObject>();

                foreach (var item in todo)
                {
                    account = JsonConvert.DeserializeObject<ElemenToolItem>(item.Value.ToString());
                    break;
                }
              return account;
            }
            catch (Exception ex)
            {
                //_sentryLog.Capture(new SentryEvent(ex));
            }

            return null;
        }

        public void AddToProcessTable(string userName, string accountName)
        {
            var process = new AccountProcess { AccountName = accountName, Status = "Incomplete" };

            FirebaseResponse response = _client.Get("/ProcessAccount");
            JObject accounts = response.ResultAs<JObject>();

            if (accounts == null)
            {
                PushResponse pushResponse = _client.Push("/ProcessAccount", process);
            }
            else
            {
                foreach (var item in accounts)
                {
                    var d = JsonConvert.DeserializeObject<AccountProcess>(item.Value.ToString());
                    if (d.AccountName != accountName)
                    {
                        PushResponse pushResponse = _client.Push("/ProcessAccount", process);
                    }
                    break;
                }
            }

        }
    }
}