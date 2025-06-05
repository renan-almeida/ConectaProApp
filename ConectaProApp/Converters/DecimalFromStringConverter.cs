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
    public class DecimalFromStringConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (decimal.TryParse(str, NumberStyles.Any, new CultureInfo("pt-BR"), out var value))
                return value;

            throw new JsonException($"Valor decimal inválido: {str}");
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("N2", new CultureInfo("pt-BR")));
        }
    }
}
