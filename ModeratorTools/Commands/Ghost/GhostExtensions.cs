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
            var controller = player.GhostController;
            return controller.IsFullyInvisible || controller.InvisibleTo.Contains(observer.UserId);
        }

    }

}
