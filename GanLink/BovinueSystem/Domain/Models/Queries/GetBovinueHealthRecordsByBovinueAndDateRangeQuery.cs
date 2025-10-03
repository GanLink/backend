namespace GanLink.BovinueSystem.Domain.Models.Queries;

public record GetBovinueHealthRecordsByBovinueAndDateRangeQuery(
    long bovinueId,
    DateTime from,
    DateTime to
);
