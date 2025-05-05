using System.Runtime.CompilerServices;

namespace ModeratorTools.Commands.Toggles;

public static class PlayerDataManager
{

    private static readonly ConditionalWeakTable<Player, PlayerData> Data = new();

    public static PlayerData GetData(this Player player) => Data.GetOrCreateValue(player);

    public static PlayerData GetData(this ReferenceHub hub) => Player.Get(hub).GetData();

    public static IEnumerable<(Player Player, PlayerData Data)> Defined
    {
        get
        {
            foreach (var player in Player.ReadyList)
                if (Data.TryGetValue(player, out var data))
                    yield return (player, data);
        }
    }

}
