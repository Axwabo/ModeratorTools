using Axwabo.CommandSystem.PropertyManager.Resolvers;

namespace ModeratorTools.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class ModeratorPermissionsResolver : Attribute, IAttributeBasedPermissionResolver<ModeratorPermissions>
{

    public IPermissionChecker ResolvePermissionChecker(ModeratorPermissions attribute)
        => ModeratorToolsPlugin.Cfg?.VanillaPermissions ?? false
            ? new SimpleVanillaPlayerPermissionChecker(attribute.VanillaPermission)
            : new StringPermissionChecker($"mt.{attribute.StringPermission}");

}
