namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record UpdateBovinueCattleHealthRecordCommand(
    long id,
    string activityName,
    int frequency,
    string description);