namespace GanLink.BovinueSystem.Domain.Models.Commands;

public record UpdateBovinueCattleHealthRecordCommand(
    string ActivityName,
    int Frequency,
    string Description
);
