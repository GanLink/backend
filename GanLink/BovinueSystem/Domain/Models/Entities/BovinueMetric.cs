
using GanLink.BovinueSystem.Domain.Models.Aggregates;

namespace GanLink.BovinueSystem.Domain.Models.Entities;

/// <summary>
/// Entidad para representar una métrica de un bovino
/// </summary>
public class BovinueMetric
{
    /// <summary>
    /// Identificador único de la métrica
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Identificador del parámetro de métrica asociado
    /// </summary>
    public long BovinueMPId { get; private set; }
    
    /// <summary>
    /// Fecha de la medición
    /// </summary>
    public DateTime Date { get; private set; }
    
    /// <summary>
    /// Identificador del bovino al que pertenece la métrica
    /// </summary>
    public long BovinueId { get; private set; }
    public Bovinue Bovinue { get; private set; }
    
    /// <summary>
    /// Cantidad o valor numérico de la métrica
    /// </summary>
    public double Quantity { get; private set; }

    /// <summary>
    /// Constructor vacío para ORM
    /// </summary>
    protected BovinueMetric() { }

    /// <summary>
    /// Constructor para crear una nueva métrica de bovino
    /// </summary>
    public BovinueMetric(long bovinueMPId, DateTime date, long bovinueId, double quantity)
    {
        if (bovinueMPId <= 0)
            throw new ArgumentException("BovinueMPId debe ser mayor que 0", nameof(bovinueMPId));
        
        if (bovinueId <= 0)
            throw new ArgumentException("BovinueId debe ser mayor que 0", nameof(bovinueId));
        
        BovinueMPId = bovinueMPId;
        Date = date;
        BovinueId = bovinueId;
        Quantity = quantity;
    }

    /// <summary>
    /// Actualiza el valor de la métrica
    /// </summary>
    public void UpdateQuantity(double quantity)
    {
        Quantity = quantity;
    }
}