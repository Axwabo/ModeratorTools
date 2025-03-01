using Axwabo.Helpers.PlayerInfo;
using ModeratorTools.Commands.Toggles;
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
        JailConfigUtils.ValidateEntry(hub, info.Info);
        Entries[id] = new JailEntry(info);
        var ccm = hub.characterClassManager;
        hub.inventory.ClearEverything();
        hub.GetData().GodModeBeforeJail = ccm.GodMode;
        hub.roleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.RemoteAdmin);
        ccm.GodMode = JailConfigUtils.GodMode;
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
            hub.ApplyPreviousGodMode();
            return true;
        }

        JailConfigUtils.ValidateExit(entry.Info.Info);
        entry.Info.SetClassAndApplyInfo(Player.Get(hub));
        hub.ApplyPreviousGodMode();
        return true;
    }

    private static void ApplyPreviousGodMode(this ReferenceHub hub) => hub.characterClassManager.GodMode = hub.GetData().GodModeBeforeJail;

}
