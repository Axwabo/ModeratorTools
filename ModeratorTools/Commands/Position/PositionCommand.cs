using Axwabo.CommandSystem.Attributes.Containers;

namespace ModeratorTools.Commands.Position;

[CommandProperties(CommandHandlerType.RemoteAdmin, "position", "Player position querying and modification", "pos")]
[UsesSubcommands(typeof(Get), typeof(Set), typeof(Add))]
public sealed class PositionCommand : ContainerCommand;
