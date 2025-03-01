using Axwabo.CommandSystem.Attributes.Containers;
using MEC;

// ReSharper disable UnusedParameter.Global

namespace ModeratorTools.Commands.Toggles;

[CommandProperties(CommandHandlerType.RemoteAdmin, "regeneration")]
[Aliases("regen")]
[ModeratorPermissions("regeneration", PlayerPermissions.Effects)]
[TogglesFeature("periodic health regeneration")]
public sealed class RegenerationCommand : ToggleContainerBase
{

    private static float _interval = 1;

    private static float _amount = 1;

    public static void Tick()
    {
        if (ModeratorToolsPlugin.Instance == null)
            return;
        Timing.CallDelayed(_interval, Tick);
        foreach (var (player, data) in PlayerDataManager.Defined)
            if (data.Regeneration)
                player.Heal(_amount);
    }

    protected override bool this[PlayerData data]
    {
        get => data.Regeneration;
        set => data.Regeneration = value;
    }

    [MethodBasedSubcommand]
    [CommandProperties("heal", "Gets or sets regeneration amount", "h")]
    [Usage("[HP]")]
    public CommandResult Heal(ArraySegment<string> arguments, CommandSender sender)
    {
        if (arguments.Count == 0)
            return $"Current regeneration amount is {_amount} HP.";
        if (!arguments.ParseFloat(out var amount) || amount <= 0)
            return "!Invalid amount.";
        _amount = amount;
        return $"Regeneration amount set to {amount} HP.";
    }

    [MethodBasedSubcommand]
    [CommandProperties("time", "Gets or sets regeneration interval", "t")]
    [Usage("[timeSeconds]")]
    public CommandResult Time(ArraySegment<string> arguments, CommandSender sender)
    {
        if (arguments.Count == 0)
            return $"Current regeneration interval is {Seconds(_interval)}.";
        if (!arguments.ParseFloat(out var interval) || interval <= 0)
            return "!Invalid interval.";
        _interval = interval;
        return $"Regeneration interval set to {Seconds(interval)}.";
    }

    private static string Seconds(float interval) => $"{interval} second{(Mathf.Approximately(interval, 1) ? "" : "s")}";

}
