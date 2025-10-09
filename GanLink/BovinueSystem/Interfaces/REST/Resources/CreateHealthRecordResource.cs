namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record CreateHealthRecordResource(
        long BovinueCHRId,
        long BovinueId,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate
    );
}