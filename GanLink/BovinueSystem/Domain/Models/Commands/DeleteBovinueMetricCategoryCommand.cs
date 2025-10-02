using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

/// <summary>
/// Comando para eliminar una categoría de métrica
/// </summary>
public record DeleteBovinueMetricCategoryCommand(long Id);