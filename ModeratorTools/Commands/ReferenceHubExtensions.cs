using PlayerRoles.FirstPersonControl;

namespace ModeratorTools.Commands;

public static class ReferenceHubExtensions
{

    public static bool TryGetFpcModule(this ReferenceHub hub, out FirstPersonMovementModule module)
    {
        if (hub.roleManager.CurrentRole is IFpcRole {FpcModule: var fpcModule})
        {
            module = fpcModule;
            return true;
        }

        module = null;
        return false;
    }

    public static bool TryGetPosition(this ReferenceHub hub, out Vector3 position)
    {
        if (hub.TryGetFpcModule(out var module))
        {
            position = module.Position;
            return true;
        }

        position = Vector3.zero;
        return false;
    }

}
