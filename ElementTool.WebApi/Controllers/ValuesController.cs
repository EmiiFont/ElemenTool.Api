using System.Collections.Generic;
using System.Web.Http;

namespace ElementTool.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        public ValuesController()
        {
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public IEnumerable<string> Get(string id)
        {
            return new string[] { "value1", id }; 
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
