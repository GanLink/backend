using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record CreateBovinueHealthRecordCommand(
    long bovinueId,
    long bovinueCHRId,
    DateTime startDate,
    DateTime? endDate
);
