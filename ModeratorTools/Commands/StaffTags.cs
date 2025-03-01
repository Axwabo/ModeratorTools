namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RaAndServer, "staffTags", 1, "Shows or hides tags of all staff")]
[ModeratorPermissions("staffTags", PlayerPermissions.SetGroup)]
[Usage("show/hide")]
public sealed class StaffTags : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        var value = arguments.At(0).ToLower() switch
        {
            "show" or "true" or "1" or "enable" => true,
            "hide" or "false" or "0" or "disable" => false,
            _ => (bool?) null
        };
        if (!value.HasValue)
            return CommandResult.Failed(CombinedUsage);
        foreach (var player in Player.List)
        {
            var group = player.UserGroup;
            if (!player.RemoteAdminAccess || group == null)
                continue;
            if (!value.Value)
            {
                player.ReferenceHub.serverRoles.TryHideTag();
                continue;
            }

            player.GroupName = group.BadgeColor;
            player.GroupColor = group.BadgeColor;
        }

        return $"Staff tags have been {(value.Value ? "shown" : "hidden")}";
    }

}
