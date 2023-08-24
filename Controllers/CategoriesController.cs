using Microsoft.AspNetCore.Mvc;
using LibraryAPI.DTOs;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class CategoriesController : ControllerBase
        {
        private readonly ICategoryService _categoryService;

            public CategoriesController( ICategoryService service)
            {
                _categoryService = service;
            }
        [HttpGet("/api/category/")]

        public async Task<ActionResult<List<Category>>> GetAll()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet("/api/category/{id}")]
        public async Task<ActionResult<List<BookDTO>>> Get([FromRoute] int id)
        {
            return Ok(await _categoryService.GetBooks(id));
        }
        [HttpPost("/api/category/")]
        public async Task<ActionResult> Create([FromForm] CreateCategoryDTO createCategoryDTO)
        {
            var id = await _categoryService.CreateCategory(createCategoryDTO);
            return Created($"/api/category/{id}", null);
        }

        [HttpPut("/api/category/{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromForm] CreateCategoryDTO categoryDto)
        {
            await _categoryService.UpdateCategory(id, categoryDto);
            return Ok();
        }

        [HttpDelete("/api/category/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }

    }
}
