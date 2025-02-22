namespace ModeratorTools.Commands.Jail;

internal static class JailWithMeHelpers
{

    public static CommandResult CompileResult(List<CommandResultOnTarget> success, bool senderAffected, string adjective) => new(
        success.Count != 0,
        $"{(senderAffected ? $"You have been {adjective}." : $"You're already {adjective}.")} {success.Affected()}"
    );

    private static string Affected(this List<CommandResultOnTarget> results) => results.Count switch
    {
        0 => "No other players were affected.",
        1 => $"The request affected {results[0].Nick}.",
        _ => $"The request affected {results.Count} other players."
    };

}
