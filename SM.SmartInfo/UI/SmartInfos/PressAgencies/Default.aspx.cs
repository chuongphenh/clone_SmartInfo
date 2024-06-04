using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using SoftMart.Core.Security.Entity;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Service.Reporting.Engine;

namespace SM.SmartInfo.UI.SmartInfos.PressAgencies
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
            (this.Master as MasterPages.Common.SmartInfo).Search += PressAgency_Search;

            try
            {
                ucDisplay.RequestItemPermission += uc_RequestItemPermission;
                ucDisplay.RequestEdit += ucDisplay_RequestEdit;

                ucEdit.RequestSaveContinue += ucEdit_RequestSaveContinue;
                ucEdit.RequestItemPermission += uc_RequestItemPermission;
                ucEdit.RequestExit += ucEdit_RequestExit;

                if (!IsPostBack)
                {
                    SetupForm();
                    SearchItemForView();

                    var x = Request.Params["ID"];
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void PressAgency_Search(string searchText)
        {
            Response.Redirect(string.Format(PageURL.SearchPage, searchText, SMX.TypeSearch.PressAgency));
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ucDisplay.Visible = false;

                ucEdit.SetupForm();
                ucEdit.BindData(new agency_PressAgency());
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
                if (ucEdit.PressAgencyID.HasValue)
                {
                    GetListPressAgencyByFilter(null);
                    LoadDataDisplayWithID(ucEdit.PressAgencyID);

                    IDActive = ucEdit.PressAgencyID;

                    string url = ResolveUrl(string.Format("Default.aspx?ID={0}&P={1}", ucEdit.PressAgencyID, hidPage.Value));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);
                }
                else
                    Response.Redirect(PageURL.Default);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetListPressAgencyByFilter(null);
                popSearch.Hide();
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

        protected void btnSearchPressAgencyHR_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ResolveUrl("~/UI/Shared/Common/SearchPressAgencyHR.aspx"));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        private void ucEdit_RequestSaveContinue(int? pressAgencyID)
        {
            try
            {
                IDActive = pressAgencyID;

                GetListPressAgencyByFilter(null);
                LoadDataEditWidthID(pressAgencyID);

                string url = ResolveUrl(string.Format("Default.aspx?ID={0}&P={1}", pressAgencyID, hidPage.Value));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptPressAgencies_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

        protected void rptPressAgencies_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDisplay:
                        var paID = Utility.GetNullableInt(e.CommandArgument.ToString());

                        IDActive = paID;
                        LoadDataDisplayWithID(paID);

                        string url = ResolveUrl(string.Format("Default.aspx?ID={0}&P={1}", IDActive, hidPage.Value));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);

                        GetListPressAgencyByFilter(null);
                        break;
                    case SMX.ActionDelete:
                        var pressAgencyID = Utility.GetNullableInt(e.CommandArgument.ToString());

                        var param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgency);
                        param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
                        MainController.Provider.Execute(param);

                        GetListPressAgencyByFilter(null);
                        break;
                }
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
                GetListPressAgencyByFilter(null);
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
                Export(SMX.TemplateExcel.ExcelExport_ListPressAgency, new Dictionary<string, object>());
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
            var page = Request.Params["P"];
            if (string.IsNullOrWhiteSpace(page))
                hidPage.Value = "1";
            else
                hidPage.Value = page;

            UIUtility.BindDicToDropDownList(ddlAttitude, SMX.Attitude.dicDesc);

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SetupFormDefault);
            param.txtSearchUserShared = txtSearch.Text;
            MainController.Provider.Execute(param);

            rptPressAgencyType.DataSource = param.ListPressAgency.OrderBy(x => x.Type);
            rptPressAgencyType.DataBind();

            hidType.Value = Utility.GetString(SMX.PressAgencyType.All);
        }

        private void SearchItemForView()
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetItemsForView);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };
            MainController.Provider.Execute(param);

            if (param.ListPressAgency != null && param.ListPressAgency.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(Request.Params[SMX.Parameter.ID]))
                    Response.Redirect(ResolveUrl(string.Format("Default.aspx?ID={0}&P={1}", param.ListPressAgency.FirstOrDefault()?.PressAgencyID, hidPage.Value)));
                else
                    LoadDataDisplay();
            }

            rptPressAgencies.DataSource = param.ListPressAgency;
            rptPressAgencies.DataBind();

            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniTen, int.Parse(hidPage.Value), 5);
        }

        private void GetListPressAgencyByFilter(int? type)
        {
            agency_PressAgency pa = new agency_PressAgency()
            {
                FromEstablishedDTG = dpkFromEstablishedDTG.SelectedDate,
                ToEstablishedDTG = dpkToEstablishedDTG.SelectedDate,
                Type = type == null ? Utility.GetNullableInt(hidType.Value) : type,
                TextSearch = txtSearch.Text,
            };

            agency_PressAgencyHR paHR = new agency_PressAgencyHR()
            {
                Attitude = Utility.GetNullableInt(ddlAttitude.SelectedValue),
                FromDOB = dpkFromDOB.SelectedDate,
                ToDOB = dpkToDOB.SelectedDate,
                TextSearch = txtSearch.Text
            };

            agency_PressAgencyHRHistory paHRHistory = new agency_PressAgencyHRHistory()
            {
                FromMeetDTG = dpkFromPAHRHistoryMeetDTG.SelectedDate,
                ToMeetDTG = dpkToPAHRHistoryMeetDTG.SelectedDate,
                TextSearch = txtSearch.Text
            };

            agency_PressAgencyHRRelatives paHRRelatives = new agency_PressAgencyHRRelatives()
            {
                FromDOB = dpkFromPAHRRelativesDOB.SelectedDate,
                ToDOB = dpkToPAHRRelativesDOB.SelectedDate,
                TextSearch = txtSearch.Text
            };

            agency_PressAgencyHistory paHistory = new agency_PressAgencyHistory()
            {
                FromChangeDTG = dpkFromChangeDTG.SelectedDate,
                ToChangeDTG = dpkToChangeDTG.SelectedDate,
                TextSearch = txtSearch.Text
            };

            agency_PressAgencyMeeting paMeeting = new agency_PressAgencyMeeting()
            {
                FromContractDTG = dpkFromContractDTG.SelectedDate,
                ToContractDTG = dpkToContractDTG.SelectedDate,
                FromMeetDTG = dpkFromMeetDTG.SelectedDate,
                ToMeetDTG = dpkToMeetDTG.SelectedDate,
                TextSearch = txtSearch.Text
            };

            agency_RelationsPressAgency paRelations = new agency_RelationsPressAgency()
            {
                TextSearch = txtSearch.Text
            };

            adm_Attachment att = new adm_Attachment()
            {
                TextSearch = txtSearch.Text
            };

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SearchItemsForView);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };
            param.PressAgency = pa;
            param.PressAgencyHR = paHR;
            param.PressAgencyHRHistory = paHRHistory;
            param.PressAgencyHRRelatives = paHRRelatives;
            param.PressAgencyHistory = paHistory;
            param.PressAgencyMeeting = paMeeting;
            param.RelationsPressAgency = paRelations;
            param.Attachment = att;
            
            MainController.Provider.Execute(param);

            rptPressAgencies.DataSource = param.ListPressAgency;
            rptPressAgencies.DataBind();

            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniTen, int.Parse(hidPage.Value), 5);
        }

        private void LoadDataDisplay()
        {
            ucEdit.Visible = false;

            var pressAgencyID = Request.Params[SMX.Parameter.ID];
            if (!string.IsNullOrWhiteSpace(pressAgencyID))
            {
                var param = new PressAgencyParam(FunctionType.PressAgency.LoadDataDisplay);

                param.PressAgency = new agency_PressAgency() { PressAgencyID = Utility.GetNullableInt(pressAgencyID) };

                MainController.Provider.Execute(param);

                IDActive = Utility.GetNullableInt(pressAgencyID);

                ucDisplay.SetupForm();
                ucDisplay.BindData(param.PressAgency);
                ucDisplay.Visible = true;
            }
            else
                ucDisplay.Visible = false;
        }

        private void LoadDataDisplayWithID(int? pressAgencyID)
        {
            ucEdit.Visible = false;

            if (pressAgencyID != null)
            {
                var param = new PressAgencyParam(FunctionType.PressAgency.LoadDataDisplay);
                param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
                MainController.Provider.Execute(param);

                ucDisplay.SetupForm();
                ucDisplay.BindData(param.PressAgency);
                ucDisplay.Visible = true;
            }
            else
                ucDisplay.Visible = false;
        }

        private void LoadDataEditWidthID(int? pressAgencyID)
        {
            ucDisplay.Visible = false;

            if (pressAgencyID != null)
            {
                var param = new PressAgencyParam(FunctionType.PressAgency.LoadDataDisplay);
                param.PressAgency = new agency_PressAgency() { PressAgencyID = pressAgencyID };
                MainController.Provider.Execute(param);

                ucEdit.SetupForm();
                ucEdit.BindData(param.PressAgency);
                ucEdit.Visible = true;
            }
            else
                ucEdit.Visible = false;
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgency item = rptItem.DataItem as agency_PressAgency;

            Literal ltrName = (Literal)rptItem.FindControl("ltrName");
            ltrName.Text = item.Name;

            UIUtility.SetRepeaterItemIText(rptItem, "ltrAddress", item.Address);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrCountHR", item.CountHR);

            LinkButton btnViewDisplay = rptItem.FindControl("btnViewDisplay") as LinkButton;
            btnViewDisplay.CommandArgument = Utility.GetString(item.PressAgencyID);
            btnViewDisplay.CommandName = SMX.ActionDisplay;

            HtmlGenericControl divLink = rptItem.FindControl("divLink") as HtmlGenericControl;
            divLink.Attributes.Add("onclick", "clickViewDetail('" + btnViewDisplay.ClientID + "');");

            if (IDActive != null && IDActive == item.PressAgencyID)
                divLink.Attributes.Add("class", "div-active");

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
                url = ResolveUrl("~/Repository/ECM/" + imageFileName);
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
                    { this          , FunctionCode.VIEW },
                    { btnAddNew     , FunctionCode.ADD },
                };
            }
        }

        protected void rptPressAgencyType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    agency_PressAgency pa = e.Item.DataItem as agency_PressAgency;

                    AgencyType agt = e.Item.DataItem as AgencyType;

                    PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListAgencyType);
                    MainController.Provider.Execute(param);

                    LinkButton btnSwitchPressAgentType = e.Item.FindControl("btnSwitchPressAgentType") as LinkButton;
                    btnSwitchPressAgentType.CommandName = SMX.ActionDisplay;

                    var type = string.IsNullOrEmpty(Request.QueryString["AgencyType"]) ? -1 : Convert.ToInt32(Request.QueryString["AgencyType"]);

                    var typeDic = new Dictionary<int?, string>();
                    typeDic.Add(0, "Tất cả");
                    foreach (var item in Utility.CreateTypeDictionary(param))
                    {
                        typeDic.Add(item.Key, item.Value);
                    }
                    btnSwitchPressAgentType.Text = string.Format("{0} ({1})", Utility.GetDictionaryValue(typeDic, pa.Type), pa.CountByType);

                    if (!string.IsNullOrEmpty(Request.QueryString["AgencyType"]))
                    {
                        if (btnSwitchPressAgentType.Text.Equals(string.Format("{0} ({1})", Utility.GetDictionaryValue(typeDic, type), pa.CountByType)))
                        {
                            btnSwitchPressAgentType.CssClass = "title-active";

                            btnSwitchPressAgentType.Enabled = true;

                            hidType.Value = type.ToString();

                            GetListPressAgencyByFilter(type);
                        }
                    }

                    btnSwitchPressAgentType.CommandArgument = Utility.GetString(pa.Type == type ? pa.Type : pa.Type);

                    if (e.Item.ItemIndex == 0 && string.IsNullOrEmpty(Request.QueryString["AgencyType"]) || !string.IsNullOrEmpty(Request.QueryString["AgencyType"]) && Request.Params[SMX.Parameter.ID] == "0")
                    {
                        GetListPressAgencyByFilter(null);
                        btnSwitchPressAgentType.CssClass = "title-active";
                    }  
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptPressAgencyType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDisplay:
                        foreach (RepeaterItem item in rptPressAgencyType.Items)
                        {
                            LinkButton btnSwitch = item.FindControl("btnSwitchPressAgentType") as LinkButton;
                            btnSwitch.CssClass = "";
                        }

                        LinkButton btnSwitchPressAgentType = e.Item.FindControl("btnSwitchPressAgentType") as LinkButton;
                        btnSwitchPressAgentType.CssClass = "title-active";

                        hidType.Value = btnSwitchPressAgentType.CommandArgument.ToString();

                        GetListPressAgencyByFilter(null);
                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnTextSearch_Click(object sender, EventArgs e)
        {
            SetupForm();
            GetListPressAgencyByFilter(null);
        }
    }
}