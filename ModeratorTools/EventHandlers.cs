using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.CustomHandlers;
using ModeratorTools.Commands.Muting;
using ModeratorTools.Commands.Toggles;
using ModeratorTools.Jail;
using PlayerRoles;
using PlayerStatsSystem;

namespace ModeratorTools;

internal sealed class EventHandlers : CustomEventsHandler
{

    public override void OnPlayerJoined(PlayerJoinedEventArgs ev) => MuteHandler.OnJoined(ev.Player);

    public override void OnServerRoundStarted()
    {
        MuteHandler.UnmuteLobby();
        JailHandler.OnRoundStarted();
    }

    public override void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs ev)
    {
        if ((!ev.Door.IsLocked || ev.Player.IsBypassEnabled)
            && ev.Door is Gate gate
            && ev.Player.GetData().PryGates
            && gate.Base.TryPryGate(ev.Player.ReferenceHub))
            ev.IsAllowed = false;
    }

    public override void OnPlayerHurting(PlayerHurtingEventArgs ev)
    {
        if (ev.DamageHandler is AttackerDamageHandler {Attacker.Hub: var attacker} adh
            && attacker != null
            && attacker.GetData().InstantKill)
            adh.Damage = -1;
    }

    public override void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev)
    {
        var p = ev.Target;
        if (ModeratorToolsPlugin.Cfg.TutorialsImmuneToScp096 && p.Role == RoleTypeId.Tutorial || p.GetData().Scp096Immunity)
            ev.IsAllowed = false;
    }

    public override void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev)
    {
        var p = ev.Target;
        if (ModeratorToolsPlugin.Cfg.TutorialsImmuneToScp173 && p.Role == RoleTypeId.Tutorial || p.GetData().Scp173Immunity)
            ev.IsAllowed = false;
    }

}
