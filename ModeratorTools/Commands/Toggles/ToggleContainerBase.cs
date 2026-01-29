using System.Reflection;
using Axwabo.CommandSystem.Attributes.Containers;

// ReSharper disable UnusedParameter.Global

namespace ModeratorTools.Commands.Toggles;

[ToggleDescription("Manages {0}")]
public abstract class ToggleContainerBase : ContainerCommand
{

    public string FeatureName { get; }

    protected abstract bool this[PlayerData data] { get; set; }

    protected ToggleContainerBase()
    {
        var type = GetType();
        FeatureName = type.GetCustomAttribute<TogglesFeatureAttribute>()?.Name ?? throw new MissingFeatureNameException(type);
    }

    [MethodBasedSubcommand]
    [ToggleDescription("Lists all players with {0} enabled")]
    [Aliases("l")]
    public CommandResult List(ArraySegment<string> arguments, CommandSender sender)
    {
        var nicknames = new List<string>();
        foreach (var (player, data) in PlayerDataManager.Defined)
            if (this[data])
                nicknames.Add(player.Nickname);
        return nicknames.Count == 0
            ? $"!Nobody has {FeatureName} enabled."
            : $"The following players have {FeatureName} enabled:\n{string.Join(", ", nicknames)}";
    }

    [MethodBasedSubcommand]
    [ToggleDescription("Toggles {0} for the specified players")]
    [Aliases("t")]
    public CommandResult Toggle(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        var enabled = new List<string>();
        var disabled = new List<string>();
        foreach (var hub in targets)
        {
            var nick = hub.nicknameSync.MyNick;
            var data = hub.Data;
            var state = this[data] = !this[data];
            if (state)
                enabled.Add(nick);
            else
                disabled.Add(nick);
        }

        return this.ToggledResults(enabled, disabled);
    }

    [MethodBasedSubcommand]
    [ToggleDescription("Disables {0} for all players")]
    [Aliases("c")]
    public CommandResult Clear(ArraySegment<string> arguments, CommandSender sender)
    {
        foreach (var (_, data) in PlayerDataManager.Defined)
            this[data] = false;
        return $"Disabled {FeatureName} for everyone.";
    }

    [MethodBasedSubcommand]
    [ToggleDescription("Enables {0} for the specified players")]
    [Aliases("e")]
    public CommandResult Enable(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
        => this.ChangedResults(SetState(targets, true), ToggleCommandExtensions.Enabled, ToggleCommandExtensions.Enabled);

    [MethodBasedSubcommand]
    [ToggleDescription("Disables {0} for the specified players")]
    [Aliases("d")]
    public CommandResult Disable(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
        => this.ChangedResults(SetState(targets, false), ToggleCommandExtensions.Disabled, ToggleCommandExtensions.Disabled);

    private List<string> SetState(List<ReferenceHub> targets, bool enabled)
    {
        var affected = new List<string>();
        foreach (var hub in targets)
        {
            var data = hub.Data;
            if (this[data] == enabled)
                continue;
            this[data] = enabled;
            affected.Add(hub.nicknameSync.MyNick);
        }

        return affected;
    }

}
