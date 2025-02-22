namespace ModeratorTools;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class ModeratorPermissions : Attribute
{

    public string StringPermission { get; }

    public PlayerPermissions VanillaPermission { get; }

    public ModeratorPermissions(string stringPermission, PlayerPermissions vanillaPermission)
    {
        StringPermission = stringPermission;
        VanillaPermission = vanillaPermission;
    }

}
