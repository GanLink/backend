namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record CreateBovinueMetricCommand(
    long bovinueId,
    long bovinueMPId,
    DateTime date,
    double quantity
);
