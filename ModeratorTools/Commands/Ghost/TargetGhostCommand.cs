using Axwabo.CommandSystem.Extensions;
using Axwabo.CommandSystem.Registration;

namespace ModeratorTools.Commands.Ghost;

[CommandProperties(CommandHandlerType.RemoteAdmin, "targetGhost", 2, "Makes the specified players invisible to select players")]
[ModeratorPermissions("targetGhost", PlayerPermissions.Effects)]
[Usage("<enable/disable> <targets>")]
public sealed class TargetGhostCommand : FilteredTargetingCommand, IRegistrationFilter, ICustomResultCompiler
{

    public bool AllowRegistration => GhostExtensions.Enabled;

    private bool _state;

    private readonly List<(string Id, string Nick)> _toTargets = [];

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!arguments.ParseVisibility(out _state))
            return CommandResult.Failed(CombinedUsage);
        var selectedIds = targets.Select(e => e.authManager.UserId).ToHashSet();
        _toTargets.Clear();
        foreach (var hub in arguments.GetTargets(out _, 1))
        {
            var id = hub.authManager.UserId;
            if (!selectedIds.Contains(id))
                _toTargets.Add((id, hub.nicknameSync.MyNick));
        }

        return _toTargets.Count == 0 ? "!No targets were found to make the players invisible to." : CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        var controller = target.GetGhostController();
        var ids = _toTargets.Select(e => e.Id);
        if (_state)
            controller.InvisibleTo.UnionWith(ids);
        else
            controller.InvisibleTo.ExceptWith(ids);
        return !controller.IsFullyInvisible;
    }

    public CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures) => (success.Count, failures.Count) switch
    {
        (0, 0) => CommandResult.Null,
        (_, 0) => (true, Affected(success, success.Count)),
        (_, 1) => (true, $"""
                          {Affected(success.Concat(failures), success.Count + 1)}
                          {failures[0].Nick} is also fully invisible.
                          """),
        (_, _) => (true, $"""
                          {Affected(success.Concat(failures), success.Count + failures.Count)}
                          The following players are also fully invisible: {failures.CombineNicknames()}
                          """)
    };

    private string Affected(IEnumerable<CommandResultOnTarget> success, int count)
        => $"{(count == 1 ? $"{success.First().Nick} is" : $"{count} players are")} now {StateString} to the following: {TargetNicknames}";

    private string StateString => _state ? "invisible" : "visible";

    private string TargetNicknames => string.Join(", ", _toTargets.Select(e => e.Nick));

}
