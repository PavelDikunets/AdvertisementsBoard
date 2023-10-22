using System.Reflection;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapProfiles;

/// <summary>
///     Утилиты для работы с профилями AutoMapper.
/// </summary>
public static class ProfileUtilites
{
    /// <summary>
    ///     Игнорирует все несуществующие свойства в исходном типе при отображении на целевой тип.
    /// </summary>
    /// <param name="expression">Выражение отображения, которое нужно модифицировать.</param>
    /// <typeparam name="TSource">Тип исходного объекта.</typeparam>
    /// <typeparam name="TDestination">Тип целевого объекта.</typeparam>
    /// <returns>Модифицированное выражение отображения.</returns>
    public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
        (this IMappingExpression<TSource, TDestination> expression)
    {
        var flags = BindingFlags.Public | BindingFlags.Instance;
        var sourceType = typeof(TSource);
        var destinationProperties = typeof(TDestination).GetProperties(flags);

        foreach (var property in destinationProperties)
            if (sourceType.GetProperty(property.Name, flags) == null)
                expression.ForMember(property.Name, opt => opt.Ignore());
        return expression;
    }
}