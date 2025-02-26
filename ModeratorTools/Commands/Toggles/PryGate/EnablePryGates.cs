using Axwabo.CommandSystem.Attributes.Containers;

namespace ModeratorTools.Commands.Toggles.PryGate;

[SubcommandOfContainer(typeof(PryGatesCommand))]
public sealed class EnablePryGates : EnableCommandBase
{

    protected override ToggleCommandInfo Info => PryGatesCommand.Info;

}
