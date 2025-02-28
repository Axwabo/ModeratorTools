namespace ModeratorTools.Commands.Muting;

[CommandProperties("intercom", "Intercom-unmutes every non-staff", "icom")]
public sealed class UnmuteIntercom : GlobalMuteCommandBase
{

    protected override string Response => "All non-staff have been intercom-muted.";
    protected override void Execute(Player player) => player.IntercomMute(true);

}
