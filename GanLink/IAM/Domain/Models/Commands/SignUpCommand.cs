using System;

namespace GanLink.IAM.Domain.Models.Commands;

public record SignUpCommand(string Username, 
    string Email,
    string Password, 
    string TypeUser, 
    TimeSpan MaxDailyReservationHour, 
    string IdentificationUser );