using HarmonyLib;
using ModeratorTools.Commands.Ghost;
using PlayerRoles.FirstPersonControl.NetworkMessages;
using PlayerRoles.Visibility;
using static Axwabo.Helpers.Harmony.InstructionHelper;

// ReSharper disable All

namespace ModeratorTools.Patches;

[HarmonyPatch(typeof(FpcServerPositionDistributor), nameof(FpcServerPositionDistributor.WriteAll))]
file static class GhostPatch
{

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var list = instructions.ToList();
        var index = list.FindCall(nameof(VisibilityController.ValidateVisibility)) + 6;
        list.InsertRange(index, [
            This,
            Ldloc(5),
            Ldloca(7),
            Call(GhostExtensions.OverrideVisibility)
        ]);
        return list;
    }

}
