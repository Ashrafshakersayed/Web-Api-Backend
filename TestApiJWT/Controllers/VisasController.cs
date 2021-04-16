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
    public class VisasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public VisasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Visas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisaModel>>> GetVisas()
        {
            var visas = await _context.Visas.ToListAsync();
            return _mapper.Map<VisaModel[]>(visas);
        }

        // GET: api/Visas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisaModel>> GetVisa(int id)
        {
            var visa = await _context.Visas.FindAsync(id);

            if (visa == null)
            {
                return NotFound();
            }

            return _mapper.Map<VisaModel>( visa );
        }

        // PUT: api/Visas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisa(int id, VisaModel visaModel)
        {
            if (id != visaModel.Id)
            {
                return BadRequest();
            }

            var visa = await _context.Visas.FindAsync(id);
            _mapper.Map(visaModel, visa);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisaExists(id))
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

        // POST: api/Visas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VisaModel>> PostVisa(VisaModel visaModel)
        {
            var visa = _mapper.Map<Visa>(visaModel);
            _context.Visas.Add(visa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisa", new { id = visa.Id }, visaModel);
        }

        // DELETE: api/Visas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisa(int id)
        {
            var visa = await _context.Visas.FindAsync(id);
            if (visa == null)
            {
                return NotFound();
            }

            _context.Visas.Remove(visa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisaExists(int id)
        {
            return _context.Visas.Any(e => e.Id == id);
        }
    }
}
