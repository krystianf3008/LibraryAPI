using LibraryAPI.DTOs;
using LibraryAPI.Entities;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService service)
        {
            _authorService = service;
        }
        [HttpGet("/api/author/")]

        public async Task<ActionResult<List<Author>>> GetAll()
        {
            return Ok(await _authorService.GetAll());
        }

        [HttpGet("/api/author/{id}")]
        public async Task<ActionResult<List<BookDTO>>> Get([FromRoute] Guid id)
        {
            return Ok(await _authorService.GetBooks(id));
        }
        [HttpPost("/api/author/")]
        public async Task<ActionResult> Create([FromForm] CreateAuthorDto createAuthorDTO)
        {
            var id = await _authorService.CreateAuthor(createAuthorDTO);
            return Created($"/api/author/{id}", null);
        }

        [HttpPut("/api/author/{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromForm] CreateAuthorDto authorDto)
        {
            await _authorService.UpdateAuthor(id, authorDto);
            return Ok();
        }

        [HttpDelete("/api/author/{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _authorService.DeleteAuthor(id);
            return Ok();
        }

    }
}
