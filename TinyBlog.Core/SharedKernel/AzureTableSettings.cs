using System;
using System.Collections.Generic;
using System.Text;

namespace TinyBlog.Core.SharedKernel
{
    //
    // Reference: https://tahirnaushad.com/2017/08/29/azure-table-storage-in-asp-net-core-2-0/
    public class AzureTableSettings
    {
        public AzureTableSettings(string storageAccount, string storageKey, string tableName)
        {
            if (string.IsNullOrEmpty(storageAccount))
                throw new ArgumentNullException("StorageAccount");

            if (string.IsNullOrEmpty(storageKey))
                throw new ArgumentNullException("StorageKey");

            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("TableName");

            this.StorageAccount = storageAccount;
            this.StorageKey = storageKey;
            this.TableName = tableName;
        }

        public string StorageAccount { get; }
        public string StorageKey { get; }
        public string TableName { get; }
    }
}
