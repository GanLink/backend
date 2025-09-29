using System.ComponentModel.DataAnnotations.Schema;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueMetricParameter
{
    /// <summary>Fecha/hora de creación (mapeada a 'CreatedAt').</summary>
    [Column("CreatedAt")]
    public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>Fecha/hora de última actualización (mapeada a 'UpdatedAt').</summary>
    [Column("UpdatedAt")]
    public DateTimeOffset? UpdatedDate { get; set; }
}