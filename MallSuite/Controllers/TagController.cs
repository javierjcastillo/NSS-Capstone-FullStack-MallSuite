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
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tagRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_tagRepository.GetById(id));
        }

        [HttpPost]
        public IActionResult Post(Tag tag)
        {
            _tagRepository.Add(tag);
            return CreatedAtAction(
            nameof(Get),
            new { id = tag.Id }, tag);
        }

        [HttpDelete("{tagId}")]
        public IActionResult Delete(int tagId)
        {
            _tagRepository.Delete(tagId);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Tag tag)
        {
            _tagRepository.Edit(tag);
            return NoContent();
        }

        //[HttpPost("{tagId}/addtostorerestaurant/{storeRestaurantId}")]
        //public IActionResult AddTagToStoreRestaurant(int tagId, int storeRestaurantId)
        //{
        //    try
        //    {
        //        _tagRepository.AddTagToStoreRestaurant(storeRestaurantId, tagId);
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        //[HttpDelete("{tagId}/removefromstorerestaurant/{storeRestaurantId}")]
        //public IActionResult RemoveTagFromStoreRestaurant(int tagId, int storeRestaurantId)
        //{
        //    _tagRepository.RemoveTagFromStoreRestaurant(storeRestaurantId, tagId);
        //    return NoContent();
        //}
    }
}

