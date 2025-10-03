using System;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueMetric : IEntityWithCreatedUpdatedDate
{
    /// <summary>Fecha/hora de creación (mapeada a 'CreatedAt').</summary>
    [Column("CreatedAt")]
    public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>Fecha/hora de última actualización (mapeada a 'UpdatedAt').</summary>
    [Column("UpdatedAt")]
    public DateTimeOffset? UpdatedDate { get; set; }
}