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
    public class StoreRestaurantTagController : ControllerBase
    {
        private readonly IStoreRestaurantTagRepository _storeRestaurantTagRepository;
        public StoreRestaurantTagController(IStoreRestaurantTagRepository storeRestaurantTagRepository)
        {
            _storeRestaurantTagRepository = storeRestaurantTagRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_storeRestaurantTagRepository.GetAll());
        }
    }
}
