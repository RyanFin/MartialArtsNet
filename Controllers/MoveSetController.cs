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
        public async Task<ActionResult<IEnumerable<MoveSetDTO>>> GetMoveSetDTO()
        {
            return await _context.MoveSetDTO.ToListAsync();
        }

        // GET: api/MoveSet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoveSetDTO>> GetMoveSetDTO(long id)
        {
            var moveSetDTO = await _context.MoveSetDTO.FindAsync(id);

            if (moveSetDTO == null)
            {
                return NotFound();
            }

            return moveSetDTO;
        }

        // PUT: api/MoveSet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoveSetDTO(long id, MoveSetDTO moveSetDTO)
        {
            if (id != moveSetDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(moveSetDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoveSetDTOExists(id))
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
        public async Task<ActionResult<MoveSetDTO>> PostMoveSetDTO(MoveSetDTO moveSetDTO)
        {
            _context.MoveSetDTO.Add(moveSetDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoveSetDTO", new { id = moveSetDTO.Id }, moveSetDTO);
        }

        // DELETE: api/MoveSet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoveSetDTO(long id)
        {
            var moveSetDTO = await _context.MoveSetDTO.FindAsync(id);
            if (moveSetDTO == null)
            {
                return NotFound();
            }

            _context.MoveSetDTO.Remove(moveSetDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoveSetDTOExists(long id)
        {
            return _context.MoveSetDTO.Any(e => e.Id == id);
        }
    }
}
