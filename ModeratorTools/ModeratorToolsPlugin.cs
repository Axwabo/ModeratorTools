using Axwabo.CommandSystem.Registration;
using LabApi.Events.CustomHandlers;
using LabApi.Loader.Features.Plugins;
using PlayerRoles;

namespace ModeratorTools;

[ModeratorPermissionsResolver]
[ToggleDescriptionResolver]
public sealed class ModeratorToolsPlugin : Plugin<ModeratorToolsConfig>
{

    internal static ModeratorToolsPlugin Instance { get; private set; }

    internal static ModeratorToolsConfig Cfg => Instance?.Config;

    public override string Name => "ModeratorTools";
    public override string Description => "Tools for server staff";
    public override string Author => "Axwabo";
    public override Version Version => GetType().Assembly.GetName().Version;
    public override Version RequiredApiVersion { get; } = new(1, 0, 0);

    private readonly EventHandlers _handlers = new();

    public override void Enable()
    {
        Instance = this;
        CustomHandlersManager.RegisterEventsHandler(_handlers);
        PlayerRoleManager.OnRoleChanged += EventHandlers.HandleGodTuts; // TODO: PlayerChangedRoleEvent is currently broken
        CommandRegistrationProcessor.RegisterAll(this);
    }

    public override void Disable()
    {
        Instance = null;
        CustomHandlersManager.UnregisterEventsHandler(_handlers);
        PlayerRoleManager.OnRoleChanged -= EventHandlers.HandleGodTuts;
        CommandRegistrationProcessor.UnregisterAll(this);
    }

}
