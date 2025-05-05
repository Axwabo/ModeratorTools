using System.Runtime.CompilerServices;

namespace ModeratorTools.Commands.Ghost;

public static class GhostExtensions
{

    private static readonly ConditionalWeakTable<Player, GhostController> Controllers = new();

    public static bool Enabled => ModeratorToolsPlugin.Cfg?.Ghost ?? true;

    public static GhostController GetGhostController(this ReferenceHub hub) => Player.Get(hub).GetGhostController();

    public static GhostController GetGhostController(this Player player) => Controllers.GetOrCreateValue(player);

    public static bool IsGhostInvisibleTo(this Player target, Player observer)
    {
        var controller = target.GetGhostController();
        return controller.IsFullyInvisible || controller.InvisibleTo.Contains(observer.UserId);
    }

}
