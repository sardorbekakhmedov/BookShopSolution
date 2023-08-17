using BookShop.Domain.Shared;
using BookShop.Service.DTOs.Publisher;
using BookShop.Service.Exceptions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.PageFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly IPublisherManager _publisherManager;

    public PublishersController(IPublisherManager publisherManager)
    {
        _publisherManager = publisherManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> InsertPublisher(CreatePublisherDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(dto);

        try
        {
            return Created("InsertPublisher", await _publisherManager.InsertPublisherAsync(dto));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    [Authorize(Policy = UserRoles.User)]
    public async Task<IActionResult> GetAllPublishers(PublisherFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(filter);

        try
        {
            return Ok(await _publisherManager.GetAllPublishersAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }


    [HttpGet("{publisherId:guid}")]
    [Authorize(Policy = UserRoles.User)]
    public async Task<IActionResult> GetPublisherById(Guid publisherId)
    {
        try
        {
            return Ok(await _publisherManager.GetPublisherByIdAsync(publisherId));
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

    [HttpPut("{publisherId:guid}")]
    [Authorize(Policy = UserRoles.User)]
    public async Task<IActionResult> UpdatePublisher(Guid publisherId, UpdatePublisherDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(dto);

        try
        {
            return Ok(await _publisherManager.UpdatePublisherAsync(publisherId, dto));
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

    [HttpDelete("{publisherId:guid}")]
    [Authorize(Policy = UserRoles.SuperAdmin)]
    public async Task<IActionResult> DeletePublisher(Guid publisherId)
    {
        try
        {
            await _publisherManager.DeletePublisherAsync(publisherId);
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