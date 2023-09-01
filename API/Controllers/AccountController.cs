using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context,ITokenService tokenService)
        {
           _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (await userexist(registerDto.UserName)) return BadRequest("username taken");

            using var hmac = new HMACSHA512();

            AppUser user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return new UserDto
            {
                username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == loginDto.UserName.ToLower());

            if (user == null) return Unauthorized("User not found");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("invalid passowrd");
            }

             return new UserDto
            {
                username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> userexist(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}