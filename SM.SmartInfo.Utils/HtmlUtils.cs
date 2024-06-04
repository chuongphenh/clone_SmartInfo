namespace SM.SmartInfo.Utils
{
    public class HtmlUtils
    {
        public static string EncodeHtml(string value)
        {
            return System.Web.HttpUtility.HtmlEncode(value);
        }

        public static string DecodeHtml(string value)
        {
            return System.Web.HttpUtility.HtmlDecode(value);
        }

        public static string UrlEncode(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            return System.Web.HttpUtility.UrlEncode(url);
        }

        public static string UrlDecode(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            return System.Web.HttpUtility.UrlDecode(url);
        }
    }
}