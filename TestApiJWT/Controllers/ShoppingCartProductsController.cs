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
    public class ShoppingCartProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingCartProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartProducts>>> GetShoppingCartProducts()
        {
            return await _context.ShoppingCartProducts.ToListAsync();
        }

        // GET: api/ShoppingCartProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartProducts>> GetShoppingCartProducts(int id)
        {
            var shoppingCartProducts = await _context.ShoppingCartProducts.FindAsync(id);

            if (shoppingCartProducts == null)
            {
                return NotFound();
            }

            return shoppingCartProducts;
        }

        // PUT: api/ShoppingCartProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartProducts(int id, ShoppingCartProducts shoppingCartProducts)
        {
            if (id != shoppingCartProducts.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCartProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartProductsExists(id))
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

        // POST: api/ShoppingCartProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingCartProducts>> PostShoppingCartProducts(ShoppingCartProducts shoppingCartProducts)
        {
            _context.ShoppingCartProducts.Add(shoppingCartProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCartProducts", new { id = shoppingCartProducts.Id }, shoppingCartProducts);
        }

        // DELETE: api/ShoppingCartProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartProducts(int id)
        {
            var shoppingCartProducts = await _context.ShoppingCartProducts.FindAsync(id);
            if (shoppingCartProducts == null)
            {
                return NotFound();
            }

            _context.ShoppingCartProducts.Remove(shoppingCartProducts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartProductsExists(int id)
        {
            return _context.ShoppingCartProducts.Any(e => e.Id == id);
        }
    }
}
