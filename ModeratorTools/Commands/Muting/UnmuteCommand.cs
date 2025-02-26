using Axwabo.CommandSystem.Attributes.Containers;

namespace ModeratorTools.Commands.Muting;

[CommandProperties(CommandHandlerType.RemoteAdmin, "pUnmute", "Temporary mute revocation")]
[ModeratorPermissions("mute", PlayerPermissions.PlayersManagement)]
[UsesSubcommands(typeof(UnmuteRoundStart))]
public sealed class UnmuteCommand : ContainerCommand;
