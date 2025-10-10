
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GanLink.BovinueSystem.Domain.Models.Commands;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates
{
    public partial class BovinueMetric
    {
        protected BovinueMetric()
        {
            BovinueMPId = 0;
            BovinueId = 0;
            Date = DateTime.UtcNow;
            Quantity = 0;
            deleted = false;
        }

        public BovinueMetric(CreateBovinueMetricCommand command)
        {
            BovinueMPId = command.bovinueMPId;
            BovinueId = command.bovinueId;
            Date = command.date;
            Quantity = command.quantity;
            deleted = false;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public long BovinueMPId { get; set; } // FK to BovinueMetricParameter

        [Required]
        public long BovinueId { get; set; } // FK to Bovinue

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public bool deleted { get; set; }

        // Navigation properties
        [ForeignKey("BovinueId")]
        public virtual Bovinue Bovinue { get; set; }

        [ForeignKey("BovinueMPId")]
        public virtual BovinueMetricParameter BovinueMetricParameter { get; set; }

        public void UpdateFromCommand(UpdateBovinueMetricCommand command)
        {
            BovinueMPId = command.bovinueMPId;
            Date = command.date;
            Quantity = command.quantity;
        }

        public void DeleteFromCommand(DeleteBovinueMetricCommand command)
        {
            if (command.id != this.Id)
                throw new InvalidOperationException("El id del comando no coincide con la entidad.");

            if (!deleted) deleted = true;
        }
    }
}