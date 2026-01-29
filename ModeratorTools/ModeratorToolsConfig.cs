using System.ComponentModel;
using Axwabo.Helpers.Config;
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

    [Description("If enabled, tutorial roles will not be kicked for AFK.")]
    public bool TutorialsImmuneToAfkKick { get; set; }

    [Description("If enabled, tutorial roles will be in god mode.")]
    public bool GodModeTutorials { get; set; }

    [Description("Whether the ghost command should be registered.")]
    public bool Ghost { get; set; } = true;

    public JailConfig Jail { get; set; } = new();

    [Description("Position offset to teleport to in rooms which have a fatal/unreachable origin point.")]
    public List<MapPointByRoomType> RoomTeleportOffsets { get; set; } =
    [
        new(RoomType.EzCollapsedTunnel, 0, 0, 5),
        new(RoomType.HczAcroamaticAbatement, 2.5f, 0, 2.5f),
        new(RoomType.HczArmory, -2),
        new(RoomType.HczTesla, 4),
        new(RoomType.HczTestroom, 0, 0, 5.5f),
        new(RoomType.Lcz173, -5),
        new(RoomType.Lcz330, -5),
        new(RoomType.LczArmory, -4.5f),
        new(RoomType.LczCurve, 1, 0, -1)
    ];

}
