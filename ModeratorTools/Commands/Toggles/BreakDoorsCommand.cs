namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "breakDoors")]
[ModeratorPermissions("breakDoors", PlayerPermissions.FacilityManagement)]
[TogglesFeature("door breaking")]
public sealed class BreakDoorsCommand : ToggleContainerBase
{

    protected override bool this[PlayerData data]
    {
        get => data.BreakDoors;
        set => data.BreakDoors = value;
    }

}
