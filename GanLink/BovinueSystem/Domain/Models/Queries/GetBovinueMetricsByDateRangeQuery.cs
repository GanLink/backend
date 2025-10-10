namespace GanLink.BovinueSystem.Domain.Models.Queries;

public record GetBovinueMetricsByDateRangeQuery(
    long bovinueId,
    DateTime startDate,
    DateTime endDate);