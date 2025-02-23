using Axwabo.Helpers;
using ModeratorTools.Jail;
using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "jail", "Jails the specified players")]
[ModeratorPermissions("jail.jail", PlayerPermissions.PlayersManagement)]
[Usage("[index]")]
public class JailCommand : SeparatedTargetingCommand, ITargetingPreExecutionFilter
{

    protected Vector3? TargetPosition;

    public virtual CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        TargetPosition = null;
        if (arguments.Count == 0)
            return CommandResult.Null;
        if (!arguments.ParseInt(out var index))
            return "!Invalid index.";
        if (index == 0)
            return CommandResult.Null;
        var result = JailPositionValidator.TryGetCustomJailPosition(index - 1, out var position);
        if (!result)
            return result;
        TargetPosition = position;
        return CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!target.TryJail(sender))
            return false;
        if (TargetPosition.HasValue)
            target.TryOverridePosition(TargetPosition.Value);
        return true;
    }

}
