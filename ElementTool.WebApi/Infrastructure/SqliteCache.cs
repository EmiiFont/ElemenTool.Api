using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElemenTool.CacheLayer.Entities;
using ElemenTool.Api.DataObjects;
using ElementTool.WebApi.DataObjects;

namespace ElemenTool.CacheLayer.Infrastructure
{
    public class SqliteCache //: ICache
    {
    //    public async Task<IssueDetails> AddIssueDetails(string cacheKey, IssueDetails item)
    //    {
    //        var conn = new SQLiteAsyncConnection("foofoo.db");
               
    //        await conn.CreateTableAsync<IssueDetails>();
                
    //        var primaryKey = await conn.InsertOrReplaceAsync(item);
               
    //        return await conn.GetAsync<IssueDetails>(primaryKey);
    //    }

    //    public void AddIssueList(object p, List<Issue> issuelist)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<List<Issue>> AddIssueList(string cacheKey, List<Issue> item)
    //    {
    //        var conn = new SQLiteAsyncConnection("foofoo.db");
            
    //        await conn.CreateTableAsync<Issue>();
            
    //        await conn.InsertAllAsync(item);
            
    //        return await conn.Table<Issue>().ToListAsync();
    //    }

    //    public void AddToProcessTable(string userName, string accountName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<ElemenToolItem> GetAccountItem(string accountName, string userName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IssueDetails GetIssueDetailsFromStore(int issueNumber)
    //    {
    //        using (var conn = new SQLiteConnection("foofoo.db"))
    //        {
    //            conn.CreateTable<IssueDetails>();
    //            return conn.Table<IssueDetails>().FirstOrDefault(c=> c.IssueNumber == issueNumber);
    //        }
    //    }

    //    public List<Issue> GetIssueListFromStore()
    //    {
    //        using (var conn = new SQLiteConnection("foofoo.db"))
    //        {
    //            conn.CreateTable<Issue>();
    //            return conn.Table<Issue>().ToList();
    //        }
    //    }

    //    public void InsertElementToolEntity(ElemenToolItem item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateAccountItem(ElemenToolItem item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    void ICache.AddIssueList(string cacheKey, List<Issue> item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    List<Issue> ICache.GetIssueListFromStore(string accountName)
    //    {
    //        throw new NotImplementedException();
    //    }
    }
}
