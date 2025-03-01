using Axwabo.Helpers.Config;

namespace ModeratorTools.Commands.RoomTeleports;

public static class RoomTeleportExtensions
{

    private static bool TryGetTeleportOffset(this RoomType type, out Vector3 offset)
    {
        if (ModeratorToolsPlugin.Cfg?.RoomTeleportOffsets is not { } offsets)
        {
            offset = Vector3.zero;
            return false;
        }

        foreach (var point in offsets)
            if (point.Type == type)
            {
                offset = point.PositionOffset;
                return true;
            }

        offset = Vector3.zero;
        return false;
    }

    public static Vector3 GetTeleportPosition(this Room room)
    {
        var type = ConfigHelper.GetRoomType(room.GameObject.name);
        return type != RoomType.Unknown && type.TryGetTeleportOffset(out var offset)
            ? room.Transform.TransformPoint(offset) + Vector3.up
            : room.Position + Vector3.up;
    }

    public static bool TryGetTeleportPosition(this RoomType roomType, out Vector3 position)
    {
        var room = ConfigHelper.GetRoomByType(roomType);
        if (room)
        {
            position = roomType.TryGetTeleportOffset(out var offset)
                ? room.transform.TransformPoint(offset) + Vector3.up
                : room.transform.position + Vector3.up;
            return true;
        }

        position = Vector3.zero;
        return false;
    }

}
