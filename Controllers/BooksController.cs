using LibraryAPI.Models.Books;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Entities;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using LibraryAPI.Models;
using System.Security.Claims;

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

        public async Task<ActionResult<List<BookDTO>>> GetAll()
        {
            return Ok(await _bookService.GetAll());
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("/api/books/verify")]

        public async Task<ActionResult<List<Book>>> GetBooksToVerify()
        {
            return Ok(await _bookService.GetBooksToVerify());
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("/api/books/verify")]
        public async Task<ActionResult<List<Author>>> VerifyBook([FromBody] Guid id)
        {
            await _bookService.VerifyBook(id);
            return Ok();
        }
        [HttpGet("/api/books/{id}")]
        public async Task<ActionResult<OneBookDTO>> Get([FromRoute] Guid id)
        {
            return  Ok(await _bookService.GetById(id));
        }
        [Authorize(Roles = "User,Admin,Moderator")]
        [HttpPost("/api/books/")]
        public async Task<ActionResult> Create([FromForm] CreateBookDTO createBookDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var guid = await _bookService.CreateBook(createBookDTO,userId);
            return Created($"/api/books/{guid}",null);
        }
        [Authorize(Roles = "User,Admin,Moderator")]
        [HttpPut("/api/books/{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromForm] CreateBookDTO bookDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _bookService.UpdateBook(id, bookDTO, userId);
            return Ok();
        }

        [Authorize(Roles = "User,Admin,Moderator")]
        [HttpDelete("/api/books/{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _bookService.DeleteBook(id, userId);
            return Ok();
        }












    }
}
