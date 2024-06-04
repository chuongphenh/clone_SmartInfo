using SM.SmartInfo.BIZ;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SoftMart.Kernel.Exceptions;
using System;
using System.Web.UI;

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
    public partial class Create : BasePagePopup
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateAndSave_Click(object sender, EventArgs e)
        {

            try
            {
                var name = txtAgencyName.Text;
                var code = txtAgencyCode.Text;

                PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.AddNewAgencyType);

                param.AgencyType = new AgencyType()
                {
                    TypeName = txtAgencyName.Text,
                    Code = txtAgencyCode.Text,
                    DateModified = DateTime.Now,
                    Creator = Profiles.MyProfile.EmployeeID.ToString(),
                    Modifier = Profiles.MyProfile.UserName.ToString()
                };
                param.IsSaveComplete = true;
                MainController.Provider.Execute(param);

                ShowMessage("Lưu thành công");
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }
    }
}