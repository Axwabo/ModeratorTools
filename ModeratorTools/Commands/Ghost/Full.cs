namespace ModeratorTools.Commands.Ghost;

[CommandProperties("full", 1, "Sets overall visibility for the specified players")]
[Usage("<enable/disable>")]
public sealed class Full : FilteredTargetingCommand
{

    private bool _state;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        var value = arguments.At(0).ToLower() switch
        {
            "show" or "true" or "1" or "enable" => true,
            "hide" or "false" or "0" or "disable" => false,
            _ => (bool?) null
        };
        if (!value.HasValue)
            return CommandResult.Failed(CombinedUsage);
        _state = value.Value;
        return CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        var controller = target.GetGhostController();
        if (controller.IsFullyInvisible == _state)
            return false;
        controller.IsFullyInvisible = _state;
        return true;
    }

}
