using LabApi.Features.Wrappers;

namespace ModeratorTools.Commands.Muting;

public static class MuteHandler
{

    public static readonly HashSet<string> LobbyMutes = [];

    public static bool IsMuteApplicable(this Player player) => !player.IsNorthwoodStaff && !player.RemoteAdminAccess;

    internal static void OnJoined(Player player)
    {
        if (LobbyMutes.Count == 0 || !player.IsMuteApplicable())
            return;
        player.Mute();
        LobbyMutes.Add(player.UserId);
    }

    public static void UnmuteLobby()
    {
        foreach (var player in Player.List)
            if (LobbyMutes.Contains(player.UserId))
                player.Unmute(false);
        LobbyMutes.Clear();
    }

}
