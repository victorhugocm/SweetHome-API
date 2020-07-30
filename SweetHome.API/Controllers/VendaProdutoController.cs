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
    public class VendaProdutoController : ControllerBase
    {
        private readonly SweetHomeContext _context;

        public VendaProdutoController(SweetHomeContext context)
        {
            _context = context;
        }

        // GET: api/VendaProduto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendaProduto>>> GetVendaProduto()
        {
            return await _context.VendaProduto.ToListAsync();
        }

        // GET: api/VendaProduto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendaProduto>> GetVendaProduto(long id)
        {
            var vendaProduto = await _context.VendaProduto.FindAsync(id);

            if (vendaProduto == null)
            {
                return NotFound();
            }

            return vendaProduto;
        }

        // PUT: api/VendaProduto/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendaProduto(long id, VendaProduto vendaProduto)
        {
            if (id != vendaProduto.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendaProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaProdutoExists(id))
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

        // POST: api/VendaProduto
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VendaProduto>> PostVendaProduto(VendaProduto vendaProduto)
        {
            _context.VendaProduto.Add(vendaProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendaProduto", new { id = vendaProduto.Id }, vendaProduto);
        }

        // DELETE: api/VendaProduto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VendaProduto>> DeleteVendaProduto(long id)
        {
            var vendaProduto = await _context.VendaProduto.FindAsync(id);
            if (vendaProduto == null)
            {
                return NotFound();
            }

            _context.VendaProduto.Remove(vendaProduto);
            await _context.SaveChangesAsync();

            return vendaProduto;
        }

        private bool VendaProdutoExists(long id)
        {
            return _context.VendaProduto.Any(e => e.Id == id);
        }
    }
}
