using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Service.Helper;
using Service.Dto;
using Service;
using Service.Dto.Request;

namespace WebService.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserRequest dto)
        {
            return Ok(await userService.Create(dto));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            return Ok(await userService.Login(dto));
        }
    }
}