using Axwabo.CommandSystem.Attributes.Containers;

namespace ModeratorTools.Commands.Toggles.PryGate;

[SubcommandOfContainer(typeof(PryGatesCommand))]
public sealed class TogglePryGates : ToggleCommandBase
{

    protected override ToggleCommandInfo Info => PryGatesCommand.Info;

}
