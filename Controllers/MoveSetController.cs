using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MartialArtsNet.Models;
using MartialArtsNet.Services;

namespace MartialArtsNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveSetController : ControllerBase
    {
        private readonly MoveSetContext _context;
        private readonly MovesService _movesService;

        public MoveSetController(MoveSetContext context, MovesService movesService)
        {
            _context = context;
            _movesService = movesService;
        }

        // GET: api/MoveSet
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<MoveSetDTO>> GetMoveSetDTO()
        // {
        //     // return await _context.MoveSetDTO.ToListAsync();
        //     return await _movesService.GetAsync();
        // }
        [HttpGet]
        public async Task<List<MoveSetDTO>> GetMoveSetDTO(){
            return await _movesService.GetAsync();
        }


        // GET: api/MoveSet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoveSetDTO>> GetMoveSetDTO(string id)
        {
            // var moveSetDTO = await _context.MoveSetDTO.FindAsync(id);
            var moveSetDTO = await _movesService.GetAsync(id);

            if (moveSetDTO == null)
{
                return NotFound();
            }

            return moveSetDTO;
        }

        // PUT: api/MoveSet/507f1f77bcf86cd799439011
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoveSetDTO(string id, MoveSetDTO updatedMoveSetDTO){
            await _movesService.UpdateAsync(id, updatedMoveSetDTO);
            return NoContent();
        }

        // POST: api/MoveSet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoveSetDTO>> PostMoveSetDTO(MoveSetDTO newMoveSetDTO)
        {
            await _movesService.CreateAsync(newMoveSetDTO);

            return CreatedAtAction(nameof(GetMoveSetDTO), new { id = newMoveSetDTO.Id }, newMoveSetDTO);
        }

        // DELETE: api/MoveSet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoveSetDTO(string id)
        {
            await _movesService.RemoveAsync(id);
            return NoContent();
        }

        private bool MoveSetDTOExists(string id)
        {
            return _context.MoveSetDTO.Any(e => e.Id == id);
        }
    }
}
