namespace GraphQLEngine;

public static class IEnumerableExtensions
{
    public static bool NotEmpty<T>(this IEnumerable<T>? items) => items != null && items.Any();
    public static bool Empty<T>(this IEnumerable<T>? items) => !items.NotEmpty();

}