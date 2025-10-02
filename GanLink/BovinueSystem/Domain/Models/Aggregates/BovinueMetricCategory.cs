using System;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueMetricCategory
{
    /// <summary>
    /// Identificador único de la categoría.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Nombre visible de la categoría. Ej.: "Eficiencia alimenticia".
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Constructor por defecto requerido por EF/Core serializers.
    /// </summary>
    public BovinueMetricCategory() {}

    /// <summary>
    /// Crea una categoría garantizando reglas mínimas.
    /// </summary>
    public BovinueMetricCategory(string category)
    {
        ReassignCategory(category);
    }

    /// <summary>
    /// Renombra la categoría aplicando validaciones mínimas.
    /// </summary>
    public void ReassignCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category es requerido.", nameof(category));

        category = category.Trim();
        if (category.Length > 100)
            throw new ArgumentException("Category excede 100 caracteres.", nameof(category));

        Category = category;
    }
}