namespace HRPortal.BLL
{
    public static class Utilities
    {
        public static string AddHttp(string url)
        {
            if (url.Length > 4 && url.Substring(0, 3) == "www")
            {
                return "http://" + url;
            }
            return url;
        }
    }
}
