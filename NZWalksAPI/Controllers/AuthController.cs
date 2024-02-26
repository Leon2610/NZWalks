using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to this user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {

                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }
            return BadRequest("Something went wrong!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var identityUser = await userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if (identityUser != null && await userManager.CheckPasswordAsync(identityUser, loginRequestDTO.Password))
            {
                //Get roles for this user
                var roles = await userManager.GetRolesAsync(identityUser);

                if (roles != null && roles.Any())
                {
                    //Create token
                    var token = tokenRepository.CreateJWTToken(identityUser, roles.ToList());

                    var response = new LoginResponseDTO
                    {
                        Token = token
                    };

                    return Ok(response);
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
