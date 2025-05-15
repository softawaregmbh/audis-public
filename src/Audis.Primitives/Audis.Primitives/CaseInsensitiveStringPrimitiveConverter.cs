#pragma warning disable SA1649 // File name should match first type name

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Audis.Primitives;

public class CaseInsensitiveStringPrimitiveConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
        => typeToConvert.IsAssignableTo(typeof(CaseInsensitiveStringPrimitive));

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert.IsAssignableTo(typeof(CaseInsensitiveStringPrimitive)))
        {
            return (JsonConverter?)Activator.CreateInstance(
                typeof(CaseInsensitiveStringPrimitiveConverterInner<>).MakeGenericType(typeToConvert));
        }

        return null;
    }

    private class CaseInsensitiveStringPrimitiveConverterInner<TPrimitive> : JsonConverter<TPrimitive>
        where TPrimitive : CaseInsensitiveStringPrimitive
    {
        public override TPrimitive Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var constructor = typeToConvert.GetConstructor([typeof(string)]);
                if (constructor != null)
                {
                    return (TPrimitive)constructor.Invoke([reader.GetString()!]);
                }

                throw new JsonException($"No suitable constructor found for type {typeToConvert}.");
            }

            throw new JsonException($"Unexpected token type {reader.TokenType} when reading {typeToConvert.Name}.");
        }

        public override void Write(Utf8JsonWriter writer, TPrimitive value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value?.Value);
    }
}