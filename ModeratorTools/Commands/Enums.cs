using System.Reflection;
using Axwabo.Helpers.Config;
using PlayerRoles;

namespace ModeratorTools.Commands;

[CommandProperties(CommandHandlerType.RaAndServer, "enums", "Lists enums used for commands")]
public sealed class Enums : CommandBase
{

    private static readonly Dictionary<string, string[]> EnumNames = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
        .WithEnum<RoleTypeId>()
        .WithEnum<ItemType>(EnumFilters.Exclude(nameof(ItemType.None)))
        .WithEnum<RoomType>(EnumFilters.Exclude(nameof(RoomType.Unknown)).And(EnumFilters.ExcludeObsolete<RoomType>()));

    protected override CommandResult Execute(ArraySegment<string> arguments, CommandSender sender)
        => arguments.Count == 0
            ? $"Enum types available for listing:\n{string.Join(", ", EnumNames.Keys)}"
            : EnumNames.TryGetValue(arguments.At(0), out var array)
                ? $"Values for {arguments.At(0)}:\n{string.Join(", ", array)}"
                : "!Unknown enum type.";

}

file delegate IEnumerable<string> EnumFilter(IEnumerable<string> names);

file static class EnumFilters
{

    public static Dictionary<string, string[]> WithEnum<T>(this Dictionary<string, string[]> dictionary, EnumFilter filter = null) where T : struct, Enum
    {
        var names = typeof(T).Enums<T>().OrderBy(e => e).Select(e => e.ToString());
        dictionary[typeof(T).Name] = filter == null ? names.ToArray() : filter(names).ToArray();
        return dictionary;
    }

    public static EnumFilter Exclude(string name) => names => names.Where(x => x != name);

    public static EnumFilter ExcludeObsolete<T>() where T : struct, Enum
        => names => names.Where(e => e.IsNotObsolete<T>());

    public static EnumFilter And(this EnumFilter first, EnumFilter second)
        => names => second(first(names));

    private static bool IsNotObsolete<T>(this string name) where T : struct, Enum
        => typeof(T).GetField(name, BindingFlags.Public | BindingFlags.Static)?.GetCustomAttribute<ObsoleteAttribute>() == null;

}
