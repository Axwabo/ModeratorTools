using Axwabo.CommandSystem.Attributes.RaExt;
using Axwabo.CommandSystem.RemoteAdminExtensions;
using Axwabo.CommandSystem.RemoteAdminExtensions.Interfaces;
using Axwabo.Helpers;
using RemoteAdmin;
using RemoteAdmin.Communication;

namespace ModeratorTools.Jail;

[RemoteAdminOptionIdentifier("jailed")]
[ModeratorPermissions("jail.history", PlayerPermissions.PlayersManagement)]
[StaticOptionText("Previously Jailed", "#ff00ee")]
[OptionIcon("🔒", OverallColor = "#ff00ee", ShouldBlink = false)]
[VisibleByDefault]
public sealed class PreviouslyJailedGUI : ButtonBasedRemoteAdminOption, IOptionVisibilityController
{

    private const string Buttons = "<color=red>▼ Previous Entry</color>       <color=red>▼ Next Entry</color>             <color=red>▼ Expose Data</color>         <color=red>▼ Clear List</color>";
    private const string CopyId = "<color=green><link=CP_ID>\uf0c5</link></color>";
    private const string CopyUserId = "<color=green><link=CP_USERID>\uF0C5</link></color>";
    private const string CopyIp = "<color=green><link=CP_IP>\uF0C5</link></color>";

    private static readonly Dictionary<string, List<PreviouslyJailed>> PreviouslyJailedPlayers = new();
    private static readonly Dictionary<string, int> Indexes = new();

    private static readonly string VerticalPadding = new('\n', 17);
    private static readonly string DataPadding = new('\n', 14);
    private static readonly string NotJailedAnyoneYet = "You have not jailed anyone yet.".Color("red") + VerticalPadding + Buttons;
    private static readonly string NotSelectedPlayerYet = "You have not selected a player yet.".Color("red") + VerticalPadding + Buttons;

    protected override string OnBasicInfoClicked(PlayerCommandSender sender) => TurnPage(sender, false);

    protected override string OnRequestIPClicked(PlayerCommandSender sender) => TurnPage(sender, true);

    [ModeratorPermissions("jail.data", PlayerPermissions.PlayerSensitiveDataAccess)]
    protected override string OnRequestAuthClicked(PlayerCommandSender sender) => ExposeData(sender);

    protected override string OnExternalLookupClicked(PlayerCommandSender sender) => ClearList(sender);

    private static string ClearList(CommandSender sender)
    {
        PreviouslyJailedPlayers.Remove(sender.SenderId);
        Indexes.Remove(sender.SenderId);
        return "Cleared the list of previously jailed players.".Color("green") + VerticalPadding + Buttons;
    }

    private static string TurnPage(CommandSender sender, bool forwards)
    {
        if (!PreviouslyJailedPlayers.TryGetValue(sender.SenderId, out var list) || list.Count < 1)
            return NotJailedAnyoneYet;
        if (!Indexes.TryGetValue(sender.SenderId, out var index))
            Indexes[sender.SenderId] = index = 0;
        else if (forwards)
        {
            if (++index >= list.Count)
                index = 0;
        }
        else
        {
            if (--index < 0)
                index = list.Count - 1;
        }

        Indexes[sender.SenderId] = index;
        var entry = list[index];
        RaClipboard.Send(sender, RaClipboard.RaClipBoardType.PlayerId, entry.Nickname);
        return $"""
                #{index + 1}
                Nickname: {entry.Nickname} {CopyId}
                UserID: [REDACTED]
                IP Address: [REDACTED]
                """.Color("yellow") + DataPadding + Buttons;
    }

    private static string ExposeData(CommandSender sender)
    {
        if (!PreviouslyJailedPlayers.TryGetValue(sender.SenderId, out var list) || list.Count < 1)
            return NotJailedAnyoneYet;
        if (!Indexes.TryGetValue(sender.SenderId, out var index) || index < 0 || index >= list.Count)
            return NotSelectedPlayerYet;
        var entry = list[index];
        RaClipboard.Send(sender, RaClipboard.RaClipBoardType.PlayerId, entry.Nickname);
        RaClipboard.Send(sender, RaClipboard.RaClipBoardType.UserId, entry.UserID);
        RaClipboard.Send(sender, RaClipboard.RaClipBoardType.Ip, entry.IPAddress);
        return $"""
                #{index + 1}
                Nickname: {entry.Nickname} {CopyId}
                UserID: {entry.UserID} {CopyUserId}
                IP Address: {entry.IPAddress} {CopyIp}
                """.Color("yellow") + DataPadding + Buttons;
    }

    public static void AddPlayer(CommandSender sender, ReferenceHub target)
    {
        var list = PreviouslyJailedPlayers.GetOrAdd(sender.SenderId, () => []);
        var entry = new PreviouslyJailed(target.nicknameSync.MyNick, target.authManager.UserId, target.queryProcessor._ipAddress);
        if (list.Count == 0)
        {
            list.Add(entry);
            return;
        }

        var index = list.FindIndex(x => x.UserID == target.authManager.UserId);
        if (index >= 0)
            list[index] = entry;
        else
            list.Add(entry);
    }

    public bool IsVisibleTo(CommandSender sender) => PreviouslyJailedPlayers.TryGetValue(sender.SenderId, out var list) && list.Count != 0;

    public bool AllowInteractionsWhenHidden => true;

}
