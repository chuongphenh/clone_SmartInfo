using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.CommonList.Attachment
{
    public partial class Download : SoftMart.Core.Security.UnsecuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DownloadAttachment();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex.Message);
            }
        }

        private void DownloadAttachment()
        {
            string ticket = Request.Params[SMX.Parameter.ID];
            int? empID = null;
            int[] attID = null;
            Utils.Utility.Decrypt(ticket, out empID, out attID);

            if (empID != Profiles.MyProfile.EmployeeID || attID == null || attID.Length == 0)
                throw new SMXException("Không tồn tại file này");

            if (attID.Length == 1)
            {
                //Download 1
                AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.Download);
                param.adm_Attachment = new adm_Attachment() { AttachmentID = attID[0] };

                MainController.Provider.Execute(param);
            }
            else
            {
                //Download many
                AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.DownloadAll);
                param.AttachmentIDs = attID.ToList();

                MainController.Provider.Execute(param);
            }
        }
    }
}