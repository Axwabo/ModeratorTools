namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "pryGates")]
[ModeratorPermissions("pryGates", PlayerPermissions.FacilityManagement)]
[TogglesFeature("gate prying")]
public sealed class PryGatesCommand : ToggleContainerBase
{

    protected override bool this[PlayerData data]
    {
        get => data.PryGates;
        set => data.PryGates = value;
    }

}
