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
        private readonly CatalogContext _con;
        private readonly ProductRepo _productRepo;
        public CatalogRepositoryTests()
        {
            _con = new CatalogContext(Connection.MongoDbConnectionUri());
            _productRepo = new ProductRepo(_con);
        }

        [Fact]
        public async Task TestReturnOneProduct()
        {

            var product = await _productRepo.GetProductsByCategory("Smart Phone");
            Assert.Equal("IPhone X", product.First().Name);
        }

        [Fact]
        public void TestCountProducts()
        {
            int count = _productRepo.GetProducts().Result.Count();
            Assert.Equal(6, count);
        }

        [Fact]
        public async Task TestCreateNewProduct()
        {
            var product = new Product
            {
                Id = "602d2149e773f2a3990b47g6",
                Name = "Test 1",
                Summary = "Test 1 summary",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "Test_1.png",
                Price = 90.00M,
                Category = "Test Phone"
            };
            try
            {
                var result = _productRepo.CreateProduct(product);
                Assert.NotNull(result.Result.Name);
                var getProduct = await _productRepo.GetProductsByName(product.Name);
                Assert.Equal(result.Result.Name, getProduct.Name);
                Assert.Equal(result.Result.Price, getProduct.Price);
            }
            finally
            {
                var deleted = await _productRepo.DeleteProduct(product.Id);
                Assert.True(deleted);
            }

        }

        [Fact]
        public async Task TestUpdateNewProduct()
        {
            var product = new Product
            {
                Id = "602d2149e773f2a3990b47g6",
                Name = "Test 1",
                Summary = "Test 1 summary",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "Test_1.png",
                Price = 90.00M,
                Category = "Test Phone"
            };
            var updatedProduct = new Product
            {
                Id = "602d2149e773f2a3990b47g6",
                Name = "Test 1",
                Summary = "Test 1 summary",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "Test_1.png",
                Price = 100.00M,
                Category = "Test 1 Phone"
            };
            try
            {
               await _productRepo.CreateProduct(product);
                var result = await _productRepo.UpdateProduct(updatedProduct);
                Assert.True(result);
                var fetchedProduct = await _productRepo.GetProductsByName(product.Name);
                Assert.Equal(product.Name,fetchedProduct.Name);
                Assert.Equal(updatedProduct.Price, fetchedProduct.Price);
            }
            finally
            {
                var deleted = await _productRepo.DeleteProduct(product.Id);
                Assert.True(deleted);
            }
        }
    }
}
