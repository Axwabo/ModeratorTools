namespace ModeratorTools.Commands.Muting;

[CommandProperties("all", "Unmutes every non-staff", "*")]
public sealed class UnmuteAll : GlobalMuteCommandBase
{

    protected override string Response => "All non-staff have been unmuted.";
    protected override void Execute(Player player) => player.Unmute(false);

}
