using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.Utils;

using SoftMart.Kernel.Exceptions;

using SM.SmartInfo.PermissionManager.Shared;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Categories
{
    public partial class AddNew : BasePage, ISMFormAddNew<Category>
    {
        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    SetupForm();
            }
            catch (SMXException ex) 
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                object itemID = AddNewItem();
                Response.Redirect(string.Format(PageURL.Display, itemID.ToString()));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
        #endregion

        #region Common

        public void SetupForm()
        {
            lnkExit.NavigateUrl = PageURL.Default;
        }

        public object AddNewItem()
        {
            CategoryParam param = new CategoryParam(FunctionType.Administration.Category.AddNewItem);

            param.Category = BindFormToObject();
            MainController.Provider.Execute(param);

            return param.Category.CategoryID;
        }

        #endregion

        #region Specifix

        public Category BindFormToObject()
        {
            Category item = new Category();

            item.CategoryName = txtName.Text;
            //item.Version = 1;
            //item.CreatedDTG = DateTime.Now;
            
            return item;
        }

        #endregion

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { btnSave   , FunctionCode.ADD  },
                };
            }
        }
    }
}