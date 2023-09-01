using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        public DataContext _Context { get; }

        public UsersController(DataContext context)
        {
            _Context = context;

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser()
        {
            var users = await _Context.Users.ToListAsync();

            return users;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> Get(int id)
        {
            var user = await _Context.Users.FindAsync(id);

            return user;
        }


    }
}