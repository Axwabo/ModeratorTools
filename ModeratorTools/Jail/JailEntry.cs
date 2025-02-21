using Axwabo.Helpers.PlayerInfo;

namespace ModeratorTools.Jail;

public sealed class JailEntry
{

    public IPlayerInfoWithRole Info { get; }

    public bool ThisRound { get; set; } = true;

    public JailEntry(IPlayerInfoWithRole info) => Info = info;

}
