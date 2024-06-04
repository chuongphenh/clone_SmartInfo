using System;
using System.Text.RegularExpressions;
using System.Web;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class Error : SoftMart.Core.Security.UnsecuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Exception ex = HttpContext.Current.Server.GetLastError();
                string message = "Bạn không có quyền truy cập, vui lòng liên hệ với admin để biết thêm chi tiết.";
                if (ex != null)
                {
#if DEBUG
                    lblMesage.Text = ex.Message;
#else
                    string errorCode = Guid.NewGuid().ToString();
                    message = "Có lỗi xảy ra trong quá trình xử lý. Liên hệ admin để được hỗ trợ. Mã lỗi = " + errorCode;
                    lblMesage.Text = message;
                    Utils.LogManager.WebLogger.LogError("ErrorCode = " + errorCode, ex);
#endif
                    string url = Server.UrlDecode(Request.QueryString["aspxerrorpath"]);
                    hplUrl.Text = hplUrl.NavigateUrl = RemoveXSS(url);
                }
                else
                {
                    lblMesage.Text = message;
                    string url = Server.UrlDecode(Request.QueryString["Url"]);
                    hplUrl.NavigateUrl = hplUrl.Text = RemoveXSS(url);
                }
            }
            catch { }

            Context.ClearError();
        }

        private string RemoveXSS(string dirtyString)
        {
            if (string.IsNullOrWhiteSpace(dirtyString))
            {
                return dirtyString;
            }
            else
            {
                Regex rg = new Regex("<.*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string result = rg.Replace(dirtyString, string.Empty);

                rg = new Regex("javascript:", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                result = rg.Replace(result, string.Empty);

                return result;
            }
        }
    }
}