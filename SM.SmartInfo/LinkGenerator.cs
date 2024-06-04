using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.CollateralManagement.UI
{
    public class LinkGenerator
    {
        public static string GetDownloadAttachmentLink(int? attID)
        {
            return string.Format(PageURL.DownloadDocument,
                    Utility.Encrypt(SmartInfo.CacheManager.Profiles.MyProfile.EmployeeID, attID));
        }
    }
}