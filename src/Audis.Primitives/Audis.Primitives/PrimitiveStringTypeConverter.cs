using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Audis.Primitives;

/// <summary>
/// Type converter for <see cref="Primitive{TValue}" /> where TValue is <see langword="string"/> to allow seamingless serialization and deserialization.
/// Use this type as parameter for the <see cref="TypeConverterAttribute" /> for subclasses of <see cref="Primitive{TValue}" />.
/// </summary>
/// <typeparam name="TPrimitive">The primitive type inherited from <see cref="Primitive{TValue}"/>.</typeparam>
public class PrimitiveStringTypeConverter<TPrimitive> : TypeConverter
    where TPrimitive : Primitive<string>
{
    private static readonly Func<string, TPrimitive> PrimitiveTypeInstanceCreator;

    static PrimitiveStringTypeConverter()
    {
        var ctor = typeof(TPrimitive).GetTypeInfo().DeclaredConstructors.First(c =>
        {
            // Each Primitive<T> has at least one constructor with one single parameter of type TValue (string).
            var parameters = c.GetParameters();
            return parameters.Length == 1 && parameters[0].ParameterType == typeof(string);
        });

        var tValueParameter = Expression.Parameter(typeof(string));
        var newExp = Expression.New(ctor, tValueParameter);
        var lambda = Expression.Lambda(typeof(Func<string, TPrimitive>), newExp, tValueParameter);
        PrimitiveTypeInstanceCreator = (Func<string, TPrimitive>)lambda.Compile();
    }

    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string) || sourceType == typeof(DateTime))
        {
            return true;
        }

        return base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc/>
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string stringValue)
        {
            return PrimitiveTypeInstanceCreator(stringValue);
        }

        if (value is DateTime date && date.ToString("o") is string dateStringValue)
        {
            return PrimitiveTypeInstanceCreator(dateStringValue);
        }

        return base.ConvertFrom(context, culture, value);
    }

    /// <inheritdoc/>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            return true;
        }

        return base.CanConvertTo(context, destinationType);
    }

    /// <inheritdoc/>
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
        Type destinationType)
    {
        if (value is Primitive<string> t && (destinationType == typeof(string) || destinationType == typeof(DateTime)))
        {
            return t.Value;
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}