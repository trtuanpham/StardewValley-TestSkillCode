using Newtonsoft.Json;

public static class JsonDataHelper
{
    public static string ToJson(this object data)
    {
        return JsonConvert.SerializeObject(data);
    }
}
