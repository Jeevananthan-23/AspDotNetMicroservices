using Catalog.API.Controllers;
using Catalog.API.Data;
using Catalog.API.Entity;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.UnitTest
{
  public class CatalogControllerTests
    {
        private readonly CatalogController _catalogController;
        private  ILogger<CatalogController> logger;
        private IProductRepo repo;
        private CatalogContext _con;

        public CatalogControllerTests()
        {
            _con = new CatalogContext(Connection.MongoDbConnectionUri());
            repo = new ProductRepo(_con);
            _catalogController = new CatalogController(repo,logger);
        }

        [Fact]
        public async Task TestGetProductByName()
        {
            var result = await _catalogController.GetProductByName("Huawei Plus");
            var okResult= (OkObjectResult) result;
            var product = (Product)okResult.Value;
            Assert.Equal("602d2149e773f2a3990b47f7", product.Id);

            //valid Name, but no match
            result = await _catalogController.GetProductByName("Motorola");
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        [Fact]
        public async Task TestFormattedOutputFromController()
        {
            var response = await _catalogController.GetProducts();
            var jsonResult = response.Result as OkObjectResult;
            Assert.NotNull(jsonResult);
            var responseResult = (Product)jsonResult.Value ;
            Assert.NotNull(responseResult);
            Assert.Equal(6, responseResult.Id.Count());
        }
    }
}
