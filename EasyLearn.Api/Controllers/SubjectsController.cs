using EasyLearn.Application.DTOs;
using EasyLearn.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyLearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _service;
    private readonly ILogger<SubjectsController> _logger;

    public SubjectsController(
        ISubjectService service,
        ILogger<SubjectsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GET all subjects called");
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("GET subject by id {Id}", id);

        var subject = await _service.GetByIdAsync(id);
        return subject == null ? NotFound() : Ok(subject);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SubjectCreateDto dto)
    {
        _logger.LogInformation("POST create subject called");

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] SubjectUpdateDto dto)
    {
        _logger.LogInformation("PUT update subject {Id}", id);

        var ok = await _service.UpdateAsync(id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE subject {Id}", id);

        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}