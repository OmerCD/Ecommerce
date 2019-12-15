using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Entities;
using Ecommerce.Application.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IRepository<UserEntity> _userRepository;

        public TestController(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("p")]
        public IActionResult CheckPassword([FromQuery] string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault();
            
            return Ok(user.CheckPassword(password));
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var entity = new UserEntity();
            entity.BirthDay = DateTime.Now;
            entity.EMail = "Email";
            entity.FirstName = "Scribo";
            entity.LastName = "Scribo";
            entity.UserName = "Best User";
            entity.SetPassword("Ömer");
            entity.IsDeleted = false;
            _userRepository.Insert(entity);
            return Ok(_userRepository.GetAll());
        }

        [HttpGet("auth")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AuthTest()
        {
            return Ok();
        }
    }
}
