namespace ModeratorTools.Commands.Muting;

[CommandProperties("roundStart", "Temporarily mutes the players in the lobby", "rs")]
public sealed class MuteRoundStart : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        if (Round.IsRoundStarted)
            return "!You can only use this command before the round starts.";
        MuteHandler.LobbyMutes.Add("");
        foreach (var player in Player.ReadyList)
            if (player.IsMuteApplicable() && MuteHandler.LobbyMutes.Add(player.UserId))
                player.Mute();
        return "All non-staff have been temporarily muted until the round starts.";
    }

}
