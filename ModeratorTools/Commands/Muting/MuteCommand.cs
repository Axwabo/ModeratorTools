using Axwabo.CommandSystem.Attributes.Containers;

namespace ModeratorTools.Commands.Muting;

[CommandProperties(CommandHandlerType.RemoteAdmin, "pMute", "Temporarily mutes everyone or the lobby")] // TODO: target-based
[ModeratorPermissions("mute", PlayerPermissions.PlayersManagement)]
[UsesSubcommands(typeof(MuteRoundStart))]
public sealed class MuteCommand : ContainerCommand;
