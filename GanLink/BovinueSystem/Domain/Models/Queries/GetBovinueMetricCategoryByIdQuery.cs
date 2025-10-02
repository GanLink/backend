using System;
using System.Collections.Generic;

namespace GanLink.BovinueSystem.Domain.Models.Queries;

/// <summary>
/// Query para obtener una categoría de métrica por su ID
/// </summary>
public record GetBovinueMetricCategoryByIdQuery(long Id);