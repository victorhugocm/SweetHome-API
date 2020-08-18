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
    public class ProdutoController : ControllerBase
    {
        private readonly SweetHomeContext _context;

        public ProdutoController(SweetHomeContext context)
        {
            _context = context;
        }

        // GET: api/Produto/Get
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            var listProduto = await _context.Produto.ToListAsync();

            if (listProduto.Any())
            {
                foreach (var item in listProduto)
                {
                    item.Cor = await _context.Cor.FindAsync(item.CorId);
                    item.Tamanho = await _context.Tamanho.FindAsync(item.TamanhoId);
                }
            }

            return listProduto;
        }

        // GET: api/Produto/GetById?id=long
        [HttpGet("GetById")]
        public async Task<ActionResult<Produto>> GetProduto(long id)
        {
            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }
            else 
            {
                produto.Cor = await _context.Cor.FindAsync(produto.CorId);
                produto.Tamanho = await _context.Tamanho.FindAsync(produto.TamanhoId);
            }

            return produto;
        }

        // PUT: api/Produto/Put
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("Put")]
        public async Task<IActionResult> PutProduto(Produto produto)
        {
            if (!ProdutoExists(produto.Id))
            {
                return NotFound();
            }

            _context.Entry(produto).State = EntityState.Modified;

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

        // POST: api/Produto/Post
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Post")]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
        }

        // DELETE: api/Produto/Delete?id=long
        [HttpDelete("Delete")]
        public async Task<ActionResult<Produto>> DeleteProduto(long id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        private bool ProdutoExists(long id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }
    }
}
