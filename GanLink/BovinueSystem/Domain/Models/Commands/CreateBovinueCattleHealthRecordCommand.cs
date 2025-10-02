namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record CreateBovinueCattleHealthRecordCommand(
    string ActivityName,
    int Frequency,
    string Description
);
