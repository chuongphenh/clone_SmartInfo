using System;
using System.Linq;
using SM.SmartInfo.BIZ;
using System.Collections;
using System.Web.Services;
using SoftMart.Core.UIControls;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;

namespace SM.CollateralManagement
{
    public partial class ClientAPI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string SearchEmployee(string keyword, string optionValue)
        {
            CommonParam param = new CommonParam(FunctionType.Common.SearchShortEmployee);
            param.EmployeeName = keyword;
            MainController.Provider.Execute(param);

            // Chuyển đổi dữ liệu tìm kiếm được về IList<DynamicObject>
            IList lstCommittee = param.Employees.Select(en => new { en.EmployeeID, en.Name, UserName = en.Username }).ToList();

            // Chuyển đổi IList<DynamicObject> về JSONString
            return AjaxSearchBox.ParseClientData(lstCommittee);
        }

        [WebMethod]
        public static string SearchvOrgWithAll(string keyword, string optionValue)
        {
            int? orgType = SmartInfo.Utils.Utility.GetNullableInt(optionValue);
            var lstItem = SmartInfo.CacheManager.GlobalCache.SearchvOrganization(orgType, keyword).Take(50).ToList();

            // Chuyển đổi dữ liệu tìm kiếm được về IList<DynamicObject>
            lstItem.Insert(0, new SmartInfo.SharedComponent.EntityViews.vOrganization() { OrganizationID = 0, Name = "All", BreadCrumb = "All branch" });
            IList lstOrg = lstItem.Select(en => new { en.OrganizationID, en.Name, en.BreadCrumb }).ToList();

            // Chuyển đổi IList<DynamicObject> về JSONString
            return AjaxSearchBox.ParseClientData(lstOrg);
        }
    }
}