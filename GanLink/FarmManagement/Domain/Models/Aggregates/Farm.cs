namespace GanLink.FarmManagement.Domain.Models.Aggregates;

public partial class Farm
{
    public int Id { get; private set; }
    public string Alias { get; private set; }
    public int UserId { get; private set; }
    public string MainActivity { get; private set; }
    public string OwnerDNI { get; private set; }
}