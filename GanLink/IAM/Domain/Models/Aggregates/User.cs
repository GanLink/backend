using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Models.Commands;
using GanLink.IAM.Domain.Models.ValueObjects;

namespace GanLink.IAM.Domain.Models.Aggregates;

public partial class User
{
    public User(SignUpCommand command, string hashedPassword)
    {
        Username = command.Username;
        Firstname = command.FirstName;
        Lastname = command.LastName;
        Email = command.Email;
        Ruc = new RUC(command.Ruc);
        Password = hashedPassword;
    }
    public int Id { get; set; }
    
    [Required]
    [StringLength(10)]
    public string Username { get; private set; }
    
    [Required]
    [StringLength(10)]
    public string Firstname { get; private set; }
    
    [Required]
    [StringLength(20)]
    public string Lastname { get; private set; }
    
    [Required]
    [StringLength(20)] 
    public string Email { get; private set; }
    
    [Required]
    [StringLength(20)]
    public RUC Ruc { get; private set; }
    
    [Required]
    public string Password { get; private set; }
    
    public ICollection<Farm> Farms { get; set; } = new List<Farm>();
  
    public User()
    {
        this.Username = string.Empty;
        this.Firstname = string.Empty;
        this.Lastname = string.Empty;
        this.Email = string.Empty;
        this.Ruc = new RUC(string.Empty);
        this.Password = string.Empty;
    }
}