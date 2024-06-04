using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SoftMart.Core.Security.Entity;
using SM.SmartInfo.Service.Reporting.Engine;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.ComponentModel;

namespace SM.SmartInfo.UI.SmartInfos.NegativeNews
{
    public partial class Default : BasePage
    {
        public class RequestPermissionArgs : EventArgs
        {
            public List<IFunctionRight> lstRight { get; set; }
        }

        public int? IDActive
        {
            get
            {
                return (int?)ViewState["ReportDisplayName"];
            }
            set
            {
                ViewState["ReportDisplayName"] = value;
            }
        }

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as MasterPages.Common.SmartInfo).Search += NegativeNews_Search;
            
            try
            {
                ucDisplay.RequestItemPermission += uc_RequestItemPermission;
                ucDisplay.RequestFinish += ucDisplay_RequestFinish;
                ucDisplay.RequestEdit += ucDisplay_RequestEdit;

                ucEdit.RequestSaveContinue += ucEdit_RequestSaveContinue;
                ucEdit.RequestItemPermission += uc_RequestItemPermission;
                ucEdit.RequestExit += ucEdit_RequestExit;

                if (!IsPostBack)
                {
                    SetupForm();
                    GetListNegativeNew();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void NegativeNews_Search(string searchText)
        {
            Response.Redirect(ResolveUrl(string.Format(PageURL.SearchPage, searchText, SMX.TypeSearch.NegativeNews)));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetListNegativeNewByFilter();
                popSearch.Hide();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSwitchAll_Click(object sender, EventArgs e)
        {
            try
            {
                hidType.Value = string.Empty;
                hidPage.Value = "1";
                ActiveTab();

                GetListNegativeNewByFilter();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSwitchAlready_Click(object sender, EventArgs e)
        {
            try
            {
                hidType.Value = Utils.Utility.GetString(SMX.News.NegativeNews.DaPhatSinh);
                hidPage.Value = "1";
                ActiveTab();

                GetListNegativeNewByFilter();
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
                ucDisplay.Visible = false;

                ucEdit.SetupForm();
                ucEdit.BindData(new SharedComponent.Entities.News());
                ucEdit.Visible = true;
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void ucEdit_RequestExit(object sender, EventArgs e)
        {
            try
            {
                if (ucEdit.NewsID.HasValue)
                {
                    GetListNegativeNewByFilter();
                    LoadDataDisplayWithID(ucEdit.NewsID);

                    IDActive = ucEdit.NewsID;

                    string url = ResolveUrl(string.Format("Default.aspx?ID={0}&T={1}&F={2}", ucEdit.NewsID, hidType.Value, ddlFilterTime.SelectedValue));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);
                }
                else
                    Response.Redirect(ResolveUrl(PageURL.Default));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void ucDisplay_RequestEdit(object sender, EventArgs e)
        {
            try
            {
                LoadDataEditWidthID(IDActive);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void ucDisplay_RequestFinish(object sender, EventArgs e)
        {
            try
            {
                GetListNegativeNewByFilter();
                LoadDataDisplay();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSwitchNotYet_Click(object sender, EventArgs e)
        {
            try
            {
                hidType.Value = Utils.Utility.GetString(SMX.News.NegativeNews.ChuaPhatSinh);
                hidPage.Value = "1";
                ActiveTab();

                GetListNegativeNewByFilter();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void uc_RequestItemPermission(RequestPermissionArgs param)
        {
            try
            {
                param.lstRight = GetPagePermission();
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

        private void ucEdit_RequestSaveContinue(int? newsID)
        {
            try
            {
                IDActive = newsID;

                GetListNegativeNewByFilter();
                LoadDataEditWidthID(newsID);

                string url = ResolveUrl(string.Format("Default.aspx?ID={0}&T={1}&F={2}", newsID, hidType.Value, ddlFilterTime.SelectedValue));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeaterItem(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDisplay:
                        var itemID = Utility.GetNullableInt(e.CommandArgument.ToString());

                        IDActive = itemID;
                        LoadDataDisplayWithID(itemID);

                        string url = ResolveUrl(string.Format("Default.aspx?ID={0}&T={1}&F={2}", itemID, hidType.Value, ddlFilterTime.SelectedValue));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);

                        GetListNegativeNewByFilter();
                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ddlFilterTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetListNegativeNewByFilter();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnShowPopupUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string url = ResolveUrl(string.Format("/UI/PopupPages/ListAttachments/Edit.aspx?ID={0}", Utils.Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { 0, SMX.AttachmentRefType.AllNegativeNews })));
                UIUtility.OpenPopupWindow(this.Page, url);
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
                Export(SMX.TemplateExcel.ExcelExport_ListNegativeNews, new Dictionary<string, object>());
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
            var tab = Request.Params["T"];
            var page = Request.Params["P"];
            if(string.IsNullOrEmpty(page))
            {
                hidPage.Value = "1";
            }
            else
            {
                hidPage.Value = page;
            }

            if (string.IsNullOrWhiteSpace(tab))
            {
                hidType.Value = string.Empty;
            }     
            else
                hidType.Value = tab;

            ActiveTab();

            UIUtility.BindDicToDropDownList(ddlFilterTime, SMX.FilterTime.dicDesc, false);

            var filterTime = Request.Params["F"];
            if (string.IsNullOrWhiteSpace(filterTime))
                ddlFilterTime.SelectedValue = Utility.GetString(SMX.FilterTime.All);
            else
                ddlFilterTime.SelectedValue = filterTime;

            UIUtility.BindDicToDropDownList(ddlClassification, SMX.News.Classification.dicClassification);
            //UIUtility.BindDicToDropDownList(ddlStatusNegativeNews, SMX.News.Status.dicDesc);
            UIUtility.BindDicToDropDownList(ddlType, SMX.News.NegativeNews.dicType);
            UIUtility.BindDicToDropDownList(ddlStatus, SMX.News.Status.dicDesc);
        }

        private void GetListNegativeNew()
        {
            NewsParam param = new NewsParam(FunctionType.NegativeNew.GetItemsForView);
            param.TypeTime = Utility.GetNullableInt(ddlFilterTime.SelectedValue);
            param.News = new SharedComponent.Entities.News()
            {
                NegativeType = Utility.GetNullableInt(hidType.Value),
                Status = Utility.GetNullableInt(ddlStatus.SelectedValue)
            };

            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniEight,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };

            MainController.Provider.Execute(param);

            var param1 = new NewsParam(FunctionType.NegativeNew.GetAllNegativeNews);
            MainController.Provider.Execute(param1);

            btnSwitchAlready.Text += "(" + param1.ListNews.Count(x => x.NegativeType.Equals(SMX.News.NegativeNews.DaPhatSinh)) + ")";
            btnSwitchNotYet.Text += "(" + param1.ListNews.Count(x => x.NegativeType.Equals(SMX.News.NegativeNews.ChuaPhatSinh)) + ")";
            btnSwitchAll.Text += "(" + param1.ListNews.Count() + ")";

            if (param.ListNews != null && param.ListNews.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(Request.Params[SMX.Parameter.ID]))
                {
                    Response.Redirect(ResolveUrl(string.Format("~/UI/SmartInfos/NegativeNews/Default.aspx?ID={0}&T={1}&F={2}&P={3}", param.ListNews.FirstOrDefault()?.NewsID, 
                        hidType.Value, ddlFilterTime.SelectedValue, hidPage.Value)));
                }
                else
                    LoadDataDisplay();
            }

            rptData.DataSource = param.ListNews;
            rptData.DataBind();
            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniEight, int.Parse(hidPage.Value), 5);
        }

        private void GetListNegativeNewByFilter()
        {
            string negativeType;
            if (!string.IsNullOrEmpty(hidType.Value))
                negativeType = hidType.Value;
            else
                negativeType = ddlType.SelectedValue;
            SharedComponent.Entities.News filterNews = new SharedComponent.Entities.News()
            {
                FromIncurredDTG = dpkFromIncurredDTG.SelectedDate,
                ToIncurredDTG = dpkToIncurredDTG.SelectedDate,
                Classification = Utils.Utility.GetNullableInt(ddlClassification.SelectedValue),
                Status = Utils.Utility.GetNullableInt(ddlStatus.SelectedValue),
                SearchText = txtTextSearch.Text,
                //NegativeType = Utils.Utility.GetNullableInt(hidType.Value),
                NegativeType = Utils.Utility.GetNullableInt(negativeType),
            };

            SharedComponent.Entities.NegativeNews filterNegativeNews = new SharedComponent.Entities.NegativeNews()
            {
                FromIncurredDTG = null,
                ToIncurredDTG = null,
                PressAgencyID = null,
                SearchText = null,
                Status = null,
                Type = null
            };
            //SharedComponent.Entities.NegativeNews filterNegativeNews = new SharedComponent.Entities.NegativeNews()
            //{
            //    FromIncurredDTG = dpkFromCreatedDTG.SelectedDate,
            //    ToIncurredDTG = dpkToCreatedDTG.SelectedDate,
            //    PressAgencyID = Utils.Utility.GetNullableInt(ucPressAgencySelector.SelectedValue),
            //    SearchText = txtTextSearchNegativeNews.Text,
            //    Status = Utils.Utility.GetNullableInt(ddlStatusNegativeNews.SelectedValue),
            //    Type = Utils.Utility.GetNullableInt(ddlType.SelectedValue)
            //};

            NewsParam param = new NewsParam(FunctionType.NegativeNew.SearchNegativeNews);
            param.TypeTime = Utility.GetNullableInt(ddlFilterTime.SelectedValue);
            param.News = filterNews;
            param.NegativeNews = filterNegativeNews;

            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniEight,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };
            MainController.Provider.Execute(param);

            rptData.DataSource = param.ListNews;
            rptData.DataBind();

            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniEight, int.Parse(hidPage.Value), 5);
        }

        private void LoadDataDisplay()
        {
            ucEdit.Visible = false;

            var newsID = Request.Params[SMX.Parameter.ID];
            if (!string.IsNullOrWhiteSpace(newsID))
            {
                var param = new NewsParam(FunctionType.NegativeNew.LoadDataDisplay);
                param.NewsID = Utility.GetNullableInt(newsID);
                MainController.Provider.Execute(param);

                IDActive = Utility.GetNullableInt(newsID);

                ucDisplay.BindData(param.News);
                ucDisplay.Visible = true;
            }
            else
                ucDisplay.Visible = false;
        }

        private void LoadDataDisplayWithID(int? newsID)
        {
            ucEdit.Visible = false;

            if (newsID != null)
            {
                var param = new NewsParam(FunctionType.NegativeNew.LoadDataDisplay);
                param.NewsID = newsID;
                MainController.Provider.Execute(param);

                ucDisplay.BindData(param.News);
                ucDisplay.Visible = true;
            }
            else
                ucDisplay.Visible = false;
        }

        private void LoadDataEditWidthID(int? newsID)
        {
            ucDisplay.Visible = false;

            if (newsID != null)
            {
                var param = new NewsParam(FunctionType.NegativeNew.LoadDataDisplay);
                param.NewsID = newsID;
                MainController.Provider.Execute(param);

                ucEdit.SetupForm();
                ucEdit.BindData(param.News);
                ucEdit.Visible = true;
            }
            else
                ucEdit.Visible = false;
        }

        private void BindObjectToRepeaterItem(RepeaterItem rptItem)
        {
            SharedComponent.Entities.News item = rptItem.DataItem as SharedComponent.Entities.News;

            Literal ltrStatus = (Literal)rptItem.FindControl("ltrStatus");
            Label ltrNegativeType = (Label)rptItem.FindControl("ltrNegativeType");
            Literal ltrIncurredDTG = (Literal)rptItem.FindControl("ltrIncurredDTG");

            Label lblName = (Label)rptItem.FindControl("lblName");
            lblName.Text = item.Name;

            ltrIncurredDTG.Text = Utils.Utility.GetDateTimeString(item.IncurredDTG, "HH:mm - dd/MM/yyyy");
            ltrNegativeType.Text = Utils.Utility.GetDictionaryValue(SMX.News.NegativeNews.dicType, item.NegativeType);
            ltrStatus.Text = Utils.Utility.GetDictionaryValue(SMX.News.Status.dicDesc, item.Status);

            switch (item.NegativeType)
            {
                case SMX.News.NegativeNews.ChuaPhatSinh:
                    ltrNegativeType.Attributes.Add("class", "chua-phat-sinh");
                    break;
                case SMX.News.NegativeNews.DaPhatSinh:
                    ltrNegativeType.Attributes.Add("class", "da-phat-sinh");
                    break;
            }

            HtmlGenericControl iStatus = rptItem.FindControl("iStatus") as HtmlGenericControl;
            HtmlGenericControl spanStatus = rptItem.FindControl("spanStatus") as HtmlGenericControl;

            if (item.Status == SMX.News.Status.HoanThanh)
            {
                spanStatus.Attributes.Add("class", "done");
                iStatus.Attributes.Add("class", "fa fa-check fa-negative-news-done");
            }
            else
            {
                spanStatus.Attributes.Add("class", "inprogress");
                iStatus.Attributes.Add("class", "fa fa-arrow-right fa-negative-news-inprogress");
            }

            LinkButton btnViewDisplay = rptItem.FindControl("btnViewDisplay") as LinkButton;
            btnViewDisplay.CommandArgument = Utility.GetString(item.NewsID);
            btnViewDisplay.CommandName = SMX.ActionDisplay;

            HtmlGenericControl divLink = rptItem.FindControl("divLink") as HtmlGenericControl;
            divLink.Attributes.Add("onclick", "clickViewDetail('" + btnViewDisplay.ClientID + "');");

            if (IDActive != null && IDActive == item.NewsID)
                divLink.Attributes.Add("class", "div-active");
        }

        private void ActiveTab()
        {
            switch (Utils.Utility.GetNullableInt(hidType.Value))
            {
                case SMX.News.NegativeNews.ChuaPhatSinh:
                    btnSwitchAll.CssClass = "";
                    btnSwitchNotYet.CssClass = "title-active";
                    btnSwitchAlready.CssClass = "";
                    break;
                case SMX.News.NegativeNews.DaPhatSinh:
                    btnSwitchAll.CssClass = "";
                    btnSwitchNotYet.CssClass = "";
                    btnSwitchAlready.CssClass = "title-active";
                    break;
                default:
                    btnSwitchAll.CssClass = "title-active";
                    btnSwitchNotYet.CssClass = "";
                    btnSwitchAlready.CssClass = "";
                    break;
            }
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
                    { btnAddNew             , FunctionCode.ADD },
                    { btnShowPopupUpload    , FunctionCode.ADD },
                };
            }
        }

        protected void Pager_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPage.Value = e.NewPageIndex.ToString();
                GetListNegativeNewByFilter();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
    }
}