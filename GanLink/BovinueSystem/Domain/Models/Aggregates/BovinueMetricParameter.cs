using System;
using GanLink.BovinueSystem.Domain.Models.ValueObjects;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueMetricParameter
{

    public long Id { get; set; }
    
    public long CategoryId { get; set; }
    public BovinueMetricCategory Category { get; set; }
    
    public MetricParameter Parameter { get; set; } 

    public string Description { get; set; } = string.Empty;

    public BovinueMetricParameter() {}
    
    public BovinueMetricParameter(long categoryId, string parameter, string? description = null)
    {
        ReassignCategory(categoryId);
        ReassignParameter(parameter);
        ReassignDescription(description);
    }
    
    public void ReassignCategory(long categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentOutOfRangeException(nameof(categoryId), "CategoryId debe ser > 0.");

        CategoryId = categoryId;
    }
    
    public void ReassignParameter(string parameter)
    {
        if (string.IsNullOrWhiteSpace(parameter))
            throw new ArgumentException("Parameter es requerido.", nameof(parameter));

        parameter = parameter.Trim();
        if (parameter.Length > 160)
            throw new ArgumentException("Parameter excede 160 caracteres.", nameof(parameter));

    }

    public void ReassignDescription(string? description)
    {
        var d = (description ?? string.Empty).Trim();
        if (d.Length > 280)
            throw new ArgumentException("Description excede 280 caracteres.", nameof(description));

        Description = d;
    }
}
