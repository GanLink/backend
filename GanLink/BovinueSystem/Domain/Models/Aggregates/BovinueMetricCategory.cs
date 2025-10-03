using System;
using System.Collections.Generic;
using GanLink.BovinueSystem.Domain.Models.ValueObjects;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueMetricCategory
{

    public long Id { get; private set; }

    public MetricCategory Category { get; private set; }
    
    public ICollection<BovinueMetricParameter> Parameters { get; private set; }


    public BovinueMetricCategory() {}

    public BovinueMetricCategory(string category)
    {
        ReassignCategory(category);
    }

    public void ReassignCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category es requerido.", nameof(category));

        category = category.Trim();
        if (category.Length > 100)
            throw new ArgumentException("Category excede 100 caracteres.", nameof(category));
        
    }
}