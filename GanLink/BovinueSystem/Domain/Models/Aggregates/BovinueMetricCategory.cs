using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GanLink.BovinueSystem.Domain.Models.ValueObjects;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates
{
    // DATASET - Solo lectura, sin métodos de comando
    public partial class BovinueMetricCategory
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        public bool deleted { get; set; }

        // Navigation properties
        public virtual ICollection<BovinueMetricParameter> MetricParameters { get; set; }
    }
}