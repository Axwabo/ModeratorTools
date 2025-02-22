using System.ComponentModel;
using ModeratorTools.Jail;

namespace ModeratorTools;

[Serializable]
public sealed class ModeratorToolsConfig
{

    [Description("Whether to use base-game permissions instead of string-based ones.")]
    public bool VanillaPermissions { get; set; }

    public JailConfig Jail { get; set; } = new();

}
