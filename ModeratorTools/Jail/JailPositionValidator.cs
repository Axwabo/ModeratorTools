using Axwabo.Helpers.PlayerInfo;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;
using LightContainmentZoneDecontamination;
using MapGeneration;
using PlayerRoles.PlayableScps.Scp106;

namespace ModeratorTools.Jail;

public static class JailPositionValidator
{

    private static readonly FacilityZone[] ValidExitZones = [FacilityZone.LightContainment, FacilityZone.HeavyContainment, FacilityZone.Entrance, FacilityZone.Surface];

    private static Vector3 RandomPosition(FacilityZone zone) => Scp106PocketExitFinder.GetPosesForZone(zone).RandomItem().position;

    public static void ValidateEntry(ReferenceHub hub, PlayerInfoBase info)
    {
        if (!TryGetConfig(out var config)
            || !config.PocketFix
            || !Room.TryGetRoomAtPosition(info.Position, out var room)
            || room.Name != RoomName.Pocket)
            return;
        var capturePosition = hub.playerEffectsController.GetEffect<PocketCorroding>().CapturePosition.Position;
        info.Position = Room.TryGetRoomAtPosition(capturePosition, out _)
            ? capturePosition
            : RandomPosition(ValidExitZones.RandomItem());
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

        if (!config.DecontaminationTeleport || info.Position.y is > DecontaminationController.UpperBoundLCZ or < DecontaminationController.LowerBoundLCZ)
            return;
        var room = Room.Get(Random.value < 0.5f ? RoomName.HczCheckpointA : RoomName.HczCheckpointB).FirstOrDefault();
        if (room != null)
            info.Position = room.Position + Vector3.up;
    }

    private static bool TryGetConfig(out JailConfig config)
    {
        config = ModeratorToolsPlugin.Cfg?.Jail;
        return config != null;
    }

}
