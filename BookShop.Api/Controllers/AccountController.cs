using BookShop.Service.DTOs.User;
using BookShop.Service.Exceptions;
using BookShop.Service.Managers.IManagers;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserManager _userManager;

    public AccountController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> InsertUser([FromForm] UserCreationDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(dto);

        try
        {
            return Created("InsertUser", await _userManager.InsertAsync(dto));
        }
        catch (ThisObjectAlreadyExistsException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        if (!ModelState.IsValid)
            return BadRequest($"{username}, {password}");

        try
        {
            var (jwtToken, expiresJwtToken) = await _userManager.LoginAsync(username, password);

            return Ok( new { Token = jwtToken, ExpiresToken = expiresJwtToken } );
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (PasswordInCorrectException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}