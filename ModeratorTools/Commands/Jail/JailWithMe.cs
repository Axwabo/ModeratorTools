using Axwabo.CommandSystem.Extensions;
using ModeratorTools.Jail;
using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "jailWithMe", "Jails/unjails you and the specified players based on your jail state", "jwm")]
[PlayerOnlyCommand]
[Usage("[towerIndex]")]
public sealed class JailWithMe : JailCommand
{

    private bool _shouldJail;

    private string Op => _shouldJail ? "jailed" : "unjailed";

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        var result = base.OnBeforeExecuted(targets, arguments, sender);
        if (result.HasValue)
            return result;
        _shouldJail = !sender.Hub().IsJailed();
        if (!_shouldJail)
        {
            sender.Hub().TryUnjail();
            return CommandResult.Null;
        }

        sender.Hub().TryJail();
        if (TargetPosition.HasValue)
            sender.Hub().TryOverridePosition(TargetPosition.Value);
        return CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!_shouldJail)
            return target.TryUnjail();
        if (!target.TryJail(sender))
            return false;
        if (TargetPosition.HasValue)
            target.TryOverridePosition(TargetPosition.Value);
        return true;
    }

    public override CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures)
        => $"You have been {Op.ToLower()}.\n{(success.Count, failures.Count) switch
        {
            (1, 0) => $"{success[0].Nick} has been {Op}.",
            (0, 1) => $"{failures[0].Nick} is already {Op}.",
            (1, 1) => $"{success[0].Nick} has been {Op}.\n{failures[0].Nick} is already {Op}.",
            (_, 0) => $"Successfully {Op}: {success.CombineNicknames()}",
            (0, _) => $"Already {Op} players: {failures.CombineNicknames()}",
            (_, _) => $"Successfully {Op}: {success.CombineNicknames()}\nAlready {Op} players: {failures.CombineNicknames()}"
        }}";

    protected override string NoTargetsFoundMessage => $"!You have been {Op.ToLower()}.\n{base.NoTargetsFoundMessage}";

}
