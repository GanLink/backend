using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

/// <summary>
/// Comando para actualizar un parámetro de métrica existente
/// </summary>
public record UpdateBovinueMetricParameterCommand(
    long Id,
    long CategoryId,
    string Parameter,
    string Description
);