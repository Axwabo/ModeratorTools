using Axwabo.Helpers.Config;
using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands.RoomTeleports;

[CommandProperties(CommandHandlerType.RemoteAdmin, "goToRoom", 1, "Teleports you to the specified room", "gtr")]
[VanillaPermissions(PlayerPermissions.PlayersManagement)]
[Usage("<roomType>")]
[PlayerOnlyCommand]
public sealed class GoToRoom : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
        => !arguments.ParseEnumIgnoreCase(out RoomType roomType)
            ? "!Invalid room type."
            : !roomType.TryGetTeleportPosition(out var position)
                ? "!Couldn't find the room."
                : sender.Hub().TryOverridePosition(position)
                    ? "You've been teleported."
                    : "Your current role doesn't support this operation.";

}
