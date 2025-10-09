namespace GanLink.BovinueSystem.Domain.Models.Queries;

public record GetBovinueMetricsByParameterQuery(
    long bovinueId,
    long parameterId);