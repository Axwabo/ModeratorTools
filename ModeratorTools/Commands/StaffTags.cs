namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RaAndServer, "staffTags", 1, "Shows or hides tags of all staff")]
[ModeratorPermissions("staffTags", PlayerPermissions.SetGroup)]
[Usage("show/hide")]
public sealed class StaffTags : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        if (!arguments.ParseVisibility(out var state))
            return CommandResult.Failed(CombinedUsage);
        foreach (var player in Player.List)
        {
            var group = player.UserGroup;
            if (!player.RemoteAdminAccess || player.ReferenceHub.authManager.RemoteAdminGlobalAccess || group == null)
                continue;
            if (!state)
                player.ReferenceHub.serverRoles.TryHideTag();
            else
                player.ReferenceHub.serverRoles.RefreshHiddenTag();
        }

        return $"Staff tags have been {(state ? "shown" : "hidden")}";
    }

}
