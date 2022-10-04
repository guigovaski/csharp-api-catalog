using ApiCatalog.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace ApiCatalog.Controllers;

[ApiConventionType(typeof(DefaultApiConventions))]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUser>? _userManager;
    private readonly SignInManager<IdentityUser>? _signInManager;
    private readonly IConfiguration _configuration;

    public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(UserDto userDto)
    {
        var user = new IdentityUser
        {
            UserName = userDto.Email,
            Email = userDto.Email,
            EmailConfirmed = true
        };

        var res = await _userManager.CreateAsync(user, userDto.Password);
        
        if (!res.Succeeded)
        {
            return BadRequest(res.Errors);
        }

        await _signInManager.SignInAsync(user, false);

        return Ok(GenerateToken(userDto));
        
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(UserDto userDto)
    {
        var res = await _signInManager.PasswordSignInAsync(userDto.Email, userDto.Password, isPersistent: false, lockoutOnFailure: false);
        
        if (!res.Succeeded)
        {
            return Forbid();
        }

        return Ok(GenerateToken(userDto));
    }

    private UserTokenDto GenerateToken(UserDto userDto)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userDto.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(double.Parse(_configuration["TokenConfiguration:ExpireHours"]));

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["TokenConfiguration:Issuer"],
            audience: _configuration["TokenConfiguration:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new UserTokenDto
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
