using System;

namespace GanLink.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username,
    string FirstName,
    string LastName,
    string Email,
    string Ruc,
    string Password);