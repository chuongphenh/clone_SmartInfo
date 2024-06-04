using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SoftMart.Core.Utilities.Profiles;
using SM.SmartInfo.DAO.CommonList;

namespace SM.SmartInfo.UI.Administrations.Users
{
    public partial class UserDetailUC : System.Web.UI.UserControl
    {
        public string Username { get { return ltrUserName.Text; } }

        protected void grdRole_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Role item = e.Item.DataItem as Role;
                HiddenField hiRoleID = e.Item.FindControl("hiRoleID") as HiddenField;
                hiRoleID.Value = item.RoleID.ToString();
            }
        }

        public void BinData(int? itemID, Employee emp, List<Role> role, List<EmployeeRole> empRole, bool noData, EmployeeImage empImage = null)
        {
            if (noData)
            {
                UserParam param = new UserParam(FunctionType.Administration.User.LoadDataDisplayUser);
                param.EmployeeId = itemID;
                MainController.Provider.Execute(param);

                BindObjectToForm(param.Employee);
                BindRole(param.Roles, param.UserRoles);

                if (param.EmployeeImage != null && param.EmployeeImage.SignImage != null)
                    imgSignImage.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(param.EmployeeImage.SignImage);
                else
                    imgSignImage.Visible = false;
            }
            else
            {
                BindObjectToForm(emp);
                BindRole(role, empRole);

                if (empImage != null && empImage.SignImage != null)
                {
                    imgSignImage.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(empImage.SignImage);
                }
                else
                    imgSignImage.Visible = false;
            }
        }

        private void BindObjectToForm(Employee item)
        {
            ltrUserName.Text = item.Username;
            ltrFullName.Text = item.Name;
            ltrName.Text = item.Name;
            ltrEmployeeCode.Text = item.EmployeeCode;
            //ltrCIFCode.Text = item.CIFCode;
            ltrDOB.Text = Utils.Utility.GetDateString(item.DOB);
            ltrGender.Text = Utils.Utility.GetDictionaryValue(SMX.dicGender, item.Gender);
            ltrHomePhone.Text = item.Phone;
            ltrMobilePhone.Text = item.Mobile;
            ltrEmail.Text = item.Email;
            ltrStatus.Text = Utils.Utility.GetDictionaryValue(SMX.Status.dctStatus, item.Status);
            ltrNote.Text = item.Description;
            ltrOrganizationEmployee.Text = string.Join("<br/>", item.ListOrganizationEmployee.Select(c => c.Name));
            ltrOrganizationManager.Text = string.Join("<br/>", item.ListOrganizationManager.Select(c => c.Name));
            ltrSector.Text = CacheManager.GlobalCache.GetNameByID(item.Sector);
            //ltrLevel.Text = CacheManager.GlobalCache.GetNameByID(item.Level);
            ltrAuthorizationType.Text = Utils.Utility.GetDictionaryValue(SMX.dicAuthenticationType, item.AuthorizationType);
            ltrLdapCnnName.Text = item.LdapCnnName;
            //ltrListBranchCode.Text = item.ListBranchCode;
            //chkIsManager.Checked = (item.IsManager ?? false);
            AttachmentDao _attDao = new AttachmentDao();
            List<adm_Attachment> itemCheck = _attDao.GetAttachmentByRefIDAndRefTypeJoinCacheECM(item.EmployeeID, SMX.AttachmentRefType.PressAgencyHR);
            adm_Attachment itemImage = new adm_Attachment();
            if (itemCheck.Any())
            {
                itemImage = itemCheck.FirstOrDefault();
            }
            BindDataImagee(UserImage, itemImage);
        }

        private void BindRole(List<Role> datas, List<EmployeeRole> selectedRoles)
        {
            foreach (Role role in datas)
            {
                if (selectedRoles.Exists(r => r.RoleID == role.RoleID))
                    role.IsSelect = true;
            }
            grdRole.DataSource = datas;
            grdRole.DataBind();
        }
        private void BindDataImagee(System.Web.UI.WebControls.Image imgUI, adm_Attachment imgData)
        {
            if (imgData != null)
            {
                imgUI.AlternateText = imgData.Description;
                imgUI.ImageUrl = GetImageURLL(imgData);
            }
            else
                imgUI.ImageUrl = SMX.DefaultImage;
        }


        private string GetImageURLL(adm_Attachment image)
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
    }
}