namespace ModeratorTools.Commands;

public static class VectorParsingExtensions
{

    public static CommandResult? ParseVector(this ArraySegment<string> arguments, out Vector3 position, int start = 0)
    {
        position = Vector3.zero;
        if (!arguments.ParseFloat(out var x, start))
            return "!Invalid X value.";
        if (!arguments.ParseFloat(out var y, start + 1))
            return "!Invalid Y value.";
        if (!arguments.ParseFloat(out var z, start + 2))
            return "!Invalid Z value.";
        position = new Vector3(x, y, z);
        return CommandResult.Null;
    }

}
