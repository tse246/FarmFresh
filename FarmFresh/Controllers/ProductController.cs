using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FarmFresh.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FarmFresh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Product> Get(string productName)
        {
            List<Product> productList;
            productName = string.IsNullOrWhiteSpace(productName) || productName.ToLower().Equals("null") ? string.Empty : productName;

            using (var context = new Context())
            {
                productList = (from p in context.Products
                               where p.Name.Contains(productName)
                               select new Product
                               {
                                   Name = p.Name,
                                   Detail = p.Detail,
                                   Path = p.Path
                               }).OrderBy(p => p.Name).ToList();
            }

            return productList;
        }

        [HttpGet]
        [Route("{Category:int}")]
        public List<Product> Get(int Category)
        {
            List<Product> productList;

            using (var context = new Context())
            {
                productList = (from p in context.Products
                               join pc in context.ProductCategories
                               on p.Id equals pc.ProductID
                               where pc.CategoryID.Equals(Category)
                               select new Product
                               {
                                   Name = p.Name,
                                   Detail = p.Detail,
                                   Path = p.Path
                               }).ToList();
            }

            return productList;
        }
    }
}
