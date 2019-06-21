using Newtonsoft.Json;
using System;

namespace ElementTool.WebApi.DataObjects
{
    public class ElemenToolItem 
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string FullAccount { get; set; }
        public string AccountName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string JwtToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}