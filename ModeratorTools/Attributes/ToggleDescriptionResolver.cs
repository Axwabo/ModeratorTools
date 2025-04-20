using System.Reflection;
using Axwabo.CommandSystem.PropertyManager.Resolvers;
using Axwabo.CommandSystem.Registration;

namespace ModeratorTools.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class ToggleDescriptionResolver : Attribute, ICommandDescriptionResolver<ToggleDescriptionAttribute>
{

    public string ResolveDescription(ToggleDescriptionAttribute attribute)
    {
        var name = CommandRegistrationProcessor.Current?.RegisteringCommandType?.GetCustomAttribute<TogglesFeatureAttribute>()?.Name;
        return string.Format(attribute.Format, name ?? "this feature");
    }

}
