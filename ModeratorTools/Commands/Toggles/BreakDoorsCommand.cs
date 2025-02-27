namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "breakDoors", "Manage door breaking")]
[ModeratorPermissions("breakDoors", PlayerPermissions.FacilityManagement)]
[ToggleFeature("door breaking")]
public sealed class BreakDoorsCommand : ToggleContainerBase
{

    protected override bool this[PlayerData data]
    {
        get => data.BreakDoors;
        set => data.BreakDoors = value;
    }

}
