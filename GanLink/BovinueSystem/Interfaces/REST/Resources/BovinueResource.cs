namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record BovinueResource(
        long Id,
        int FarmId,
        DateTimeOffset? CreatedDate,
        DateTimeOffset? UpdatedDate
    );
}