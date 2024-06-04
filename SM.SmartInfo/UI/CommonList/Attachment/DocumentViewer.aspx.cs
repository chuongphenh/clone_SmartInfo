using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.UI.CommonList.Attachment
{
    public partial class DocumentViewer : SoftMart.Core.Security.UnsecuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewDocument();
            }
        }

        private void ViewDocument()
        {
            string ticket = Request.Params[SMX.Parameter.ID];
            int? empID = null;
            int? attID = null;
            Utils.Utility.Decrypt(ticket, out empID, out attID);

            if (empID == Profiles.MyProfile.EmployeeID && attID.HasValue)
            {
                AttachmentParam param = new AttachmentParam(FunctionType.CommonList.Attachment.ViewDocument);
                param.adm_Attachment = new SharedComponent.Entities.adm_Attachment();
                param.adm_Attachment.AttachmentID = attID;

                MainController.Provider.Execute(param);
            }
            else
            {
                ucErr.ShowError("File không tồn tại");
            }
        }
    }
}