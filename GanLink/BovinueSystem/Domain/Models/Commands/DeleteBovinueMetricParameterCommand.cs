using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

/// <summary>
/// Comando para eliminar un parámetro de métrica
/// </summary>
public record DeleteBovinueMetricParameterCommand(long Id);