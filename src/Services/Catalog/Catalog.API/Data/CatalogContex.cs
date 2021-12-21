using Catalog.API.Entity;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContex : ICatalogContex
    {
        public CatalogContex(IConfiguration configution)
        {
            var client = new MongoClient(configution.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database=client.GetDatabase(configution.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products= database.GetCollection<Product>(configution.GetValue<string>("DatabaseSettings:CollectionName"));
        }

        public IMongoCollection<Product> Products { get; }
    }
}
