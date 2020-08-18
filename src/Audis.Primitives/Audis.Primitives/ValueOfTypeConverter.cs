using System;
using System.ComponentModel;
using System.Globalization;

namespace Audis.Primitives
{
    /// <summary>
    /// Type converter for <see cref="ValueOf{TValue, TThis}"/> types to allow seamingless serialization and deserialization.
    /// Use this type as parameter for the <see cref="TypeConverterAttribute"/> for subclasses of <see cref="ValueOf{TValue, TThis}"/>.
    /// </summary>
    public class ValueOfTypeConverter<TValue, TThis> : TypeConverter
        where TThis : ValueOf<TValue, TThis>, new()
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(TValue))
            {
                return true;
            }

            if (sourceType == typeof(DateTime) && typeof(TValue) == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is TValue tValue)
            {
                return ValueOf<TValue, TThis>.From(tValue);
            }

            if (value is DateTime date && typeof(TValue) == typeof(string) && date.ToString("o") is TValue stringValue)
            {
                return ValueOf<TValue, TThis>.From(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(TValue))
            {
                return ((TThis)value).Value;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
