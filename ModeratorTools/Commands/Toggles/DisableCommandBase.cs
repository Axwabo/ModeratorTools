namespace ModeratorTools.Commands.Toggles;

internal abstract class DisableCommandBase : SeparatedTargetingCommand
{

    protected abstract ToggleCommandInfo Info { get; }

    public override string Name => "disable";

    public override string Description => $"Disables {Info.Name} for the specified players";

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
        => Info.SetState(target, false);

}
