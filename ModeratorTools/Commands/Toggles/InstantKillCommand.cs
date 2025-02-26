namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "instaKill", "Manage instant killing")]
[ModeratorPermissions("instaKill", PlayerPermissions.ForceclassWithoutRestrictions)]
[ToggleFeature("instant killing")]
public sealed class InstantKillCommand : ToggleContainerBase
{

    protected override bool this[PlayerData data]
    {
        get => data.InstantKill;
        set => data.InstantKill = value;
    }

}
