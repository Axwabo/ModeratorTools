using Axwabo.CommandSystem.Registration;
using LabApi.Loader.Features.Plugins;

namespace ModeratorTools;

public sealed class ModeratorToolsPlugin : Plugin<ModeratorToolsConfig>
{

    public override string Name => "ModeratorTools";
    public override string Description => "Tools for server staff";
    public override string Author => "Axwabo";
    public override Version Version => GetType().Assembly.GetName().Version;
    public override Version RequiredApiVersion { get; } = new(1, 0, 0);

    public override void Enable() => CommandRegistrationProcessor.RegisterAll(this);

    public override void Disable() => CommandRegistrationProcessor.RegisterAll(this);

}
