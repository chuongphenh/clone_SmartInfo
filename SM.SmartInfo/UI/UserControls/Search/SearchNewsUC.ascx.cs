using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;

namespace SM.SmartInfo.UI.UserControls.Search
{
    public partial class SearchNewsUC : UserControl
    {
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

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                BindObjectToGridItem(e.Item);
            }
        }

        #region Public Methods

        public void SetupForm()
        {
            hidPage.Value = "1";
        }

        public void SetHeightAuto()
        {
            foreach (RepeaterItem rptItem in rptData.Items)
            {
                HtmlImage img = (HtmlImage)rptItem.FindControl("img");
                img.Attributes["class"] = "height-auto";
            }
        }

        public void LoadData(string searchText, int? searchType)
        {
            CommonParam param = new CommonParam(FunctionType.Common.NewsSearch);
            param.SearchText = searchText;
            param.SearchType = searchType;
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetNullableInt(hidPage.Value).GetValueOrDefault(1) - 1
            };
            MainController.Provider.Execute(param);

            rptData.DataSource = param.ListTinTuc;
            rptData.DataBind();

            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;
        }

        #endregion

        private void BindObjectToGridItem(RepeaterItem rptItem)
        {
            News item = rptItem.DataItem as News;

            HyperLink hypName = (HyperLink)rptItem.FindControl("hypName");

            hypName.Text = item.Name;
            hypName.NavigateUrl = string.Format("~/UI/SmartInfos/News/Display.aspx?ID={0}", item.NewsID);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrPostingFromDTG", Utils.Utility.GetDateTimeString(item.CreatedDTG, "dd/MM/yyy - HH:mm"));

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
    }
}