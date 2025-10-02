using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

/// <summary>
/// Comando para crear un nuevo parámetro de métrica para bovinos
/// </summary>
public record CreateBovinueMetricParameterCommand(
    long CategoryId,
    string Parameter,
    string Description
);