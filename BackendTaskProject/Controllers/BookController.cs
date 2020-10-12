using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendTaskProject.Data;
using BackendTaskProject.Models;

namespace BackendTaskProject
{
    [Produces("application/json")]
    [Route("book")]
    public class BookController : Controller
    {
        private string view = "Views/Book/Book.cshtml";

        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        // GET: Book
        [HttpGet]
        public IActionResult Book()
        {
            return View(view);
        }

        // GET: Book/values
        [HttpGet("values")]
        public IEnumerable<BookModel> GetBook()
        {
            return _context.BookModel;
        }

        // GET: Book/values/5
        [HttpGet("values/{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var BookModel = await _context.BookModel.SingleOrDefaultAsync(m => m.ID == id);

            if (BookModel == null)
            {
                return NotFound();
            }

            return Ok(BookModel);
        }

        // PUT: Book/values/5
        [HttpPut("values/{id}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] BookModel BookModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != BookModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(BookModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: Book/values
        [HttpPost("values")]
        public async Task<IActionResult> PostBook([FromBody] BookModel BookModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BookModel.Add(BookModel);
            await _context.SaveChangesAsync();

            // CreatedAtAction gives response 500. No idea why it happens so I skipped to just sending the correct response
            return StatusCode(201);
        }

        // DELETE: Book/values/5
        [HttpDelete("values/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var BookModel = await _context.BookModel.SingleOrDefaultAsync(m => m.ID == id);
            if (BookModel == null)
            {
                return NotFound();
            }

            _context.BookModel.Remove(BookModel);
            await _context.SaveChangesAsync();

            return Ok(BookModel);
        }

        private bool BookExists(int id)
        {
            return _context.BookModel.Any(e => e.ID == id);
        }
    }
}