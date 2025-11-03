using EmailNotifications.Application.Dtos;
using EmailNotifications.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailNotifications.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateService _templateService;

    public TemplatesController(ITemplateService templateService)
    {
        _templateService = templateService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _templateService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _templateService.GetAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] TemplateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _templateService.SaveAsync(request);
        var message = request.Id.HasValue && request.Id.Value > 0 ? "Template updated" : "Template created";
        return Ok(new { Message = message, TemplateId = result.Id });
    }
}