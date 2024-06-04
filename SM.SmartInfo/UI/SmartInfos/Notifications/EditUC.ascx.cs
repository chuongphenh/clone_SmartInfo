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
    public partial class EditUC : BaseUserControl
    {
        public event EventHandler RequestExit;

        public delegate void RequestPermission(RequestPermissionArgs param);

        public event RequestPermission RequestItemPermission;

        public int? NotificationID
        {
            get { return Utility.GetNullableInt(hidNotificationID.Value); }
            set { hidNotificationID.Value = Utility.GetString(value); }
        }

        public int? NotificationType
        {
            get { return Utility.GetNullableInt(hidType.Value); }
            set { hidType.Value = Utility.GetString(value); }
        }

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                NotificationParam param = new NotificationParam(FunctionType.Notification.UpdateItem);
                param.Notification = GetData();
                MainController.Provider.Execute(param);

                if (RequestExit != null)
                    RequestExit(null, null);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (RequestExit != null)
                    RequestExit(null, null);
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

                if (!param.lstRight.Exists(x => x.FunctionCode == FunctionCode.EDIT))
                    Response.Redirect(PageURL.ErrorPage);
            }
        }

        public void BindData(ntf_Notification noti)
        {
            NotificationID = noti.NotificationID;
            NotificationType = noti.Type;
            BindObject2Form(noti);

            ucComment.SetupForm();
            ucComment.LoadData(noti.NotificationID, noti.Type, true);
        }

        public ntf_Notification GetData()
        {
            ntf_Notification item = new ntf_Notification();

            item.NotificationID = NotificationID;
            item.Comment = txtComment.Text;

            return item;
        }

        #endregion

        #region Private Methods

        private void BindObject2Form(ntf_Notification noti)
        {
            if (noti != null)
            {
                var doDTG = new DateTime(DateTime.Now.Year, noti.DoDTG.Value.Month, noti.DoDTG.Value.Day);
                ltrDoDTG.Text = Utility.GetDateString(doDTG);

                ltrContent.Text = UIUtility.ConvertBreakLine2Html(noti.Content);
                ltrNote.Text = UIUtility.ConvertBreakLine2Html(noti.Note);
                txtComment.Text = noti.Comment;
            }
        }

        #endregion
    }
}