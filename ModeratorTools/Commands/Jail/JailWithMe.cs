using System.Collections.Generic;
using Axwabo.CommandSystem.Commands.Interfaces;
using ModeratorTools.Jail;

namespace ModeratorTools.Commands.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "jailWithMe", "Jails you and the specified players", "jwm")]
[PlayerOnlyCommand]
public sealed class JailWithMe : JailCommand, ITargetingPreExecutionFilter, ICustomResultCompiler
{

    private bool _senderJailed;

    public CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        _senderJailed = sender.Hub().TryJail();
        return CommandResult.Null;
    }

    public CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures)
        => JailWithMeHelpers.CompileResult(success, _senderJailed, "jailed");

}
