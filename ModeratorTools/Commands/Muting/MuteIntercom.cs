namespace ModeratorTools.Commands.Muting;

[CommandProperties("intercom", "Temporarily intercom-mutes every non-staff", "icom")]
public sealed class MuteIntercom : GlobalMuteCommandBase
{

    protected override string Response => "All non-staff have been intercom-muted.";
    protected override void Execute(Player player) => player.IntercomMute(true);

}
