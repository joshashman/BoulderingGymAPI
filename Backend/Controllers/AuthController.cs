using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BoulderingGymAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BoulderingGymAPI.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace BoulderingGymAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("register")]
        [SwaggerOperation(
            Summary = "Register a new user",
            Description = "Creates a new user account and assigns default roles."
        )]
        [SwaggerResponse(200, "User registered successfully")]
        [SwaggerResponse(400, "Invalid registration details")]
    
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            _logger.LogInformation("Registering new user");

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                _logger.LogWarning("User registration failed");
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, "User");

            _logger.LogInformation("User registered successfully");

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "User login",
            Description = "Authenticates a user and returns a JWT token for API access."
        )]
        [SwaggerResponse(200, "Login successful and token returned")]
        [SwaggerResponse(401, "Invalid login credentials")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            _logger.LogInformation("User login attempt");

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                _logger.LogWarning("Invalid login attempt");
                return Unauthorized("Invalid email or password");
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!validPassword)
            {
                _logger.LogWarning("Invalid login attempt");
                return Unauthorized("Invalid email or password");
            }

            var jwtSettings = HttpContext.RequestServices
                .GetRequiredService<IConfiguration>()
                .GetSection("JwtSettings");

            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(jwtSettings["ExpiryMinutes"])
                ),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            _logger.LogInformation("User logged in successfully");

            return Ok(new { token = tokenString });
        }
    }
}