using System.Reflection;
using Axwabo.CommandSystem.PropertyManager;
using Axwabo.CommandSystem.PropertyManager.Resolvers;
using ModeratorTools.Commands.Toggles;

namespace ModeratorTools;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class ToggleDescriptionResolver : Attribute, ICommandDescriptionResolver<ToggleDescriptionAttribute>
{

    public string ResolveDescription(ToggleDescriptionAttribute attribute)
    {
        var name = BaseCommandPropertyManager.CurrentProcessor?.RegisteringCommandType?.GetCustomAttribute<ToggleFeatureAttribute>()?.Name;
        return string.Format(attribute.Format, name ?? "this feature");
    }

}
