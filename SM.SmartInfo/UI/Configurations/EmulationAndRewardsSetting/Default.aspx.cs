using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.Service.Reporting.Engine;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Entity;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Configurations.EmulationAndRewardsSetting
{
    public partial class Default : BasePage
    {
        protected bool IsChecked { get; set; }
        private static Dictionary<int, string> levelDic = new Dictionary<int, string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(levelDic.Count == 0)
            {
                levelDic.Add(1, "Cá nhân");
                levelDic.Add(2, "Tổ chức");
            }

            if (string.IsNullOrEmpty(hidPageAwardingCatalog.Value))
            {
                hidPageAwardingCatalog.Value = "1";
            }
            if (string.IsNullOrEmpty(hidPageAwardingPeriod.Value))
            {
                hidPageAwardingPeriod.Value = "1";
            }
            if (string.IsNullOrEmpty(hidAwardingLevel.Value))
            {
                hidAwardingLevel.Value = "1";
            }
            if (string.IsNullOrEmpty(hidPageAwardingType.Value))
            {
                hidPageAwardingType.Value = "1";
            }
            if (!IsPostBack)
            {
                //Get First Setting
                SettingParam settingParam = new SettingParam(FunctionType.Administration.Setting.GetSettingFirst);
                MainController.Provider.Execute(settingParam);
                var settingRecord = settingParam.Setting;
                CheckBox1.Checked = settingRecord.Status ?? true;

                // Lưu trạng thái của checkbox vào ViewState
                ViewState["IsChecked"] = settingRecord.Status;
            }
           
            SetUpForm();
        }
        //Start Code Update
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox checkBox = (CheckBox)sender;

                bool isChecked = (bool)ViewState["IsChecked"];

                //Get First Setting
                SettingParam settingParam = new SettingParam(FunctionType.Administration.Setting.GetSettingFirst);
                MainController.Provider.Execute(settingParam);
                var settingRecord = settingParam.Setting;

                // Cập nhật dữ liệu của SettingRecord với giá trị của checkbox
                settingRecord.Status = checkBox.Checked;

                // Cập nhật Record Setting
                var param = new SettingParam(FunctionType.Administration.Setting.UpdateDataSetting);
                param.Setting = settingRecord;
                MainController.Provider.Execute(param);
            }
        }
        //End Code Update
        private void SetUpForm()
        {
            // awarding catalog
            EmulationAndRewardParam param1 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingCatalog);
            
            param1.PagingInfo = new PagingInfo()
            {
                PageIndex = int.Parse(hidPageAwardingCatalog.Value) - 1,
                PageSize = SMX.smx_PageMiniSize
            };

            MainController.Provider.Execute(param1);

            rptAwardingCatalog.DataSource = param1.ListAwardingCatalog;
            rptAwardingCatalog.DataBind();

            Pager.Visible = true;
            Pager.BuildPager(param1.PagingInfo.RecordCount, SMX.smx_PageMiniSize, int.Parse(hidPageAwardingCatalog.Value));

            // awarding period

            EmulationAndRewardParam param2 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingPeriod);
            
            param2.PagingInfo = new PagingInfo()
            {
                PageIndex = int.Parse(hidPageAwardingPeriod.Value) - 1,
                PageSize = SMX.smx_PageMiniSize
            };

            MainController.Provider.Execute(param2);

            rptAwardingPeriod.DataSource = param2.ListAwardingPeriod;
            rptAwardingPeriod.DataBind();

            PagerUC1.Visible = true;
            PagerUC1.BuildPager(param2.PagingInfo.RecordCount, SMX.smx_PageMiniSize, int.Parse(hidPageAwardingPeriod.Value));

            // awarding level

            EmulationAndRewardParam param3 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingLevel);
            
            param3.PagingInfo = new PagingInfo()
            {
                PageIndex = int.Parse(hidAwardingLevel.Value) - 1,
                PageSize = SMX.smx_PageMiniSize
            };

            MainController.Provider.Execute(param3);

            rptAwardingLevel.DataSource = param3.ListAwardingLevel;
            rptAwardingLevel.DataBind();

            PagerUC2.Visible = true;
            PagerUC2.BuildPager(param3.PagingInfo.RecordCount, SMX.smx_PageMiniSize, int.Parse(hidAwardingLevel.Value));

            // awarding type

            EmulationAndRewardParam param4 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingType);
            
            param4.PagingInfo = new PagingInfo()
            {
                PageIndex = int.Parse(hidPageAwardingType.Value) - 1,
                PageSize = SMX.smx_PageMiniSize
            };

            MainController.Provider.Execute(param4);

            rptAwardingType.DataSource = param4.ListAwardingType;
            rptAwardingType.DataBind();

            PagerUC3.Visible = true;
            PagerUC3.BuildPager(param4.PagingInfo.RecordCount, SMX.smx_PageMiniSize, int.Parse(hidPageAwardingType.Value));

            /*EmulationAndRewardParam param5 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetListAwardingPeriodResult);
            MainController.Provider.Execute(param5);*/

        }

        protected void rptAwardingCatalog_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            AwardingCatalog item = e.Item.DataItem as AwardingCatalog;

            Literal name = e.Item.FindControl("ltrAwardingCatalogName") as Literal;
            name.Text = item.Name;

            LinkButton btnEdit = e.Item.FindControl("btnShowPopUpEditAwardingCatalog") as LinkButton;
            btnEdit.CommandName = SMX.ActionEdit;
            btnEdit.CommandArgument = item.Id.ToString();

            LinkButton btnDelete = e.Item.FindControl("btnDeleteAwardingCatalog") as LinkButton;
            btnDelete.CommandName = SMX.ActionDelete;
            btnDelete.CommandArgument = item.Id.ToString();
        }

        protected void rptAwardingPeriod_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            AwardingPeriod item = e.Item.DataItem as AwardingPeriod;

            Literal name = e.Item.FindControl("ltrAwardingPeriodName") as Literal;
            name.Text = item.Name;

            Literal time = e.Item.FindControl("ltrAwardingTime") as Literal;
            time.Text = (Convert.ToDateTime(item.AwardingTime)).ToString("dd/MM/yyyy");

            LinkButton btnEdit = e.Item.FindControl("btnShowPopupEditAwardingPeriod") as LinkButton;
            btnEdit.CommandName = SMX.ActionEdit;

            LinkButton btnDelete = e.Item.FindControl("btnDeleteAwardingPeriod") as LinkButton;
            btnDelete.CommandName = SMX.ActionDelete;

            btnEdit.CommandArgument = btnDelete.CommandArgument = item.Id.ToString();
        }

        protected void rptAwardingPeriodResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }

        protected void btnCreateAwardingCatalog_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCreateAwardingCatalog.Text))
                {
                    ShowError("Chưa nhập loại khen thưởng");
                }
                else
                {
                    EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.AddNewAwardingCatalog);
                    param.AwardingCatalog = new AwardingCatalog()
                    {
                        Name = txtCreateAwardingCatalog.Text,
                        CreatedDTG = DateTime.Now,
                        CreateUserId = Profiles.MyProfile.EmployeeID,
                        isDeleted = 0
                    };

                    MainController.Provider.Execute(param);

                    popupUpCreateAwardingCatalog.Hide();
                    
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnClosePopUpAwardingCatalog_Click(object sender, EventArgs e)
        {
            popupUpCreateAwardingCatalog.Hide();
        }

        protected void btnShowPopUpCreateAwardingCatalog_Click(object sender, EventArgs e)
        {
            txtCreateAwardingCatalog.Text = string.Empty;
            popupUpCreateAwardingCatalog.Show();
        }

        protected void btnEditAwardingCatalog_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEditAwardingCatalog.Text))
                {
                    ShowError("Chưa nhập loại khen thưởng");
                }
                else
                {
                    EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.EditAwardingCatalog);
                    param.AwardingCatalogName = txtEditAwardingCatalog.Text;
                    param.AwardingCatalogId = Utility.GetNullableInt(hidItemId.Value).GetValueOrDefault(0);
                    MainController.Provider.Execute(param);

                    popupEditAwardingCatalog.Hide();
                    txtEditAwardingCatalog.Text = string.Empty;
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnClosePopupEditAwardingCatalog_Click(object sender, EventArgs e)
        {
            popupEditAwardingCatalog.Hide();
        }

        protected void rptAwardingCatalog_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case SMX.ActionEdit:
                    {
                        try
                        {
                            hidItemId.Value = e.CommandArgument.ToString();

                            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetAwardingCatalogById);
                            param.AwardingCatalogId = Convert.ToInt32(e.CommandArgument);
                            MainController.Provider.Execute(param);

                            if (param.AwardingCatalog == null)
                            {
                                Response.Redirect(Request.RawUrl);
                            }
                            else
                            {
                                txtEditAwardingCatalog.Text = param.AwardingCatalog.Name;
                                popupEditAwardingCatalog.Show();
                            }
                        }
                        catch(SMXException ex)
                        {
                            ShowError(ex);
                        }
                        
                        break;
                    }
                case SMX.ActionDelete:
                    {
                        try
                        {
                            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.DeleteSelectedAwardingCatalog);
                            param.AwardingCatalogId = Convert.ToInt32(e.CommandArgument);
                            MainController.Provider.Execute(param);

                            ShowMessage("Xóa thành công");

                            SetUpForm();
                        }
                        catch(SMXException ex)
                        {
                            ShowError(ex);
                        }

                        break;
                    }
            }
        }

        protected void btnCreateAwardingLevel_Click(object sender, EventArgs e)
        {
            UIUtility.BindDicToDropDownList(ddlCreateAwardingLevelCategory, levelDic, true);
            ddlCreateAwardingLevelCategory.Items[0].Text = "Tất cả";
            txtAwardingLevel.Text = string.Empty;
            popupCreateAwardingLevel.Show();
        }

        protected void btnSaveAwardingLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtAwardingLevel.Text))
                {
                    ShowError("Chưa nhập đủ thông tin");
                }
                else
                {
                    EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.CreateAwardingLevel);
                    param.AwardingLevel = new AwardingLevel()
                    {
                        Level = txtAwardingLevel.Text,
                        Description = txtCreateAwardingLevelDescription.Text,
                        CreatedDTG = DateTime.Now,
                        CreateUserId = Profiles.MyProfile.EmployeeID,
                        Category = ddlCreateAwardingLevelCategory.SelectedIndex,
                        isDeleted = 0
                    };
                    MainController.Provider.Execute(param);

                    popupCreateAwardingLevel.Hide();
                    
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnClosepopupCreateAwardingLevel_Click(object sender, EventArgs e)
        {
            popupCreateAwardingLevel.Hide();
        }

        protected void rptAwardingLevel_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case SMX.ActionEdit:
                    {
                        try
                        {
                            hidItemId.Value = e.CommandArgument.ToString();

                            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetAwardingLevelById);
                            param.AwardingLevelId = Convert.ToInt32(hidItemId.Value);
                            MainController.Provider.Execute(param);

                            if (param.AwardingLevel != null)
                            {
                                txtAwardingLevelEdit.Text = param.AwardingLevel.Level;
                                txtEditAwardingLevelDescription.Text = param.AwardingLevel.Description;
                                ddlEditAwardingLevelCategory.SelectedIndex = Convert.ToInt32(param.AwardingLevel.Category) - 1;

                                UIUtility.BindDicToDropDownList(ddlEditAwardingLevelCategory, levelDic);

                                popupEditAwardingLevel.Show();
                            }
                            else
                            {
                                Response.Redirect(Request.RawUrl);
                            }
                        }
                        catch(SMXException ex)
                        {
                            ShowError(ex);
                        }
                        
                        break;
                    }
                case SMX.ActionDelete:
                    {
                        try
                        {
                            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.DeleteSelectedAwardingLevel);
                            param.AwardingLevelId = Convert.ToInt32(e.CommandArgument);
                            MainController.Provider.Execute(param);

                            SetUpForm();
                        }
                        catch(SMXException ex)
                        {
                            ShowError(ex);
                        }
                        break;
                    }
            }
        }

        protected void btnSaveAwardingTypeEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtAwardingLevelEdit.Text))
                {
                    ShowError("Chưa điền đủ thông tin");
                }
                else
                {
                    EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.EditAwardingLevel);
                    param.Level = txtAwardingLevelEdit.Text;
                    param.AwardingLevelDescription = txtEditAwardingLevelDescription.Text;
                    param.AwardingLevelId = Convert.ToInt32(hidItemId.Value);
                    param.AwardingLevelCategory = ddlEditAwardingLevelCategory.SelectedIndex;
                    MainController.Provider.Execute(param);

                    popupEditAwardingLevel.Hide();

                    Response.Redirect(Request.RawUrl);

                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnClosePopupAwardingTypeEdit_Click(object sender, EventArgs e)
        {
            popupEditAwardingLevel.Hide();
        }

        protected void btnShowPopupCreateAwardingPeriod_Click(object sender, EventArgs e)
        {
            txtCreateAwardingPeriod.Text = string.Empty;
            dpkCreateAwardingPeriod.SelectedDate = null;
            PopUpCreateAwardingPeriod.Show();
        }

        protected void btnSaveCreateAwardingPeriod_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCreateAwardingPeriod.Text) || dpkCreateAwardingPeriod.SelectedDate == null)
                {
                    ShowError("Vui lòng nhập đủ thông tin");
                }
                else
                {
                    EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.CreateAwardingPeriod);
                    param.AwardingPeriod = new AwardingPeriod()
                    {
                        Name = txtCreateAwardingPeriod.Text,
                        AwardingTime = dpkCreateAwardingPeriod.SelectedDate,
                        CreatedDTG = DateTime.Now,
                        CreateUserId = Profiles.MyProfile.EmployeeID,
                        isDeleted = 0
                    };
                    MainController.Provider.Execute(param);

                    PopUpCreateAwardingPeriod.Hide();
                    
                    Response.Redirect(Request.RawUrl);

                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnClosePopUpCreateAwardingPeriod_Click(object sender, EventArgs e)
        {
            PopUpCreateAwardingPeriod.Hide();
        }

        protected void btnClosePopupAwardingPeriodEdit_Click(object sender, EventArgs e)
        {
            PopupEditAwardingPeriod.Hide();
        }

        protected void btnSaveAwardingPeriodEdit_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtEditAwardingPeriodName.Text) || dpkEditAwardingPeriodTime.SelectedDate == null)
            {
                ShowError("Vui lòng nhập đầy đủ thông tin");
            }
            else
            {
                try
                {
                    EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.EditAwardingPeriod);
                    param.AwardingPeriodId = Convert.ToInt32(hidItemId.Value);
                    param.AwardingPeriodName = txtEditAwardingPeriodName.Text;
                    param.AwardingTime = Convert.ToDateTime(dpkEditAwardingPeriodTime.SelectedDate);

                    MainController.Provider.Execute(param);

                    PopupEditAwardingPeriod.Hide();
                    txtEditAwardingPeriodName.Text = string.Empty;
                    dpkEditAwardingPeriodTime.SelectedDate = null;
                    Response.Redirect(Request.RawUrl);
                }
                catch(SMXException ex)
                {
                    ShowError(ex);
                }
            }
            
            
        }

        protected void rptAwardingPeriod_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionEdit:
                        {
                            hidItemId.Value = e.CommandArgument.ToString();

                            EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetAwardingPeriodById);
                            param.AwardingPeriodId = Convert.ToInt32(hidItemId.Value);
                            MainController.Provider.Execute(param);

                            if (param.AwardingPeriod == null)
                            {
                                ShowError("Chưa chọn mục muốn chỉnh sửa");
                            }
                            else
                            {
                                txtEditAwardingPeriodName.Text = param.AwardingPeriod.Name;

                                dpkEditAwardingPeriodTime.SelectedDate = param.AwardingPeriod.AwardingTime;

                                PopupEditAwardingPeriod.Show();
                            }

                            break;
                        }
                    case SMX.ActionDelete:
                        {
                            try
                            {
                                EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.DeleteSelectedAwardingPeriod);
                                param.AwardingPeriodId = Convert.ToInt32(e.CommandArgument);
                                MainController.Provider.Execute(param);

                                SetUpForm();
                            }
                            catch(SMXException ex)
                            {
                                ShowError(ex);
                            }

                            break;
                        }
                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnShowPopupCreateAwardingPeriodResult_Click(object sender, EventArgs e)
        {
            popupCreateAwardingPeriodResult.Show();
        }

        protected void btnSaveCreateAwardingPeriodResult_Click(object sender, EventArgs e)
        {

        }

        protected void btnClosePopUpCreateAwardingPeriodResult_Click(object sender, EventArgs e)
        {
            popupCreateAwardingPeriodResult.Hide();
        }

        protected void btnSaveAwardingPeriodResultEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnClosePopupEditAwardingPeriodResult_Click(object sender, EventArgs e)
        {
            PopupEditAwardingPeriodResult.Hide();
        }

        protected void rptAwardingType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            AwardingType item = e.Item.DataItem as AwardingType;

            Literal name = e.Item.FindControl("ltrAwardingType") as Literal;
            name.Text = item.Name;

            LinkButton btnEdit = e.Item.FindControl("btnShowPopupEditAwardingType") as LinkButton;
            btnEdit.CommandName = SMX.ActionEdit;

            LinkButton btnDelete = e.Item.FindControl("btnDeleteAwardingType") as LinkButton;
            btnDelete.CommandName = SMX.ActionDelete;

            btnEdit.CommandArgument = btnDelete.CommandArgument = item.Id.ToString();
        }

        protected void rptAwardingType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionEdit:
                        {
                            try
                            {
                                hidItemId.Value = e.CommandArgument.ToString();

                                EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetAwardingTypeById);
                                param.AwardingTypeId = Convert.ToInt32(hidItemId.Value);
                                MainController.Provider.Execute(param);

                                txtEditAwardingType.Text = param.AwardingType.Name;
                                PopupEditAwardingType.Show();
                            }
                            catch(SMXException ex)
                            {
                                ShowError(ex);
                            }
                            break;
                        }
                    case SMX.ActionDelete:
                        {
                            try
                            {
                                EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.DeleteSelectedAwardingType);
                                param.AwardingTypeId = Convert.ToInt32(e.CommandArgument);
                                MainController.Provider.Execute(param);

                                SetUpForm();
                            }
                            catch(SMXException ex)
                            {
                                ShowError(ex);
                            }
                            break;
                        }
                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnShowPopupCreateAwardingType_Click(object sender, EventArgs e)
        {
            txtCreateAwardingType.Text = string.Empty;
            PopupCreateAwardingType.Show();
        }

        protected void btnSaveCreateAwardingType_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCreateAwardingType.Text))
                {
                    ShowError("Vui lòng nhập thông tin");
                }
                else
                {
                    EmulationAndRewardParam param4 = new EmulationAndRewardParam(FunctionType.EmulationAndReward.GetAwardingTypeCount);
                    MainController.Provider.Execute(param4);

                    EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.CreateAwardingType);
                    param.AwardingType = new AwardingType()
                    {
                        Id = param4.AwardingTypeId,
                        Name = txtCreateAwardingType.Text,
                        CreatedDTG = DateTime.Now,
                        CreateUserId = Profiles.MyProfile.EmployeeID,
                        isDeleted = 0
                    };
                    MainController.Provider.Execute(param);

                    PopupCreateAwardingType.Hide();

                    Response.Redirect(Request.RawUrl);
                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnClosePopupCreateAwardingType_Click(object sender, EventArgs e)
        {
            PopupCreateAwardingType.Hide();
        }

        protected void btnSaveEditAwardingType_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtEditAwardingType.Text))
                {
                    ShowError("Vui lòng nhập đầy đủ thông tin");
                }
                else
                {
                    EmulationAndRewardParam param = new EmulationAndRewardParam(FunctionType.EmulationAndReward.EditAwardingType);
                    param.AwardingTypeId = Convert.ToInt32(hidItemId.Value);
                    param.AwardingTypeName = txtEditAwardingType.Text;
                    MainController.Provider.Execute(param);

                    PopupEditAwardingType.Hide();
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnClosePopupEditAwardingType_Click(object sender, EventArgs e)
        {
            PopupEditAwardingType.Hide();
        }

        protected void rptAwardingLevel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            AwardingLevel item = e.Item.DataItem as AwardingLevel;

            Literal level = e.Item.FindControl("ltrAwardingLevel") as Literal;
            level.Text = item.Level;

            Literal type = e.Item.FindControl("ltrAwardingLevelDescription") as Literal;
            type.Text = item.Description;

            Literal category = e.Item.FindControl("ltrAwardingLevelCategory") as Literal;
            category.Text = item.Category == 1 ? "Cá nhân" : item.Category == 2 ? "Tổ chức" : "Tất cả";

            LinkButton btnEdit = e.Item.FindControl("btnShowPopupEditAwardingLevel") as LinkButton;
            btnEdit.CommandName = SMX.ActionEdit;

            LinkButton btnDelete = e.Item.FindControl("btnDeleteAwardingLevel") as LinkButton;
            btnDelete.CommandName = SMX.ActionDelete;

            btnEdit.CommandArgument = btnDelete.CommandArgument = item.Id.ToString();
        }

        protected void btnExportExcelAwardingCatalog_Click(object sender, EventArgs e)
        {
            try
            {
                Export(SMX.TemplateExcel.ExcelExport_ListAwardingCatalog, new Dictionary<string, object>());
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnExportExcelAwardingPeriod_Click(object sender, EventArgs e)
        {
            try
            {
                Export(SMX.TemplateExcel.ExcelExport_ListAwardingPeriod, new Dictionary<string, object>());
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnExportExcelAwardingLevel_Click(object sender, EventArgs e)
        {
            try
            {
                Export(SMX.TemplateExcel.ExcelExport_ListAwardingLevel, new Dictionary<string, object>());
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnExportExcelAwardingType_Click(object sender, EventArgs e)
        {
            try
            {
                Export(SMX.TemplateExcel.ExcelExport_ListAwardingType, new Dictionary<string, object>());
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnExportExcelAwardingPeriodResult_Click(object sender, EventArgs e)
        {
            try
            {
                Export(SMX.TemplateExcel.ExcelExport_ListAwardingCatalog, new Dictionary<string, object>());
            }
            catch (SMXException ex)
            {
                ShowError(ex);
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

        protected void Pager_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPageAwardingCatalog.Value = e.NewPageIndex.ToString();
                SetUpForm();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void PagerUC1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPageAwardingPeriod.Value = e.NewPageIndex.ToString();
                SetUpForm();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void PagerUC2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            
            try
            {
                hidAwardingLevel.Value = e.NewPageIndex.ToString();
                SetUpForm();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void PagerUC3_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPageAwardingType.Value = e.NewPageIndex.ToString();
                SetUpForm();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
    }
}