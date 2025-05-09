using System.Text.Json;

namespace ERP_Service.Shared.Utilities;

public static class JsonHelper
{
    public static string ConvertToJsonString(object? input)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        return JsonSerializer.Serialize(input, options);
    }
}
