using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElemenTool.Api.DataObjects
{
    public class ElemenToolItem : TableEntity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
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