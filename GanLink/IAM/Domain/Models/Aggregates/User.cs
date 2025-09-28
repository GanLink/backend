namespace GanLink.IAM.Domain.Models.Aggregates;

public partial class User
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Ruc { get; private set; }
    public string PasswordHashed { get; private set; }
}