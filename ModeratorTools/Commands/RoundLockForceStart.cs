using LabApi.Features.Wrappers;
using LightContainmentZoneDecontamination;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RemoteAdmin, "roundLockForceStart", "Enables round lock, disables decontamination and starts the round", "rlfs")]
[Usage("[keepDecontamination]")]
[VanillaPermissions(PlayerPermissions.RoundEvents)]
public sealed class RoundLockForceStart : CommandBase
{

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
    {
        if (Round.IsRoundStarted)
            return "!You can only use this command before the round starts.";
        var decontDisabled = false;
        if (arguments.Count == 0 || arguments.At(0).ToLower() is not ("1" or "true" or "yes"))
        {
            Decontamination.Status = DecontaminationController.DecontaminationStatus.Disabled;
            decontDisabled = true;
        }

        Round.IsLocked = true;
        Round.Start();
        return $"Enabled round lock, {(decontDisabled ? "disabled decontamination," : "")} forced round start.";
    }

}
