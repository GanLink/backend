using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GanLink.BovinueSystem.Domain.Models.Commands;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates
{
    public partial class BovinueHealthRecord
    {
        protected BovinueHealthRecord()
        {
            BovinueCHRId = 0;
            BovinueId = 0;
            StartDate = DateTime.UtcNow;
            deleted = false;
        }

        public BovinueHealthRecord(CreateBovinueHealthRecordCommand command)
        {
            BovinueCHRId = command.bovinueCHRId;
            BovinueId = command.bovinueId;
            StartDate = command.startDate;
            EndDate = command.endDate;
            deleted = false;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public long BovinueCHRId { get; set; } // FK to BovinueCattleHealthRecord

        [Required]
        public long BovinueId { get; set; } // FK to Bovinue

        [Required]
        public DateTimeOffset? StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        [Required]
        public bool deleted { get; set; }

        // Navigation properties
        [ForeignKey("BovinueId")]
        public virtual Bovinue Bovinue { get; set; }

        [ForeignKey("BovinueCHRId")]
        public virtual BovinueCattleHealthRecord BovinueCattleHealthRecord { get; set; }

        public void UpdateFromCommand(UpdateBovinueHealthRecordCommand command)
        {
            BovinueCHRId = command.bovinueCHRId;
            StartDate = command.startDate;
            EndDate = command.endDate;
        }

        public void DeleteFromCommand(DeleteBovinueHealthRecordCommand command)
        {
            if (command.id != this.Id)
                throw new InvalidOperationException("El id del comando no coincide con la entidad.");

            if (!deleted) deleted = true;
        }
    }
}