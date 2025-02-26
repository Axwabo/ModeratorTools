namespace ModeratorTools.Commands.Toggles;

public static class ToggleCommandExtensions
{

    public delegate string OneAffected(ToggleContainerBase container, string nickname);

    public delegate string MultipleAffected(ToggleContainerBase container, List<string> nicknames);

    public static string Enabled(this ToggleContainerBase container, string nickname) => $"Enabled {container.FeatureName} for {nickname}.";

    public static string Disabled(this ToggleContainerBase container, string nickname) => $"Disabled {container.FeatureName} for {nickname}.";

    public static string Enabled(this ToggleContainerBase container, List<string> success) => $"Enabled {container.FeatureName} for the following players: {string.Join(", ", success)}";

    public static string Disabled(this ToggleContainerBase container, List<string> failures) => $"Disabled {container.FeatureName} for the following players: {string.Join(", ", failures)}";

    public static CommandResult ToggledResults(this ToggleContainerBase container, List<string> enabled, List<string> disabled) => (enabled.Count, disabled.Count) switch
    {
        (0, 0) => "!No players were affected.",
        (1, 0) => container.Enabled(enabled[0]),
        (0, 1) => container.Disabled(disabled[0]),
        (1, 1) => $"{container.Enabled(enabled[0])}\n{container.Disabled(disabled[0])}",
        (_, 0) => container.Enabled(enabled),
        (0, _) => container.Disabled(disabled),
        _ => $"{container.Enabled(enabled)}\n{container.Disabled(disabled)}"
    };

    public static CommandResult ChangedResults(this ToggleContainerBase container, List<string> affected, OneAffected one, MultipleAffected multiple) => affected.Count switch
    {
        0 => "!No players were affected.",
        1 => one(container, affected[0]),
        _ => multiple(container, affected)
    };

}
