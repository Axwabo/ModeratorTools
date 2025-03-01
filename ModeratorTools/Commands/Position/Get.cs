using Axwabo.CommandSystem.Extensions;

namespace ModeratorTools.Commands.Position;

[CommandProperties("get", "Gets the position of the specified players")]
[ModeratorPermissions("position.get", PlayerPermissions.GameplayData)]
[ShouldAffectSpectators(false)]
[NoPlayersAffectedMessage(QueriesFailed)]
public sealed class Get : SeparatedTargetingCommand, ICustomResultCompiler
{

    private const string QueriesFailed = "No positions could be queried.";

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
        => target.TryGetPosition(out var position) ? position.ToString() : false;

    public CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures) => (success.Count, failures.Count) switch
    {
        (_, 0) => Positions(success),
        (0, _) => CommandResult.Failed(QueriesFailed),
        _ => $"{Positions(success)}\nFailed to get the positions of the following players: {failures.CombineNicknames()}"
    };

    private static string Positions(List<CommandResultOnTarget> success)
        => string.Join("\n", success.Select(e => $"\"{e.Nick}\" is at {e.Response}"));

}
