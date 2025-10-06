using System;

namespace GanLink.IAM.Interfaces.REST.Resources;

public record SignUpResource(string Username, 
    string Email,
    string Password, 
    string TypeUser, 
    TimeSpan MaxDailyReservationHour, 
    string IdentificationUser );