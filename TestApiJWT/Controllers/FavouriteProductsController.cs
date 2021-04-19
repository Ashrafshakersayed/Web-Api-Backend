using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiJWT.Models;
using TestApiJWT.Services;

namespace TestApiJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFavouriteProductService _favouriteProductService;

        public FavouriteProductsController(ApplicationDbContext context, IMapper mapper, IFavouriteProductService favouriteProductService)
        {
            _context = context;
            _mapper = mapper;
            _favouriteProductService = favouriteProductService;
        }

        // GET: api/FavouriteProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavouriteProductsModel>>> GetFavouriteProducts()
        {
            var favouriteProducts = await _context.FavouriteProducts.ToListAsync();
            return _mapper.Map<FavouriteProductsModel[]>(favouriteProducts);
        }

        // GET: api/FavouriteProducts
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FavouriteProductsModel>>> GetFavouriteProductsByUserId(string userId)
        {
            return await _favouriteProductService.GetFavouriteProductByUserId(userId);
        }


        // GET: api/FavouriteProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavouriteProductsModel>> GetFavouriteProducts(int id)
        {
            var favouriteProducts = await _context.FavouriteProducts.FindAsync(id);

            if (favouriteProducts == null)
            {
                return NotFound();
            }

            return _mapper.Map<FavouriteProductsModel>(favouriteProducts);
        }

        // PUT: api/FavouriteProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavouriteProducts(int id, FavouriteProductsModel favouriteProductsModel)
        {
            if (id != favouriteProductsModel.Id)
            {
                return BadRequest();
            }

            var favouriteProducts = await _context.FavouriteProducts.FindAsync(id);
            _mapper.Map(favouriteProductsModel, favouriteProducts);  //Note the way that we use to map here.
            //_context.Entry(favouriteProducts).State = EntityState.Modified;

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
        public async Task<ActionResult<FavouriteProductsModel>>
            PostFavouriteProducts(FavouriteProductsModel favouriteProductsModel)
        {
            var favouriteProduct = _mapper.Map<FavouriteProducts>(favouriteProductsModel);


            if (!FavouriteProductsUniqe(favouriteProduct.productId, favouriteProduct.userId))
            {
                _context.FavouriteProducts.Add(favouriteProduct);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetFavouriteProducts", new { id = favouriteProduct.Id }, favouriteProductsModel);
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

        private bool FavouriteProductsUniqe(int prdId, string userId)
        {
            return _context.FavouriteProducts
                .Any(e => (e.productId == prdId) && (e.userId == userId));
        }
    }
}
