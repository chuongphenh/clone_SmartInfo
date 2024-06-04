using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;

namespace SM.SmartInfo.UI.UserControls.Search
{
    public partial class SearchNegativeNewsUC : UserControl
    {
        #region Events

        protected void btnNext_Click(object sender, EventArgs e)
        {
            hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) + 1);
            LoadData(Request.Params["q"], Utility.GetNullableInt(Request.Params["t"]));
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) - 1);
            LoadData(Request.Params["q"], Utility.GetNullableInt(Request.Params["t"]));
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                BindObjectToRepeaterItem(e.Item);
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            hidPage.Value = "1";
        }

        public void LoadData(string searchText, int? searchType)
        {
            CommonParam param = new CommonParam(FunctionType.Common.NegativeNewsSearch);
            param.SearchText = searchText;
            param.SearchType = searchType;
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetNullableInt(hidPage.Value).GetValueOrDefault(1) - 1
            };
            MainController.Provider.Execute(param);

            rptData.DataSource = param.ListSuVu;
            rptData.DataBind();

            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;
        }

        #endregion

        #region Private Method

        private void BindObjectToRepeaterItem(RepeaterItem rptItem)
        {
            SharedComponent.Entities.News item = rptItem.DataItem as SharedComponent.Entities.News;

            HyperLink hypName = (HyperLink)rptItem.FindControl("hypName");
            Literal ltrStatus = (Literal)rptItem.FindControl("ltrStatus");
            Label ltrNegativeType = (Label)rptItem.FindControl("ltrNegativeType");
            Literal ltrIncurredDTG = (Literal)rptItem.FindControl("ltrIncurredDTG");

            hypName.Text = item.Name;
            hypName.NavigateUrl = string.Format("~/UI/SmartInfos/NegativeNews/Default.aspx?ID={0}", item.NewsID);

            ltrIncurredDTG.Text = Utils.Utility.GetDateTimeString(item.IncurredDTG, "HH:mm - dd/MM/yyyy");
            ltrNegativeType.Text = Utils.Utility.GetDictionaryValue(SMX.News.NegativeNews.dicType, item.NegativeType);
            ltrStatus.Text = Utils.Utility.GetDictionaryValue(SMX.News.Status.dicDesc, item.Status);

            switch (item.NegativeType)
            {
                case SMX.News.NegativeNews.ChuaPhatSinh:
                    ltrNegativeType.Attributes.Add("class", "chua-phat-sinh");
                    break;
                case SMX.News.NegativeNews.DaPhatSinh:
                    ltrNegativeType.Attributes.Add("class", "da-phat-sinh");
                    break;
            }

            HtmlGenericControl iStatus = rptItem.FindControl("iStatus") as HtmlGenericControl;
            HtmlGenericControl spanStatus = rptItem.FindControl("spanStatus") as HtmlGenericControl;

            if (item.Status == SMX.News.Status.HoanThanh)
            {
                spanStatus.Attributes.Add("class", "done");
                iStatus.Attributes.Add("class", "fa fa-check fa-negative-news-done");
            }
            else
            {
                spanStatus.Attributes.Add("class", "inprogress");
                iStatus.Attributes.Add("class", "fa fa-arrow-right fa-negative-news-inprogress");
            }

            HtmlGenericControl divLink = rptItem.FindControl("divLink") as HtmlGenericControl;
            divLink.Attributes.Add("onclick", "window.location='Default.aspx?ID=" + item.NewsID + "'");

            var newsID = Request.Params[SMX.Parameter.ID];
            if (!string.IsNullOrWhiteSpace(newsID) && Utils.Utility.GetNullableInt(newsID) == item.NewsID)
                divLink.Attributes.Add("class", "div-active");
        }

        #endregion
    }
}