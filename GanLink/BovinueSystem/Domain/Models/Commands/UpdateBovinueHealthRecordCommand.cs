
using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record UpdateBovinueHealthRecordCommand(
    long bovinueId,
    long bovinueCHRId,
    DateTime startDate,
    DateTime? endDate
);
