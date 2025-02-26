using Axwabo.CommandSystem.Extensions;

namespace ModeratorTools.Commands.Toggles;

public abstract class ToggleCommandBase : SeparatedTargetingCommand, ICustomResultCompiler
{

    public override string Name => "toggle";

    public override string Description => $"Toggles {Info.Name} for the specified players";

    protected abstract ToggleCommandInfo Info { get; }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
        => Info.Toggle(target);

    public CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures) => (success.Count, failures.Count) switch
    {
        (0, 0) => "!No players were affected.",
        (1, 0) => Enabled(success[0]),
        (0, 1) => Disabled(failures[0]),
        (1, 1) => $"{Enabled(success[0])}\n{Disabled(success[0])}",
        (_, 0) => Enabled(success),
        (0, _) => Disabled(failures),
        _ => $"{Enabled(success)}\n{Disabled(failures)}"
    };

    private string Enabled(CommandResultOnTarget result) => $"Enabled {Info.Name} for {result.Nick}.";

    private string Disabled(CommandResultOnTarget result) => $"Disabled {Info.Name} for {result.Nick}.";

    private string Enabled(List<CommandResultOnTarget> success) => $"Enabled {Info.Name} for the following players: {success.CombineNicknames()}";

    private string Disabled(List<CommandResultOnTarget> failures) => $"Disabled {Info.Name} for the following players: {failures.CombineNicknames()}";

}
