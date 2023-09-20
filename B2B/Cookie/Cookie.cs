namespace B2B
{
    public class Cookie
    {
        public static string Get(HttpRequest Request, string key) => Request.Cookies[key];

        public static void Set(HttpResponse Response, string key, string value, int? expireTime) => Response.Cookies.Append(key, value, new CookieOptions()
        {
            Expires = !expireTime.HasValue ? new DateTimeOffset?((DateTimeOffset)DateTime.Now.AddMilliseconds(10.0)) : new DateTimeOffset?((DateTimeOffset)DateTime.Now.AddMinutes((double)expireTime.Value))
        });

        public static void Remove(HttpResponse Response, string key) => Response.Cookies.Delete(key);
    }
}
