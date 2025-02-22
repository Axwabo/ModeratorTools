using PlayerRoles.FirstPersonControl;
using UnityEngine;

namespace ModeratorTools;

public static class ReferenceHubExtensions
{

    public static bool TryGetPosition(this ReferenceHub hub, out Vector3 position)
    {
        if (hub.roleManager.CurrentRole is IFpcRole {FpcModule.Position: var pos})
        {
            position = pos;
            return true;
        }

        position = Vector3.zero;
        return false;
    }

}
