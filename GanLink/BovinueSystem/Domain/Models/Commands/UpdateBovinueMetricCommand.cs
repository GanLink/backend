using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record UpdateBovinueMetricCommand(
        long id,
        long bovinueMPId,
        long bovinueId,
        DateTime date,
        double quantity);

