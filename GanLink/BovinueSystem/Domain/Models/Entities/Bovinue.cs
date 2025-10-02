using System;

namespace GanLink.BovinueSystem.Domain.Models.Entities;

/// <summary>
/// Entidad principal que representa un bovino en el sistema
/// </summary>
public class Bovinue
{
    /// <summary>
    /// Identificador único del bovino
    /// </summary>
    public long Id { get; private set; }
    
    /// <summary>
    /// Identificador de la granja a la que pertenece el bovino
    /// </summary>
    public long FarmId { get; private set; }
    
    /// <summary>
    /// Constructor vacío para ORM
    /// </summary>
    protected Bovinue() { }
    
    /// <summary>
    /// Constructor para crear un nuevo bovino
    /// </summary>
    public Bovinue(long farmId)
    {
        if (farmId <= 0)
            throw new ArgumentException("FarmId debe ser mayor que 0", nameof(farmId));
        
        FarmId = farmId;
    }
    
    /// <summary>
    /// Actualiza la granja a la que pertenece el bovino
    /// </summary>
    public void UpdateFarm(long farmId)
    {
        if (farmId <= 0)
            throw new ArgumentException("FarmId debe ser mayor que 0", nameof(farmId));
        
        FarmId = farmId;
    }
}