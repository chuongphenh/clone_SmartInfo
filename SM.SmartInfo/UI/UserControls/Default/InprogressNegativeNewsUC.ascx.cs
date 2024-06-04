using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using SoftMart.Kernel.Exceptions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.UserControls.Default
{
    public partial class InprogressNegativeNewsUC : UserControl
    {
        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToGridItem(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #region Public Methods

        public void BindData(List<News> lstNews)
        {
            rptData.DataSource = lstNews;
            rptData.DataBind();
        }

        #endregion

        private void BindObjectToGridItem(RepeaterItem rptItem)
        {
            News item = rptItem.DataItem as News;

            HyperLink hypName = (HyperLink)rptItem.FindControl("hypName");
            Literal ltrStatus = (Literal)rptItem.FindControl("ltrStatus");
            Literal ltrIncurredDTG = (Literal)rptItem.FindControl("ltrIncurredDTG");

            hypName.Text = item.Name;
            hypName.NavigateUrl = string.Format("/UI/SmartInfos/NegativeNews/Default.aspx?ID={0}", item.NewsID);

            ltrIncurredDTG.Text = Utils.Utility.GetDateTimeString(item.IncurredDTG, "HH:mm - dd/MM/yyyy");
            ltrStatus.Text = Utils.Utility.GetDictionaryValue(SMX.News.Status.dicDesc, item.Status);

            HtmlGenericControl divData = rptItem.FindControl("divData") as HtmlGenericControl;
            if (rptItem.ItemIndex != 0 && rptItem.ItemIndex != 1)
            {
                if (rptItem.ItemIndex % 2 == 0)
                    divData.Attributes.Add("class", "even second-row");
                else
                    divData.Attributes.Add("class", "odd second-row");
            }
            else
            {
                if (rptItem.ItemIndex % 2 == 0)
                    divData.Attributes.Add("class", "even");
                else
                    divData.Attributes.Add("class", "odd");
            }

            HtmlGenericControl pTime = rptItem.FindControl("pTime") as HtmlGenericControl;
            HtmlGenericControl pName = rptItem.FindControl("pName") as HtmlGenericControl;
            HtmlGenericControl iStatus = rptItem.FindControl("iStatus") as HtmlGenericControl;
            HtmlGenericControl spanStatus = rptItem.FindControl("spanStatus") as HtmlGenericControl;

            if (item.Status == SMX.News.Status.HoanThanh)
            {
                pTime.Attributes.Add("class", "done");
                hypName.Attributes.Add("class", "done");
                spanStatus.Attributes.Add("class", "done");
                pName.Attributes.Add("class", "done description-negative-news");
                iStatus.Attributes.Add("class", "fa fa-check fa-negative-news-done");
            }
            else
            {
                pTime.Attributes.Add("class", "inprogress");
                hypName.Attributes.Add("class", "inprogress");
                spanStatus.Attributes.Add("class", "inprogress");
                pName.Attributes.Add("class", "inprogress description-negative-news");
                iStatus.Attributes.Add("class", "fa fa-arrow-right fa-negative-news-inprogress");
            }
        }
    }
}