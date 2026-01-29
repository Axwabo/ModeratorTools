using AFK;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.CustomHandlers;
using ModeratorTools.Commands.Ghost;
using ModeratorTools.Commands.Muting;
using ModeratorTools.Commands.Toggles;
using ModeratorTools.Jail;
using PlayerRoles;
using PlayerStatsSystem;

namespace ModeratorTools;

internal sealed class EventHandlers : CustomEventsHandler
{

    public override void OnPlayerJoined(PlayerJoinedEventArgs ev) => MuteHandler.OnJoined(ev.Player);

    public override void OnPlayerLeft(PlayerLeftEventArgs ev) => JailHandler.RemoveEntry(ev.Player.UserId);

    public override void OnServerWaitingForPlayers() => JailHandler.ClearEntries();

    public override void OnServerRoundStarted() => MuteHandler.UnmuteLobby();

    public override void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs ev)
    {
        var p = ev.Player;
        if (ev.Door.IsLocked && !p.IsBypassEnabled)
            return;
        var data = p.Data;
        switch (ev.Door)
        {
            case Gate gate when data.PryGates && gate.TryPry(p):
            case BreakableDoor breakableDoor when data.BreakDoors && breakableDoor.TryBreak():
                ev.IsAllowed = false;
                break;
        }
    }

    public override void OnPlayerHurting(PlayerHurtingEventArgs ev)
    {
        if (ev.DamageHandler is AttackerDamageHandler {Attacker.Hub: var attacker} adh
            && attacker != null
            && attacker.Data.InstantKill)
            adh.Damage = -1;
    }

    public override void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev)
    {
        var p = ev.Target;
        if (ModeratorToolsPlugin.Cfg.TutorialsImmuneToScp096 && p.Role == RoleTypeId.Tutorial || p.Data.Scp096Immunity)
            ev.IsAllowed = false;
    }

    public override void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev)
    {
        var p = ev.Target;
        if (ModeratorToolsPlugin.Cfg.TutorialsImmuneToScp173 && p.Role == RoleTypeId.Tutorial || p.Data.Scp173Immunity)
            ev.IsAllowed = false;
    }

    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        var wasTutorial = ev.OldRole == RoleTypeId.Tutorial;
        var isTutorial = ev.NewRole.RoleTypeId == RoleTypeId.Tutorial;
        if (wasTutorial == isTutorial)
            return;
        if (ModeratorToolsPlugin.Cfg.GodModeTutorials)
            HandleGodTutorial(wasTutorial, ev.Player);
        if (ModeratorToolsPlugin.Cfg.TutorialsImmuneToAfkKick)
            HandleAfkTutorial(wasTutorial, ev.Player);
    }

    public override void OnPlayerValidatedVisibility(PlayerValidatedVisibilityEventArgs ev)
    {
        if (GhostExtensions.Enabled && ev.Target.IsGhostInvisibleTo(ev.Player))
            ev.IsVisible = false;
    }

    private static void HandleGodTutorial(bool wasTutorial, Player p)
    {
        var data = p.Data;
        if (wasTutorial)
        {
            p.IsGodModeEnabled = data.WasInGodMode;
            return;
        }

        data.WasInGodMode = p.IsGodModeEnabled;
        p.IsGodModeEnabled = true;
    }

    private static void HandleAfkTutorial(bool wasTutorial, Player p)
    {
        var data = p.Data;
        if (wasTutorial)
        {
            p.AfkImmune = data.WasAfkImmune;
            return;
        }

        data.WasAfkImmune = p.AfkImmune;
        p.AfkImmune = true;
    }

}

file static class PlayerExtensions
{

    extension(Player player)
    {

        public bool AfkImmune
        {
            get => !AFKManager.AFKTimers.ContainsKey(player.ReferenceHub);
            set
            {
                if (value)
                    AFKManager.RemovePlayer(player.ReferenceHub);
                else
                    AFKManager.AddPlayer(player.ReferenceHub);
            }
        }

    }

}
