using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace ModeratorTools;

public sealed class ModeratorToolsConfig
{

    [Description("Whether to use base-game permissions instead of string-based ones.")]
    public bool VanillaPermissions { get; set; }

}
