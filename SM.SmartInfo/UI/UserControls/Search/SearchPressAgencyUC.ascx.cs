using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;

namespace SM.SmartInfo.UI.UserControls.Search
{
    public partial class SearchPressAgencyUC : UserControl
    {
        #region Events

        protected void btnNext_Click(object sender, EventArgs e)
        {
            hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) + 1);
            LoadData(Request.Params["q"], Utility.GetNullableInt(Request.Params["t"]));
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) - 1);
            LoadData(Request.Params["q"], Utility.GetNullableInt(Request.Params["t"]));
        }

        protected void rptPressAgencies_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                BindObjectToRepeaterItem(e.Item);
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            hidPage.Value = "1";
        }

        public void LoadData(string searchText, int? searchType)
        {
            CommonParam param = new CommonParam(FunctionType.Common.PressAgencySearch);
            param.SearchText = searchText;
            param.SearchType = searchType;
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetNullableInt(hidPage.Value).GetValueOrDefault(1) - 1
            };
            MainController.Provider.Execute(param);

            rptPressAgencies.DataSource = param.ListPressAgency;
            rptPressAgencies.DataBind();

            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;
        }

        #endregion

        #region Private Method

        private void BindObjectToRepeaterItem(RepeaterItem rptItem)
        {
            agency_PressAgency item = rptItem.DataItem as agency_PressAgency;

            Literal ltrName = (Literal)rptItem.FindControl("ltrName");
            ltrName.Text = item.Name;

            UIUtility.SetRepeaterItemIText(rptItem, "ltrAddress", item.Address);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrCountHR", item.CountHR);

            HtmlGenericControl divLink = rptItem.FindControl("divLink") as HtmlGenericControl;
            //divLink.Attributes.Add("onclick", "window.location='/UI/SmartInfos/PressAgencies/Default.aspx?ID=" + item.PressAgencyID + "'");

            HtmlImage img = (HtmlImage)rptItem.FindControl("img");
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