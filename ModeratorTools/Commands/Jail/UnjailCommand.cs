using ModeratorTools.Jail;

namespace ModeratorTools.Commands.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "unjail", "Unjails the specified players")]
[ModeratorPermissions("jail.jail", PlayerPermissions.PlayersManagement)]
[ShouldAffectSpectators]
[JailRegistrationFilter]
public class UnjailCommand : SeparatedTargetingCommand
{

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender) => target.TryUnjail();

}
