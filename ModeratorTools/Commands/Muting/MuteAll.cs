namespace ModeratorTools.Commands.Muting;

[CommandProperties("all", "Temporarily mutes every non-staff", "*")]
public sealed class MuteAll : MutePlayersCommandBase
{

    protected override string Response => "All non-staff have been muted.";
    protected override void Execute(Player player) => player.Mute();

}
