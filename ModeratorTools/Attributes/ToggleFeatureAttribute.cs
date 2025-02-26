namespace ModeratorTools.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ToggleFeatureAttribute : Attribute
{

    public string Name { get; }

    public ToggleFeatureAttribute(string name) => Name = name;

}
