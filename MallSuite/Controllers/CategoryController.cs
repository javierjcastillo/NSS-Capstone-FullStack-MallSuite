using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using MallSuite.Models;
using MallSuite.Repositories;

namespace MallSuite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_categoryRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            _categoryRepository.Add(category);
            return CreatedAtAction(
            nameof(Get),
            new { id = category.Id }, category);
        }

        [HttpDelete("{categoryId}")]
        public IActionResult Delete(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Category category)
        {
            _categoryRepository.Edit(category);
            return NoContent();
        }
    }
}
