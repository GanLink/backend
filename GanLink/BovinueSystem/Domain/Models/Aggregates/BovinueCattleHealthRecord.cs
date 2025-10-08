using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GanLink.BovinueSystem.Domain.Models.Commands;

namespace GanLink.BovinueSystem.Domain.Models.Aggregates;

public partial class BovinueCattleHealthRecord
{
    protected BovinueCattleHealthRecord()
    {
        ActivityName = string.Empty;
        Description = string.Empty;
        deleted = false;
    }

    public long Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string ActivityName { get; set; }

    [Required]
    [Column("Frecuency")]
    public int Frequency { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string Description { get; set; }

    [Required]
    public bool deleted { get; set; }
    
    public virtual ICollection<BovinueHealthRecord> BovinueHealthRecords { get; set; }
}
    

