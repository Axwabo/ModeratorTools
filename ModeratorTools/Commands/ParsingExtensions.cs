namespace ModeratorTools.Commands;

public static class ParsingExtensions
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

    public static bool ParseVisibility(this ArraySegment<string> arguments, out bool result, int index = 0)
    {
        switch (arguments.At(index).ToLower())
        {
            case "show" or "enable" or "true" or "1":
                result = true;
                return true;
            case "hide" or "disable" or "false" or "0":
                result = false;
                return true;
            default:
                result = false;
                return false;
        }
    }

}
