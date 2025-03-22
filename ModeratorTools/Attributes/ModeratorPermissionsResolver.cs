using Axwabo.CommandSystem.PropertyManager.Resolvers;

namespace ModeratorTools.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class ModeratorPermissionsResolver : Attribute, IAttributeBasedPermissionResolver<ModeratorPermissions>
{

    public IPermissionChecker ResolvePermissionChecker(ModeratorPermissions attribute)
        => ModeratorToolsPlugin.Cfg?.VanillaPermissions ?? false
            ? new SimpleVanillaPlayerPermissionChecker(attribute.VanillaPermission)
            : new StringPermissionChecker(EnsurePrefix(attribute.StringPermission));

    public static string EnsurePrefix(string permission) => permission.StartsWith("mt.") ? permission : $"mt.{permission}";

}
