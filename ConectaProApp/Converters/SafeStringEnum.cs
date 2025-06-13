using Newtonsoft.Json;

public class SafeStringEnumConverter<T> : JsonConverter where T : struct
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(T) || objectType == typeof(T?);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        try
        {
            if (reader.TokenType == JsonToken.Null || reader.Value == null)
                return default(T); // ou null, se for Nullable

            var enumText = reader.Value.ToString();

            if (Enum.TryParse(enumText, true, out T result))
            {
                return result;
            }

            return default(T); // se não conseguir converter, retorna valor default (zero do enum)
        }
        catch
        {
            return default(T);
        }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value?.ToString());
    }
}
