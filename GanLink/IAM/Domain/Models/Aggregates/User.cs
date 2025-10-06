using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GanLink.IAM.Domain.Models.Commands;

namespace GanLink.IAM.Domain.Models.Aggregates;

public partial class User
{
    public User(SignUpCommand command, string hashedPassword)
    {
        Username = command.Username;
        Email = command.Email;
        Password = hashedPassword;
        TypeUser = command.TypeUser;
        MaxDailyReservationHour = command.MaxDailyReservationHour;
        IdentificationUser = command.IdentificationUser;
    }
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Username { get; private set; }
    [Required]
    [StringLength(100)]
    public string Email { get; private set; }
    [Required]
    public string Password { get; private set; }
    [Required]
    [StringLength(10)]
    public string TypeUser { get; private set; }
    [Required]
    public TimeSpan MaxDailyReservationHour { get; private set; }
    [StringLength(50)]
    public string IdentificationUser { get; private set; }
    
    //public ICollection<PaymentInformation> PaymentInformation { get; set; } = new List<PaymentInformation>();
    //public ICollection<Rent> Rents { get; set; } = new List<Rent>();
    
    public User()
    {
        this.Username = string.Empty;
        this.Email = string.Empty;
        this.Password = string.Empty;
        this.TypeUser = string.Empty;
        this.MaxDailyReservationHour = TimeSpan.Zero;
        this.IdentificationUser = string.Empty;
    }
}