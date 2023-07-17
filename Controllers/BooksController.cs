using LibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        public BooksController()
        {

        }


        [HttpGet()]

        public async Task<ActionResult<List<Book>>> GetAll()
        {
            var books = new List<Book>();
            return Ok(books);
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<Book>> Get([FromRoute] int id)
        {
            return Ok(new Book());
        }












}
}
