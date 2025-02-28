using MEC;
using Mirror;
using PlayerRoles;
using PlayerRoles.Ragdolls;
using PlayerStatsSystem;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "ragdoll", 1, "Spawns ragdolls on the specified players")]
[ModeratorPermissions("ragdoll", PlayerPermissions.ForceclassWithoutRestrictions)]
[Usage("<roleType> [countPerPlayer]")]
[ShouldAffectSpectators(false)]
public sealed class Ragdoll : FilteredTargetingCommand
{

    private static readonly UniversalDamageHandler DamageHandler = new(0, DeathTranslations.Unknown);

    private RoleTypeId _role;

    private IRagdollRole _template;

    private int _count;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!arguments.ParseRole(out _role) || !PlayerRoleLoader.TryGetRoleTemplate(_role, out _template))
            return "!Invalid role.";
        _count = 1;
        return arguments.Count == 1 || arguments.ParseInt(out _count, 1) && _count > 0
            ? CommandResult.Null
            : "!Invalid count per player.";
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        Timing.RunCoroutine(SpawnRagdolls(target.transform, _template.Ragdoll, _role, _count), target.gameObject);
        return true;
    }

    public static IEnumerator<float> SpawnRagdolls(Transform transform, BasicRagdoll template, RoleTypeId roleTypeId, int count)
    {
        for (var i = 0; i < count; i++)
        {
            var position = transform.position;
            var rotation = transform.rotation;
            var clone = Object.Instantiate(template, position, rotation);
            clone.NetworkInfo = new RagdollData(null, DamageHandler, roleTypeId, position, rotation, "SCP-343", NetworkTime.time);
            NetworkServer.Spawn(clone.gameObject);
            yield return Timing.WaitForOneFrame;
        }
    }

}
