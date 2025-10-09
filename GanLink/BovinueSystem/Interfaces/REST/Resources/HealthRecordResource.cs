namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record HealthRecordResource(
        long Id,
        long BovinueCHRId,
        long BovinueId,
        DateTimeOffset? StartDate,
        DateTimeOffset? EndDate,
        string ActivityName,
        string Description,
        DateTimeOffset? CreatedDate,
        DateTimeOffset? UpdatedDate
    );
}