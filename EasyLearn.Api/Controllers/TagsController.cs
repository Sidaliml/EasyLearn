using EasyLearn.Api.DTOs;
using EasyLearn.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyLearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly TagService _service;

    public TagsController(TagService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tags = await _service.GetAllAsync();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tag = await _service.GetByIdAsync(id);
        return tag == null ? NotFound() : Ok(tag);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TagUpdateDto dto)
    {
        var created = await _service.CreateAsync(dto.Name);
        if (created == null) return BadRequest("Invalid name or already exists.");

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TagUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto.Name);
        if (updated == null) return BadRequest("Invalid id or name.");

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    // GET /api/Tags/assignment/{assignmentId}
    [HttpGet("assignment/{assignmentId}")]
    public async Task<IActionResult> GetTagsForAssignment(int assignmentId)
    {
        var tags = await _service.GetTagsForAssignmentAsync(assignmentId);
        return Ok(tags);
    }

    // POST /api/Tags/assignment/{assignmentId}  body: { "tagId": 1 }
    public class AddTagDto
    {
        public int TagId { get; set; }
    }

    [HttpPost("assignment/{assignmentId}")]
    public async Task<IActionResult> AddTagToAssignment(int assignmentId, [FromBody] AddTagDto dto)
    {
        var ok = await _service.AddTagToAssignmentAsync(assignmentId, dto.TagId);
        if (!ok) return BadRequest("Invalid assignmentId/tagId or tag already added.");

        return Ok();
    }

    // DELETE /api/Tags/assignment/{assignmentId}/{tagId}
    [HttpDelete("assignment/{assignmentId}/{tagId}")]
    public async Task<IActionResult> RemoveTagFromAssignment(int assignmentId, int tagId)
    {
        var ok = await _service.RemoveTagFromAssignmentAsync(assignmentId, tagId);
        if (!ok) return BadRequest("Invalid assignmentId/tagId or link not found.");

        return NoContent();
    }
}