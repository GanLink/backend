namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record CreateMetricResource(
        long BovinueId,
        long BovinueMPId,
        DateTime Date,
        double Quantity
    );
}