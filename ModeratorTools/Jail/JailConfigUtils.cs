using AFK;
using Axwabo.CommandSystem.Extensions;
using Axwabo.Helpers.PlayerInfo;
using Axwabo.Helpers.PlayerInfo.Effect;
using CustomPlayerEffects;
using MapGeneration;
using PlayerRoles.PlayableScps.Scp106;

namespace ModeratorTools.Jail;

public static class JailConfigUtils
{

    private static readonly FacilityZone[] ValidExitZones = [FacilityZone.LightContainment, FacilityZone.HeavyContainment, FacilityZone.Entrance, FacilityZone.Surface];

    private static Vector3 RandomPosition(FacilityZone zone) => Scp106PocketExitFinder.GetPosesForZone(zone).RandomItem().position + Vector3.up;

    public static void ValidateEntry(ReferenceHub hub, PlayerInfoBase info)
    {
        if (!TryGetConfig(out var config) || !config.PocketFix)
            return;
        var effect = hub.playerEffectsController.GetEffect<PocketCorroding>();
        if (!effect.IsEnabled)
            return;
        var capturePosition = effect.CapturePosition.Position;
        info.Position = Room.TryGetRoomAtPosition(capturePosition, out var captureRoom) && captureRoom.Name != RoomName.Pocket
            ? capturePosition
            : RandomPosition(ValidExitZones.RandomItem());
        foreach (var effectInfo in info.Effects)
            if (effectInfo is StandardEffectInfo {EffectType: EffectType.PocketCorroding or EffectType.Sinkhole})
                effectInfo.IsEnabled = false;
    }

    public static void ValidateExit(PlayerInfoBase info)
    {
        if (!TryGetConfig(out var config))
            return;
        if (config.WarheadTeleport && Warhead.IsDetonated && AlphaWarheadController.CanBeDetonated(info.Position))
        {
            info.Position = RandomPosition(FacilityZone.Surface);
            return;
        }

        if (!config.DecontaminationTeleport
            || !Decontamination.IsDecontaminating
            || info.Position.GetZone() != FacilityZone.LightContainment)
            return;
        var room = Room.Get(Random.value < 0.5f ? RoomName.HczCheckpointA : RoomName.HczCheckpointB).FirstOrDefault();
        if (room != null)
            info.Position = room.Position + Vector3.up;
    }

    public static CommandResult TryGetCustomJailPosition(int index, out Vector3 position)
    {
        if (!TryGetConfig(out var config))
        {
            position = Vector3.zero;
            return "!Couldn't find custom configured positions.";
        }

        if (index < 0 || index >= config.ExtraPositions.Count)
        {
            position = Vector3.zero;
            return $"!Invalid index. There are only {"extra position".PluralizeWithCount(config.ExtraPositions.Count)}.";
        }

        position = (Vector3) config.ExtraPositions[index] + Vector3.up + RoomIdentifier.AllRoomIdentifiers
            .Where(e => e.Name == RoomName.Outside)
            .Select(e => e.transform.position)
            .FirstOrDefault();
        return true;
    }

    private static bool TryGetConfig(out JailConfig config)
    {
        config = ModeratorToolsPlugin.Cfg?.Jail;
        return config != null;
    }

    public static void OnJailed(ReferenceHub hub)
    {
        if (!TryGetConfig(out var config))
            return;
        hub.characterClassManager.GodMode = config.GodMode;
        if (config.PreventAfkKick)
            AFKManager.RemovePlayer(hub);
    }

    public static void OnUnjailed(ReferenceHub hub)
    {
        if (TryGetConfig(out var config) && config.PreventAfkKick)
            AFKManager.AddPlayer(hub);
    }

}
