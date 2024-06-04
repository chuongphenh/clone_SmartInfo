using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.CacheManager;
using System;
using System.Web.UI.HtmlControls;
using System.Linq;

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
    public partial class PressAgencyHRUC : UserControl
    {

        #region Events


        protected void rptHR_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeater(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void BindData(int? pressAgencyID, int? attitude, int? pressAgencyHRID, bool? isEdit)
        {
            hidIsEdit.Value = Utility.GetString(isEdit);
            hidPressAgencyID.Value = Utility.GetString(pressAgencyID);
            hidAttitude.Value = Utility.GetString(attitude);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHR_ByPressAgencyID);
            param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID, Attitude = attitude };
            MainController.Provider.Execute(param);

            rptHR.DataSource = param.ListPressAgencyHR.Where(c => c.PressAgencyHRID != pressAgencyHRID);
            rptHR.DataBind();
        }

        #endregion

        #region Private Methods

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgencyHR item = rptItem.DataItem as agency_PressAgencyHR;

            Literal ltrFullName = rptItem.FindControl("ltrFullName") as Literal;
            ltrFullName.Text = item.FullName;

            UIUtility.SetRepeaterItemIText(rptItem, "ltrPosition", item.Position);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrAge", item.DOB.HasValue ? Utility.GetString(DateTime.Now.Year - item.DOB.Value.Year) : string.Empty);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrDOB", Utility.GetDateString(item.DOB));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrAddress", item.Address);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrMobile", item.Mobile);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrEmail", item.Email);

            Label ltrAttitude = rptItem.FindControl("ltrAttitude") as Label;
            ltrAttitude.Text = Utility.GetDictionaryValue(SMX.Attitude.dicDesc, item.Attitude);
            switch (item.Attitude)
            {
                case SMX.Attitude.TichCuc:
                    ltrAttitude.Attributes["class"] = "positive";
                    break;
                case SMX.Attitude.TieuCuc:
                    ltrAttitude.Attributes["class"] = "negative";
                    break;
                case SMX.Attitude.TrungLap:
                    ltrAttitude.Attributes["class"] = "medium";
                    break;
            }

            HtmlImage img = rptItem.FindControl("img") as HtmlImage;
            BindDataImage(img, item.Attachment);
        }

        private void BindDataImage(HtmlImage imgUI, adm_Attachment imgData)
        {
            if (imgData != null)
            {
                imgUI.Alt = imgData.Description;
                imgUI.Src = GetImageURL(imgData);
            }
            else
                imgUI.Src = SMX.DefaultImage;
        }

        private string GetImageURL(adm_Attachment image)
        {
            string url = SMX.DefaultImage;

            if (image != null && image.FileContent != null)
            {
                string imageFileName = string.Format("{0}_{1}", image.AttachmentID, image.FileName);
                string imageFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository", "ECM");
                if (!System.IO.Directory.Exists(imageFilePath))
                    System.IO.Directory.CreateDirectory(imageFilePath);
                imageFilePath = System.IO.Path.Combine(imageFilePath, imageFileName);
                if (!System.IO.File.Exists(imageFilePath))
                    System.IO.File.WriteAllBytes(imageFilePath, image.FileContent);
                url = "~/Repository/ECM/" + imageFileName;
            }

            return ResolveUrl(url);
        }

        #endregion
    }
}