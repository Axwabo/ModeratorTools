using Axwabo.CommandSystem.PropertyManager.Resolvers;

namespace ModeratorTools;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class ModeratorPermissionsResolver : Attribute, IAttributeBasedPermissionResolver<ModeratorPermissions>
{

    public IPermissionChecker CreatePermissionCheckerInstance(ModeratorPermissions attribute)
        => ModeratorToolsPlugin.Cfg?.VanillaPermissions ?? false
            ? new SimpleVanillaPlayerPermissionChecker(attribute.VanillaPermission)
            : new StringPermissionChecker(attribute.StringPermission);

}
