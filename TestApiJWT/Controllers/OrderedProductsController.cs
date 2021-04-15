using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApiJWT.Models;

namespace TestApiJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderedProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderedProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderedProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderedProducts>>> GetOrderedProducts()
        {
            return await _context.OrderedProducts.ToListAsync();
        }

        // GET: api/OrderedProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderedProducts>> GetOrderedProducts(int id)
        {
            var orderedProducts = await _context.OrderedProducts.FindAsync(id);

            if (orderedProducts == null)
            {
                return NotFound();
            }

            return orderedProducts;
        }

        // PUT: api/OrderedProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderedProducts(int id, OrderedProducts orderedProducts)
        {
            if (id != orderedProducts.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderedProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderedProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderedProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderedProducts>> PostOrderedProducts(OrderedProducts orderedProducts)
        {
            _context.OrderedProducts.Add(orderedProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderedProducts", new { id = orderedProducts.Id }, orderedProducts);
        }

        // DELETE: api/OrderedProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderedProducts(int id)
        {
            var orderedProducts = await _context.OrderedProducts.FindAsync(id);
            if (orderedProducts == null)
            {
                return NotFound();
            }

            _context.OrderedProducts.Remove(orderedProducts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderedProductsExists(int id)
        {
            return _context.OrderedProducts.Any(e => e.Id == id);
        }
    }
}
