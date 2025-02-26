namespace ModeratorTools.Commands.Muting;

[CommandProperties("roundStart", "Unmutes players in the lobby")]
public sealed class UnmuteRoundStart : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        if (MuteHandler.LobbyMutes.Count == 0)
            return "!No players to unmute.";
        MuteHandler.UnmuteLobby();
        return "Players in the lobby have been unmuted.";
    }

}
