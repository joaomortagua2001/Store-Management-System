using RESTfulAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using RESTfulAPI.Data;
using RESTfulAPI.DTOs;

namespace RESTfulAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UtilizadoresController : ControllerBase
{
    private readonly IConfiguration _config;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UtilizadoresController(IConfiguration config, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegistarUser([FromBody] RegistarUserDto utilizadorDto)
    {
        // Verifica se o e-mail já existe
        var utilizadorExiste = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == utilizadorDto.Email);

        if (utilizadorExiste is not null)
        {
            return BadRequest("Já existe um utilizador com este email");
        }

        // Cria o novo utilizador a partir do DTO
        var novoUtilizador = new ApplicationUser
        {
            UserName = utilizadorDto.Email,
            Email = utilizadorDto.Email,
            Nome = utilizadorDto.Nome,
            Apelido = utilizadorDto.Apelido,
            NIF = utilizadorDto.NIF,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        // Registra o utilizador no sistema
        var resultado = await _userManager.CreateAsync(novoUtilizador, utilizadorDto.Password);
        if (!resultado.Succeeded)
        {
            return BadRequest("Erro ao criar o utilizador.");
        }

        await _userManager.AddToRoleAsync(novoUtilizador, "Cliente");

        return StatusCode(StatusCodes.Status201Created);
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
    {
        var utilizadorAtual = await _userManager.Users.FirstOrDefaultAsync(u =>
                                     u.Email == loginRequest.Email);

        if (utilizadorAtual is null)
        {
            return NotFound("Utilizador não encontrado");
        }

        var result = await _signInManager.PasswordSignInAsync(
            loginRequest.Email, loginRequest.Password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var userRoles = await _userManager.GetRolesAsync(utilizadorAtual);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Email , loginRequest.Email),
            new Claim(ClaimTypes.Role, userRoles[0]!)
        };

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new ObjectResult(new
            {
                accesstoken = jwt,
                tokentype = "bearer",
                utilizadorid = utilizadorAtual.Id,
                utilizadornome = utilizadorAtual.Nome
            });
        }

        return BadRequest("Erro: Login Inválido!");
    }

}
