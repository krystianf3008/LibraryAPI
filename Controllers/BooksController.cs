using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("/api/books/")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("/api/books/")]

        public async Task<ActionResult<List<Book>>> GetAll()
        {
            return Ok(await _bookService.GetAll());
        }

        [HttpGet("/api/books/{id}")]
        public async Task<ActionResult<Book>> Get([FromRoute] Guid id)
        {
            return  Ok(await _bookService.GetById(id));
        }
        [HttpPost("/api/books/")]
        public async Task<ActionResult> Create([FromForm] CreateBookDTO createBookDTO)
        {
            var guid = await _bookService.CreateBook(createBookDTO);
            return Created($"/api/books/{guid}",null);
        }

        [HttpPut("/api/books/{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromForm] UpdateBookDTO updateBookDTO)
        {
            await _bookService.UpdateBook(id, updateBookDTO);
            return Ok();
        }

        [HttpDelete("/api/books/{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _bookService.DeleteBook(id);
            return Ok();
        }












    }
}
