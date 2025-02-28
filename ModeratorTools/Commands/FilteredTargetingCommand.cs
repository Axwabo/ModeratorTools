namespace ModeratorTools.Commands;

public abstract class FilteredTargetingCommand : SeparatedTargetingCommand, ITargetingPreExecutionFilter
{

    public abstract CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender);

}
