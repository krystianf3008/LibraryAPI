using LibraryAPI.Models.Authors;
using LibraryAPI.Models.Books;
using LibraryAPI.Entities;
using LibraryAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Claims;

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
        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet("/api/author/verify")]

        public async Task<ActionResult<List<Author>>> GetAuthorsToVerify()
        {
            return Ok(await _authorService.GetAuthorsToVerify());
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost("/api/author/verify")]
        public async Task<ActionResult<List<Author>>> VerifyAuthor([FromBody] Guid id)
        {
            await _authorService.VerifyAuthor(id);
            return Ok();
        }

        [HttpGet("/api/author/{id}")]
        public async Task<ActionResult<List<BookDTO>>> Get([FromRoute] Guid id)
        {
            return Ok(await _authorService.GetBooks(id));
        }
        [Authorize(Roles = "User,Admin,Moderator")]
        [HttpPost("/api/author/")]
        public async Task<ActionResult> Create([FromForm] CreateAuthorDto createAuthorDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var id = await _authorService.CreateAuthor(createAuthorDTO,userId);
            return Created($"/api/author/{id}", null);
        }
        [Authorize(Roles = "User,Admin,Moderator")]
        [HttpPut("/api/author/{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromForm] CreateAuthorDto authorDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _authorService.UpdateAuthor(id, authorDto, userId);
            return Ok();
        }
        [Authorize(Roles = "User,Admin,Moderator")]
        [HttpDelete("/api/author/{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _authorService.DeleteAuthor(id, userId);
            return Ok();
        }


    }
}
