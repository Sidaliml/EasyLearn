using EasyLearn.Api.DTOs;
using EasyLearn.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyLearn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly TagService _service;
    private readonly ILogger<TagsController> _logger;

    public TagsController(
        TagService service,
        ILogger<TagsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GET all tags called");
        var tags = await _service.GetAllAsync();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("GET tag by id {Id}", id);

        var tag = await _service.GetByIdAsync(id);
        return tag == null ? NotFound() : Ok(tag);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TagUpdateDto dto)
    {
        _logger.LogInformation("POST create tag called");

        var created = await _service.CreateAsync(dto.Name);
        if (created == null) return BadRequest("Invalid name or already exists.");

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TagUpdateDto dto)
    {
        _logger.LogInformation("PUT update tag {Id}", id);

        var updated = await _service.UpdateAsync(id, dto.Name);
        if (updated == null) return BadRequest("Invalid id or name.");

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("DELETE tag {Id}", id);

        var ok = await _service.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    // GET /api/Tags/assignment/{assignmentId}
    [HttpGet("assignment/{assignmentId}")]
    public async Task<IActionResult> GetTagsForAssignment(int assignmentId)
    {
        _logger.LogInformation("GET tags for assignment {AssignmentId}", assignmentId);

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
        _logger.LogInformation("POST add tag {TagId} to assignment {AssignmentId}", dto.TagId, assignmentId);

        var ok = await _service.AddTagToAssignmentAsync(assignmentId, dto.TagId);
        if (!ok) return BadRequest("Invalid assignmentId/tagId or tag already added.");

        return Ok();
    }

    // DELETE /api/Tags/assignment/{assignmentId}/{tagId}
    [HttpDelete("assignment/{assignmentId}/{tagId}")]
    public async Task<IActionResult> RemoveTagFromAssignment(int assignmentId, int tagId)
    {
        _logger.LogInformation("DELETE remove tag {TagId} from assignment {AssignmentId}", tagId, assignmentId);

        var ok = await _service.RemoveTagFromAssignmentAsync(assignmentId, tagId);
        if (!ok) return BadRequest("Invalid assignmentId/tagId or link not found.");

        return NoContent();
    }
}