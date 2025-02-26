namespace ModeratorTools.Commands.Toggles;

public abstract class ListCommandBase : CommandBase
{

    public override string Name => "list";

    public override string Description => $"Lists all players with {Info.Name} enabled";

    protected abstract ToggleCommandInfo Info { get; }

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        var nicknames = new List<string>();
        foreach (var (player, data) in PlayerDataManager.Defined)
            if (Info.Get(data))
                nicknames.Add(player.Nickname);
        return nicknames.Count == 0
            ? $"!Nobody has {Info.Name} enabled."
            : $"The following players have {Info.Name} enabled:\n{string.Join(", ", nicknames)}";
    }

}
