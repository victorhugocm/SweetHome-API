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
    public class VendaController : ControllerBase
    {
        private readonly SweetHomeContext _context;

        public VendaController(SweetHomeContext context)
        {
            _context = context;
        }

        // GET: api/Venda/Get
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVenda()
        {
            var listVenda = await _context.Venda.ToListAsync();

            if (listVenda.Any())
            {
                foreach (var item in listVenda)
                {
                    item.Vendedor = await _context.Vendedor.FindAsync(item.VendedorId);
                }
            }

            return listVenda;
        }

        // GET: api/Venda/GetById?id=long
        [HttpGet("GetById")]
        public async Task<ActionResult<Venda>> GetVenda(long id)
        {
            var venda = await _context.Venda.FindAsync(id);

            if (venda == null)
            {
                return NotFound();
            }
            else 
            {
                venda.Vendedor = await _context.Vendedor.FindAsync(venda.VendedorId);
            }

            return venda;
        }

        // PUT: api/Venda/Put
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("Put")]
        public async Task<IActionResult> PutVenda(Venda venda)
        {
            if (!VendaExists(venda.Id))
            {
                return NotFound();
            }

            _context.Entry(venda).State = EntityState.Modified;

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

        // POST: api/Venda/Post
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Post")]
        public async Task<ActionResult<Venda>> PostVenda(Venda venda)
        {
            _context.Venda.Add(venda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVenda", new { id = venda.Id }, venda);
        }

        // DELETE: api/Venda/Delete?id=long
        [HttpDelete("Delete")]
        public async Task<ActionResult<Venda>> DeleteVenda(long id)
        {
            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Venda.Remove(venda);
            await _context.SaveChangesAsync();

            return venda;
        }

        private bool VendaExists(long id)
        {
            return _context.Venda.Any(e => e.Id == id);
        }
    }
}
