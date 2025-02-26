using MapGeneration;
using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "randomTeleport", "Teleports the selected players to a random room on the map or in the specified zones")]
[ModeratorPermissions("randomTeleport", PlayerPermissions.PlayersManagement)]
[Usage("[...zones]")]
public sealed class RandomTeleport : SeparatedTargetingCommand, ITargetingPreExecutionFilter
{

    private static readonly ValueRange<FacilityZone> ValidZones = ValueRange<FacilityZone>.Create(FacilityZone.LightContainment, FacilityZone.Surface);

    private List<Room> _targetRooms;

    public CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (arguments.Count == 0)
        {
            _targetRooms = Room.List.ToList();
            return CommandResult.Null;
        }

        var set = new HashSet<FacilityZone>();
        foreach (var s in arguments)
        {
            if (!Parse.EnumIgnoreCase(s, ValidZones, out var result))
                return $"!Invalid zone: {s}";
            set.Add(result);
        }

        _targetRooms = Room.List.Where(e => set.Contains(e.Zone)).ToList();
        return _targetRooms.Count == 0 ? "!No rooms were found." : CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
        => target.TryOverridePosition(_targetRooms.RandomItem().Position + Vector3.up);

}
