using BookShop.Domain.Shared;
using BookShop.Service.DTOs.User;
using BookShop.Service.Exceptions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.PageFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserManager _userManager;

    public UsersController(IUserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> GetAllUsers([FromQuery] UserFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(filter);

        try
        {
            return Ok(await _userManager.GetAllUsersAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{userId:guid}")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> GetById(Guid userId)
    {
        try
        {
            return Ok(await _userManager.GetByIdAsync(userId));
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

    [HttpGet("{username:alpha}")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> GetByUsername(string username)
    {
        try
        {
            return Ok(await _userManager.GetByUsernameAsync(username));
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

    [HttpPut("{userId:guid}")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> Update(Guid userId, UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(dto);

        try
        {
            return Ok(await _userManager.UpdateAsync(userId, dto));
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

    [HttpDelete("{userId:guid}")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> Update(Guid userId)
    {
        try
        {
            await _userManager.DeleteAsync(userId);
            return Ok();
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

    [HttpPatch("{userId:guid}/image")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> AddUserImage(Guid userId,[FromForm] IFormFile[] images)
    {
        try
        {
           await  _userManager.SaveUserImagesAsync(userId, images);
           return Ok();
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

    [HttpDelete("{userId:guid}/image")]
    [Authorize(Policy = UserRoles.SuperAdmin)]
    public async ValueTask<IActionResult> DeleteUserImage(Guid userImageId)
    {
        try
        {
            await _userManager.DeleteImageAsync(userImageId);
            return NoContent();
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
}