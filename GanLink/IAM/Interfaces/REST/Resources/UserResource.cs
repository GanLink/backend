using System;
using GanLink.IAM.Domain.Models.ValueObjects;

namespace GanLink.IAM.Interfaces.REST.Resources;

public record UserResource(string Username,
    string FirstName,
    string LastName,
    string Email,
    RUC Ruc,
    string Password 
    );