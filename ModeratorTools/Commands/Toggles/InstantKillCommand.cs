namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "instaKill")]
[ModeratorPermissions("instaKill", PlayerPermissions.ForceclassWithoutRestrictions)]
[TogglesFeature("instant killing")]
public sealed class InstantKillCommand : ToggleContainerBase
{

    protected override bool this[PlayerData data]
    {
        get => data.InstantKill;
        set => data.InstantKill = value;
    }

}
