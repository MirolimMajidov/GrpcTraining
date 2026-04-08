using GrpcServiceClient.Contracts;
using GrpcServiceClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrpcServiceClient.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(userService.GetAll());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var user = userService.GetById(id);
        if (user is null)
            return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create(CreateUser request)
    {
        var user = userService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateUser request)
    {
        var success = userService.Update(id, request);
        if (!success)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var success = userService.DeleteById(id);
        if (!success)
            return NotFound();
        return NoContent();
    }
}