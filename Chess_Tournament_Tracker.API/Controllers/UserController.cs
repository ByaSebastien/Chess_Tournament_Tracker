using Microsoft.AspNetCore.Http;
using Chess_Tournament_Tracker.BLL.DTO.Users;
using Chess_Tournament_Tracker.BLL.Services;
using Chess_Tournament_Tracker.IL.TokenInfrastructures;
using Chess_Tournament_Tracker.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chess_Tournament_Tracker.BLL.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Chess_Tournament_Tracker.API.Extensions;

namespace Chess_Tournament_Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        private TokenManager _tokenManager;

        public UserController(IUserService service, TokenManager tokenManager)
        {
            _service = service;
            _tokenManager = tokenManager;
        }
        [HttpPost]
        public IActionResult Register(RegisterDTO registerUser)
        {
            try
            {
                User user = _service.Register(registerUser);
                string Token = _tokenManager.GenerateToken(user);
                return Ok(Token);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SmtpFailedRecipientException)
            {
                return BadRequest("Mail not valid");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                _service.Delete(User.GetId());
                return Ok();
            }
            catch(KeyNotFoundException Ex)
            {
                return NotFound(Ex.Message);
            }
            catch(UserRules ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDTO user)
        {
            try
            {
                return Ok(_service.Login(user));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return BadRequest("Wrong password");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
