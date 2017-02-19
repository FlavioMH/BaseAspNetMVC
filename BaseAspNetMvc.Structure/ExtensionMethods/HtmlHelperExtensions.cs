using System.Web.Mvc;

namespace System.Web
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString MetaTagLanguage<T>(this HtmlHelper<T> html)
        {
            var currentLanguage = HttpUtility.HtmlAttributeEncode(Threading.Thread.CurrentThread.CurrentUICulture.ToString());
            return new MvcHtmlString(String.Format("<meta name=\"accept-language\" content=\"{0}\" >", currentLanguage));
        }
    }
}
