using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Planszex.Services
{
    public static class SessionService
    {
        public static void SetSession(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetSession<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            if(string.IsNullOrEmpty(value)) return JsonConvert.DeserializeObject<T>("");
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
