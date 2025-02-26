namespace ModeratorTools.Commands.Toggles;

internal abstract class EnableCommandBase : SeparatedTargetingCommand
{

    protected abstract ToggleCommandInfo Info { get; }

    public override string Name => "enable";

    public override string Description => $"Enables {Info.Name} for the specified players";

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
        => Info.SetState(target, true);

}
