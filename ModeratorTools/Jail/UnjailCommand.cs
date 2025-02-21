namespace ModeratorTools.Jail;

[CommandProperties(CommandHandlerType.RemoteAdmin, "unjail", "Unjails the specified players")]
[VanillaPermissions(PlayerPermissions.PlayersManagement)]
public sealed class UnjailCommand : SeparatedTargetingCommand
{

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender) => target.TryUnjail();

}
