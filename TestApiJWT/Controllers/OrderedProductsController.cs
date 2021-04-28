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
    public class OrderedProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderedProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OrderedProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderedProductsModel>>> GetOrderedProducts()
        {
            var orderedProducts = await _context.OrderedProducts.ToListAsync();
            return _mapper.Map<OrderedProductsModel[]>(orderedProducts);
        }

        // GET: api/OrderedProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderedProductsModel>> GetOrderedProducts(int id)
        {
            var orderedProducts = await _context.OrderedProducts.FindAsync(id);

            if (orderedProducts == null)
            {
                return NotFound();
            }

            return _mapper.Map<OrderedProductsModel>(orderedProducts);
        }

        // PUT: api/OrderedProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderedProducts(int id, OrderedProductsModel orderedProductsModel)
        {
            if (id != orderedProductsModel.Id)
            {
                return BadRequest();
            }
            var orderedProducts = await _context.OrderedProducts.FindAsync(id);
            _mapper.Map(orderedProductsModel, orderedProducts);  //Note the way that we use to map here.
            //_context.Entry(orderedProducts).State = EntityState.Modified;

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
        public async Task<ActionResult<OrderedProducts>> PostOrderedProducts(OrderedProductsModel orderedProductsModel)
        {
            var orderedProduct= _mapper.Map<OrderedProducts>(orderedProductsModel);
            _context.OrderedProducts.Add(orderedProduct);

            var prd = _context.Products.FirstOrDefault(p=>p.Id==orderedProduct.productId);
            prd.Quantity -= orderedProduct.Quantity;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderedProducts", new { id = orderedProduct.Id }, orderedProductsModel);
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

        [HttpGet, Route("Order/{id}")]
        public ActionResult<IEnumerable<OrderedProductsModel>> GetOrderedProductsByOrderId(int id)
        {
            var orderedProducts = _context.OrderedProducts.Where(op => op.orderId == id);
            if (orderedProducts == null)
            {
                return NotFound();
            }
            return _mapper.Map<OrderedProductsModel[]>(orderedProducts);
        }

        private bool OrderedProductsExists(int id)
        {
            return _context.OrderedProducts.Any(e => e.Id == id);
        }
    }
}
