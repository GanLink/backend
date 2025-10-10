namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record UpdateMetricResource(
        long BovinueMPId,
        long BovinueId,
        DateTime Date,
        double Quantity
    );
}