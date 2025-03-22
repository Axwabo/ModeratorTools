namespace ModeratorTools.Commands.Position;

[CommandProperties("add", 3, "Moves the specified players by the given amount, optionally accounting for camera rotation")]
[ModeratorPermissions("setPosition", PlayerPermissions.PlayersManagement)]
[Usage("<x> <y> <z> [cameraSpace]")]
[ShouldAffectSpectators(false)]
public sealed class Add : FilteredTargetingCommand
{

    private Vector3 _offset;

    private bool _cameraSpace;

    public override CommandResult? OnBeforeExecuted(List<ReferenceHub> targets, ArraySegment<string> arguments, CommandSender sender)
    {
        var result = arguments.ParseVector(out _offset);
        if (result.HasValue)
            return result;
        if (arguments.Count == 3)
            return CommandResult.Null;
        _cameraSpace = arguments.At(3).ToLower() is "true" or "1";
        return CommandResult.Null;
    }

    protected override CommandResult ExecuteOn(ReferenceHub target, ArraySegment<string> arguments, CommandSender sender)
    {
        if (!target.TryGetFpcModule(out var module))
            return false;
        var delta = _cameraSpace ? target.PlayerCameraReference.TransformDirection(_offset) : _offset;
        module.ServerOverridePosition(module.Position + delta);
        return true;
    }

}
