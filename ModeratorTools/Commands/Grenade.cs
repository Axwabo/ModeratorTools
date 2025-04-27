using Footprinting;
using InventorySystem;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using ThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "grenade", 1, "Spawns an active grenade on the specified players")]
[ModeratorPermissions("grenade", PlayerPermissions.GivingItems)]
[Usage("<grenadeType> [fuseTime]")]
[ShouldAffectSpectators(false)]
public sealed class Grenade : FilteredTargetingCommand
{

    private ThrowableItem _template;

    private float _fuseTime;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!arguments.ParseItem(out var type) || !InventoryItemLoader.TryGetItem(type, out _template))
            return "!Invalid grenade type.";
        _fuseTime = -1;
        return arguments.Count == 1 || arguments.ParseFloat(out _fuseTime, 1) && _fuseTime >= 0
            ? CommandResult.Null
            : "!Invalid fuse time.";
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!target.TryGetPosition(out var position))
            return false;
        var clone = Object.Instantiate(_template.Projectile, position, Quaternion.identity);
        if (_fuseTime >= 0 && clone is TimeGrenade timeGrenade)
            timeGrenade._fuseTime = _fuseTime;
        clone.NetworkInfo = new PickupSyncInfo(_template.ItemTypeId, _template.Weight);
        clone.PreviousOwner = new Footprint(target);
        if (clone.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.linearVelocity = Random.onUnitSphere;
        NetworkServer.Spawn(clone.gameObject);
        clone.ServerActivate();
        return true;
    }

}
