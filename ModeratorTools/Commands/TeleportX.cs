using Axwabo.CommandSystem.Extensions;
using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "teleportX", 1, "Teleports the specified player(s) to another", "tpx")]
[ModeratorPermissions("teleportX", PlayerPermissions.PlayersManagement)]
[Usage("<target>")]
[ShouldAffectSpectators(false)]
public sealed class TeleportX : FilteredTargetingCommand
{

    private Vector3 _position;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        foreach (var hub in arguments.GetTargets(out _))
            if (hub.TryGetPosition(out _position))
                return CommandResult.Null;
        return "!No target found to teleport to.";
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
        => target.TryOverridePosition(_position);

}
