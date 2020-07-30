﻿using System;
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
    public class CorController : ControllerBase
    {
        private readonly SweetHomeContext _context;

        public CorController(SweetHomeContext context)
        {
            _context = context;
        }

        // GET: api/Cor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cor>>> GetCor()
        {
            return await _context.Cor.ToListAsync();
        }

        // GET: api/Cor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cor>> GetCor(long id)
        {
            var cor = await _context.Cor.FindAsync(id);

            if (cor == null)
            {
                return NotFound();
            }

            return cor;
        }

        // PUT: api/Cor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCor(long id, Cor cor)
        {
            if (id != cor.Id)
            {
                return BadRequest();
            }

            _context.Entry(cor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorExists(id))
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

        // POST: api/Cor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cor>> PostCor(Cor cor)
        {
            _context.Cor.Add(cor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCor", new { id = cor.Id }, cor);
        }

        // DELETE: api/Cor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cor>> DeleteCor(long id)
        {
            var cor = await _context.Cor.FindAsync(id);
            if (cor == null)
            {
                return NotFound();
            }

            _context.Cor.Remove(cor);
            await _context.SaveChangesAsync();

            return cor;
        }

        private bool CorExists(long id)
        {
            return _context.Cor.Any(e => e.Id == id);
        }
    }
}
