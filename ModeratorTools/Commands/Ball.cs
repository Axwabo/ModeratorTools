using Footprinting;
using InventorySystem;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using RemoteAdmin;
using ThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "ball", "Spawns a bouncy ball (SCP-018) on the specified players")]
[ModeratorPermissions("ball", PlayerPermissions.GivingItems)]
[ShouldAffectSpectators(false)]
public sealed class Ball : UnifiedTargetingCommand
{

    private ThrownProjectile _template;

    protected override CommandResult ExecuteOnTargets(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!InventoryItemLoader.TryGetItem(ItemType.SCP018, out ThrowableItem throwable))
            return "!SCP-018 item template not found!";
        _template = throwable.Projectile;
        var count = 0;
        ReferenceHub target = null;
        foreach (var hub in targets)
        {
            if (!SpawnBall(hub, sender))
                continue;
            target = hub;
            count++;
        }

        if (count == 0)
            return "!No players were affected.";
        if (count == 1)
            return $"{target!.nicknameSync.MyNick} has received a bouncing ball!";
        LabApi.Features.Wrappers.Cassie.Message("pitch_1.5 xmas_bouncyballs . pitch_1", false, false, false);
        return $"The balls are bouncing for {count} players!";
    }

    private bool SpawnBall(ReferenceHub hub, CommandSender sender)
    {
        if (!hub.TryGetPosition(out var position))
            return false;
        var grenade = Object.Instantiate(_template, position, Quaternion.identity);
        grenade.PreviousOwner = new Footprint(sender is PlayerCommandSender {ReferenceHub: var senderHub} ? senderHub : ReferenceHub._hostHub);
        grenade.NetworkInfo = new PickupSyncInfo(ItemType.SCP018, 0, 0, true);
        grenade.GetComponent<Rigidbody>().linearVelocity = Random.onUnitSphere;
        NetworkServer.Spawn(grenade.gameObject);
        grenade.ServerActivate();
        return true;
    }

}
