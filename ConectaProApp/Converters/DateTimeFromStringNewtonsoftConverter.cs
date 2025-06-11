using System;
using System.Globalization;
using Newtonsoft.Json;

namespace ConectaProApp.Converters
{
    public class DateTimeFromStringNewtonsoftConverter : JsonConverter
    {
        private readonly string _format;

        public DateTimeFromStringNewtonsoftConverter(string format)
        {
            _format = format;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value?.ToString();

            if (string.IsNullOrWhiteSpace(value))
            {
                if (objectType == typeof(DateTime?))
                    return null;

                throw new JsonException("Valor de data vazio.");
            }

            if (DateTime.TryParseExact(value, _format, new CultureInfo("pt-BR"), DateTimeStyles.None, out var result))
                return result;

            throw new JsonException($"Data inválida: '{value}' (esperado: {_format})");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;
            writer.WriteValue(date.ToString(_format, new CultureInfo("pt-BR")));
        }
    }
}