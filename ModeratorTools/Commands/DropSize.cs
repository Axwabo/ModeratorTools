using InventorySystem;
using InventorySystem.Items;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "dropSize", 2, "Drops an item with a given size on the specified players")]
[ModeratorPermissions("dropSize", PlayerPermissions.GivingItems)]
[Usage("<item> <scalar>", "<item> <x> <y> <z>")]
[ShouldAffectSpectators(false)]
public sealed class DropSize : SeparatedTargetingCommand, ITargetingPreExecutionFilter
{

    private ItemBase _item;

    private Vector3 _scale;

    public CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!arguments.ParseItem(out var type) || !InventoryItemLoader.AvailableItems.TryGetValue(type, out _item))
            return "!Invalid item type.";
        if (arguments.Count == 2)
        {
            if (!arguments.ParseFloat(out var scalar, 1))
                return "!Invalid scalar.";
            _scale = Vector3.one * scalar;
            return CommandResult.Null;
        }

        if (arguments.Count < 5)
            return CommandResult.Failed(CombinedUsage);
        if (!arguments.ParseFloat(out var x, 1))
            return "!Invalid X value.";
        if (!arguments.ParseFloat(out var y, 2))
            return "!Invalid Y value.";
        if (!arguments.ParseFloat(out var z, 3))
            return "!Invalid Z value.";
        _scale = new Vector3(x, y, z);
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
