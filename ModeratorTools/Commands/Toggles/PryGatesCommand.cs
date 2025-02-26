namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "pryGates", "Manage gate prying")]
[ModeratorPermissions("pryGates", PlayerPermissions.FacilityManagement)]
[ToggleFeature("gate prying")]
public sealed class PryGatesCommand : ToggleContainerBase
{

    protected override bool this[PlayerData data]
    {
        get => data.PryGates;
        set => data.PryGates = value;
    }

}
