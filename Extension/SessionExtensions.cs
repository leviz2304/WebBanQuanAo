using System.Text.Json;
namespace WebBanHangLapTop.Extension
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default :
            JsonSerializer.Deserialize<T>(value);
        }
        //public static async Task SetObjectAsJsonAsync<T>(this ISession session, string key, T value)
        //{
        //    var json = JsonSerializer.Serialize(value);
        //    await session.SetStringAsync(key, json);
        //}

        //public static async Task<T> GetObjectFromJsonAsync<T>(this ISession session, string key)
        //{
        //    var json = await session.GetStringAsync(key);
        //    return json == null ? default : JsonSerializer.Deserialize<T>(json);
        //}
    }
}
