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
    public class BovinueController : ControllerBase
    {
        private readonly IBovinueCommandService _commandService;
        private readonly IBovinueQueryService _queryService;

        public BovinueController(
            IBovinueCommandService commandService,
            IBovinueQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBovinuesQuery();
            var bovinues = await _queryService.Handle(query);
            var resources = bovinues.Select(BovinueResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetBovinueByIdQuery(id);
            var bovinue = await _queryService.Handle(query);
            
            if (bovinue == null)
                return NotFound();
            
            var resource = BovinueResourceFromEntityAssembler.ToResourceFromEntity(bovinue);
            return Ok(resource);
        }

        [HttpGet("farm/{farmId}")]
        public async Task<IActionResult> GetByFarmId(int farmId)
        {
            var query = new GetBovinuesByFarmIdQuery(farmId);
            var bovinues = await _queryService.Handle(query);
            var resources = bovinues.Select(BovinueResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new SearchBovinuesQuery(keyword, page, pageSize);
            var bovinues = await _queryService.Handle(query);
            var resources = bovinues.Select(BovinueResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBovinueResource resource)
        {
            var command = CreateBovinueCommandFromResourceAssembler.ToCommandFromResource(resource);
            var bovinue = await _commandService.Handle(command);
            var bovinueResource = BovinueResourceFromEntityAssembler.ToResourceFromEntity(bovinue);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = bovinue.Id },
                bovinueResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateBovinueResource resource)
        {
            var command = UpdateBovinueCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var bovinue = await _commandService.Handle(command);
            var bovinueResource = BovinueResourceFromEntityAssembler.ToResourceFromEntity(bovinue);
            return Ok(bovinueResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteBovinueCommand(id);
            await _commandService.Handle(command);
            return NoContent();
        }
    }
}