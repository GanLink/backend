using System;

namespace GanLink.IAM.Interfaces.REST.Resources;

public record UserResource(string Username, 
    string Email,
    string Password, 
    string TypeUser, 
    TimeSpan MaxDailyReservationHour, 
    string IdentificationUser );