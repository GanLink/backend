using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GanLink.FarmManagement.Domain.Models.ValueObjects;
using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.IAM.Domain.Models.Aggregates;

namespace GanLink.FarmManagement.Domain.Models.Aggregates;

public partial class Farm
{

    public Farm(CreateFarmCommand command)
    {
        this.Alias = command.Alias;
        this.UserId = command.UserId;
        this.MainActivity = command.MainActivity;
        this.OwnerDni = command.OwnerDni;
    }
    
    public int Id { get; private set; }
    public string Alias { get; private set; }
    public int UserId { get; private set; }
    public Activity MainActivity { get; private set; }
    public string OwnerDni { get; private set; }
    
    [Required]
    [ForeignKey("UserId")]
    public required User user { get; set; }

    public Farm()
    {
        Id = 0;
        Alias = string.Empty;
        UserId = 0;
        MainActivity = Activity.CARNE;
        OwnerDni = string.Empty;
    }
}