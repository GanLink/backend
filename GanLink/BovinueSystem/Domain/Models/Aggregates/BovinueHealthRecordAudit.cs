using System;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates
{
    public partial class BovinueHealthRecord : IEntityWithCreatedUpdatedDate
    {
        [Column("CreatedAt")] 
        public DateTimeOffset? CreatedDate { get; set; }
        
        [Column("UpdatedAt")] 
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}