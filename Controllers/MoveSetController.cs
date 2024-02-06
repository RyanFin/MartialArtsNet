using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MartialArtsNet.Models;

namespace MartialArtsNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveSetController : ControllerBase
    {
        private readonly MoveSetContext _context;

        public MoveSetController(MoveSetContext context)
        {
            _context = context;
        }

        // GET: api/MoveSet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoveSet>>> GetMoveSets()
        {
            return await _context.MoveSets.ToListAsync();
        }

        // GET: api/MoveSet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoveSet>> GetMoveSet(long id)
        {
            var moveSet = await _context.MoveSets.FindAsync(id);

            if (moveSet == null)
            {
                return NotFound();
            }

            return moveSet;
        }

        // PUT: api/MoveSet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoveSet(long id, MoveSet moveSet)
        {
            if (id != moveSet.Id)
            {
                return BadRequest();
            }

            _context.Entry(moveSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoveSetExists(id))
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

        // POST: api/MoveSet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoveSet>> PostMoveSet(MoveSet moveSet)
        {
            _context.MoveSets.Add(moveSet);
            await _context.SaveChangesAsync();

            Console.WriteLine("nameof output: " + nameof(GetMoveSet));
            return CreatedAtAction(nameof(GetMoveSet), new { id = moveSet.Id }, moveSet);
        }

        // DELETE: api/MoveSet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoveSet(long id)
        {
            var moveSet = await _context.MoveSets.FindAsync(id);
            if (moveSet == null)
            {
                return NotFound();
            }

            _context.MoveSets.Remove(moveSet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoveSetExists(long id)
        {
            return _context.MoveSets.Any(e => e.Id == id);
        }
    }
}
