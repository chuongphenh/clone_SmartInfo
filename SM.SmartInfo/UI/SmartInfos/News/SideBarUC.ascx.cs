using System;
using System.Linq;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class SideBarUC : UserControl
    {
        #region Events

        protected void rptDataFollow_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                BindObjectToRepeaterItemFollow(e.Item);
        }

        #endregion

        #region Public Methods

        public void LoadData()
        {
            NewsParam param = new NewsParam(FunctionType.News.GetNewsForView);
            MainController.Provider.Execute(param);

            rptDataFollow.DataSource = param.ListNews.Take(3);
            rptDataFollow.DataBind();
        }

        #endregion

        #region Private Methods

        private void BindObjectToRepeaterItemFollow(RepeaterItem rptItem)
        {
            SharedComponent.Entities.News item = rptItem.DataItem as SharedComponent.Entities.News;

            HyperLink hypLink = (HyperLink)rptItem.FindControl("hypLink");
            HyperLink hypName = (HyperLink)rptItem.FindControl("hypName");
            Literal ltrPostingFromDTG = (Literal)rptItem.FindControl("ltrPostingFromDTG");

            ltrPostingFromDTG.Text = Utils.Utility.GetDateTimeString(item.CreatedDTG, "HH:mm. dd/MM/yyyy");

            hypName.Text = item.Name;
            hypLink.NavigateUrl = hypName.NavigateUrl = string.Format(PageURL.Display, item.NewsID);

            HtmlImage imgRpt = (HtmlImage)hypLink.FindControl("imgRpt");
            BindDataImage(imgRpt, item.Attachment);
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