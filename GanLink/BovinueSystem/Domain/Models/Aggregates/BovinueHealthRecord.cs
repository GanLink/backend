namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovineHealthRecord
{
    public int Id { get; private set; }
    public int BovineCHRId { get; private set; } // FK a catálogo/registro de tipo CHR
    public int BovineId { get; private set; }    // FK al bovino
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
}