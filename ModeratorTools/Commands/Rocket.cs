using CustomPlayerEffects;
using MEC;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using Utils;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "rocket", 1, "Sends players high into the sky and explodes them")]
[ModeratorPermissions("rocket", PlayerPermissions.ForceclassWithoutRestrictions)]
[ShouldAffectSpectators(false)]
[Usage("<speed>")]
public sealed class Rocket : FilteredTargetingCommand, ICustomResultCompiler
{

    private const string BeenRocketed = "been rocketed into the sky (we're going on a trip, in our favorite rocketship)";

    private static readonly ValueRange<float> SpeedRange = ValueRange<float>.StartOnly(0.1f);

    private static readonly CustomReasonDamageHandler DamageHandler = new("Went on a trip in their favorite rocket ship.");

    private float _speed;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
        => arguments.ParseFloat(SpeedRange, out _speed)
            ? CommandResult.Null
            : "!Invalid speed! Must be at least 0.1";

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!target.TryGetFpcModule(out var module))
            return false;
        Timing.RunCoroutine(FlyRocket(target, module, _speed));
        return true;
    }

    public CommandResult? CompileResultCustom(List<CommandResultOnTarget> success, List<CommandResultOnTarget> failures)
    {
        var count = success.Count;
        return count switch
        {
            0 => "!Noone was rocketed into the sky :(",
            1 => $"{success[0].Nick} has {BeenRocketed}",
            _ => IsEveryoneAffectedInternal(count)
                ? $"Everyone has {BeenRocketed}"
                : $"{count} have {BeenRocketed}"
        };
    }

    private static IEnumerator<float> FlyRocket(ReferenceHub target, FirstPersonMovementModule module, float speed)
    {
        target.playerEffectsController.EnableEffect<Ensnared>();
        module.Motor.GravityController.Gravity = Vector3.zero;
        var life = target.roleManager.CurrentRole.UniqueLifeIdentifier;
        for (var i = 0; i < 50; i++)
        {
            if (!target)
                yield break;
            if (life != target.roleManager.CurrentRole.UniqueLifeIdentifier)
            {
                if (target.TryGetFpcModule(out module) && module.Motor.GravityController.Gravity == Vector3.zero)
                    module.Motor.GravityController.Gravity = FpcGravityController.DefaultGravity;
                yield break;
            }

            module.ServerOverridePosition(module.Position + Vector3.up * speed);
            yield return Timing.WaitForOneFrame;
        }

        module.Motor.GravityController.Gravity = FpcGravityController.DefaultGravity;
        target.playerStats.KillPlayer(DamageHandler);
        ExplosionUtils.ServerSpawnEffect(module.Position, ItemType.GrenadeHE);
    }

}
