using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.FarmManagement.Domain.Models.ValueObjects;
using GanLink.IAM.Domain.Models.Aggregates;

namespace GanLink.FarmManagement.Domain.Models.Aggregates;

public partial class Farm
{
    public int Id { get; private set; }
    public string Alias { get; private set; }
    public int UserId { get; private set; }
    // Enum del dominio (ver abajo)
    public Activity MainActivity { get; private set; }

    // DNI como string de 8 dígitos (o VO Dni)
    [Required, RegularExpression(@"^\d{8}$")]
    public string OwnerDni { get; private set; }

    // Ctor de caso de uso
    public Farm(CreateFarmCommand command)
    {
        SetAlias(command.Alias);
        SetUser(command.UserId);
        SetActivity(command.MainActivity);   // mapear a enum
        SetOwnerDni(command.OwnerDni);
    }

    // Ctor para EF
    protected Farm() { }

    // Métodos de dominio para proteger invariantes
    public void SetAlias(string alias)
    {
        if (string.IsNullOrWhiteSpace(alias)) throw new ArgumentException("Alias requerido");
        if (alias.Length > 80) throw new ArgumentException("Alias demasiado largo");
        Alias = alias.Trim();
    }

    public void SetUser(int userId)
    {
        if (userId <= 0) throw new ArgumentOutOfRangeException(nameof(userId));
        UserId = userId;
    }

    public void SetActivity(Activity activity)
    {
        if (!Enum.IsDefined(typeof(Activity), activity))
            throw new ArgumentException("Actividad inválida");
        MainActivity = activity;
    }

    public void SetOwnerDni(string dni)
    {
        if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8 || !dni.All(char.IsDigit))
            throw new ArgumentException("DNI inválido");
        OwnerDni = dni;
    }
}


