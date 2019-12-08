using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Entities;
using Ecommerce.Application.Repositories.Interfaces;
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

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _userRepository.Insert(new UserEntity
            {
                BirthDay = DateTime.Now,
                EMail = "Email",
                FirstName = "Scribo",
                HashedPassword = "Hashed",
                LastName = "Scribo",
                Salt = "Salty",
                UserName = "Best User"
            });
            return _userRepository.GetAll().Select(x=>x.UserName);
        }
    }
}
