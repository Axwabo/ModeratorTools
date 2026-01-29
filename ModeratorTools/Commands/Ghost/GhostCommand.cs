namespace ModeratorTools.Commands.Ghost;

[CommandProperties(CommandHandlerType.RemoteAdmin, "ghost", 1, "Player visibility management")]
[ModeratorPermissions("ghost", PlayerPermissions.Effects)]
[Usage("<enable/disable>")]
public sealed class GhostCommand : FilteredTargetingCommand
{

    public bool AllowRegistration => GhostExtensions.Enabled;

    private bool _state;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
        => arguments.ParseVisibility(out _state)
            ? CommandResult.Null
            : CommandResult.Failed(CombinedUsage);

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        var controller = target.GhostController;
        if (controller.IsFullyInvisible == _state)
            return false;
        controller.IsFullyInvisible = _state;
        return true;
    }

}
