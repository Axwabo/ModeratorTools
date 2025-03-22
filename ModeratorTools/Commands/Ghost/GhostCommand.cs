using Axwabo.CommandSystem.Attributes.Containers;
using Axwabo.CommandSystem.Registration;

namespace ModeratorTools.Commands.Ghost;

[CommandProperties(CommandHandlerType.RemoteAdmin, "ghost", "Player visibility management")]
[UsesSubcommands(typeof(Full))]
public sealed class GhostCommand : ContainerCommand, IRegistrationFilter
{

    public bool AllowRegistration => ModeratorToolsPlugin.Cfg?.Ghost ?? true;

}
