using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure; // Namespace for CloudConfigurationManager 
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using Microsoft.WindowsAzure;
using ElemenTool.Api.DataObjects;

namespace ElemenTool.Api.Infrastructure.AzureStorage
{
    public class AzureContext
    {
        private CloudStorageAccount _storageAccount;
        private CloudTable _table;
        public AzureContext()
        {
            _storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            // Create the table client.
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference("Account");
            _table.CreateIfNotExists();
        }
        
        public void InsertElementToolEntity(ElemenToolItem item)
        {
            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(item);

            // Execute the insert operation.
            _table.Execute(insertOperation);
        }

        public ElemenToolItem GetAccountItem(string accountName, string userName)
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<ElemenToolItem>(accountName, userName);

            // Execute the retrieve operation.
            TableResult retrievedResult = _table.Execute(retrieveOperation);

            // Print the phone number of the result.
            if (retrievedResult.Result != null)
               return (ElemenToolItem)retrievedResult.Result;
            else
               return null;
        }
    }
}