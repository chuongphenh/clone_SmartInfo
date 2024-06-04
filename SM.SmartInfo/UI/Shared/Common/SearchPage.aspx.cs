using System;
using System.Collections.Generic;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class SearchPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as MasterPages.Common.SmartInfo).Search += Default_Search;

            if (!IsPostBack)
            {
                SetupForm();
                LoadData();
            }
        }

        private void Default_Search(string searchText)
        {
            Response.Redirect(string.Format(PageURL.SearchPage, searchText, hidType.Value));
        }

        private void SetupForm()
        {
            hidType.Value = Request.Params["t"];
            hidTextSearch.Value = Request.Params["q"];
        }

        private void LoadData()
        {
            ucSearchNotification.SetupForm();
            ucSearchNotification.LoadData(hidTextSearch.Value, Utils.Utility.GetNullableInt(hidType.Value));

            ucSearchPressAgency.SetupForm();
            ucSearchPressAgency.LoadData(hidTextSearch.Value, Utils.Utility.GetNullableInt(hidType.Value));

            ucSearchNegativeNews.SetupForm();
            ucSearchNegativeNews.LoadData(hidTextSearch.Value, Utils.Utility.GetNullableInt(hidType.Value));

            ucSearchNews.SetupForm();
            ucSearchNews.LoadData(hidTextSearch.Value, Utils.Utility.GetNullableInt(hidType.Value));

            switch (Utils.Utility.GetNullableInt(hidType.Value))
            {
                case SMX.TypeSearch.News:
                    ucSearchNews.SetHeightAuto();
                    divNews.Attributes["class"] = "col-sm-12";
                    divNegativeNews.Visible = divNotification.Visible = divPressAgency.Visible = false;
                    break;
                case SMX.TypeSearch.NegativeNews:
                    divNegativeNews.Attributes["class"] = "col-sm-12";
                    divNews.Visible = divNotification.Visible = divPressAgency.Visible = false;
                    break;
                case SMX.TypeSearch.Notification:
                    divNotification.Attributes["class"] = "col-sm-12";
                    divNegativeNews.Visible = divNews.Visible = divPressAgency.Visible = false;
                    break;
                case SMX.TypeSearch.PressAgency:
                    divPressAgency.Attributes["class"] = "col-sm-12";
                    divNegativeNews.Visible = divNews.Visible = divNotification.Visible = false;
                    break;
            }
        }

        protected override Dictionary<object, string> FunctionCodeMapping
        {
            get
            {
                return new Dictionary<object, string>()
                {
                    { this          , FunctionCode.VIEW },
                };
            }
        }
    }
}