using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using ModeratorTools.Commands.Muting;
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

}
