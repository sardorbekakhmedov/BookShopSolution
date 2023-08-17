using BookShop.Domain.Shared;
using BookShop.Service.DTOs.Discount;
using BookShop.Service.Exceptions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.PageFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountManager _discountManager;

    public DiscountsController(IDiscountManager discountManager)
    {
        _discountManager = discountManager;
    }

    [HttpPost]
    [Authorize]
    public async ValueTask<IActionResult> InsertDiscount(CreateDiscountDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(dto);

        if (dto.PublisherId is null && dto.UserId is null)
            return BadRequest("Publisher or UserId cannot be null!");

        try
        {
            return Created("InsertDiscount",  await _discountManager.InsertDiscountAsync(dto));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> GetAllGenres([FromQuery] DiscountFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(filter);

        try
        {
            return Ok(await _discountManager.GetAllDiscountsAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{discountId}")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> GetDiscountById(Guid discountId)
    {
        try
        {
            return Ok(await _discountManager.GetDiscountByIdAsync(discountId));
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

    [HttpPut]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> UpdateDiscount(Guid discountId, UpdateDiscountDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(dto);

        try
        {
            return Ok(await _discountManager.UpdateDiscountAsync(discountId, dto));
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

    [HttpDelete]
    [Authorize(Policy = UserRoles.SuperAdmin)]
    public async ValueTask<IActionResult> DeleteDiscount(Guid discountId)
    {
        try
        {
            await _discountManager.DeleteDiscountAsync(discountId);
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