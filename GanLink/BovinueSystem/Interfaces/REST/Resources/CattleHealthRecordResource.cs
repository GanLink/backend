namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record CattleHealthRecordResource(
        long Id,
        string ActivityName,
        int Frequency,
        string Description
    );
}