using EasyLearn.Application.DTOs;
using EasyLearn.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly IAssignmentService _service;

    public AssignmentsController(IAssignmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var assignment = await _service.GetByIdAsync(id);
        return assignment == null ? NotFound() : Ok(assignment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AssignmentCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        if (created == null) return BadRequest("SubjectId does not exist.");

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] AssignmentUpdateDto dto)
    {
        var ok = await _service.UpdateAsync(id, dto);
        return ok ? NoContent() : BadRequest("Invalid data or assignment not found.");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpPatch("{id:int}/toggle")]
    public async Task<IActionResult> ToggleCompleted(int id)
    {
        var ok = await _service.ToggleCompletedAsync(id);
        return ok ? NoContent() : NotFound();
    }
}