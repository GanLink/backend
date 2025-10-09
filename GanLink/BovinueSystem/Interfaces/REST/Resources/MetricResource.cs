namespace GanLink.BovinueSystem.Interfaces.REST.Resources
{
    public record MetricResource(
        long Id,
        long BovinueMPId,
        long BovinueId,
        DateTime Date,
        double Quantity,
        string ParameterName,
        string CategoryName,
        DateTimeOffset? CreatedDate,
        DateTimeOffset? UpdatedDate
    );
}