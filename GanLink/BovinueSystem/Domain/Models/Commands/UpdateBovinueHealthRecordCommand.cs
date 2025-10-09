namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record UpdateBovinueHealthRecordCommand(
    long id,
    long bovinueCHRId,
    long bovinueId,
    DateTimeOffset? startDate,
    DateTimeOffset? endDate
    );