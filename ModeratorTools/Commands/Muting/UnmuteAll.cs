using LabApi.Features.Wrappers;

namespace ModeratorTools.Commands.Muting;

[CommandProperties("all", "Unmutes every non-staff")]
public sealed class UnmuteAll : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        foreach (var player in Player.List)
            if (player.IsMuteApplicable())
                player.Unmute(false);
        return "All non-staff have been unmuted.";
    }

}
