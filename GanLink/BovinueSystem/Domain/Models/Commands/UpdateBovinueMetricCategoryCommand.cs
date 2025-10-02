using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

/// <summary>
/// Comando para actualizar una categoría de métrica existente
/// </summary>
public record UpdateBovinueMetricCategoryCommand(long Id, string Category);