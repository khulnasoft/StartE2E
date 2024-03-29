﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Start.Models;

namespace Start.Api
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IStartContext _context;

        public ProductsController(IStartContext context)
        {
            _context = context;
        }

        [HttpGet, Route]
        public IEnumerable<Product> Get(bool sale = false)
        {
            if (!sale)
            {
                return _context.Products;
            }

            return _context.Products.Where(p => p.Price != p.SalePrice);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return Content(HttpStatusCode.OK, product);
        }
    }
}
