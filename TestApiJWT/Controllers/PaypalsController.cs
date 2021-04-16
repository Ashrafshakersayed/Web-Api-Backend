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
    public class PaypalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public PaypalsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: api/Paypals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaypalModel>>> GetPaypals()
        {
            var paypals = await _context.Paypals.ToListAsync();
            return _mapper.Map<PaypalModel[]>(paypals);
        }

        // GET: api/Paypals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaypalModel>> GetPaypal(int id)
        {
            var paypal = await _context.Paypals.FindAsync(id);

            if (paypal == null)
            {
                return NotFound();
            }

            return _mapper.Map<PaypalModel>(paypal);
        }

        // PUT: api/Paypals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaypal(int id, PaypalModel paypalModel)
        {
            if (id != paypalModel.Id)
            {
                return BadRequest();
            }

            var paypal = await _context.Paypals.FindAsync(id);
            _mapper.Map(paypalModel, paypal);

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
        public async Task<ActionResult<PaypalModel>> PostPaypal(PaypalModel paypalModel)
        {
            var paypal = _mapper.Map<Paypal>(paypalModel);
            _context.Paypals.Add(paypal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaypal", new { id = paypal.Id }, paypalModel);
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
