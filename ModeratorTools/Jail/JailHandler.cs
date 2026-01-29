using Axwabo.Helpers.PlayerInfo;
using ModeratorTools.Commands.Toggles;
using PlayerRoles;

namespace ModeratorTools.Jail;

public static class JailHandler
{

    private static readonly Dictionary<string, IPlayerInfoWithRole> Entries = [];

    internal static void RemoveEntry(string userId)
    {
        if (userId != null)
            Entries.Remove(userId);
    }

    internal static void ClearEntries() => Entries.Clear();

    public static bool IsJailed(this ReferenceHub hub) => Entries.ContainsKey(hub.authManager.UserId);

    public static bool TryJail(this ReferenceHub hub, CommandSender sender = null)
    {
        var id = hub.authManager.UserId;
        if (Entries.ContainsKey(id))
            return false;
        if (sender != null && hub.queryProcessor._sender != sender)
            PreviouslyJailedGUI.AddPlayer(sender, hub);
        var info = hub.GetInfoWithRole();
        JailConfigUtils.ValidateEntry(hub, info.Info);
        Entries[id] = info;
        hub.inventory.ClearEverything();
        hub.Data.GodModeBeforeJail = hub.characterClassManager.GodMode;
        hub.roleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.RemoteAdmin);
        JailConfigUtils.OnJailed(hub);
        return true;
    }

    public static bool TryUnjail(this ReferenceHub hub)
    {
        var id = hub.authManager.UserId;
        if (!Entries.TryGetValue(id, out var entry))
            return false;
        Entries.Remove(id);
        JailConfigUtils.ValidateExit(entry.Info);
        entry.SetClassAndApplyInfo(Player.Get(hub));
        hub.characterClassManager.GodMode = hub.Data.GodModeBeforeJail;
        JailConfigUtils.OnUnjailed(hub);
        return true;
    }

}
