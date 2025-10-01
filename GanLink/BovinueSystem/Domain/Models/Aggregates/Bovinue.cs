namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

/// <summary>
/// Representa un bovino dentro del sistema Bovinue.
/// Solo contiene la referencia a la granja a la que pertenece.
/// </summary>
public partial class Bovinue
{
    /// <summary>
    /// Identificador único del bovino.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Identificador de la granja (clave foránea con Farm).
    /// </summary>
    public int FarmId { get; private set; }
    
}