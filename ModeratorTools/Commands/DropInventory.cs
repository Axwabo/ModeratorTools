using InventorySystem;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "dropInventory", "Drops everything or the given items from the specified players' inventories", "dropInv")]
[ModeratorPermissions("dropInventory", PlayerPermissions.PlayersManagement)]
[Usage("[...items]")]
public sealed class DropInventory : FilteredTargetingCommand
{

    private readonly HashSet<ItemType> _types = [];

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        _types.Clear();
        if (arguments.Count == 0)
            return CommandResult.Null;
        foreach (var s in arguments)
        {
            if (!Parse.Item(s, out var itemType))
                return $"!Invalid item type: {s}";
            _types.Add(itemType);
        }

        return CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        var inventory = target.inventory;
        var info = inventory.UserInventory;
        if (info.Items.Count == 0 && info.ReserveAmmo.Values.All(e => e == 0))
            return false;
        if (_types.Count == 0)
        {
            inventory.ServerDropEverything();
            return true;
        }

        var any = false;
        foreach (var item in info.Items.Values.Where(e => _types.Contains(e.ItemTypeId)).ToArray())
            any |= item.ServerDropItem(true);
        foreach (var type in info.ReserveAmmo.Keys.Where(e => _types.Contains(e)).ToArray())
            any |= inventory.ServerDropAmmo(type, ushort.MaxValue).Count != 0;
        return any;
    }

}
