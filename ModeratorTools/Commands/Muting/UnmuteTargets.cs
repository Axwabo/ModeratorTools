namespace ModeratorTools.Commands.Muting;

[CommandProperties("targets", "Unmutes the specified players")]
public sealed class UnmuteTargets : SeparatedTargetingCommand
{

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!Player.Dictionary.TryGetValue(target, out var player) || !player.IsMuteApplicable())
            return false;
        player.Unmute(false);
        return true;
    }

}
