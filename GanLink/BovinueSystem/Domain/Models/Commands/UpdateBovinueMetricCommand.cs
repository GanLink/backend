using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record UpdateBovinueMetricCommand(
    long bovinueId,
    long bovinueMPId,
    DateTime date,
    double quantity
);
