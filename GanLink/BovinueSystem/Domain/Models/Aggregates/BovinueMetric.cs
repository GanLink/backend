
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GanLink.BovinueSystem.Domain.Models.Commands;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueMetric
{
    protected BovinueMetric()
    {
        deleted = false;
    }

    public BovinueMetric(CreateBovinueMetricCommand command)
    {
        BovinueId    = command.bovinueId;
        BovinueMPId  = command.bovinueMPId;
        Date         = command.date;
        Quantity     = command.quantity;
        deleted      = false;
    }

    public long Id { get; set; }

    [Required]
    public long BovinueId { get; set; }

    [ForeignKey("BovinueId")]
    [Required]
    public required Bovinue bovinue { get; set; }

    [Required]
    public long BovinueMPId { get; set; }

    [ForeignKey("BovinueMPId")]
    [Required]
    public required BovinueMetricParameter parameter { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double Quantity { get; set; }

    [Required]
    public bool deleted { get; set; }

    public void UpdateFromCommand(UpdateBovinueMetricCommand command)
    {
        BovinueId   = command.bovinueId;
        BovinueMPId = command.bovinueMPId;
        Date        = command.date;
        Quantity    = command.quantity;
    }

    public void DeleteFromCommand(DeleteBovinueMetricCommand command)
    {
        if (command.id != this.Id)
            throw new InvalidOperationException("El id del comando no coincide con la entidad BovinueMetric.");

        if (!deleted) deleted = true; // idempotente
    }
}
