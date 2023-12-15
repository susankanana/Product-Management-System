using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Management_System.models;
using Product_Management_System.models.Dtos;
using Product_Management_System.Services.IService;
using System.IdentityModel.Tokens.Jwt;

namespace Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUser _userService;
        private readonly IJwt _jwtService;
        public UserController(IMapper mapper, IUser usr, IJwt jwt)
        {
            _mapper = mapper;
            _userService = usr;
            _jwtService = jwt;
        }
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            var response = await _userService.GetUserByEmail(email);
            if(response == null)
            {
                return NotFound("User not found");
            }
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> RegisterUser(AddUserDto newUser)
        {
            var _newUser = _mapper.Map<User>(newUser);
            //password is not encrypted 
            _newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            var checkUser = await _userService.GetUserByEmail(newUser.Email);
            if(checkUser!=null)
            {
                return BadRequest("User already exists");
            }

            var response = await _userService.RegisterUser(_newUser);
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginUser(LogUserDto retUser)
        {
            var checkUser = await _userService.GetUserByEmail(retUser.Email);
            if (checkUser == null)
            {
                return BadRequest("Invalid Credentials");
            }
            //verify password
            var isCorrect = BCrypt.Net.BCrypt.Verify(retUser.Password, checkUser.Password);
            if (!isCorrect)
            {
                return BadRequest("Invalid Credentials");
            }
            //return Ok($"Welcome {checkUser.Name}");
            var token = _jwtService.GenerateToken(checkUser);
            return Ok(token);
        }

    }
}
