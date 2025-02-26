namespace ModeratorTools.Commands.Toggles;

public abstract class ClearCommandBase : CommandBase
{

    public override string Name => "clear";

    public override string Description => $"Disables {Info.Name} for all players";
    protected abstract ToggleCommandInfo Info { get; }

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        foreach (var (_, data) in PlayerDataManager.Defined)
            Info.Set(data, false);
        return $"Disabled {Info.Name} for everyone.";
    }

}
