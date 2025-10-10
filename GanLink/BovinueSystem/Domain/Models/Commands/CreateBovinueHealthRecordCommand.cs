using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record CreateBovinueHealthRecordCommand(
    long bovinueId,
    long bovinueCHRId,
    DateTimeOffset? startDate,
    DateTimeOffset? endDate
);
