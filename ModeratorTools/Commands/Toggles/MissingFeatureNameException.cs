namespace ModeratorTools.Commands.Toggles;

public sealed class MissingFeatureNameException : Exception
{

    public MissingFeatureNameException(Type type) : base($"Feature name on type \"{type}\" is not specified using a TogglesFeatureAttribute.")
    {
    }

}
