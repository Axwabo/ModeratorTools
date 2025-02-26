using System.ComponentModel;
using ModeratorTools.Jail;

namespace ModeratorTools;

[Serializable]
public sealed class ModeratorToolsConfig
{

    [Description("Whether to use base-game permissions instead of string-based ones.")]
    public bool VanillaPermissions { get; set; }

    [Description("If enabled, tutorial roles will not be targeted by SCP-096.")]
    public bool TutorialsImmuneToScp096 { get; set; }

    [Description("If enabled, tutorial roles will not count as observers of SCP-173.")]
    public bool TutorialsImmuneToScp173 { get; set; }

    public JailConfig Jail { get; set; } = new();

}
