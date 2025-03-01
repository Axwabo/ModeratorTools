namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "scp173Immunity")]
[Aliases("173immunity")]
[ModeratorPermissions("scp173Immunity", PlayerPermissions.PlayersManagement)]
[TogglesFeature("SCP-173 immunity")]
public sealed class Scp173ImmunityCommand : ToggleContainerBase
{

    protected override bool this[PlayerData data]
    {
        get => data.Scp173Immunity;
        set => data.Scp173Immunity = value;
    }

}
