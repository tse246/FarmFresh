using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FarmFresh.Model;
using FarmFresh.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FarmFresh.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private IAuthenticationService _authenticationService;

        public CategoryController(ILogger<CategoryController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"];
            if (_authenticationService.ValidateJwtToken(accessToken))
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

                return Ok(categories);
            }

            return Unauthorized();
        }
    }
}
