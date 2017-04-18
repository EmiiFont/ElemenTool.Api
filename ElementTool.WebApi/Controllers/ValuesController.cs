using ElemenTool.CacheLayer.Entities;
using ElemenTool.CacheLayer.Infrastructure;
using ElementTool.WebApi.Infrastructure;
using ElementTool.WebApi.Infrastructure.DbStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElementTool.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private ElementService _elementService;
        private ICache _cacheLayer;
        private ICache _context;

        public ValuesController()
        {
            _context = new FirebaseStorage();

            _cacheLayer = new FirebaseStorage();
            _elementService = new ElementService(_cacheLayer);
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public List<Issue> Get(string id)
        {
            var username = id.Split('@')[0];
            var accountName = id.Split('@')[1];

            var item = _context.GetAccountItem(accountName, username);
            _elementService._accountItem = item;

            var result = _elementService.GetIssueList();
            return result;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
