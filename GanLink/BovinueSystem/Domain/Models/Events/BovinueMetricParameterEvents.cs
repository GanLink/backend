using System;

namespace GanLink.BovinueSystem.Domain.Models.Events;

/// <summary>
/// Evento que se dispara cuando se crea un nuevo parámetro de métrica
/// </summary>
public record BovinueMetricParameterCreatedEvent : DomainEvent
{
    public long ParameterId { get; }
    public long CategoryId { get; }
    public string Parameter { get; }
    public string? Description { get; }
    
    public BovinueMetricParameterCreatedEvent(long parameterId, long categoryId, string parameter, string? description) : base()
    {
        ParameterId = parameterId;
        CategoryId = categoryId;
        Parameter = parameter;
        Description = description;
    }
}

/// <summary>
/// Evento que se dispara cuando se actualiza un parámetro de métrica
/// </summary>
public record BovinueMetricParameterUpdatedEvent : DomainEvent
{
    public long ParameterId { get; }
    public long CategoryId { get; }
    public string Parameter { get; }
    public string? Description { get; }
    
    public BovinueMetricParameterUpdatedEvent(long parameterId, long categoryId, string parameter, string? description) : base()
    {
        ParameterId = parameterId;
        CategoryId = categoryId;
        Parameter = parameter;
        Description = description;
    }
}

/// <summary>
/// Evento que se dispara cuando se elimina un parámetro de métrica
/// </summary>
public record BovinueMetricParameterDeletedEvent : DomainEvent
{
    public long ParameterId { get; }
    
    public BovinueMetricParameterDeletedEvent(long parameterId) : base()
    {
        ParameterId = parameterId;
    }
}