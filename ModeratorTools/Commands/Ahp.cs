using PlayerStatsSystem;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "ahp", 1, "Sets the artificial health of the specified players")]
[ModeratorPermissions("ahp", PlayerPermissions.PlayersManagement)]
[Usage("<value>")]
[ShouldAffectSpectators(false)]
public sealed class Ahp : FilteredTargetingCommand
{

    private static readonly ValueRange<float> Range = ValueRange<float>.Create(0, 75);

    private float _value;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
        => arguments.ParseFloat(Range, out _value)
            ? CommandResult.Null
            : "!Invalid AHP value. Must be in range 0-75.";

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        var stat = target.playerStats.GetModule<AhpStat>();
        if (_value == 0)
            stat.ServerKillAllProcesses();
        else if (stat._activeProcesses.Count == 0)
            stat.ServerAddProcess(_value);
        else
            stat._activeProcesses[0].CurrentAmount = _value;
        return true;
    }

}
