namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueCattleHealthRecord
{
    public int Id { get; private set; }

    public string ActivityName { get; private set; } = null!; // Nombre de la actividad (vacunación, control, etc.)

    public int Frecuency { get; private set; } // Frecuencia recomendada (en días)

    public string? Description { get; private set; } // Detalle o notas adicionales
}