using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.Utils;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.PopupPages.ImageLibrary
{
    public partial class CreateNewFolder : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Request.QueryString["ParentId"]))
            {
                lbTxtBox.Text = "Tên thư mục";
            }
            else
            {
                lbTxtBox.Text = "Tên album";
            }
        }

        protected void btnCreateAndSave_Click(object sender, EventArgs e)
        {
            try
            {
                var noteParentId = Request.QueryString["ParentId"];
                if (string.IsNullOrEmpty(txtFolderName.Text))
                {
                    errMessage.Text = "Bạn chưa nhập tên thư mục";
                }
                else
                {
                    if (string.IsNullOrEmpty(noteParentId))
                    {
                        ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.AddNewNode);
                        param.ImageCatalog = new ImageCatalog()
                        {
                            CatalogName = txtFolderName.Text,
                            CreatedDTG = DateTime.Now,
                            CreatedBy = Profiles.MyProfile.UserName,
                            CreatedUserId = Profiles.MyProfile.EmployeeID
                        };
                        MainController.Provider.Execute(param);
                    }
                    else
                    {
                        ImageLibraryParam param = new ImageLibraryParam(FunctionType.ImageLibrary.AddNewNode);
                        param.ImageCatalog = new ImageCatalog()
                        {
                            CatalogName = txtFolderName.Text,
                            CreatedDTG = DateTime.Now,
                            CreatedBy = Profiles.MyProfile.UserName,
                            CreatedUserId = Profiles.MyProfile.EmployeeID,
                            ParentId = Convert.ToInt32(noteParentId)
                        };
                        MainController.Provider.Execute(param);
                    }

                    ShowMessage("Lưu thành công");
                }
            }
            catch(SMXException ex)
            {
                ShowError(ex);
            }
        }
    }
}