using System;

namespace GanLink.BovinueSystem.Domain.Models.Events;

/// <summary>
/// Evento base para todos los eventos de dominio
/// </summary>
public abstract record DomainEvent
{
    /// <summary>
    /// Identificador único del evento
    /// </summary>
    public Guid EventId { get; }
    
    /// <summary>
    /// Marca de tiempo en la que ocurrió el evento
    /// </summary>
    public DateTime OccurredOn { get; }
    
    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
}

/// <summary>
/// Evento que se dispara cuando se crea una nueva categoría de métrica
/// </summary>
public record BovinueMetricCategoryCreatedEvent : DomainEvent
{
    public long CategoryId { get; }
    public string Category { get; }
    
    public BovinueMetricCategoryCreatedEvent(long categoryId, string category) : base()
    {
        CategoryId = categoryId;
        Category = category;
    }
}

/// <summary>
/// Evento que se dispara cuando se actualiza una categoría de métrica
/// </summary>
public record BovinueMetricCategoryUpdatedEvent : DomainEvent
{
    public long CategoryId { get; }
    public string Category { get; }
    
    public BovinueMetricCategoryUpdatedEvent(long categoryId, string category) : base()
    {
        CategoryId = categoryId;
        Category = category;
    }
}

/// <summary>
/// Evento que se dispara cuando se elimina una categoría de métrica
/// </summary>
public record BovinueMetricCategoryDeletedEvent : DomainEvent
{
    public long CategoryId { get; }
    
    public BovinueMetricCategoryDeletedEvent(long categoryId) : base()
    {
        CategoryId = categoryId;
    }
}