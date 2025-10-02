using System;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueMetricParameter
{
    /// <summary>
    /// Identificador único del parámetro/métrica.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id de la categoría a la que pertenece (BovinueMetricCategory).
    /// </summary>
    public long CategoryId { get; set; }
    public BovinueMetricCategory Category { get; set; }

    /// <summary>
    /// Nombre del parámetro. Ej.: "Índice de conversión alimenticia".
    /// </summary>
    public string Parameter { get; set; } = string.Empty;

    /// <summary>
    /// Descripción opcional del parámetro.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Constructor por defecto requerido por EF/Core serializers.
    /// </summary>
    public BovinueMetricParameter() {}

    /// <summary>
    /// Crea un parámetro garantizando reglas mínimas.
    /// </summary>
    public BovinueMetricParameter(long categoryId, string parameter, string? description = null)
    {
        ReassignCategory(categoryId);
        ReassignParameter(parameter);
        ReassignDescription(description);
    }

    /// <summary>
    /// Reasigna la categoría (debe ser &gt; 0).
    /// </summary>
    public void ReassignCategory(long categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentOutOfRangeException(nameof(categoryId), "CategoryId debe ser > 0.");

        CategoryId = categoryId;
    }

    /// <summary>
    /// Renombra el parámetro aplicando validaciones mínimas.
    /// </summary>
    public void ReassignParameter(string parameter)
    {
        if (string.IsNullOrWhiteSpace(parameter))
            throw new ArgumentException("Parameter es requerido.", nameof(parameter));

        parameter = parameter.Trim();
        if (parameter.Length > 160)
            throw new ArgumentException("Parameter excede 160 caracteres.", nameof(parameter));

        Parameter = parameter;
    }

    /// <summary>
    /// Actualiza la descripción (opcional, longitud acotada).
    /// </summary>
    public void ReassignDescription(string? description)
    {
        var d = (description ?? string.Empty).Trim();
        if (d.Length > 280)
            throw new ArgumentException("Description excede 280 caracteres.", nameof(description));

        Description = d;
    }
}
