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
    public class VendedorController : ControllerBase
    {
        private readonly SweetHomeContext _context;

        public VendedorController(SweetHomeContext context)
        {
            _context = context;
        }

        // GET: api/Vendedor/Get
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Vendedor>>> GetVendedor()
        {
            return await _context.Vendedor.ToListAsync();
        }

        // GET: api/Vendedor/GetById?id=long
        [HttpGet("GetById")]
        public async Task<ActionResult<Vendedor>> GetVendedor(long id)
        {
            var vendedor = await _context.Vendedor.FindAsync(id);

            if (vendedor == null)
            {
                return NotFound();
            }

            return vendedor;
        }

        // PUT: api/Vendedor/Put
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("Put")]
        public async Task<IActionResult> PutVendedor(Vendedor vendedor)
        {
            if (!VendedorExists(vendedor.Id))
            {
                return NotFound();
            }

            _context.Entry(vendedor).State = EntityState.Modified;

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

        // POST: api/Vendedor/Post
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Post")]
        public async Task<ActionResult<Vendedor>> PostVendedor(Vendedor vendedor)
        {
            _context.Vendedor.Add(vendedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendedor", new { id = vendedor.Id }, vendedor);
        }

        // DELETE: api/Vendedor/Delete?id=long
        [HttpDelete("Delete")]
        public async Task<ActionResult<Vendedor>> DeleteVendedor(long id)
        {
            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }

            _context.Vendedor.Remove(vendedor);
            await _context.SaveChangesAsync();

            return vendedor;
        }

        private bool VendedorExists(long id)
        {
            return _context.Vendedor.Any(e => e.Id == id);
        }
    }
}
