namespace ModeratorTools.Commands.Muting;

[CommandProperties("all", "Temporarily mutes every non-staff", "*")]
public sealed class MuteAll : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        foreach (var player in Player.List)
            if (player.IsMuteApplicable())
                player.Mute();
        return "All non-staff have been muted.";
    }

}
