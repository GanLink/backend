using System.ComponentModel.DataAnnotations;
using GanLink.FarmManagement.Domain.Models.ValueObjects;
using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.IAM.Domain.Models.Aggregates;

namespace GanLink.FarmManagement.Domain.Models.Aggregates;

public partial class Farm
{
    // Ctor principal (dominio)
    public Farm(string alias, string description, int userId, Activity mainActivity, string ownerDni)
    {
        SetAlias(alias);
        SetDescription(description);
        SetOwnerDni(ownerDni);

        UserId = userId;
        SetMainActivity(mainActivity);
    }

    // Conveniencia: desde el comando
    public Farm(CreateFarmCommand command)
        : this(command.Alias, command.Description, command.UserId, command.MainActivity, command.OwnerDni) { }

    // EF Core necesita esto (mejor protegido)
    protected Farm() { }

    public int Id { get; private set; }

    [Required, StringLength(120)]
    public string Alias { get; private set; } = null!;

    [Required, StringLength(500)] 
    public string Description { get; private set; } = null!;

    public int UserId { get; private set; }

    [Required(ErrorMessage = "La 'MainActivity' es obligatoria.")]
    [EnumDataType(typeof(Activity), ErrorMessage = "El valor proporcionado para 'MainActivity' no es válido.")]
    public Activity MainActivity { get; private set; }

    // Perú: 8 dígitos
    [Required, StringLength(8, MinimumLength = 8)]
    [RegularExpression(@"^\d{8}$")]
    public string OwnerDni { get; private set; } = null!;

    // Navegación (por convención FK = UserId)
    public required User User { get; set; } = null!;
    
    [StringLength(1024)]
    public string? ImageUrl { get; private set; }

    // --- Comportamientos del agregado ---
    public void SetAlias(string alias)
    {
        if (string.IsNullOrWhiteSpace(alias))
            throw new ArgumentException("Alias no puede estar vacío.", nameof(alias));
        Alias = alias.Trim();
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description no puede estar vacio.", nameof(description));
        Description = description.Trim();
    }

    public void SetOwnerDni(string dni)
    {
        if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8 || !dni.All(char.IsDigit))
            throw new ArgumentException("OwnerDNI debe tener 8 dígitos.", nameof(dni));
        OwnerDni = dni;
    }
    
    
    public void SetMainActivity(Activity activity)
    {
        
        if (!Enum.IsDefined(typeof(Activity), activity))
        {
            throw new ArgumentOutOfRangeException(nameof(activity), 
                $"El valor '{(int)activity}' no es una actividad principal (MainActivity) válida.");
        }
    
        MainActivity = activity;
    }

    public void ChangeMainActivity(Activity activity)
    {
        SetMainActivity(activity); // <-- ¡CAMBIO AQUÍ!
    
        
    }
    
    public void SetImageUrl(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            ImageUrl = null;
            return;
        }

        if (imageUrl.Length > 1024)
            throw new ArgumentException("La URL de la imagen es deamsiado larga.", nameof(imageUrl));
        ImageUrl = imageUrl;
    }
    
}