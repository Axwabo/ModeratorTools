namespace ModeratorTools.Commands.Toggles.PryGate;

[CommandProperties(CommandHandlerType.RemoteAdmin, "pryGates", "Manage gate prying")]
[ModeratorPermissions("pryGates", PlayerPermissions.FacilityManagement)]
public sealed class PryGatesCommand : ContainerCommand
{

    public static ToggleCommandInfo Info { get; } = new("gate prying", data => data.PryGates, (data, value) => data.PryGates = value);

}
