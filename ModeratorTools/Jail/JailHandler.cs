using System.Collections.Generic;
using Axwabo.Helpers.PlayerInfo;
using LabApi.Features.Wrappers;
using PlayerRoles;

namespace ModeratorTools.Jail;

public static class JailHandler
{

    private static readonly Dictionary<string, JailEntry> Entries = [];

    public static void OnRoundRestarted()
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
        Entries[id] = new JailEntry(hub.GetInfoWithRole());
        hub.inventory.ClearEverything();
        hub.roleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.RemoteAdmin);
        return true;
    }

    public static bool TryUnjail(this ReferenceHub hub)
    {
        var id = hub.authManager.UserId;
        if (!Entries.TryGetValue(id, out var entry))
            return false;
        // TODO: pocket dimension, decontaminated light & warhead teleportation
        Entries.Remove(id);
        if (entry.ThisRound)
            entry.Info.SetClassAndApplyInfo(Player.Get(hub));
        else
            hub.roleManager.ServerSetRole(RoleTypeId.Spectator, RoleChangeReason.RemoteAdmin);
        return true;
    }

}
