using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnDemandCarWashSystem.Models;
using OnDemandCarWashSystem.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnDemandCarWashSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICarWash _user;
        public UserController(ICarWash user)
        {
            _user = user;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _user.GetAllAsync(); return Ok(users);
        }
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetuserAsync")]
        public async Task<IActionResult> GetuserAsync(int id)
        {
            var users = await _user.GetAsync(id); if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> AddemployeeAsync(UserModel adduser)
        {
            var user = new Models.UserModel()
            {
                FirstName = adduser.FirstName,
                LastName = adduser.LastName,
                PhoneNo = adduser.PhoneNo,
                Email = adduser.Email,
                Password = adduser.Password,
                ConfirmPassword = adduser.ConfirmPassword,
                Address = adduser.Address,
                Role = adduser.Role,
                Status = adduser.Status
            };
           

            var e = CheckEmailStrength(adduser.Email);
            if (!string.IsNullOrEmpty(e))
                return BadRequest(new { Message = e.ToString() });
                

            //check password 
            var pass = CheckPasswordStrength(adduser.Password);
            if (!string.IsNullOrEmpty(pass))
            return BadRequest(new { Message = pass.ToString() });
            //user = await _user.AddAsync(user);
            //return Ok(new { message = "Registration Successful" });

            var confirmpass = CheckConfirmPasswordStrength(adduser.Password,adduser.ConfirmPassword);
            if (!string.IsNullOrEmpty(confirmpass))
                return BadRequest(new { Message = confirmpass.ToString() });
            user = await _user.AddAsync(user);
            return Ok(new { message = "Registration Successful" });


        }
        #region
        //delete method
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var user = await _user.DeleteAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            { }
            return Ok();
        }
        #endregion
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UserModel updateuser)
        {
            try
            {
                var user = new Models.UserModel()
                {
                    FirstName = updateuser.FirstName,
                    LastName = updateuser.LastName,
                    PhoneNo = updateuser.PhoneNo,
                    Email = updateuser.Email,
                   
                    Password = updateuser.Password,
                    ConfirmPassword = updateuser.ConfirmPassword,
                    Address = updateuser.Address,
                    Role = updateuser.Role,
                    Status = updateuser.Status
                };
                user = await _user.UpdateAsync(id, user);
                if (user == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            { }
            return Ok();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> AddLogin([FromBody] LoginModel login)
        {
            if (login == null)
            {
                return BadRequest();
            }
            var u = await _user.LoginModel(login);
            if(u == null)
            {
                return BadRequest(new { message = "User Not Found" });
            }
            await _user.LoginModel(login);
            string Token = CreateJwt(u);
            return Ok(new {Token, message = "Login Successfull" });
        }
        private string CheckConfirmPasswordStrength(string password,string confirmpassword)
        {
            StringBuilder sb = new StringBuilder();


           if (password!=confirmpassword)
                sb.Append("Password should be matched" + Environment.NewLine);
            return sb.ToString();
        }
        private string CheckEmailStrength(string email)
        {
            StringBuilder sb = new StringBuilder();

            if (!(Regex.IsMatch(email, "[a-z]") && Regex.IsMatch(email, "[@]") && Regex.IsMatch(email, "[.]")))
                sb.Append("Email ex:xyz@gmail.com" + Environment.NewLine);
            return sb.ToString();
        }
        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("password should be Alphanumeric" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[<,>,@,!,#,$,^,?]")))
                sb.Append("Password should contain special Character" + Environment.NewLine);
            return sb.ToString();
        }

        //token validation
        private string CreateJwt(UserModel user) 
        { 
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("vajjaanujagmailcom");
            var identity = new ClaimsIdentity(new Claim[] 
            { 
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}") });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256); 
            var tokenDescriptor = new SecurityTokenDescriptor { Subject = identity, Expires = DateTime.Now.AddDays(1), SigningCredentials = credentials };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }



    }
}


    
