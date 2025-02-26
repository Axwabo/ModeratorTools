namespace ModeratorTools.Commands.Toggles;

public sealed record ToggleCommandInfo(string Name, Func<PlayerData, bool> Get, Action<PlayerData, bool> Set);
