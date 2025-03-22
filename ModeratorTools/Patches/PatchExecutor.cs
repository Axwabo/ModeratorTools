using HarmonyLib;
using Logger = LabApi.Features.Console.Logger;

namespace ModeratorTools.Patches;

internal static class PatchExecutor
{

    private static Harmony _harmony;

    public static void Patch()
    {
        var shouldPatch = ModeratorToolsPlugin.Cfg?.Ghost ?? true;
        if (!shouldPatch)
            return;
        _harmony = new Harmony("Axwabo.ModeratorTools");
        try
        {
            _harmony.PatchAll();
        }
        catch (Exception e)
        {
            Logger.Error($"Patching failed!\n{e}");
            Unpatch();
        }
    }

    public static void Unpatch()
    {
        _harmony?.UnpatchAll(_harmony.Id);
        _harmony = null;
    }

}
