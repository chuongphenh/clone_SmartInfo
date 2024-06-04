using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.Service.Reporting.Engine;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    public partial class Default : BasePage
    {
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as MasterPages.Common.SmartInfo).Search += News_Search;

            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    SearchItemForView();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void News_Search(string searchText)
        {
            Response.Redirect(string.Format(PageURL.SearchPage, searchText, SMX.TypeSearch.News));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = "1";

                if (dpkPostingFromDTG.SelectedDate == null && dpkPostingToDTG.SelectedDate == null
                    && numNumberOfPublish.Value == null && string.IsNullOrWhiteSpace(ucCatalogNewsSelectorTree.SelectedValue) && string.IsNullOrWhiteSpace(txtTextSearch.Text)
                    && string.IsNullOrWhiteSpace(ddlType.SelectedValue) && string.IsNullOrWhiteSpace(ddlCampaignID.SelectedValue)
                    && string.IsNullOrWhiteSpace(ucPressAgencySelector.SelectedValue) && dpkFromPublishDTG.SelectedDate == null
                    && dpkToPublishDTG.SelectedDate == null && string.IsNullOrWhiteSpace(txtTextSearchSpecificResults.Text))
                    GetListNews();
                else
                    GetListNewByFilter();

                popSearch.Hide();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(PageURL.AddNew);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnShowPopupUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string url = string.Format("/UI/PopupPages/ListAttachments/Edit.aspx?ID={0}", Utils.Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { 0, SMX.AttachmentRefType.AllNews }));
                UIUtility.OpenPopupWindow(this.Page, url);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnShowPopupSearch_Click(object sender, EventArgs e)
        {
            try
            {
                popSearch.Show();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptDataTop_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeaterItemTop(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptDataBelow_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeaterItemBelow(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptDataFollow_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeaterItemFollow(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ucPager_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPage.Value = e.NewPageIndex.ToString();
                GetListNews();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Export(SMX.TemplateExcel.ExcelExport_ListNews, new Dictionary<string, object>());
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ucSideBarTreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                var curNode = ucSideBarTreeView.GetCurrentNode();

                var selectedYear = Utils.Utility.GetNullableInt(string.IsNullOrWhiteSpace(curNode.Parent) ? curNode.ID.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[0] : curNode.Parent);
                var category = ucSideBarTreeView.GetCurrentCategory()?.Category;

                if (selectedYear != null || (selectedYear != null && category != null))
                {
                    hidPage.Value = "1";

                    GetListNewByQuickFilter(selectedYear, category);
                }
                else
                    GetListNews();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Private Methods

        private void SetupForm()
        {
            hidPage.Value = "1";
            ucCatalogNewsSelectorTree.LoadData();
            UIUtility.BindDicToDropDownList(ddlType, SMX.News.PositiveNews.dicType);

            NewsParam param = new NewsParam(FunctionType.PositiveNews.PrepareDataCampaign);
            MainController.Provider.Execute(param);

            UIUtility.BindListToDropDownList(ddlCampaignID, param.ListCampaignNews, CampaignNews.C_CampaignNewsID, CampaignNews.C_Campaign);

            ucSideBarTreeView.SetupForm();
        }

        private void SearchItemForView()
        {
            NewsParam param = new NewsParam(FunctionType.News.GetNewsForView);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageSmallSize
            };
            MainController.Provider.Execute(param);

            //BindFirstNewsToForm(param.ListNews.FirstOrDefault());

            rptDataTop.DataSource = param.ListNews.Take(5);
            rptDataTop.DataBind();

            //rptDataBelow.DataSource = param.ListNews.Skip(5 + (int.Parse(hidPage.Value) - 1) * SMX.smx_PageSmallSize).Take(SMX.smx_PageSmallSize);
            rptDataBelow.DataSource = param.ListNews.Skip((int.Parse(hidPage.Value) - 1) * SMX.smx_PageSmallSize).Take(SMX.smx_PageSmallSize);
            rptDataBelow.DataBind();

            //Pager.BuildPager((param.ListNews.Count - 5), SMX.smx_PageSmallSize, int.Parse(hidPage.Value));  
            Pager.BuildPager((param.ListNews.Count), SMX.smx_PageSmallSize, int.Parse(hidPage.Value));
            
        }

        private void GetListNews()
        {
            NewsParam param = new NewsParam(FunctionType.News.GetNewsForView);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageSmallSize
            };
            MainController.Provider.Execute(param);

            rptDataBelow.DataSource = param.ListNews.Skip(5 + (int.Parse(hidPage.Value) - 1) * SMX.smx_PageSmallSize).Take(SMX.smx_PageSmallSize);
            rptDataBelow.DataBind();

            Pager.Visible = true;
            Pager.BuildPager(param.ListNews.Count - 5, SMX.smx_PageSmallSize, int.Parse(hidPage.Value));
        }

        private void GetListNewByFilter()
        {
            SharedComponent.Entities.News filterNews = new SharedComponent.Entities.News()
            {
                PostingFromDTG = dpkPostingFromDTG.SelectedDate,
                PostingToDTG = dpkPostingToDTG.SelectedDate,
                NumberOfPublish = (int?)numNumberOfPublish.Value,
                CatalogID = Utils.Utility.GetNullableInt(ucCatalogNewsSelectorTree.SelectedValue),
                SearchText = txtTextSearch.Text
            };

            PositiveNews filterPositiveNews = new PositiveNews()
            {
                Type = Utils.Utility.GetNullableInt(ddlType.SelectedValue),
                CampaignID = Utils.Utility.GetNullableInt(ddlCampaignID.SelectedValue),
                PressAgencryID = Utils.Utility.GetNullableInt(ucPressAgencySelector.SelectedValue),
                FromPublishDTG = dpkFromPublishDTG.SelectedDate,
                ToPublishDTG = dpkToPublishDTG.SelectedDate,
                SearchText = txtTextSearchSpecificResults.Text
            };

            NewsParam param = new NewsParam(FunctionType.News.SearchNewsForView);
            param.News = filterNews;
            param.PositiveNews = filterPositiveNews;
            MainController.Provider.Execute(param);

            rptDataBelow.DataSource = param.ListNews;
            rptDataBelow.DataBind();

            Pager.Visible = false;
        }

        private void GetListNewByQuickFilter(int? selectedYear, int? category)
        {
            SharedComponent.Entities.News filterNews = new SharedComponent.Entities.News()
            {
                YearCreated = selectedYear,
                Category = category
            };

            NewsParam param = new NewsParam(FunctionType.News.SearchNewsForView);
            param.QuickFilterNews = filterNews;
            MainController.Provider.Execute(param);

            rptDataBelow.DataSource = param.ListNews;
            rptDataBelow.DataBind();

            Pager.Visible = false;
        }

        // private void BindFirstNewsToForm(SharedComponent.Entities.News news)
        // {
        //     ltrPostingFromDTG.Text = Utils.Utility.GetDateTimeString(news.CreatedDTG, "HH:mm. dd/MM/yyyy");
        //
        //     hplName.Text = news.Name;
        //     hplName.NavigateUrl = ResolveUrl(string.Format(PageURL.Display, news.NewsID));
        //
        //     BindDataImage(img, news.Attachment);
        // }

        private void BindObjectToRepeaterItemTop(RepeaterItem rptItem)
        {
            SharedComponent.Entities.News item = rptItem.DataItem as SharedComponent.Entities.News;

            HyperLink hplName = (HyperLink)rptItem.FindControl("hplName");
            Literal ltrPostingFromDTG = (Literal)rptItem.FindControl("ltrPostingFromDTG");

            ltrPostingFromDTG.Text = Utils.Utility.GetDateTimeString(item.CreatedDTG, "HH:mm. dd/MM/yyyy");

            hplName.Text = item.Name;
            hplName.NavigateUrl = ResolveUrl(string.Format(PageURL.Display, item.NewsID));

            HtmlImage imgRpt = (HtmlImage)rptItem.FindControl("imgRpt");
            BindDataImage(imgRpt, item.Attachment);
        }

        private void BindObjectToRepeaterItemBelow(RepeaterItem rptItem)
        {
            SharedComponent.Entities.News item = rptItem.DataItem as SharedComponent.Entities.News;

            HyperLink hplName = (HyperLink)rptItem.FindControl("hplName");
            HyperLink hypReadMore = (HyperLink)rptItem.FindControl("hypReadMore");
            Label ltrContent = (Label)rptItem.FindControl("ltrContent");
            Literal ltrPostingFromDTG = (Literal)rptItem.FindControl("ltrPostingFromDTG");

            ltrPostingFromDTG.Text = Utils.Utility.GetDateTimeString(item.CreatedDTG, "HH:mm. dd/MM/yyyy");
            if (item.SingleNews != null)
            {
                ltrContent.Text = UIUtility.ConvertBreakLine2Html(item.SingleNews.Summary);
            }
            else {
                ltrContent.Text = UIUtility.ConvertBreakLine2Html(" ");
            }

            hplName.Text = item.Name;
            hypReadMore.NavigateUrl = hplName.NavigateUrl = ResolveUrl(string.Format(PageURL.Display, item.NewsID));

            HtmlImage imgRpt = (HtmlImage)rptItem.FindControl("imgRpt");
            BindDataImage(imgRpt, item.Attachment);
        }

        private void BindObjectToRepeaterItemFollow(RepeaterItem rptItem)
        {
            SharedComponent.Entities.News item = rptItem.DataItem as SharedComponent.Entities.News;

            HyperLink hypLink = (HyperLink)rptItem.FindControl("hypLink");
            HyperLink hypName = (HyperLink)rptItem.FindControl("hypName");
            Literal ltrPostingFromDTG = (Literal)rptItem.FindControl("ltrPostingFromDTG");

            ltrPostingFromDTG.Text = Utils.Utility.GetDateTimeString(item.CreatedDTG, "HH:mm. dd/MM/yyyy");

            hypName.Text = item.Name;
            hypLink.NavigateUrl = hypName.NavigateUrl = ResolveUrl(string.Format(PageURL.Display, item.NewsID));

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
                url = ResolveUrl("/Repository/ECM/" + imageFileName);
            }

            return ResolveUrl(url);
        }

        private void Export(string xmlFile, Dictionary<string, object> param, string ouputFileName = "")
        {
            ReportingEngine engine = new ReportingEngine(xmlFile);
            engine.SetParameters(param);
            var fileContent = engine.Render();

            if (string.IsNullOrWhiteSpace(ouputFileName))
                ouputFileName = engine.SaveAsFileName + ".xlsx";

            UIUtilities.ExportHelper.PushToDownload(fileContent, SoftMart.Core.Utilities.DownloadHelper.CONTENT_TYPE_XLSX, ouputFileName);
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this                  , FunctionCode.VIEW },
                    { btnShowPopupUpload    , FunctionCode.ADD },
                    { btnAddNew             , FunctionCode.ADD },
                };
            }
        }
    }
}