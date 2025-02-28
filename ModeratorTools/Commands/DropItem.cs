using Axwabo.CommandSystem.Extensions;
using InventorySystem;
using InventorySystem.Items;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "dropItem", 1, "Drops an item or multiple items on the specified players")]
[ModeratorPermissions("dropItem", PlayerPermissions.GivingItems)]
[Usage("<item> [countPerPlayer]")]
[ShouldAffectSpectators(false)]
public sealed class DropItem : FilteredTargetingCommand
{

    private const int MaxTotal = 200;

    private ItemBase _item;

    private int _countPerPlayer;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!arguments.ParseItem(out var type) || !InventoryItemLoader.AvailableItems.TryGetValue(type, out _item))
            return "!Invalid item type.";
        _countPerPlayer = 1;
        if (arguments.Count < 2)
            return CommandResult.Null;
        if (!arguments.ParseInt(out _countPerPlayer, 1) || _countPerPlayer <= 0)
            return "!Invalid count.";
        var max = MaxTotal / targets.Count;
        return max < _countPerPlayer
            ? $"!For {"target".PluralizeWithCount(targets.Count)}, only {max} items are allowed per player."
            : CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!target.TryGetPosition(out var position))
            return false;
        for (var i = 0; i < _countPerPlayer; i++)
            InventoryExtensions.ServerCreatePickup(_item, null, position);
        return true;
    }

}
