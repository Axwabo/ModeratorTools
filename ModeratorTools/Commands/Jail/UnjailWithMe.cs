using System.Collections.Generic;
using Axwabo.CommandSystem.Commands.Interfaces;
using ModeratorTools.Jail;

namespace ModeratorTools.Commands.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "unjailWithMe", "Unjails you and the specified players", "ujwm")]
[PlayerOnlyCommand]
public sealed class UnjailWithMe : UnjailCommand, ITargetingPreExecutionFilter, ICustomResultCompiler
{

    private bool _senderUnjailed;

    public CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        _senderUnjailed = sender.Hub().TryUnjail();
        return CommandResult.Null;
    }

    public CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures)
        => JailWithMeHelpers.CompileResult(success, _senderUnjailed, "unjailed");

}
