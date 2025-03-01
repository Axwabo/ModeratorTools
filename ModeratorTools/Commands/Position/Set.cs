using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands.Position;

[CommandProperties("set", 3, "Sets the position of the specified players")]
[ModeratorPermissions("position.set", PlayerPermissions.PlayersManagement)]
[Usage("<x> <y> <z>")]
[ShouldAffectSpectators(false)]
public sealed class Set : FilteredTargetingCommand
{

    private Vector3 _position;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
        => arguments.ParseVector(out _position);

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
        => target.TryOverridePosition(_position);

}
