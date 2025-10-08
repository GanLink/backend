namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record CreateBovinueCattleHealthRecordCommand(
    string activityName,
    int frequency,
    string description);