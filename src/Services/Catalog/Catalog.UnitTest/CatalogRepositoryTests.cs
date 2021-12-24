using Catalog.API.Data;
using Catalog.API.Entity;
using Catalog.API.Repositories;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.UnitTest
{
    public class CatalogRepositoryTests
    {
        CatalogContext con = new CatalogContext(Connection.MongoDbConnectionUri());

        [Fact]
        public async Task TestReturnOneProduct()
        { 
            ProductRepo productRepo = new ProductRepo(con);
            var product = await productRepo.GetProductsByCategory("Smart Phone");
            Assert.Equal("IPhone X", product.First().Name);
        }

        [Fact]
        public void TestCountProducts()
        {
            IProductRepo productRepo = new ProductRepo(con);
            var count = productRepo.GetProducts().Result.Count();
            Assert.Equal(6, count);
        }

    }
}
