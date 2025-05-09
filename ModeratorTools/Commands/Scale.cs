﻿using Mirror;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "scale", 1, "Set the size of the specified players", "size")]
[ModeratorPermissions("scale", PlayerPermissions.PlayersManagement)]
[Usage("reset", "<scalar>", "<x> <y> <z>")]
public sealed class Scale : FilteredTargetingCommand
{

    private Vector3 _scale;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        if (arguments.At(0).Equals("reset", StringComparison.OrdinalIgnoreCase))
        {
            _scale = Vector3.one;
            return CommandResult.Null;
        }

        if (arguments.Count != 1)
            return arguments.Count < 3
                ? CommandResult.Failed(CombinedUsage)
                : arguments.ParseVector(out _scale);
        if (!arguments.ParseFloat(out var scalar))
            return "!Invalid scalar value.";
        _scale = Vector3.one * scalar;
        return CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        var transform = target.transform;
        if (transform.localScale == _scale)
            return false;
        transform.localScale = _scale;
        var identity = target.networkIdentity;
        foreach (var p in Player.ReadyList)
            NetworkServer.SendSpawnMessage(identity, p.Connection); // TODO: wait for an official player scaling method
        return true;
    }

}
