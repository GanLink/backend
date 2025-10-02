using System;

namespace GanLink.BovinueSystem.Domain.Models.Commands;

/// <summary>
/// Comando para crear una nueva categoría de métrica para bovinos
/// </summary>
public record CreateBovinueMetricCategoryCommand(string Category);