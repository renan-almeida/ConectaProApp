using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConectaProApp.Converters
{
    public class DateTimeNullableFromStringConverter : JsonConverter<DateTime?>
    {
        private readonly string _format;

        public DateTimeNullableFromStringConverter(string format)
        {
            _format = format;
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();

            if (string.IsNullOrWhiteSpace(str))
                return null;

            if (DateTime.TryParseExact(str, _format, new CultureInfo("pt-BR"), DateTimeStyles.None, out var result))
                return result;

            throw new JsonException($"Data inválida: {str}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString(_format));
            else
                writer.WriteNullValue();
        }
    }

}
