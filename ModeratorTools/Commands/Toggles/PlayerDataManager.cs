using System.Runtime.CompilerServices;

namespace ModeratorTools.Commands.Toggles;

public static class PlayerDataManager
{

    private static readonly ConditionalWeakTable<Player, PlayerData> Data = new();

    extension(Player player)
    {

        public PlayerData Data => Data.GetOrCreateValue(player);

    }

    extension(ReferenceHub hub)
    {

        public PlayerData Data => Player.Get(hub).Data;

    }

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
