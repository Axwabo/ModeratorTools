using PlayerStatsSystem;
using Utils;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "explode", "Instantly explodes the specified players")]
[ModeratorPermissions("explode", PlayerPermissions.ForceclassToSpectator)]
public sealed class Explode : SeparatedTargetingCommand, ICustomResultCompiler
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

    public CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures) => success.Count switch
    {
        0 => "!No players were affected.",
        1 => $"Game ended (exploded) player {success[0].Nick}",
        _ => IsEveryoneAffectedInternal(success.Count)
            ? "Everyone exploded, Hubert can't believe you've done this"
            : $"Game ended (exploded) {success.Count} players"
    };

}
