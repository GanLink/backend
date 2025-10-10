using System;
using GanLink.IAM.Domain.Models.ValueObjects;

namespace GanLink.IAM.Domain.Models.Commands;

public record SignUpCommand(
    string Username,
    string FirstName,
    string LastName,
    string Email,
    string Ruc,
    string Password
    );