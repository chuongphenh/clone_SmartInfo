using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;
using SoftMart.Core.Utilities;
using SoftMart.Core.Utilities.Profiles;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master as MasterPages.Common.SmartInfo).Search += Default_Search;
            ucLatestNotification.Filter += ucLatestNotification_Filter;

            if (!IsPostBack)
            {
                //LoadData();
            }
        }

        private void ucLatestNotification_Filter(string filter)
        {
            CommonParam param = new CommonParam(FunctionType.Common.FilterNotification);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo(0, 100);
            param.TypeTime = Utility.GetNullableInt(string.IsNullOrEmpty(filter) ? SMX.FilterTime.All.ToString() : filter);
            param.EmployeeId = Profiles.MyProfile.EmployeeID;
            MainController.Provider.Execute(param);

            ucLatestNotification.BindData(param.ListNotification);
        }

        private void Default_Search(string searchText)
        {
            Response.Redirect(string.Format(PageURL.SearchPage, searchText, string.Empty));
        }

        private void LoadData()
        {
            CommonParam param = new CommonParam(FunctionType.Common.GetCommon);
            param.EmployeeId = Profiles.MyProfile.EmployeeID;
            MainController.Provider.Execute(param);

            ucLatestNotification.SetupForm();
            
            ucLatestNotification.BindData(param.ListNotification);
            ucInprogressNegativeNews.BindData(param.ListSuVu);
            ucLatestNews.BindData(param.ListTinTuc);
            ucChartStatistic.LoadData();
        }

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get { return new Dictionary<object, string>(); }
        }
    }
}