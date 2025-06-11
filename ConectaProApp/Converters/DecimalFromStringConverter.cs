using System;
using System.Globalization;
using Newtonsoft.Json;

namespace ConectaProApp.Converters
{
    public class DecimalFromStringNewtonsoftConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var valorStr = reader.Value?.ToString();

            if (string.IsNullOrWhiteSpace(valorStr))
                return 0m;

            if (decimal.TryParse(valorStr, NumberStyles.Any, new CultureInfo("pt-BR"), out var valorDecimal))
                return valorDecimal;

            throw new JsonException($"Valor decimal inválido: '{valorStr}'");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is decimal dec)
                writer.WriteValue(dec.ToString(new CultureInfo("pt-BR")));
            else
                writer.WriteNull();
        }
    }
}
