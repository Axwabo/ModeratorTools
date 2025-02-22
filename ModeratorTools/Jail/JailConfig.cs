using System.ComponentModel;

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

}
