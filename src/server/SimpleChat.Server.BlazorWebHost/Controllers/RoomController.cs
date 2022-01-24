using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.Core.Domain.Models;
using SimpleChat.Server.Application.Interfaces;
using SimpleChat.Server.Domain.Models;

namespace SimpleChat.Server.BlazorWebHost.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] Room room)
    {
        if (!ModelState.IsValid)
            return BadRequest(new RoomCreateResponse
            {
                Succeeded = false,
                Notifications = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
            });

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var response = await _roomService.Create(Guid.Parse(userId), room.Name);
        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _roomService.GetAll();
        if (!response.Any())
            return NoContent();
        return Ok(response);
    }
}
