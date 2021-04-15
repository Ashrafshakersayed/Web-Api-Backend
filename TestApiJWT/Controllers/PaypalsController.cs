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
    public class PaypalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaypalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Paypals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paypal>>> GetPaypals()
        {
            return await _context.Paypals.ToListAsync();
        }

        // GET: api/Paypals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paypal>> GetPaypal(int id)
        {
            var paypal = await _context.Paypals.FindAsync(id);

            if (paypal == null)
            {
                return NotFound();
            }

            return paypal;
        }

        // PUT: api/Paypals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaypal(int id, Paypal paypal)
        {
            if (id != paypal.Id)
            {
                return BadRequest();
            }

            _context.Entry(paypal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaypalExists(id))
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

        // POST: api/Paypals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paypal>> PostPaypal(Paypal paypal)
        {
            _context.Paypals.Add(paypal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaypal", new { id = paypal.Id }, paypal);
        }

        // DELETE: api/Paypals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaypal(int id)
        {
            var paypal = await _context.Paypals.FindAsync(id);
            if (paypal == null)
            {
                return NotFound();
            }

            _context.Paypals.Remove(paypal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaypalExists(int id)
        {
            return _context.Paypals.Any(e => e.Id == id);
        }
    }
}
