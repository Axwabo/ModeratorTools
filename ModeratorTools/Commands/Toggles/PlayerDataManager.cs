using System.Runtime.CompilerServices;
using LabApi.Features.Wrappers;

namespace ModeratorTools.Commands.Toggles;

public static class PlayerDataManager
{

    private static readonly ConditionalWeakTable<Player, PlayerData> Data = new();

    public static PlayerData GetData(this Player player) => Data.GetOrCreateValue(player);

    public static PlayerData GetData(this ReferenceHub hub) => Player.Get(hub).GetData();

    public static bool SetState(this ToggleCommandInfo info, ReferenceHub hub, bool state)
    {
        var data = hub.GetData();
        if (info.Get(data) == state)
            return false;
        info.Set(data, state);
        return true;
    }

    public static bool Toggle(this ToggleCommandInfo info, ReferenceHub hub)
    {
        var data = hub.GetData();
        var state = !info.Get(data);
        info.Set(data, state);
        return state;
    }

    public static IEnumerable<(Player Player, PlayerData Data)> Defined
    {
        get
        {
            foreach (var player in Player.List)
                if (Data.TryGetValue(player, out var data))
                    yield return (player, data);
        }
    }

}
