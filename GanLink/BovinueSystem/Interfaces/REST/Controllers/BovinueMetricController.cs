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
    public class BovinueMetricController : ControllerBase
    {
        private readonly IBovinueMetricCommandService _commandService;
        private readonly IBovinueMetricQueryService _queryService;

        public BovinueMetricController(
            IBovinueMetricCommandService commandService,
            IBovinueMetricQueryService queryService)
        {
            _commandService = commandService;
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllBovinueMetricsQuery();
            var metrics = await _queryService.Handle(query);
            var resources = metrics.Select(MetricResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var query = new GetBovinueMetricByIdQuery(id);
            var metric = await _queryService.Handle(query);
            
            if (metric == null)
                return NotFound();
            
            var resource = MetricResourceFromEntityAssembler.ToResourceFromEntity(metric);
            return Ok(resource);
        }

        [HttpGet("bovinue/{bovinueId}")]
        public async Task<IActionResult> GetByBovinueId(long bovinueId)
        {
            var query = new GetBovinueMetricsByBovinueIdQuery(bovinueId);
            var metrics = await _queryService.Handle(query);
            var resources = metrics.Select(MetricResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpGet("bovinue/{bovinueId}/parameter/{parameterId}")]
        public async Task<IActionResult> GetByParameter(long bovinueId, long parameterId)
        {
            var query = new GetBovinueMetricsByParameterQuery(bovinueId, parameterId);
            var metrics = await _queryService.Handle(query);
            var resources = metrics.Select(MetricResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMetricResource resource)
        {
            var command = CreateMetricCommandFromResourceAssembler.ToCommandFromResource(resource);
            var metric = await _commandService.Handle(command);
            var metricResource = MetricResourceFromEntityAssembler.ToResourceFromEntity(metric);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = metric.Id },
                metricResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateMetricResource resource)
        {
            var command = UpdateMetricCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var metric = await _commandService.Handle(command);
            var metricResource = MetricResourceFromEntityAssembler.ToResourceFromEntity(metric);
            return Ok(metricResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var command = new DeleteBovinueMetricCommand(id);
            await _commandService.Handle(command);
            return NoContent();
        }
    }
}