namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record MetricParameterResource(
        long Id,
        long CategoryId,
        string Parameter,
        string Description,
        string CategoryName
    );
}