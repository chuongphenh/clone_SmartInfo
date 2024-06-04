
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.EntityInfos;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;

using SoftMart.Kernel.Exceptions;
using SoftMart.Kernel.Entity;
using System.IO;
using SM.SmartInfo.Service.Reporting.Engine;
using System.Data;
using System.Globalization;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI.Administrations.Users
{
    public partial class Default : BasePage, ISMFormDefault<Employee>
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                    SearchItemsForView();
                }
                Page.Form.DefaultButton = btnSearch.UniqueID;
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteItems();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void grdMain_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                BindObjectToGridItem(e.Item);
            }
        }

        protected void grdMain_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                grdMain.CurrentPageIndex = e.NewPageIndex;
                SearchItemsForView();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void grdMain_ItemCommand(object sender, DataGridCommandEventArgs e)
        {
            try
            {
                //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                //{
                //    Employee item = BindGridItemToObject(e.Item);
                //    switch (e.CommandName)
                //    {
                //        case SMX.ActionEdit:
                //        default:
                //            break;
                //    }
                //}
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
                grdMain.CurrentPageIndex = 0;
                SearchItemsForView();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            // xuat excel cac thong tin sau:
            //  Tên đăng nhập: UserName, Tên đầy đủ: Name, Mã nhân viên: EmployeeCode, Chuyên viên phòng, Khối quản lý, 
            //  Ngày sinh, Giới tính, Điện thoại nhà riêng, Số Mobile, Email, Trạng thái,
            //  Chức danh, Cấp bậc, Mã chi nhánh, Là quản lý, Phương thức xác thực, Vai trò (danh sách tên role cách nhau bởi dấu ;)
            try
            {
                Export(SMX.TemplateExcel.ExcelExport_ListEmployeeInfo, new Dictionary<string, object>());
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


        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                popupUploadFile.Show();
            }
            catch (SMXException ex)
            {
                base.ShowError(ex);
            }
        }

        // đọc dữ liệu import 
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
                //SoftMart.Service.BatchProcessing.BatchProcessingApi.AddImporting(
                //    fuImportExcel.FileBytes, fuImportExcel.FileName, SMX.ImportingType.ImportUserInformation);
                List<Employee> ListEmployee = new List<Employee>();

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
                            groupNames[i] = row[i + 16].ToString().Trim();
                        }
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
                    var UserName = row[1].ToString().Trim();
                    var Name = row[2].ToString().Trim();
                    var EmployeeCode = row[3].ToString().Trim();
                    var OrganizationName = row[4].ToString().Trim();
                    var SectorName = row[5].ToString().Trim();
                    var DOB = row[6].ToString().Trim();
                    DOB = DOB.Split(' ')[0];
                    var GenderName = string.IsNullOrEmpty(row[7]?.ToString().Trim())
                                    ? (int?)null
                                    : (row[7].ToString().Trim().Equals("Nam") ? 0 : 1);

                    var Phone = row[8].ToString().Trim();
                    var Mobile = row[9].ToString().Trim();
                    var Email = row[10].ToString().Trim();
                    var StatusName = row[11].ToString().Trim().Equals("Không sử dụng") ? 2 : 1;
                    var Description = row[12].ToString().Trim();
                    var LevelName = row[13].ToString().Trim();
                    var ListBranchCode = row[14].ToString().Trim();
                    var PositionName = row[15].ToString().Trim();
                    //var Group1 = row[16].ToString().Trim();

                    List<string> namePermissionGroups = new List<string>();

                    for (int i = 0; i < 5; i++)
                    {
                        if (row[i + 16].ToString().Trim().ToLower() == "x")
                        {
                            namePermissionGroups.Add(groupNames[i]);
                        }
                    }

                    DateTime? dateOfBirth = null;
                    if (!string.IsNullOrEmpty(DOB))
                    {
                        try
                        {
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
                            LogManager.WebLogger.LogError($"ERROR: Convert ngày sinh của bản ghi UserName: {UserName}", ex);
                            //ShowError("Vui lòng nhập đủ thông tin ngày/tháng/năm sinh");
                        }
                    }

                    var employeeRecord = GetEmployeeByUserName(UserName);
                    if (employeeRecord == null)
                    {
                        ListEmployee.Add(new Employee()
                        {
                            Username = UserName,
                            Name = Name,
                            EmployeeCode = EmployeeCode,
                            OrganizationName = OrganizationName,
                            DOB = dateOfBirth,
                            Gender = GenderName,
                            Phone = Phone,
                            Mobile = Mobile,
                            Email = Email,
                            Status = StatusName,
                            Description = Description,
                            Deleted = 0,
                            Version = 1,
                            IsLocked = true,
                            IsLockByLogin = 0,
                            LoggingAttemp = 0,
                            AuthorizationType = 1,
                            CreatedBy = Profiles.MyProfile.UserName,
                            CreatedDTG = DateTime.Now,
                            NamePermissionGroups = namePermissionGroups
                        });
                    }
                    else
                    {
                        employeeRecord.Name = Name;
                        employeeRecord.EmployeeCode = EmployeeCode;
                        employeeRecord.OrganizationName = OrganizationName;
                        employeeRecord.DOB = dateOfBirth;
                        employeeRecord.Gender = GenderName;
                        employeeRecord.Phone = Phone;
                        employeeRecord.Mobile = Mobile;
                        employeeRecord.Email = Email;
                        employeeRecord.Status = StatusName;
                        employeeRecord.Description = Description;
                        employeeRecord.Version = employeeRecord.Version + 1;
                        employeeRecord.Deleted = 0;
                        employeeRecord.NamePermissionGroups = namePermissionGroups;
                        ListEmployee.Add(employeeRecord);
                    }
                }
                if (!isLeak)
                {
                    UserParam param = new UserParam(FunctionType.Administration.User.ImportOrUpdateListEmployeeFromExcel);
                    param.Employees = ListEmployee;
                    MainController.Provider.Execute(param);

                    ShowMessage("Import thành công");

                    popupUploadFile.Hide();

                    SetupForm();
                    SearchItemsForView();
                }
                else
                {
                    ShowError("Dữ liệu tải lên bị thiếu, không thể import");
                }
                //base.ShowMessage("Import thành công");

                //popupUploadFile.Hide();
            }
            catch (SMXException ex)
            {
                base.ShowError(ex);
            }
        }
        private Employee GetEmployeeByUserName(string userName)
        {
            UserParam userParam = new UserParam(FunctionType.Administration.User.GetEmployeeByUserName);
            userParam.UserName = userName;
            MainController.Provider.Execute(userParam);

            return userParam.Employee;
        }
        protected void btnImportState_Click(object sender, EventArgs e)
        {
            try
            {
                popupImportUserState.Show();
            }
            catch (SMXException ex)
            {
                base.ShowError(ex);
            }
        }
        #endregion

        #region Common
        public void SetupForm()
        {
            //1. Setup form
            hypAddNew.HRef = PageURL.AddNew;
            grdMain.PageSize = SMX.smx_PageSize;
            hplDownload.NavigateUrl = string.Format("/Templates/{0}", Utility.GetDictionaryValue(SMX.ImportingType.DicTemplate, SMX.ImportingType.ImportUserInformation));
            hplDownload1.NavigateUrl = string.Format("/Templates/{0}", Utility.GetDictionaryValue(SMX.ImportingType.DicTemplate, SMX.ImportingType.ImportUserState));

            #region 1.5 Bind to Role Dropdownlist (code change search box to dropdownlist)
            RoleParam param = new RoleParam(FunctionType.Administration.Role.GetAllRole);
            MainController.Provider.Execute(param);
            dropRole.DataTextField = "Name";
            dropRole.DataValueField = "RoleID";
            dropRole.DataSource = param.Roles;
            dropRole.DataBind();
            dropRole.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            #endregion

            //Voi danh sach don gian, dung luon GetItemsForView() de lay du lieu

            //2. Load data
            //Voi Danh sach phuc tap hon, co filter qua ComboBox -> Implement rieng de lay them thong tin cho ComboBox.
            //UserParam param = new UserParam(SharedComponent.Constants.FunctionType.Administration.User.SetupViewForm);
            //param.SearchParam = GetSearchFilter();
            //param.PagingInfo = new PagingInfo(grdMain.CurrentPageIndex, grdMain.PageSize);
            //MainController.Provider.Execute(param);

            //3. Bind data to Form
            //UIUtility.BindDataGrid(grdMain, param.EmployeeInfos, param.PagingInfo.RecordCount);
        }

        public void SearchItemsForView()
        {
            UserParam param = new UserParam(SharedComponent.Constants.FunctionType.Administration.User.GetItemsForView);
            param.SearchParam = GetSearchFilter();
            param.PagingInfo = new PagingInfo(grdMain.CurrentPageIndex, grdMain.PageSize);
            MainController.Provider.Execute(param);

            UIUtility.BindDataGrid(grdMain, param.EmployeeInfos, param.PagingInfo.RecordCount);
        }

        public void DeleteItems()
        {
            UserParam param = new UserParam(FunctionType.Administration.User.DeleteItems);
            param.Employees = GetSelectedItems();
            MainController.Provider.Execute(param);

            SearchItemsForView();
        }
        #endregion

        #region Specific
        private SearchParam GetSearchFilter()
        {
            SearchParam sp = new SearchParam();
            sp.Username = Utils.Utility.NullIfEmptyString(txtUserNameSearch.Text);

            // hungp: change to use dropdownlist
            //sp.RoleID = Utils.Utility.NullIfEmptyString(sbRole.SelectedValue);
            sp.RoleID = Utils.Utility.NullIfEmptyString(dropRole.SelectedValue);

            sp.Email = Utils.Utility.NullIfEmptyString(txtEmail.Text);

            return sp;
        }

        private List<Employee> GetSelectedItems()
        {
            List<Employee> lstSystem = new List<Employee>();

            foreach (DataGridItem gridItem in grdMain.Items)
            {
                CheckBox ckSelect = gridItem.FindControl("ckSelect") as CheckBox;
                if (ckSelect.Checked)
                {
                    Employee item = BindGridItemToObject(gridItem);
                    lstSystem.Add(item);
                }
            }

            return lstSystem;
        }

        public void BindObjectToGridItem(WebControl gridItem)
        {
            EmployeeInfo item = ((DataGridItem)gridItem).DataItem as EmployeeInfo;

            HyperLink hplEmployeeID = (HyperLink)gridItem.FindControl("hplEmployeeID");
            HyperLink hypEdit = (HyperLink)gridItem.FindControl("hypEdit");

            hplEmployeeID.Text = item.Username;
            hplEmployeeID.NavigateUrl = string.Format(PageURL.Display, item.EmployeeID);
            hypEdit.NavigateUrl = string.Format(PageURL.Edit, item.EmployeeID);

            UIUtility.SetGridItemHidden(gridItem, "hiID", item.EmployeeID);
            UIUtility.SetGridItemHidden(gridItem, "hiVersion", item.Version);
            UIUtility.SetGridItemIText(gridItem, "ltrName", item.Name);
            UIUtility.SetGridItemIText(gridItem, "ltrStatus", Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status));
            UIUtility.SetGridItemIText(gridItem, "ltrEmail", item.Email);
            UIUtility.SetGridItemIText(gridItem, "lblOrganizationName", string.IsNullOrEmpty(item.DepartmentName) ? item.OrganizationOfManagerName : item.DepartmentName);
            UIUtility.SetGridItemIText(gridItem, "lblIsOrgManager", item.IsOrgManager);
        }

        public Employee BindGridItemToObject(WebControl gridItem)
        {
            Employee item = new Employee();
            item.EmployeeID = Utility.GetNullableInt(UIUtility.GetGridItemHiddenValue(gridItem, "hiID"));
            item.Version = Utility.GetNullableInt(UIUtility.GetGridItemHiddenValue(gridItem, "hiVersion"));
            return item;
        }
        #endregion

        #region Base
        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                int editCol = grdMain.Columns.Count - 1;
                return new Dictionary<object, string>()
                {
                    { hypAddNew                 , FunctionCode.ADD      },
                    { btnDelete                 , FunctionCode.DELETE   },
                    { grdMain.Columns[editCol]  , FunctionCode.EDIT     },
                     {btnImport, PermissionManager.Shared.FunctionCode.ADD },
                };
            }
        }

        #endregion

        protected void btnUploadState_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fuImportExcel1.HasFile)
                {
                    throw new SMXException("Bạn chưa chọn tài liệu.");
                }

                bool isExcelFile = FileUtil.IsExcelFile(fuImportExcel1.FileName, fuImportExcel1.PostedFile.ContentType);
                if (!isExcelFile)
                {
                    throw new SMXException("Loại file này không được hỗ trợ. Chỉ hỗ trợ file có định dạng excel");
                }

                SoftMart.Service.BatchProcessing.BatchProcessingApi.AddImporting(
                    fuImportExcel1.FileBytes, fuImportExcel1.FileName, SMX.ImportingType.ImportUserState);

                base.ShowMessage("Import thành công");

                popupImportUserState.Hide();
            }
            catch (SMXException ex)
            {
                base.ShowError(ex);
            }
        }
    }
}