using System;
using System.Globalization;
using Newtonsoft.Json;

namespace ConectaProApp.Converters
{
    public class DateOnlyFromStringNewtonsoftConverter : JsonConverter
    {
        private readonly string _format;

        public DateOnlyFromStringNewtonsoftConverter(string format)
        {
            _format = format;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateOnly);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();

            if (string.IsNullOrWhiteSpace(value))
                throw new JsonException("Data nula ou vazia");

            if (DateOnly.TryParseExact(value, _format, new CultureInfo("pt-BR"), DateTimeStyles.None, out var result))
                return result;

            throw new JsonException($"Data inválida: '{value}' (esperado: {_format})");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateOnly dateOnly)
                writer.WriteValue(dateOnly.ToString(_format, new CultureInfo("pt-BR")));
            else
                writer.WriteNull();
        }
    }
}
