namespace ModeratorTools.Commands.Ghost;

public sealed class GhostController
{

    public bool IsFullyInvisible { get; set; }

    public HashSet<string> InvisibleTo { get; } = [];

}
