using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using ModeratorTools.Commands.Muting;
using ModeratorTools.Commands.Toggles;
using ModeratorTools.Jail;

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
        if (ev.Door is Gate gate && ev.Player.GetData().PryGates && gate.Base.TryPryGate(ev.Player.ReferenceHub))
            ev.IsAllowed = false;
    }

}
