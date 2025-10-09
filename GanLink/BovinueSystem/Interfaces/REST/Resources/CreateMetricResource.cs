namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record CreateMetricResource(
        long BovinueMPId,
        long BovinueId,
        DateTime Date,
        double Quantity
    );
}