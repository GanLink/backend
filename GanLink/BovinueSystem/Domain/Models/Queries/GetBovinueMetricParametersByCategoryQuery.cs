using System;
using System.Collections.Generic;

namespace GanLink.BovinueSystem.Domain.Models.Queries;

/// <summary>
/// Query para listar parámetros de métricas por categoría
/// </summary>
public record GetBovinueMetricParametersByCategoryQuery(long CategoryId);