namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record UpdateHealthRecordResource(
        long BovinueCHRId,
        long BovinueId,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate
    );
}