namespace ModeratorTools.Commands.Muting;

public abstract class MutePlayersCommandBase : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        foreach (var player in Player.List)
            if (player.IsMuteApplicable())
                Execute(player);
        return Response;
    }

    protected abstract string Response { get; }

    protected abstract void Execute(Player player);

}
