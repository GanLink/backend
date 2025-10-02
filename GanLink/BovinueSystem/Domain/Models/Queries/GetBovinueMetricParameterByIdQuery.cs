using System;
using System.Collections.Generic;

namespace GanLink.BovinueSystem.Domain.Models.Queries;

/// <summary>
/// Query para obtener un parámetro de métrica por su ID
/// </summary>
public record GetBovinueMetricParameterByIdQuery(long Id);