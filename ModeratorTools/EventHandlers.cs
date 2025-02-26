using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using ModeratorTools.Commands.Muting;
using ModeratorTools.Commands.Toggles;
using ModeratorTools.Jail;
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

}
