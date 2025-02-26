namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "scp096Immunity", "Manage SCP-096 immunity", "096immunity")]
[ModeratorPermissions("scp096Immunity", PlayerPermissions.PlayersManagement)]
[ToggleFeature("SCP-096 immunity")]
public sealed class Scp096ImmunityCommand : ToggleContainerBase
{

    protected override bool this[PlayerData data]
    {
        get => data.Scp096Immunity;
        set => data.Scp096Immunity = value;
    }

}
