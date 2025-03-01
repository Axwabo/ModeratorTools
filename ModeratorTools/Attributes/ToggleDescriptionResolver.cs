using System.Reflection;
using Axwabo.CommandSystem.PropertyManager;
using Axwabo.CommandSystem.PropertyManager.Resolvers;

namespace ModeratorTools.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class ToggleDescriptionResolver : Attribute, ICommandDescriptionResolver<ToggleDescriptionAttribute>
{

    public string ResolveDescription(ToggleDescriptionAttribute attribute)
    {
        var name = BaseCommandPropertyManager.CurrentProcessor?.RegisteringCommandType?.GetCustomAttribute<TogglesFeatureAttribute>()?.Name;
        return string.Format(attribute.Format, name ?? "this feature");
    }

}
