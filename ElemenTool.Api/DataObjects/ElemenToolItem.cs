using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElemenTool.Api.DataObjects
{
    public class ElemenToolItem : TableEntity
    {
        public ElemenToolItem(string AccountName, string UserName)
        {
            this.PartitionKey = AccountName;
            this.RowKey = UserName;
        }

        public ElemenToolItem() { }
  
        public string AccountName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string JwtToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}