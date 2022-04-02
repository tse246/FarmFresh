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
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Product> Get(string productName)
        {
            List<Product> productList;
            productName = string.IsNullOrWhiteSpace(productName) ? string.Empty : productName;

            using (var tx = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                using (var context = new Context())
                {
                    productList = (from p in context.Products
                                   where p.Name.Contains(productName)
                                   select new Product
                                   {
                                       Name = p.Name,
                                       Detail = p.Detail,
                                       Path = p.Path
                                   }).ToList();
                }
            }

            return productList;
        }

        
    }
}
