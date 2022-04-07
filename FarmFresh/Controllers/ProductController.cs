using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IAuthenticationService _authenticationService;

        public ProductController(ILogger<ProductController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Get(string productName)
        {
            var accessToken = HttpContext.Request.Headers["Authorization"];
            if (_authenticationService.ValidateJwtToken(accessToken))
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

                return Ok(productList);
            }

            return Unauthorized();
        }
    }
}
