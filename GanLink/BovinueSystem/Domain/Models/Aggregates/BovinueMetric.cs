namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovineMetric
{
    public int Id { get; private set; }
    public int BovineMPId { get; private set; } // FK al tipo de métrica (peso, leche, etc.)
    public int BovineId { get; private set; }   // FK al bovino
    public DateTime Date { get; private set; }
    public double Quantity { get; private set; }
}