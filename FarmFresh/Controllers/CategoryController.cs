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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            List<Category> categories;

            using (var tx = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                using (var context = new Context())
                {
                    categories = (from c in context.Category
                                  select new Category
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      Description = c.Description,
                                      Value = c.Value
                                  }).OrderBy(x => x.Value).ToList();
                }
            }

            return categories;
        }
    }
}
