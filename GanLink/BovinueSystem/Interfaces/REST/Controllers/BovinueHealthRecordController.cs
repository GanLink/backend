using System.Net.Mime;
using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Domain.Models.Queries;
using GanLink.BovinueSystem.Domain.Services;
using GanLink.BovinueSystem.Interfaces.REST.Resources;
using GanLink.BovinueSystem.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace GanLink.BovinueSystem.Interfaces.REST.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class BovinueHealthRecordController : ControllerBase
    {
        private readonly IBovinueHealthRecordCommandService _commandService;
        private readonly IBovinueHealthRecordQueryService _queryService;

        public BovinueHealthRecordController(
            IBovinueHealthRecordCommandService commandService,
            IBovinueHealthRecordQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBovinueHealthRecordsQuery();
            var records = await _queryService.Handle(query);
            var resources = records.Select(HealthRecordResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetBovinueHealthRecordByIdQuery(id);
            var record = await _queryService.Handle(query);
            
            if (record == null)
                return NotFound();
            
            var resource = HealthRecordResourceFromEntityAssembler.ToResourceFromEntity(record);
            return Ok(resource);
        }

        [HttpGet("bovinue/{bovinueId}")]
        public async Task<IActionResult> GetByBovinueId(long bovinueId)
        {
            var query = new GetBovinueHealthRecordsByBovinueIdQuery(bovinueId);
            var records = await _queryService.Handle(query);
            var resources = records.Select(HealthRecordResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpGet("bovinue/{bovinueId}/open")]
        public async Task<IActionResult> GetOpenRecordsByBovinueId(long bovinueId)
        {
            var query = new GetOpenBovinueHealthRecordsByBovinueIdQuery(bovinueId);
            var records = await _queryService.Handle(query);
            var resources = records.Select(HealthRecordResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHealthRecordResource resource)
        {
            var command = CreateHealthRecordCommandFromResourceAssembler.ToCommandFromResource(resource);
            var record = await _commandService.Handle(command);
            var recordResource = HealthRecordResourceFromEntityAssembler.ToResourceFromEntity(record);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = record.Id },
                recordResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateHealthRecordResource resource)
        {
            var command = UpdateHealthRecordCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var record = await _commandService.Handle(command);
            var recordResource = HealthRecordResourceFromEntityAssembler.ToResourceFromEntity(record);
            return Ok(recordResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteBovinueHealthRecordCommand(id);
            await _commandService.Handle(command);
            return NoContent();
        }
    }
}
