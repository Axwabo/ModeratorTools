using System.Runtime.CompilerServices;

namespace ModeratorTools.Commands.Ghost;

public static class GhostExtensions
{

    private static readonly ConditionalWeakTable<Player, GhostController> Controllers = new();

    public static bool Enabled => ModeratorToolsPlugin.Cfg?.Ghost ?? true;

    public static GhostController GetGhostController(this ReferenceHub hub) => Controllers.GetOrCreateValue(Player.Get(hub));

    public static void OverrideVisibility(ReferenceHub receiver, ReferenceHub target, ref bool isInvisible)
    {
        if (isInvisible)
            return;
        var controller = target.GetGhostController();
        isInvisible = controller.IsFullyInvisible || controller.InvisibleTo.Contains(receiver.authManager.UserId);
    }

}
