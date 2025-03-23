using Axwabo.CommandSystem.Extensions;
using ModeratorTools.Jail;
using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "jail", "Jails the specified players")]
[ModeratorPermissions("jail", PlayerPermissions.PlayersManagement)]
[Usage("[towerIndex]")]
[ShouldAffectSpectators]
[JailRegistrationFilter]
public class JailCommand : FilteredTargetingCommand, ICustomResultCompiler
{

    protected Vector3? TargetPosition;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        TargetPosition = null;
        if (arguments.Count == 0)
            return CommandResult.Null;
        if (!arguments.ParseInt(out var index))
            return "!Invalid index.";
        if (index == 0)
            return CommandResult.Null;
        var result = JailConfigUtils.TryGetCustomJailPosition(index - 1, out var position);
        if (!result)
            return result;
        TargetPosition = position;
        return CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        var shouldJail = !target.IsJailed();
        if (shouldJail)
            target.TryJail(sender);
        else
            target.TryUnjail();
        if (shouldJail && TargetPosition.HasValue)
            target.TryOverridePosition(TargetPosition.Value);
        return shouldJail ? "Jailed" : "Unjailed";
    }

    public virtual CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures)
        => success.Count == 0
            ? CommandResult.Null
            : string.Join("\n",
                success.GroupBy(e => e.Response)
                    .OrderBy(e => e.Key)
                    .Select(e => $"{e.Key}: {e.CombineNicknames()}")
            );

}
