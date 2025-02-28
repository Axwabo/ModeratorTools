using Axwabo.CommandSystem.Attributes.Containers;

namespace ModeratorTools.Commands.Muting;

[CommandProperties(CommandHandlerType.RemoteAdmin, "pMute", "Temporary muting")]
[ModeratorPermissions("mute", PlayerPermissions.PlayersManagement)]
[UsesSubcommands(typeof(MuteRoundStart), typeof(MuteAll), typeof(MuteTargets), typeof(MuteIntercom))]
public sealed class MuteCommand : ContainerCommand;
