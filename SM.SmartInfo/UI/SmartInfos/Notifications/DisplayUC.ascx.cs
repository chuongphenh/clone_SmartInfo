using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using static SM.SmartInfo.UI.SmartInfos.Notifications.Default;

namespace SM.SmartInfo.UI.SmartInfos.Notifications
{
    public partial class DisplayUC : BaseUserControl
    {
        public event EventHandler RequestEdit;

        public delegate void RequestPermission(RequestPermissionArgs param);

        public event RequestPermission RequestItemPermission;

        #region Events

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestEdit != null)
                    RequestEdit(null, null);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                NotificationParam param = new NotificationParam(FunctionType.Notification.PushNotification);
                param.Notification = new ntf_Notification()
                {
                    Content = UIUtility.ConvertHtml2BreakLine(ltrContent.Text),
                    Comment = UIUtility.ConvertHtml2BreakLine(ltrComment.Text)
                };
                MainController.Provider.Execute(param);

                ShowMessage("Thông báo thành công");
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            if (RequestItemPermission != null)
            {
                var param = new RequestPermissionArgs();
                RequestItemPermission(param);

                if (!param.lstRight.Exists(x => x.FunctionCode == FunctionCode.DISPLAY))
                    Response.Redirect(PageURL.ErrorPage);

                btnEdit.Visible
                    = btnSendMessage.Visible
                    = param.lstRight.Exists(x => x.FunctionCode == FunctionCode.EDIT);
            }
        }

        public void BindData(ntf_Notification noti)
        {
            BindObject2Form(noti);

            ucComment.SetupForm();
            //if(noti != null) ucComment.LoadData(noti.NotificationID, SMX.CommentRefType.Notification, true, noti.Type);
            if(noti != null) ucComment.LoadData(noti.NotificationID, noti.Type, true, noti.Type);
        }

        #endregion

        #region Private Methods

        private void BindObject2Form(ntf_Notification noti)
        {
            if (noti != null && noti.NotificationID.HasValue)
            {
                var doDTG = DateTime.Now;
                if (noti.Type != SMX.Notification.CauHinhGuiThongBao.SinhNhat && noti.Type != SMX.Notification.CauHinhGuiThongBao.NgayThanhLap)
                    doDTG = new DateTime(DateTime.Now.Year, noti.DoDTG.Value.Month, noti.DoDTG.Value.Day);
                else
                    doDTG = noti.DoDTG.GetValueOrDefault(DateTime.Now);
                ltrDoDTG.Text = Utility.GetDateString(doDTG);

                ltrContent.Text = UIUtility.ConvertBreakLine2Html(noti.Content);
                ltrNote.Text = UIUtility.ConvertBreakLine2Html(noti.Note);
                ltrComment.Text = UIUtility.ConvertBreakLine2Html(noti.Comment);
            }
        }

        #endregion
    }
}