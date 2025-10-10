using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.FarmManagement.Domain.Models.Aggregates;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates
{
    public partial class Bovinue
    {
        protected Bovinue()
        {
            FarmId = 0;
            deleted = false;
        }

        public Bovinue(CreateBovinueCommand command)
        {
            FarmId = command.farmId;
            deleted = false;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public int FarmId { get; set; }

        [ForeignKey("FarmId")]
        public virtual Farm farm { get; set; }

        [Required]
        public bool deleted { get; set; }

        // Navigation properties
        public virtual ICollection<BovinueHealthRecord> HealthRecords { get; set; }
        public virtual ICollection<BovinueMetric> Metrics { get; set; }

        public void UpdateFromCommand(UpdateBovinueCommand command)
        {
            FarmId = command.farmId;
        }

        public void DeleteFromCommand(DeleteBovinueCommand command)
        {
            if (command.id != this.Id)
                throw new InvalidOperationException("El id del comando no coincide con la entidad Bovinue.");

            if (!deleted) deleted = true;
        }
    }
}
