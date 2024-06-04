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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.PopupPages.PressAgencyHRs
{
    public partial class AddStaff : BasePagePopup
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                prepareItemsForView();
            }
            catch (SMXException ex)
            {
                ShowError(ex);
            }
        }

        protected void prepareItemsForView()
        {
            var empId = Request.QueryString["ID"];
            var agencyType = Request.QueryString["AgencyType"];

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.GetListPressAgencyByType);
            param.PressAgencyType = string.IsNullOrEmpty(agencyType) ? 0 : Convert.ToInt32(agencyType);
            MainController.Provider.Execute(param);

        }
    }
}