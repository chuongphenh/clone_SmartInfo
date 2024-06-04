using System;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.SmartInfos.NegativeNews
{
    public partial class ListNagativeNewsUC : UserControl
    {
        public event EventHandler RequestSave_NegativeNews;

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) + 1);
                BindData(Utility.GetNullableBool(hidIsEdit.Value), Utility.GetNullableInt(hidNewsID.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hidNewsID.Value) && RequestSave_NegativeNews != null)
                    RequestSave_NegativeNews(null, null);
                else
                {
                    string url = string.Format("/UI/PopupPages/NegativeNews/Edit.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidNewsID.Value).GetValueOrDefault(0), 0 }), btnReloadAppendix.ClientID);
                    UIUtility.OpenPopupWindow(this.Page, url, 1000, 700);
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = Utility.GetString(Utility.GetNullableInt(hidPage.Value) - 1);
                BindData(Utility.GetNullableBool(hidIsEdit.Value), Utility.GetNullableInt(hidNewsID.Value));
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnReloadAppendix_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(hidNewsID.Value))
                {
                    BindData(Utility.GetNullableBool(hidIsEdit.Value), Utility.GetNullableInt(hidNewsID.Value));

                    var tab = Request.Params["T"];
                    var filterTime = Request.Params["F"];
                    string url = string.Format("Default.aspx?ID={0}&T={1}&F={2}", hidNewsID.Value, string.IsNullOrWhiteSpace(tab) ? string.Empty : tab, string.IsNullOrWhiteSpace(filterTime) ? string.Empty : filterTime);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeURL", string.Format("changeURL('{0}');", url), true);
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeaterItem(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case SMX.ActionDelete:
                        DeleteItem(Utility.GetNullableInt(e.CommandArgument.ToString()));
                        BindData(Utility.GetNullableBool(hidIsEdit.Value), Utility.GetNullableInt(hidNewsID.Value));

                        break;
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Public Methods

        public void SetupForm()
        {
            hidPage.Value = "1";
        }

        public void BindData(bool? isEdit, int? newsID)
        {
            hidNewsID.Value = Utility.GetString(newsID);
            hidIsEdit.Value = Utility.GetString(isEdit);
            btnPrevious.Enabled = Utility.GetNullableInt(hidPage.Value) > 1;
            thAddNew.Visible = btnAddNew.Visible = thEdit.Visible = footerEdit.Visible = isEdit.GetValueOrDefault(false);

            NewsParam param = new NewsParam(FunctionType.NegativeNew.GetItemsNegativeNews);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageIndex = int.Parse(hidPage.Value) - 1,
                PageSize = SMX.smx_PageMiniTen
            };
            param.NewsID = newsID;
            MainController.Provider.Execute(param);

            btnNext.Enabled = Utility.GetNullableInt(hidPage.Value) < param.PagingInfo.PageCount;

            rptData.DataSource = param.ListNegativeNews;
            rptData.DataBind();
        }

        #endregion

        #region Private Methods

        private void BindObjectToRepeaterItem(RepeaterItem rptItem)
        {
            SharedComponent.Entities.NegativeNews item = rptItem.DataItem as SharedComponent.Entities.NegativeNews;

            UIUtility.SetRepeaterItemIText(rptItem, "ltrName", item.Name);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrType", Utility.GetDictionaryValue(SMX.News.NegativeNews.dicType, item.Type));

            HtmlGenericControl spanType = rptItem.FindControl("spanType") as HtmlGenericControl;
            switch (item.Type)
            {
                case SMX.News.NegativeNews.ChuaPhatSinh:
                    spanType.Attributes.Add("class", "chua-phat-sinh");
                    break;
                case SMX.News.NegativeNews.DaPhatSinh:
                    spanType.Attributes.Add("class", "da-phat-sinh");
                    break;
            }

            Literal ltrStatus = rptItem.FindControl("ltrStatus") as Literal;
            ltrStatus.Text = Utility.GetDictionaryValue(SMX.News.Status.dicDesc, item.Status);
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

            HtmlAnchor anchor = rptItem.FindControl("aLink") as HtmlAnchor;
            anchor.Attributes["onclick"] = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/NegativeNews/Display.aspx?ID={0}&callback={1}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { Utility.GetNullableInt(hidNewsID.Value).GetValueOrDefault(0), item.NegativeNewsID.GetValueOrDefault(0) }), btnReloadAppendix.ClientID), 1000, 700);

            LinkButton btnDelete = rptItem.FindControl("btnDelete") as LinkButton;
            btnDelete.CommandArgument = Utility.GetString(item.NegativeNewsID);
            btnDelete.CommandName = SMX.ActionDelete;

            HtmlTableCell tdEdit = rptItem.FindControl("tdEdit") as HtmlTableCell;
            tdEdit.Visible = Utility.GetNullableBool(hidIsEdit.Value).GetValueOrDefault(false);
        }

        private void DeleteItem(int? id)
        {
            NewsParam param = new NewsParam(FunctionType.NegativeNew.DeleteNewNegativeNews);
            param.NegativeNewsID = id;
            MainController.Provider.Execute(param);
        }

        #endregion
    }
}