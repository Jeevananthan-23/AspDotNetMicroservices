using Catalog.API.Entity;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configution)
        {
            var client = new MongoClient(configution.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database=client.GetDatabase(configution.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products= database.GetCollection<Product>(configution.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
