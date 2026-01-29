using System.Runtime.CompilerServices;

namespace ModeratorTools.Commands.Ghost;

public static class GhostExtensions
{

    private static readonly ConditionalWeakTable<Player, GhostController> Controllers = new();

    public static bool Enabled => ModeratorToolsPlugin.Cfg?.Ghost ?? true;

    extension(ReferenceHub hub)
    {

        public GhostController GhostController => Player.Get(hub).GhostController;

    }

    extension(Player player)
    {

        public GhostController GhostController => Controllers.GetOrCreateValue(player);

        public bool IsGhostInvisibleTo(Player observer)
        {
            if (ReferenceEquals(player, null) || ReferenceEquals(observer, null) || string.IsNullOrEmpty(observer.UserId))
                return false;
            var controller = player.GhostController;
            return controller.IsFullyInvisible || controller.InvisibleTo.Contains(observer.UserId);
        }

    }

}
