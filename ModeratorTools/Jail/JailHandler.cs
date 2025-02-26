using Axwabo.Helpers.PlayerInfo;
using PlayerRoles;

namespace ModeratorTools.Jail;

public static class JailHandler
{

    private static readonly Dictionary<string, JailEntry> Entries = [];

    internal static void OnRoundStarted()
    {
        foreach (var kvp in Entries.ToList())
        {
            var entry = kvp.Value;
            if (entry.ThisRound)
                entry.ThisRound = false;
            else
                Entries.Remove(kvp.Key);
        }
    }

    public static bool TryJail(this ReferenceHub hub, CommandSender sender = null)
    {
        var id = hub.authManager.UserId;
        if (Entries.ContainsKey(id))
            return false;
        if (sender != null && hub.queryProcessor._sender != sender)
            PreviouslyJailedGUI.AddPlayer(sender, hub);
        var info = hub.GetInfoWithRole();
        JailPositionValidator.ValidateEntry(hub, info.Info);
        Entries[id] = new JailEntry(info);
        hub.inventory.ClearEverything();
        hub.roleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.RemoteAdmin);
        return true;
    }

    public static bool TryUnjail(this ReferenceHub hub)
    {
        var id = hub.authManager.UserId;
        if (!Entries.TryGetValue(id, out var entry))
            return false;
        Entries.Remove(id);
        if (!entry.ThisRound)
        {
            hub.roleManager.ServerSetRole(RoleTypeId.Spectator, RoleChangeReason.RemoteAdmin);
            return true;
        }

        JailPositionValidator.ValidateExit(entry.Info.Info);
        entry.Info.SetClassAndApplyInfo(Player.Get(hub));
        return true;
    }

}
