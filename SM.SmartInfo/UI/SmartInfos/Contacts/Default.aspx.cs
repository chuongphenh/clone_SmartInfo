using System;
using SM.SmartInfo.BIZ;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Linq;
using DocumentFormat.OpenXml.Drawing;
using OfficeOpenXml;
using System.IO;
using SM.SmartInfo.Service.Reporting.Engine;
using System.Data;
using System.Web;
using ZXing;
using System.Drawing;
using System.Text;
using iTextSharp.text.pdf.qrcode;
using QRCoder;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Drawing.Drawing2D;
using ZXing.Common;
using ZXing.Rendering;
using log4net.Repository.Hierarchy;
using log4net.Core;
using System.Globalization;
using SoftMart.Core.Security.Entity;
using SM.SmartInfo.SharedComponent.Params.Administration;

namespace SM.SmartInfo.UI.SmartInfos.Contacts
{
    public partial class Default : BasePage
    {
        public List<IFunctionRight> lstRight { get; set; }
        #region Events
        public event EventHandler RequestSave_PressAgency;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                uc_RequestItemPermission();
                if (!IsPostBack)
                {
                    SetupForm();
                    SearchItemForView();
                }
            }
            catch (SMXException ex)
            {
                LogManager.WebLogger.LogError("Contact Error: ", ex);
                ShowError(ex);
            }
        }
        // lấy danh danh các quyền
        private void uc_RequestItemPermission()
        {
            try
            {
                lstRight = GetPagePermission();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
        // kiểm tra quyền trước khi hiển thị button
        private void RequestButtonPermisstion()
        {
            if (lstRight == null)
            {
                Response.Redirect(PageURL.ErrorPage);
                return;
            }
            foreach (RepeaterItem rptItem in rptHR.Items)
            {
                LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;
                btnDelete.Visible = lstRight.Exists(x => x.FunctionCode == FunctionCode.DELETE);

                HyperLink lnkEdit = rptItem.FindControl("lnkEdit") as HyperLink;
                lnkEdit.Visible = lstRight.Exists(x => x.FunctionCode == FunctionCode.EDIT);

                HyperLink lnkShare = rptItem.FindControl("lnkShare") as HyperLink;
                lnkShare.Visible = lstRight.Exists(x => x.FunctionCode == FunctionCode.SHARE);
            }
        }
        //--------------------------
        protected void ucPager_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                GetItemByFilter(e.NewPageIndex);
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        #endregion

        #region Private Methods

        private void SetupForm()
        {
            hidPage.Value = "1";
            hidIsEdit.Value = Utility.GetString(true);

            hplDownload.NavigateUrl = string.Format("/Templates/{0}", Utility.GetDictionaryValue(SMX.ImportingType.DicTemplate, SMX.ImportingType.ImportUserContact));

            agency_PressAgencyHR hr = new agency_PressAgencyHR()
            {
                Mobile = txtMobile.Text,
                FullName = txtName.Text,
                TextSearch = txtSearch.Text,
                Address = txtAddress.Text,
                PressAgencyTypeString = txtType.Text,
                RelatedInformation = txtRelatedInfomation.Text
            };

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHR_ByFilterNoPaging);
            param.PressAgencyHR = hr;
            param.UserId = Profiles.MyProfile.EmployeeID;
            //param.PressAgencyType = Convert.ToInt32(Utility.GetNullableInt(hidPressAgencyType.Value));
            MainController.Provider.Execute(param);

            rptPressAgencyType.DataSource = param.ListPressAgencyHR;
            rptPressAgencyType.DataBind();
        }

        private void SearchItemForView()
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHR);
            param.UserId = Profiles.MyProfile.EmployeeID;
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniNine
            };
            MainController.Provider.Execute(param);

            rptHR.DataSource = param.ListPressAgencyHR;
            rptHR.DataBind();

            Pager.Visible = true;
            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniNine, int.Parse(hidPage.Value));
            RequestButtonPermisstion();
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {

            get
            {
                return new Dictionary<object, string>()
                {
                    { this                  , FunctionCode.VIEW },
                    { dynamicLink           , FunctionCode.ADD },
                    { btnImportListUser    , FunctionCode.ADD },

                };
            }
        }


        protected void rptHR_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    agency_PressAgencyHR item = e.Item.DataItem as agency_PressAgencyHR;

                    LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;
                    btnDelete.CommandName = SMX.ActionDelete;
                    btnDelete.CommandArgument = Utility.GetString(item.PressAgencyHRID);

                    HyperLink lnkEdit = e.Item.FindControl("lnkEdit") as HyperLink;
                    string urlEdit = UIUtility.BuildHyperlinkWithPopup("<i style=\"font-size: 16px\" class=\"fas fa-pencil-alt\"></i>",
                        string.Format("~/UI/PopupPages/PressAgencyHRs/Edit.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID,
                        new int[] { item.PressAgencyID.GetValueOrDefault(0), item.PressAgencyHRID.GetValueOrDefault(0) }),
                        btnReloadAppendix.ClientID), 1300, 700);

                    HyperLink lnkShare = e.Item.FindControl("lnkShare") as HyperLink;
                    string urlShare = UIUtility.BuildHyperlinkWithPopup("<i style=\"font-size: 16px\" class=\"fas fa-solid fa-share\"></i>",
                        string.Format("~/UI/PopupPages/PressAgencyHRs/EventSharing.aspx?ID={0}&callback={1}&HRID={2}", Utility.Encrypt(Profiles.MyProfile.EmployeeID,
                        new int[] { item.PressAgencyID.GetValueOrDefault(0), item.PressAgencyHRID.GetValueOrDefault(0) }),
                        btnReloadAppendix.ClientID, item.PressAgencyHRID.GetValueOrDefault(0)), 1000, 810);

                    lnkEdit.Text = urlEdit;

                    lnkShare.Text = urlShare;

                    lnkEdit.Visible = lnkShare.Visible = btnDelete.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);

                    var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/PressAgencyHRs/Display.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { item.PressAgencyID.GetValueOrDefault(0), item.PressAgencyHRID.GetValueOrDefault(0) }), btnReloadAppendix.ClientID), 1300, 700);
                    HtmlGenericControl divLink = e.Item.FindControl("divLink") as HtmlGenericControl;
                    divLink.Attributes.Add("onclick", url);

                    UIUtility.SetRepeaterItemIText(e.Item, "ltrPosition", item.Position);
                    UIUtility.SetRepeaterItemIText(e.Item, "ltrFullName", item.FullName);
                    UIUtility.SetRepeaterItemIText(e.Item, "ltrAge", item.DOB.HasValue ? Utility.GetString(DateTime.Now.Year - item.DOB.Value.Year) : string.Empty);
                    UIUtility.SetRepeaterItemIText(e.Item, "ltrDOB", Utility.GetDateString(item.DOB));
                    UIUtility.SetRepeaterItemIText(e.Item, "ltrEmail", item.Email);
                    UIUtility.SetRepeaterItemIText(e.Item, "ltrMobile", item.Mobile);
                    UIUtility.SetRepeaterItemIText(e.Item, "ltrAddress", item.Address);

                    Label ltrAttitude = e.Item.FindControl("ltrAttitude") as Label;
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

                    UIUtility.SetRepeaterItemIText(e.Item, "ltrPressAgencyName", item.PressAgencyName);
                    UIUtility.SetRepeaterItemIText(e.Item, "ltrPressAgencyType", item.PressAgencyTypeString);
                    UIUtility.SetRepeaterItemIText(e.Item, "ltrPermissionGroup", item.PermissionGroupName);

                    HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                    BindDataImage(img, item.Attachment);

                    Bitmap qrCodeBitmap = NewQRCodeGenerator(item);
                    HtmlImage QRCode = e.Item.FindControl("qrcode") as HtmlImage;
                    string qrCodeDataUri = BitmapToDataUri(qrCodeBitmap);
                    QRCode.Src = qrCodeDataUri;
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        private Bitmap NewQRCodeGenerator(agency_PressAgencyHR item)
        {
            PayloadGenerator.ContactData contactData = new PayloadGenerator.ContactData(PayloadGenerator.ContactData.ContactOutputType.VCard3, item.FullName, null, null, null, item.Mobile, null,
                item.Email, item.DOB, null, null, null, item.Address);

            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Images\\MBBank.jpeg"; // Your specific file name

            string filePath = System.IO.Path.Combine(projectPath, fileName);

            System.Drawing.Image logoImage = System.Drawing.Image.FromFile(filePath);

            int imageWidth = 220;
            int imageHeight = 140;

            // Create a new bitmap with the desired dimensions
            Bitmap resizedImage = new Bitmap(imageWidth, imageHeight);

            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                // Use high quality resizing
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(logoImage, 0, 0, imageWidth, imageHeight);
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(contactData, QRCodeGenerator.ECCLevel.Q);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);

            using (QRCoder.QRCode qrCodex = new QRCoder.QRCode(qrCodeData))
            {
                int dpi = 300;
                int pixelsPerModule = dpi / 25;

                Bitmap qrCodeBitmap = qrCode.GetGraphic(pixelsPerModule, Color.Black, Color.White, true);

                using (Graphics graphics = Graphics.FromImage(qrCodeBitmap))
                {
                    graphics.DrawImage(resizedImage, (qrCodeBitmap.Width - resizedImage.Width) / 2, (qrCodeBitmap.Height - resizedImage.Height) / 2, resizedImage.Width, resizedImage.Height);
                }

                return qrCodeBitmap;
            }
        }

        protected void rptHR_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDelete:
                        var pressAgencyHRID = Utility.GetNullableInt(e.CommandArgument.ToString());

                        //Xóa danh bạ
                        PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgencyHR);
                        param.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = pressAgencyHRID };
                        MainController.Provider.Execute(param);

                        //Xóa bảng agency_PressAgencyHRAlert
                        PressAgencyParam pressAgencyHrAlert = new PressAgencyParam(FunctionType.PressAgency.DeletePressAgencyHRAlertByPressAgenctyHrID);
                        pressAgencyHrAlert.PressAgencyHR = new agency_PressAgencyHR() { PressAgencyHRID = pressAgencyHRID };
                        MainController.Provider.Execute(param);

                        //Xóa notification nếu scan thì sẽ lưu theo ID của danh bạ
                        NotificationParam ntfParam = new NotificationParam(FunctionType.Notification.DeleteNotificationByPressAgencyID);
                        ntfParam.PressAgencyHRID = (int)pressAgencyHRID;
                        MainController.Provider.Execute(ntfParam);

                        hidPage.Value = "1";
                        GetItemByFilter();
                        break;
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
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

        private void GetItemByFilter(int? page = 1)
        {
            SetupForm();

            hidPage.Value = Utility.GetString(page);

            agency_PressAgencyHR hr = new agency_PressAgencyHR()
            {
                Mobile = txtMobile.Text,
                FullName = txtName.Text,
                TextSearch = txtSearch.Text,
                PressAgencyType = Utility.GetNullableInt(hidPressAgencyType.Value),
                Address = txtAddress.Text,
                PressAgencyTypeString = txtType.Text,
                RelatedInformation = txtRelatedInfomation.Text,
            };

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHR_ByFilter);
            param.UserId = Profiles.MyProfile.EmployeeID;
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniNine
            };
            param.PressAgencyHR = hr;
            MainController.Provider.Execute(param);

            /* PressAgencyParam paramType = new PressAgencyParam(FunctionType.PressAgency.GetPressAgencyTypeByID);
             paramType.PressAgencyTypeID = hr.PressAgencyType.Value;
             MainController.Provider.Execute(paramType);

             if (paramType.AgencyType != null && (String.Equals(paramType.AgencyType.Code.Trim(), "MB", StringComparison.OrdinalIgnoreCase) || String.Equals(paramType.AgencyType.Code.Trim(), "MBBank", StringComparison.OrdinalIgnoreCase)))
             {
                 var resultsCode = param.ListPressAgencyHR.OrderBy(item =>
                 {
                     if (item.Position.ToLower().Contains("chủ tịch") || item.Position.ToLower().Contains("ct"))
                     {
                         return 0;
                     }
                     return 1;
                 }).ThenBy(item => item.Position).ToList();
                 rptHR.DataSource = resultsCode;
             }
             else
             {
                 var results = param.ListPressAgencyHR.OrderBy(item => item.FullName).ToList();

             }
             */

            rptHR.DataSource = param.ListPressAgencyHR;
            rptHR.DataBind();

            Pager.Visible = true;
            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniNine, int.Parse(hidPage.Value));
            RequestButtonPermisstion();
        }

        protected void btnReloadAppendix_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = "1";
                GetItemByFilter();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetItemByFilter();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            try
            {
                popSearch.Show();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnStartAdvancedSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;

                GetItemByFilter();

                popSearch.Hide();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnCancelAdvancedSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = txtName.Text = txtMobile.Text = txtType.Text = txtAddress.Text = txtRelatedInfomation.Text = string.Empty;

                SetupForm();

                SearchItemForView();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void rptPressAgencyType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    agency_PressAgencyHR pa = e.Item.DataItem as agency_PressAgencyHR;

                    PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListAgencyType);
                    MainController.Provider.Execute(param);

                    LinkButton btnSwitchPressAgentType = e.Item.FindControl("btnSwitchPressAgentType") as LinkButton;
                    btnSwitchPressAgentType.CommandName = SMX.ActionDisplay;
                    btnSwitchPressAgentType.CommandArgument = Utility.GetString(pa.PressAgencyType);

                    var typeDic = new Dictionary<int?, string>();
                    typeDic.Add(0, "Tất cả");
                    foreach (var item in Utility.CreateTypeDictionary(param))
                    {
                        typeDic.Add(item.Key, item.Value);
                    }

                    btnSwitchPressAgentType.Text = string.Format("{0} ({1})", Utility.GetDictionaryValue(typeDic, pa.PressAgencyType), pa.CountByType);

                    if (pa.PressAgencyType == Utility.GetNullableInt(hidPressAgencyType.Value) || (string.IsNullOrWhiteSpace(hidPressAgencyType.Value) && pa.PressAgencyType == SMX.PressAgencyType.All))
                    {
                        btnSwitchPressAgentType.CssClass = "title-active";

                        var action = new HyperLink();

                        if (pa.PressAgencyType == 0)
                        {
                            action.Text = UIUtility.BuildHyperlinkWithPopup("<i class=\"fa fa-plus\" aria-hidden=\"true\" title=\"Xuất Excel\"></i><span style=\"color: #595959;font-weight:bold;font-size:14px;\">Thêm loại tổ chức</span>",
                            string.Format("~/UI/PopupPages/PressAgencyHRs/Create.aspx", Utility.Encrypt(Profiles.MyProfile.EmployeeID)), 800, 300, "typeLink");
                            //action.CssClass = "custom-organization";
                        }
                        else
                        {
                            PressAgencyParam param1 = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyByType);
                            param1.PressAgencyType = hidPressAgencyType.Value == null ? 0 : Convert.ToInt32(hidPressAgencyType.Value);
                            MainController.Provider.Execute(param1);

                            var firstPress = param1.ListPressAgency.OrderByDescending(x => x.CreatedDTG).FirstOrDefault();

                            if (firstPress != null)
                            {
                                action.Text = "<span style=\"font-size: 14px;\">Thêm nhân sự</span>";
                                action.NavigateUrl = string.Format("~/UI/SmartInfos/PressAgencies/Default.aspx?ID={0}&AgencyType={1}",
                                    firstPress.PressAgencyID, pa.PressAgencyType, btnReloadAppendix.ClientID);

                            }
                            else
                            {
                                action.Text = "<p style=\"font-size: 14px;\">Thêm nhân sự</p>";
                                action.NavigateUrl = string.Format("~/UI/SmartInfos/PressAgencies/Default.aspx?AgencyType={0}",
                                    pa.PressAgencyType, btnReloadAppendix.ClientID);
                            }
                        }
                        dynamicLink.Controls.Add(action);
                    }
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
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

                        hidPressAgencyType.Value = btnSwitchPressAgentType.CommandArgument.ToString();

                        GetItemByFilter();
                        break;
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void btnImportListUser_Click(object sender, EventArgs e)
        {
            try
            {
                popupUploadFile.Show();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        // đọc dữ liệu từ excel
        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fuImportExcel.HasFile)
                {
                    throw new SMXException("Bạn chưa chọn tài liệu.");
                }

                bool isExcelFile = FileUtil.IsExcelFile(fuImportExcel.FileName, fuImportExcel.PostedFile.ContentType);

                if (!isExcelFile)
                {
                    throw new SMXException("Loại file này không được hỗ trợ. Chỉ hỗ trợ file có định dạng excel");
                }

                System.Data.DataTable dataTable = ExcelReader.ConvertExcelFileBytesToDataTable(fuImportExcel.FileBytes);

                List<agency_PressAgencyHR> ListPressAgencyHR = new List<agency_PressAgencyHR>();

                int check = 0;
                bool isLeak = false;

                //List<Role> lstRole = GetListRole();
                //List<string> naemGroupRoles = new List<string>();
                //Dictionary<string, string> employeeGroups = new Dictionary<string, string>();

                string[] groupNames = new string[5];

                foreach (DataRow row in dataTable.Rows)
                {
                    if (check == 0)
                    {
                        if (row[0].ToString() == "STT")
                        {
                            check = 1;
                        }
                        continue;
                    }
                    if (check == 1 && row.IsNull(0))
                    {
                        check = 2;
                        for (int i = 0; i < 5; i++)
                        {
                            groupNames[i] = row[i + 14].ToString().Trim();
                        }

                        //for (int i = 0; i < lstRole.Count; i++)
                        //{
                        //    // kiểm tra xem số nhóm quyền trong template có NHỎ hơn số nhóm quyền trong db không
                        //    if (row[i + 14].ToString().Trim() == "")
                        //    {
                        //        ShowMessage("Template đã cũ, vui lòng cập nhật!");
                        //        return;
                        //    }
                        //    naemGroupRoles.Add(row[i + 14].ToString().Trim());
                        //}
                        //// kiểm tra xem số nhóm quyền trong template có LỚN hơn số nhóm quyền trong db không
                        //if (row[lstRole.Count + 14].ToString().Trim() != "")
                        //{
                        //    ShowMessage("Template đã cũ, vui lòng cập nhật!");
                        //    return;
                        //}
                        continue;
                    }

                    if (row.IsNull(0) && check == 2)
                    {
                        break;
                    }

                    if (string.IsNullOrEmpty(row[1].ToString()))
                    {
                        isLeak = true;
                        break;
                    }
                    var STT = row[0].ToString().Trim();
                    var FullName = row[1].ToString().Trim();
                    var Position = row[2].ToString().Trim();
                    var Attitude = row[3].ToString().Trim().Equals("Tích cực") ? 1 : row[3].ToString().Trim().Equals("Tiêu cực") ? 2 : 3;
                    var Age = row[4].ToString().Trim();
                    var DOB = row[5].ToString().Trim();
                    DOB = DOB.Split(' ')[0];
                    var Phone = row[6].ToString().Trim();
                    var Email = row[7].ToString().Trim();
                    var Address = row[8].ToString().Trim();
                    var Hobby = row[9].ToString().Trim();
                    var RelatedInfo = row[10].ToString().Trim();
                    var AgencyType = row[11].ToString().Trim();
                    var AgencyCode = row[12].ToString().Trim();
                    var AgencyName = row[13].ToString().Trim();

                    //
                    //string groupsWithValueX = "";
                    //string[] groupsVal = new string[10];
                    List<string> namePermissionGroups = new List<string>();

                    for (int i = 0; i < 5; i++)
                    {
                        //groupsVal[i] = row[i + 14].ToString().Trim();
                        if (row[i + 14].ToString().Trim().ToLower() == "x")
                        {
                            namePermissionGroups.Add(groupNames[i]);
                            //groupsWithValueX += groupNames[i] + ",";
                        }
                    }
                    // Thêm nhóm quyền theo HR Name
                    //employeeGroups.Add(string.Join("_", FullName, STT), groupsWithValueX);



                    //string groupsWithValueX = "";
                    //for (int i = 0; i < 10; i++)
                    //{
                    //    if (groupsVal[i].ToLower() == "x")
                    //    {
                    //        groupsWithValueX += groupsVal[i] + ", ";
                    //    }
                    //}
                    //
                    var pressAgencyType = GetPressAgencyTypeByCode(AgencyCode);

                    if (pressAgencyType == null)
                    {
                        PressAgencyParam param1 = new PressAgencyParam(FunctionType.PressAgency.AddNewAgencyType);

                        param1.AgencyType = new AgencyType()
                        {
                            TypeName = AgencyType,
                            Code = AgencyCode,
                            DateModified = DateTime.Now,
                            Creator = Profiles.MyProfile.EmployeeID.ToString(),
                            Modifier = Profiles.MyProfile.UserName.ToString()
                        };
                        param1.IsSaveComplete = true;
                        MainController.Provider.Execute(param1);
                    }

                    var PressAgency = GetPressAgencyByName(AgencyName);

                    if (PressAgency == null)
                    {
                        var agencyType = GetPressAgencyTypeByCode(AgencyCode);
                        PressAgencyParam press = new PressAgencyParam(FunctionType.PressAgency.AddNewPressAgency);
                        press.PressAgency = new agency_PressAgency()
                        {
                            Name = AgencyName,
                            Type = agencyType.Id,
                            Deleted = 0,
                            CreatedDTG = DateTime.Now,
                            CreatedBy = Profiles.MyProfile.UserName,
                            TypeName = agencyType.TypeName
                        };
                        MainController.Provider.Execute(press);
                    }

                    DateTime? dateOfBirth = null;
                    if (!string.IsNullOrEmpty(DOB))
                    {
                        try
                        {
                            //string[] formats = { "d/M/yyyy", "d/MM/yyyy", "dd/M/yyyy", "dd/MM/yyyy" };
                            string[] formats = { "M/d/yyyy", "MM/d/yyyy", "M/dd/yyyy", "MM/dd/yyyy" };
                            DateTime parsedDate;
                            if (DateTime.TryParseExact(DOB, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                            {
                                dateOfBirth = parsedDate;
                            }
                            else
                            {
                                double _time;
                                if (double.TryParse(DOB, out _time))
                                {
                                    if (_time >= DateTime.MinValue.ToOADate() && _time <= DateTime.MaxValue.ToOADate())
                                    {
                                        dateOfBirth = DateTime.FromOADate(_time);
                                    }
                                }

                                //double d = double.Parse(DOB);
                                //DateTime conv = DateTime.FromOADate(d);
                                //if (DateTime.TryParseExact(DOB, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                                //{
                                //    dateOfBirth = parsedDate;
                                //}
                            }
                        }
                        catch (SMXException ex)
                        {
                            ShowError("Vui lòng nhập đủ thông tin ngày/tháng/năm sinh");
                        }
                    }
                    //var PressAgenctyHr = GetPressAgencyHRByName(FullName);
                    var pressAgencyId = GetPressAgencyByName(AgencyName).PressAgencyID;
                    var LstPressAgenctyHr = GetListPressAgencyHRByName(FullName);
                    DateTime now = DateTime.Now;
                    if (LstPressAgenctyHr == null || !LstPressAgenctyHr.Any())
                    {
                        ListPressAgencyHR.Add(new agency_PressAgencyHR()
                        {
                            FullName = FullName,
                            Position = Position,
                            Attitude = Attitude,
                            PressAgencyID = pressAgencyId,
                            DOB = dateOfBirth,
                            Mobile = Phone,
                            Email = Email,
                            Hobby = Hobby,
                            Address = Address,
                            RelatedInformation = RelatedInfo,
                            Deleted = 0,
                            CreatedBy = Profiles.MyProfile.UserName,
                            CreatedDTG = now,
                            NamePermissionGroups = namePermissionGroups
                        });
                    }
                    else
                    {
                        bool emailExists = false;
                        foreach (var pressAgencyHr in LstPressAgenctyHr)
                        {
                            if (pressAgencyHr.Email.Trim() == Email.Trim())
                            {
                                pressAgencyHr.Position = Position;
                                pressAgencyHr.Attitude = Attitude;
                                pressAgencyHr.PressAgencyID = pressAgencyId;
                                pressAgencyHr.DOB = dateOfBirth;
                                pressAgencyHr.Mobile = Phone;
                                pressAgencyHr.Hobby = Hobby;
                                pressAgencyHr.Address = Address;
                                pressAgencyHr.RelatedInformation = RelatedInfo;
                                pressAgencyHr.Deleted = 0;
                                pressAgencyHr.UpdatedBy = Profiles.MyProfile.UserName;
                                pressAgencyHr.UpdatedDTG = now;
                                pressAgencyHr.NamePermissionGroups = namePermissionGroups;
                                ListPressAgencyHR.Add(pressAgencyHr);
                                emailExists = true;
                                break;
                            }
                        }

                        if (!emailExists)
                        {
                            ListPressAgencyHR.Add(new agency_PressAgencyHR()
                            {
                                FullName = FullName,
                                Position = Position,
                                Attitude = Attitude,
                                PressAgencyID = pressAgencyId,
                                DOB = dateOfBirth,
                                Mobile = Phone,
                                Email = Email,
                                Hobby = Hobby,
                                Address = Address,
                                RelatedInformation = RelatedInfo,
                                Deleted = 0,
                                CreatedBy = Profiles.MyProfile.UserName,
                                CreatedDTG = now,
                                NamePermissionGroups = namePermissionGroups
                            });
                        }
                    }
                }

                if (!isLeak)
                {
                    PressAgencyParam pressAgencyHRs = new PressAgencyParam(FunctionType.PressAgency.ImportOrUpdateListPressAgencyHRFromExcel);
                    pressAgencyHRs.ListPressAgencyHR = ListPressAgencyHR;
                    MainController.Provider.Execute(pressAgencyHRs);
                    //Lưu nhóm quyền cho từng HR
                    //SaveGroupRoleNameForHR(employeeGroups, pressAgencyHRs.ListPressAgencyHR);
                    //
                    ShowMessage("Import thành công");

                    popupUploadFile.Hide();

                    SetupForm();
                    SearchItemForView();
                }
                else
                {
                    ShowError("Dữ liệu tải lên bị thiếu, không thể import");
                }
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }
       
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();

                var type = Utility.GetNullableInt(hidPressAgencyType.Value).GetValueOrDefault(0);

                //if (type == 0)
                //{
                //    param.Add("optionalParam", 0);
                //}
                //else
                //{
                //    param.Add("optionalParam", type);
                //}

                param.Add("empId", Profiles.MyProfile.EmployeeID);

                Export(SMX.TemplateExcel.ExcelExport_ListPressAgencyHR, param);
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

        private agency_PressAgency GetPressAgencyByName(string pressAgencyName)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetPressAgencyByName);
            param.PressAgencyName = pressAgencyName;
            MainController.Provider.Execute(param);

            return param.PressAgency;
        }
        private agency_PressAgencyHR GetPressAgencyHRByName(string pressAgencyName)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetPressAgencyHRByName);
            param.PressAgencyHRName = pressAgencyName;
            MainController.Provider.Execute(param);

            return param.PressAgencyHR;
        }
        private List<agency_PressAgencyHR> GetListPressAgencyHRByName(string pressAgencyName)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyHRByName);
            param.PressAgencyHRName = pressAgencyName;
            MainController.Provider.Execute(param);

            return param.ListPressAgencyHR;
        }

        private AgencyType GetPressAgencyTypeByCode(string pressAgencyTypeCode)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetPressAgencyTypeByCode);
            param.PressAgencyTypeCode = pressAgencyTypeCode;
            MainController.Provider.Execute(param);

            return param.AgencyType;
        }
        private List<Role> GetListRole()
        {
            RoleParam param = new RoleParam(FunctionType.Administration.Role.GetAllRole);
            MainController.Provider.Execute(param);
            return param.Roles;
        }

        private string BitmapToDataUri(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return "data:image/png;base64," + base64String;
            }
        }
    }
}