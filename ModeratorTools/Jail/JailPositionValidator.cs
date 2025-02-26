using Axwabo.CommandSystem.Extensions;
using Axwabo.Helpers.PlayerInfo;
using Axwabo.Helpers.PlayerInfo.Effect;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using LightContainmentZoneDecontamination;
using MapGeneration;
using PlayerRoles.PlayableScps.Scp106;

namespace ModeratorTools.Jail;

public static class JailPositionValidator
{

    private static readonly FacilityZone[] ValidExitZones = [FacilityZone.LightContainment, FacilityZone.HeavyContainment, FacilityZone.Entrance, FacilityZone.Surface];

    public static readonly Vector3 SurfaceUp = Vector3.up * 1001;

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
            || info.Position.y is > DecontaminationController.UpperBoundLCZ or < DecontaminationController.LowerBoundLCZ)
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

        position = (Vector3) config.ExtraPositions[index] + SurfaceUp;
        return true;
    }

    private static bool TryGetConfig(out JailConfig config)
    {
        config = ModeratorToolsPlugin.Cfg?.Jail;
        return config != null;
    }

}
