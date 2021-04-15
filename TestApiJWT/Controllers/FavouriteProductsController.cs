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
    public class FavouriteProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavouriteProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FavouriteProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavouriteProducts>>> GetFavouriteProducts()
        {
            return await _context.FavouriteProducts.ToListAsync();
        }

        // GET: api/FavouriteProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavouriteProducts>> GetFavouriteProducts(int id)
        {
            var favouriteProducts = await _context.FavouriteProducts.FindAsync(id);

            if (favouriteProducts == null)
            {
                return NotFound();
            }

            return favouriteProducts;
        }

        // PUT: api/FavouriteProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavouriteProducts(int id, FavouriteProducts favouriteProducts)
        {
            if (id != favouriteProducts.Id)
            {
                return BadRequest();
            }

            _context.Entry(favouriteProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavouriteProductsExists(id))
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

        // POST: api/FavouriteProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FavouriteProducts>> PostFavouriteProducts(FavouriteProducts favouriteProducts)
        {
            _context.FavouriteProducts.Add(favouriteProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavouriteProducts", new { id = favouriteProducts.Id }, favouriteProducts);
        }

        // DELETE: api/FavouriteProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavouriteProducts(int id)
        {
            var favouriteProducts = await _context.FavouriteProducts.FindAsync(id);
            if (favouriteProducts == null)
            {
                return NotFound();
            }

            _context.FavouriteProducts.Remove(favouriteProducts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavouriteProductsExists(int id)
        {
            return _context.FavouriteProducts.Any(e => e.Id == id);
        }
    }
}
