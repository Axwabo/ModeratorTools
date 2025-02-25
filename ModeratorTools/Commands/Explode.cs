using PlayerStatsSystem;
using Utils;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "explode", "Instantly explodes the specified players")]
[ModeratorPermissions("explode", PlayerPermissions.ForceclassToSpectator)]
public sealed class Explode : SeparatedTargetingCommand
{

    private static readonly CustomReasonDamageHandler Handler = new("Exploded by an admin.");

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!target.TryGetPosition(out var position))
            return false;
        ExplosionUtils.ServerSpawnEffect(position, ItemType.GrenadeHE);
        target.playerStats.KillPlayer(Handler);
        return true;
    }

}
