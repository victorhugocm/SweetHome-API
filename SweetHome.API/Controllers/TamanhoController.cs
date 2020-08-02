using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetHome.API.Models;

namespace SweetHome.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TamanhoController : ControllerBase
    {
        private readonly SweetHomeContext _context;

        public TamanhoController(SweetHomeContext context)
        {
            _context = context;
        }

        // GET: api/Tamanho/Get
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Tamanho>>> GetTamanho()
        {
            return await _context.Tamanho.ToListAsync();
        }

        // GET: api/Tamanho/GetById?id=long
        [HttpGet("GetById")]
        public async Task<ActionResult<Tamanho>> GetTamanho(long id)
        {
            var tamanho = await _context.Tamanho.FindAsync(id);

            if (tamanho == null)
            {
                return NotFound();
            }

            return tamanho;
        }

        // PUT: api/Tamanho/Put
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("Put")]
        public async Task<IActionResult> PutTamanho(Tamanho tamanho)
        {
            if (!TamanhoExists(tamanho.Id))
            {
                return NotFound();
            }

            _context.Entry(tamanho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Tamanho/Post
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Post")]
        public async Task<ActionResult<Tamanho>> PostTamanho(Tamanho tamanho)
        {
            _context.Tamanho.Add(tamanho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTamanho", new { id = tamanho.Id }, tamanho);
        }

        // DELETE: api/Tamanho/Delete?id=long
        [HttpDelete("Delete")]
        public async Task<ActionResult<Tamanho>> DeleteTamanho(long id)
        {
            var tamanho = await _context.Tamanho.FindAsync(id);
            if (tamanho == null)
            {
                return NotFound();
            }

            _context.Tamanho.Remove(tamanho);
            await _context.SaveChangesAsync();

            return tamanho;
        }

        private bool TamanhoExists(long id)
        {
            return _context.Tamanho.Any(e => e.Id == id);
        }
    }
}
