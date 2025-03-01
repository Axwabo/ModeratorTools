namespace ModeratorTools.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TogglesFeatureAttribute : Attribute
{

    public string Name { get; }

    public TogglesFeatureAttribute(string name) => Name = name;

}
