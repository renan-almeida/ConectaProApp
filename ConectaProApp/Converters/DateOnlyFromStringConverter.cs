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
    public class DateOnlyFromStringConverter : JsonConverter<DateTime>
    {
        private readonly string _format;

        public DateOnlyFromStringConverter(string format)
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (DateTime.TryParseExact(str, _format, new CultureInfo("pt-BR"), DateTimeStyles.None, out var date))
                return date.Date;

            throw new JsonException($"Data inválida: {str}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
