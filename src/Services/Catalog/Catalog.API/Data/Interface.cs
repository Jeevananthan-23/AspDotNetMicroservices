using Catalog.API.Entity;
using MongoDB.Driver;

namespace Catalog.API.Data
{
   public interface ICatalogContex
    {
        IMongoCollection<Product> Products { get; }
    }

}
