namespace ModeratorTools.Commands.Toggles;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ToggleDescriptionAttribute : Attribute
{

    public string Format { get; }

    public ToggleDescriptionAttribute(string format) => Format = format;

}
