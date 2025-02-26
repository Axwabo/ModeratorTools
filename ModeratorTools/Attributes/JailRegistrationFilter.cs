using Axwabo.CommandSystem.Registration;

namespace ModeratorTools.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class JailRegistrationFilter : Attribute, IRegistrationFilter
{

    public bool AllowRegistration => ModeratorToolsPlugin.Cfg?.Jail.Enabled ?? true;

}
