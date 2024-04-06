using Auth.Server.Data;
using Auth.Server.Dtos;
using Auth.Server.Helpers;
using Auth.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Auth.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            try
            {
                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };

                var createdUser = _repository.Create(user);

                return Created("Success", createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Registration failed", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            try
            {
                var user = _repository.GetByEmail(dto.Email);
                if (user == null)
                    return BadRequest(new { message = "Invalid credentials" });

                if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                    return BadRequest(new { message = "Invalid credentials" });

                var jwtString = _jwtService.Generate(user.Id);
                Response.Cookies.Append("jwt", jwtString, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true
                });

                return Ok(new { message = "Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Login failed", error = ex.Message });
            }
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                if (string.IsNullOrEmpty(jwt))
                    return Unauthorized();

                var token = _jwtService.Verify(jwt);

                if (token == null)
                    return Unauthorized();

                if (!int.TryParse(token.Issuer, out int userId))
                    return Unauthorized();

                var user = _repository.GetById(userId);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving user", error = ex.Message });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("jwt");

                return Ok(new { message = "Logout successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Logout failed", error = ex.Message });
            }
        }
    }
}
