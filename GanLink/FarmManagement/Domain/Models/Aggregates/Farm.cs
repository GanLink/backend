using System.ComponentModel.DataAnnotations;
using GanLink.FarmManagement.Domain.Models.ValueObjects;
using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.IAM.Domain.Models.Aggregates;

namespace GanLink.FarmManagement.Domain.Models.Aggregates;

public partial class Farm
{
    // Ctor principal (dominio)
    public Farm(string alias, int userId, Activity mainActivity, string ownerDni)
    {
        SetAlias(alias);
        SetOwnerDni(ownerDni);

        UserId = userId;
        MainActivity = mainActivity;
    }

    // Conveniencia: desde el comando
    public Farm(CreateFarmCommand command)
        : this(command.Alias, command.UserId, command.MainActivity, command.OwnerDni) { }

    // EF Core necesita esto (mejor protegido)
    protected Farm() { }

    public int Id { get; private set; }

    [Required, StringLength(120)]
    public string Alias { get; private set; } = null!;

    public int UserId { get; private set; }

    [Required]
    public Activity MainActivity { get; private set; }

    // Perú: 8 dígitos
    [Required, StringLength(8, MinimumLength = 8)]
    [RegularExpression(@"^\d{8}$")]
    public string OwnerDni { get; private set; } = null!;

    // Navegación (por convención FK = UserId)
    public required User User { get; set; } = null!;

    // --- Comportamientos del agregado ---
    public void SetAlias(string alias)
    {
        if (string.IsNullOrWhiteSpace(alias))
            throw new ArgumentException("Alias no puede estar vacío.", nameof(alias));
        Alias = alias.Trim();
    }

    public void SetOwnerDni(string dni)
    {
        if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8 || !dni.All(char.IsDigit))
            throw new ArgumentException("OwnerDNI debe tener 8 dígitos.", nameof(dni));
        OwnerDni = dni;
    }

    public void ChangeMainActivity(Activity activity) => MainActivity = activity;
}