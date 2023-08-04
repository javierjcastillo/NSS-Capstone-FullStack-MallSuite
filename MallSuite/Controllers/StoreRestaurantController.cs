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
    public class StoreRestaurantController : ControllerBase
    {
        private readonly IStoreRestaurantRepository _storeRestaurantRepository;
        public StoreRestaurantController(IStoreRestaurantRepository storeRestaurantRepository)
        {
            _storeRestaurantRepository = storeRestaurantRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_storeRestaurantRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_storeRestaurantRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Post(StoreRestaurant storeRestaurant)
        {
            _storeRestaurantRepository.Add(storeRestaurant);
            return CreatedAtAction(
            nameof(Get),
            new { id = storeRestaurant.Id }, storeRestaurant);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, StoreRestaurant storeRestaurant)
        {
            if (id != storeRestaurant.Id)
            {
                return BadRequest(new { message = "Mismatched Ids" });
            }

            try
            {
                _storeRestaurantRepository.Edit(storeRestaurant);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _storeRestaurantRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("ByCategory/{categoryId}")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            return Ok(_storeRestaurantRepository.GetByCategoryId(categoryId));
        }

        [HttpGet("ByTag/{tagId}")]
        public IActionResult GetByTagId(int tagId)
        {
            return Ok(_storeRestaurantRepository.GetByTagId(tagId));
        }

        [HttpPost("AddCategory/{storeRestaurantId}/{categoryId}")]
        public IActionResult AddCategory(int storeRestaurantId, int categoryId)
        {
            _storeRestaurantRepository.AddCategory(storeRestaurantId, categoryId);
            return NoContent();
        }

        [HttpPut("UpdateCategory/{storeRestaurantId}/{categoryId}")]
        public IActionResult UpdateCategory(int storeRestaurantId, int categoryId)
        {
            _storeRestaurantRepository.UpdateCategory(storeRestaurantId, categoryId);
            return NoContent();
        }

        [HttpPost("AddTag/{storeRestaurantId}/{tagId}")]
        public IActionResult AddTag(int storeRestaurantId, int tagId)
        {
            _storeRestaurantRepository.AddTag(storeRestaurantId, tagId);
            return NoContent();
        }

        [HttpDelete("RemoveTag/{storeRestaurantId}/{tagId}")]
        public IActionResult RemoveTag(int storeRestaurantId, int tagId)
        {
            _storeRestaurantRepository.RemoveTag(storeRestaurantId, tagId);
            return NoContent();
        }
    }
}
