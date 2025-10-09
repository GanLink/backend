using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GanLink.BovinueSystem.Domain.Models.ValueObjects;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates
{
    // DATASET - Solo lectura, sin métodos de comando
    public partial class BovinueMetricParameter
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long CategoryId { get; set; } // FK to BovinueMetricCategory

        [Required]
        [StringLength(100)]
        public string Parameter { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public bool deleted { get; set; } // Asegurarse de que sea público

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual BovinueMetricCategory Category { get; set; }

        public virtual ICollection<BovinueMetric> Metrics { get; set; }
    }
}
