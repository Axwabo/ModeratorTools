using System.ComponentModel;
using Axwabo.Helpers.Config;

namespace ModeratorTools.Jail;

[Serializable]
public sealed class JailConfig
{

    [Description("If enabled, players jailed in the facility will be teleported to the surface when they're unjailed following the warhead detonation.")]
    public bool WarheadTeleport { get; set; } = true;

    [Description("If enabled, players jailed in Light Containment Zone will be teleported to an exit in Heavy Containment Zone when they're unjailed after decontamination.")]
    public bool DecontaminationTeleport { get; set; } = true;

    [Description("If enabled, players jailed in the pocket dimension will be unjailed in the zone they were last in. If no zone was found, a random room will be picked.")]
    public bool PocketFix { get; set; } = true;

    [Description("Extra jail positions relative to Surface (0; 1000; 0)")]
    public List<SerializedRotation> ExtraPositions { get; set; } =
    [
        new(130.4693f, -7.594038f, 21.48689f),
        new(161.1646f, 18.50996f, -12.87285f),
        new(107.5201f, 13.08837f, -13.64667f)
    ];

}
