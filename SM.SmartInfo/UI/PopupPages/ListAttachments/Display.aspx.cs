using System;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.PopupPages.ListAttachments
{
    public partial class Display : BasePagePopup
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    LoadData();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
        #endregion

        #region Biz
        private void SetupForm()
        {
            string key = UIUtility.GetParamId();
            int? empID = 0;
            int[] arrRefParam;

            Utils.Utility.Decrypt(key, out empID, out arrRefParam);

            if (empID != CacheManager.Profiles.MyProfile.EmployeeID && arrRefParam.Length != 2)
                Response.Redirect(PageURL.ErrorPage);

            hidRefID.Value = Utils.Utility.GetString(arrRefParam[0]);
            hidRefType.Value = Utils.Utility.GetString(arrRefParam[1]);
        }

        private void LoadData()
        {
            ucAttachment.BindData(Utils.Utility.GetNullableInt(hidRefID.Value), Utils.Utility.GetNullableInt(hidRefType.Value), false, false);
        }
        #endregion
    }
}