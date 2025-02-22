using ModeratorTools.Jail;

namespace ModeratorTools.Commands.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "jail", "Jails the specified players")]
[ModeratorPermissions("jail.jail", PlayerPermissions.PlayersManagement)]
public sealed class JailCommand : SeparatedTargetingCommand
{

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender) => target.TryJail(sender);

}
