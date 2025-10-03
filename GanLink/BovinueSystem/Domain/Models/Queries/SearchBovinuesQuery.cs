namespace GanLink.BovinueSystem.Domain.Models.Queries;

public record SearchBovinuesQuery(string keyword, int page, int pageSize);
