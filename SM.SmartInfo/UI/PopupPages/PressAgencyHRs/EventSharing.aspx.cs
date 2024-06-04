using Org.BouncyCastle.Asn1.Ocsp;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
   
    public partial class EventSharing : BasePage
    {
        [System.Web.Services.WebMethod]
        public static List<string> Search(string searchValue)
        {
            List<string> searchResults = new List<string>();
            if (string.IsNullOrEmpty(searchValue))
            {
                searchResults.Add("Vui lòng nhập thông tin tìm kiếm");
            }
            else
            {
                UserParam user = new UserParam(FunctionType.Administration.User.SearchListUserForSharing);
                user.searchString = searchValue;
                MainController.Provider.Execute(user);
                foreach (var item in user.Employees)
                {
                    searchResults.Add(item.Email.ToString());
                }

            }
            return searchResults;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
              
                if (!IsPostBack)
                {
                    hidPressAgencyHRID.Value = Request.QueryString["HRID"];

                    PrepareItemForView();

                    // Khai báo danh sách rỗng
                    List<string> itemList = new List<string>();

                    // Đổ dữ liệu vào Dropdown List
                    ddlEmailList.DataSource = itemList;
                    ddlEmailList.DataBind();
               
                }

            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }
        [System.Web.Services.WebMethod]
        public static void PushData(List<string> selectedValues = null, string group = null, string hidPressAgencyHRID = null, Repeater dt = null)
        {
            List<string> listEmail = new List<string>();

            foreach (var item in selectedValues)
            {
                listEmail.Add(item);
            }

            UserParam listUser = new UserParam(FunctionType.Administration.User.ShareToStaff);
            listUser.listEmailForSharing = listEmail;
            listUser.PositionId = Convert.ToInt32(group);
            listUser.PressAgencyHRID = Convert.ToInt32(hidPressAgencyHRID);
            MainController.Provider.Execute(listUser);
            EventSharing eventSharingInstance = new EventSharing();
            eventSharingInstance.GetListSharedUser(Convert.ToInt32(hidPressAgencyHRID), dt);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFindShare.Text))
            {
                lbSearch.Text = "Vui lòng nhập thông tin tìm kiếm";
                lbSearch.ForeColor = System.Drawing.Color.Red;
                lbSearch.Visible = true;
            }
            else
            {
                UserParam user = new UserParam(FunctionType.Administration.User.SearchUserForSharing);
                user.searchString = txtFindShare.Text;
                MainController.Provider.Execute(user);
                if (user.Employee == null)
                {
                    lbError.Text = "Không tìm thấy kết quả";
                    lbError.ForeColor = System.Drawing.Color.Red;
                    lbError.Visible = true;
                }
                else
                {
                    txtFindShare.Text = user.Employee.Email;
                }
            }
        }
       
        protected void PrepareItemForView()
        {
            lbSearch.Visible = lbError.Visible = false;

            RoleParam param = new RoleParam(FunctionType.Administration.Role.GetItemsForView);
            MainController.Provider.Execute(param);
   
            var listItemBound = new List<Role>();
            listItemBound.Add(new Role() { RoleID = -1, Name = "" });
            listItemBound.AddRange(param.Roles);
            
            dropDownListStaffGroup.DataSource = listItemBound;
            dropDownListStaffGroup.DataBind();

            GetListSharedUser();
        }

        //private void GetListSharedUser(int? idHRID = null, Repeater? rptData = null)
        //{
        //    PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListSharedUser);
        //    if (idHRID.HasValue)
        //    {
        //        param.pressAgencyHRID = idHRID.GetValueOrDefault(0);
        //    }
        //    else
        //    {
        //        param.pressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value).GetValueOrDefault(0);
        //    }
        //    if (!string.IsNullOrWhiteSpace(txtSearch?.Text))
        //    {
        //        param.txtSearchUserShared = txtSearch.Text;
        //    }

        //    MainController.Provider.Execute(param);

        //    rptData.DataSource = param.listUserShared;
        //    rptData.DataBind();
        //}
        private void GetListSharedUser(int? idHRID = null, Repeater dt = null)
        {
            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListSharedUser);
            if (idHRID.HasValue)
            {
                param.pressAgencyHRID = idHRID.GetValueOrDefault(0);
            }
            else
            {
                param.pressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value).GetValueOrDefault(0);
            }
            if (!string.IsNullOrWhiteSpace(txtSearch?.Text))
            {
                param.txtSearchUserShared = txtSearch.Text;
            }

            MainController.Provider.Execute(param);

            // Nếu param.listUserShared là một đối tượng có thể chuyển đổi ngay thành chuỗi
            string userSharedAsString = param.listUserShared.ToString();
            LogManager.WebLogger.LogError("Share User List: " + userSharedAsString);
            // Kiểm tra xem tham chiếu của Repeater có null hay không trước khi gán dữ liệu
            if (dt != null)
            {
                dt.DataSource = param.listUserShared;
                dt.DataBind();
            }
            else
            {
                rptData.DataSource = param.listUserShared;
                rptData.DataBind();
            }
        }

        private void BindObjectToRepeaterItem(RepeaterItem rptItem)
        {
            Employee item = rptItem.DataItem as Employee;
            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;

            var name = rptItem.FindControl("ltrName") as Literal;
            var description = rptItem.FindControl("ltrDescription") as Literal;
            var email = rptItem.FindControl("ltrEmail") as Literal;
            name.Text = item.Name;
            email.Text = item.Email;
            description.Text = item.Description;
            btnDelete.CommandName = SMX.ActionDelete;
            btnDelete.CommandArgument = item.EmployeeID.ToString();
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
                ShowError(ex);
            }
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case SMX.ActionDelete:
                    {
                        PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.DeleteShare);
                        param.UserId = Utility.GetNullableInt(e.CommandArgument.ToString()).GetValueOrDefault(0);
                        param.pressAgencyHRID = Utility.GetNullableInt(hidPressAgencyHRID.Value).GetValueOrDefault(0);
                        MainController.Provider.Execute(param);
                        GetListSharedUser();
                        break;
                    }
            }
        }

        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            GetListSharedUser();
        }
    }
}