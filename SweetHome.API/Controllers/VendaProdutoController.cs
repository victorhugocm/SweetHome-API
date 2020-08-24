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

        // GET: api/VendaProduto/Get
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<VendaProduto>>> GetVendaProduto()
        {
            var listVendaProduto = await _context.VendaProduto.ToListAsync();

            if (listVendaProduto.Any())
            {
                foreach (var item in listVendaProduto)
                {
                    //Produto
                    item.Produto = await _context.Produto.FindAsync(item.ProdutoId);
                    item.Produto.Cor = await _context.Cor.FindAsync(item.Produto.CorId);
                    item.Produto.Tamanho = await _context.Tamanho.FindAsync(item.Produto.TamanhoId);

                    //Venda
                    item.Venda = await _context.Venda.FindAsync(item.VendaId);
                    item.Venda.Vendedor = await _context.Vendedor.FindAsync(item.Venda.VendedorId);
                }
            }

            return listVendaProduto;
        }

        // GET: api/VendaProduto/GetById?id=long
        [HttpGet("GetById")]
        public async Task<ActionResult<VendaProduto>> GetVendaProduto(long id)
        {
            var vendaProduto = await _context.VendaProduto.FindAsync(id);

            if (vendaProduto == null)
            {
                return NotFound();
            }
            else 
            {
                //Produto
                vendaProduto.Produto = await _context.Produto.FindAsync(vendaProduto.ProdutoId);
                vendaProduto.Produto.Cor = await _context.Cor.FindAsync(vendaProduto.Produto.CorId);
                vendaProduto.Produto.Tamanho = await _context.Tamanho.FindAsync(vendaProduto.Produto.TamanhoId);

                //Venda
                vendaProduto.Venda = await _context.Venda.FindAsync(vendaProduto.VendaId);
                vendaProduto.Venda.Vendedor = await _context.Vendedor.FindAsync(vendaProduto.Venda.VendedorId);
            }

            return vendaProduto;
        }

        // PUT: api/VendaProduto/Put
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("Put")]
        public async Task<IActionResult> PutVendaProduto(VendaProduto vendaProduto)
        {
            if (!VendaProdutoExists(vendaProduto.Id))
            {
                return NotFound();
            }
            
            _context.Entry(vendaProduto).State = EntityState.Modified;

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

        // POST: api/VendaProduto/Post
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Post")]
        public async Task<ActionResult<VendaProduto>> PostVendaProduto(VendaProduto vendaProduto)
        {
            _context.VendaProduto.Add(vendaProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendaProduto", new { id = vendaProduto.Id }, vendaProduto);
        }

        // DELETE: api/VendaProduto/Delete?id=long
        [HttpDelete("Delete")]
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
