using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeCode.Api.Data;
using WeCode.Api.Models;

namespace WeCode.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesContext database;

        public NotesController(NotesContext database)
        {
            this.database = database ?? throw new System.ArgumentNullException(nameof(database));
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Note>>> Read()
        {
            return Ok(await database.Notes.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Note>> Read(int id)
        {
            var note = await database.Notes.SingleOrDefaultAsync(note => note.Id == id);

            if (note is null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(Note note)
        {
            await database.Notes.AddAsync(note);

            await database.SaveChangesAsync();

            return CreatedAtAction(nameof(Read), note);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<Note> note)
        {
            var currentNote = await database.Notes.SingleOrDefaultAsync(note => note.Id == id);

            if (currentNote is null)
            {
                return NotFound();
            }

            note.ApplyTo(currentNote);

            await database.SaveChangesAsync();

            return Ok(currentNote);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await database.Notes.SingleOrDefaultAsync(note => note.Id == id);

            if (note is null)
            {
                return NotFound();
            }

            database.Remove(note);

            await database.SaveChangesAsync();

            return NoContent();
        }
    }
}
