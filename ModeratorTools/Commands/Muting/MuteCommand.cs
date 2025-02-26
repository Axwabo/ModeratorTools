using Axwabo.CommandSystem.Attributes.Containers;

namespace ModeratorTools.Commands.Muting;

[CommandProperties(CommandHandlerType.RemoteAdmin, "pMute", "Temporary muting")] // TODO: target-based
[ModeratorPermissions("mute", PlayerPermissions.PlayersManagement)]
[UsesSubcommands(typeof(MuteRoundStart), typeof(MuteAll))]
public sealed class MuteCommand : ContainerCommand;
