using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GanLink.BovinueSystem.Domain.Models.Commands;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueHealthRecord
{
    protected BovinueHealthRecord()
    {
        deleted = false;
    }

    public BovinueHealthRecord(CreateBovinueHealthRecordCommand command)
    {
        BovinueId   = command.bovinueId;
        BovinueCHRId = command.bovinueCHRId;
        StartDate   = command.startDate;
        EndDate     = command.endDate;
        deleted     = false;
    }

    public long Id { get; set; }

    [Required]
    public long BovinueId { get; set; }

    [ForeignKey("BovinueId")]
    [Required]
    public required Bovinue bovinue { get; set; }

    [Required]
    public long BovinueCHRId { get; set; }

    [ForeignKey("BovinueCHRId")]
    [Required]
    public required BovinueCattleHealthRecord bovinueCHR { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    // EndDate puede ser null si el registro está “abierto”.
    public DateTime? EndDate { get; set; }

    [Required]
    public bool deleted { get; set; }

    public void UpdateFromCommand(UpdateBovinueHealthRecordCommand command)
    {
        BovinueId    = command.bovinueId;
        BovinueCHRId = command.bovinueCHRId;
        StartDate    = command.startDate;
        EndDate      = command.endDate;
    }

    public void DeleteFromCommand(DeleteBovinueHealthRecordCommand command)
    {
        if (command.id != this.Id)
            throw new InvalidOperationException("El id del comando no coincide con la entidad BovinueHealthRecord.");

        if (!deleted) deleted = true; // Idempotente
    }
}
