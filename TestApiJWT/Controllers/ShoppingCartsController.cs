using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApiJWT.Models;

namespace TestApiJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShoppingCartsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ShoppingCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartModel>>> GetShoppingCarts()
        {
            var shopingCarts = await _context.ShoppingCarts.ToListAsync();
            return _mapper.Map<ShoppingCartModel[]>(shopingCarts);
        }

        // GET: api/ShoppingCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartModel>> GetShoppingCart(string id)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            return _mapper.Map<ShoppingCartModel>(shoppingCart);
        }

        // PUT: api/ShoppingCarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCart(string id, ShoppingCartModel shoppingCartModel)
        {
            if (id != shoppingCartModel.Id)
            {
                return BadRequest();
            }

            var ShoppingCart = await _context.ShoppingCarts.FindAsync(id);
            _mapper.Map(shoppingCartModel, ShoppingCart);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartExists(id))
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

        // POST: api/ShoppingCarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingCartModel>> PostShoppingCart(ShoppingCartModel shoppingCartModel)
        {
            var shoppingCart = _mapper.Map<ShoppingCart>(shoppingCartModel);
            _context.ShoppingCarts.Add(shoppingCart);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShoppingCartExists(shoppingCart.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShoppingCart", new { id = shoppingCart.Id }, shoppingCartModel);
        }

        // DELETE: api/ShoppingCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart(string id)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartExists(string id)
        {
            return _context.ShoppingCarts.Any(e => e.Id == id);
        }
    }
}
