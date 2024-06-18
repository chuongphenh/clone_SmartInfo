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

namespace SM.SmartInfo.UI.Administrations.Targets
{
    public partial class AddNew : BasePage, ISMFormAddNew<Target>
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
            UIUtility.BindListToDropDownList(ddTargrtType, SMX.TargetType.dctTargetTypes.ToList(), false);
        }

        public object AddNewItem()
        {
            TargetParam param = new TargetParam(FunctionType.Administration.Target.AddNewItem);

            param.Target = BindFormToObject();
            MainController.Provider.Execute(param);

            return param.Target.TargetID;
        }

        #endregion

        #region Specifix

        public Target BindFormToObject()
        {
            Target item = new Target();

            item.TargetCode = txtCode.Text;
            item.Name = txtName.Text;
            item.TargetType = Utils.Utility.GetNullableInt(ddTargrtType.SelectedValue);
            item.Description = txtDescription.Text;
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