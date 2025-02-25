using ModeratorTools.Jail;
using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "jailWithMe", "Jails you and the specified players", "jwm")]
[PlayerOnlyCommand]
[Usage("[index]")]
public sealed class JailWithMe : JailCommand, ICustomResultCompiler
{

    private bool _senderJailed;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        var result = base.OnBeforeExecuted(targets, arguments, sender);
        if (result.HasValue)
            return result;
        _senderJailed = sender.Hub().TryJail();
        if (_senderJailed && TargetPosition.HasValue)
            sender.Hub().TryOverridePosition(TargetPosition.Value);
        return CommandResult.Null;
    }

    public CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures)
        => JailWithMeHelpers.CompileResult(success, _senderJailed, "jailed");

}
