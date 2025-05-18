using InventorySystem;
using InventorySystem.Items;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "dropSize", 2, "Drops an item with a given size on the specified players")]
[ModeratorPermissions("dropSize", PlayerPermissions.GivingItems)]
[Usage("<item> <scalar>", "<item> <x> <y> <z>")]
[ShouldAffectSpectators(false)]
public sealed class DropSize : FilteredTargetingCommand
{

    private ItemBase _item;

    private Vector3 _scale;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!arguments.ParseItem(out var type) || !InventoryItemLoader.AvailableItems.TryGetValue(type, out _item))
            return "!Invalid item type.";
        if (arguments.Count != 2)
            return arguments.Count < 4
                ? CommandResult.Failed(CombinedUsage)
                : arguments.ParseVector(out _scale, 1);
        if (!arguments.ParseFloat(out var scalar, 1))
            return "!Invalid scalar.";
        _scale = Vector3.one * scalar;
        return CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!target.TryGetPosition(out var position))
            return false;
        InventoryExtensions.ServerCreatePickup(_item, null, position, true, pickup => pickup.transform.localScale = _scale);
        return true;
    }

}
