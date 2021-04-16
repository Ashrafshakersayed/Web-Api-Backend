﻿using System;
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
    public class ShoppingCartProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public ShoppingCartProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ShoppingCartProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartProductsModel>>> GetShoppingCartProducts()
        {
            var ShoppingCartProducts = await _context.ShoppingCartProducts.ToListAsync();
            return _mapper.Map<ShoppingCartProductsModel[]>(ShoppingCartProducts);
        }

        // GET: api/ShoppingCartProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartProductsModel>> GetShoppingCartProducts(int id)
        {
            var shoppingCartProducts = await _context.ShoppingCartProducts.FindAsync(id);

            if (shoppingCartProducts == null)
            {
                return NotFound();
            }

            return _mapper.Map<ShoppingCartProductsModel>(shoppingCartProducts);
        }

        // PUT: api/ShoppingCartProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartProducts(int id, ShoppingCartProductsModel shoppingCartProductsModel)
        {
            if (id != shoppingCartProductsModel.Id)
            {
                return BadRequest();
            }

            var shoppingCartProducts = await _context.ShoppingCartProducts.FindAsync(id);
            _mapper.Map(shoppingCartProductsModel, shoppingCartProducts);

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
        public async Task<ActionResult<ShoppingCartProductsModel>> PostShoppingCartProducts(ShoppingCartProductsModel shoppingCartProductsModel)
        {
            var shoppingCartProducts = _mapper.Map<ShoppingCartProducts>(shoppingCartProductsModel);
            _context.ShoppingCartProducts.Add(shoppingCartProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingCartProducts", new { id = shoppingCartProducts.Id }, shoppingCartProductsModel);
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