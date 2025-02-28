namespace ModeratorTools.Commands.Muting;

[CommandProperties("targets", "Temporarily mutes the specified players")]
public sealed class MuteTargets : SeparatedTargetingCommand
{

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!Player.Dictionary.TryGetValue(target, out var player) || !player.IsMuteApplicable())
            return false;
        player.Mute();
        return true;
    }

}
