using System;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.Utils;
using SM.SmartInfo.CacheManager;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using SM.SmartInfo.PermissionManager.Shared;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class SearchPressAgencyHR : BasePage
    {
        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as MasterPages.Common.SmartInfo).Search += Default_Search;

            try
            {
                if (!IsPostBack)
                {
                    SetupForm();
                }
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                hidPage.Value = "1";

                SearchItemForView();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void rptHR_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                    BindObjectToRepeater(e.Item);
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        protected void ucPager_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            try
            {
                hidPage.Value = e.NewPageIndex.ToString();
                SearchItemForView();
            }
            catch (SMXException ex)
            {
                ucErr.ShowError(ex);
            }
        }

        #endregion

        #region Private Methods

        private void Default_Search(string searchText)
        {
            Response.Redirect(string.Format(PageURL.SearchPage, searchText, string.Empty));
        }

        private void SetupForm()
        {
            hidPage.Value = "1";

            UIUtility.BindDicToDropDownList(ddlAttitude, SMX.Attitude.dicDesc);
        }

        private void SearchItemForView()
        {
            agency_PressAgencyHR paHR = new agency_PressAgencyHR()
            {
                Attitude = Utility.GetNullableInt(ddlAttitude.SelectedValue),
                FromDOB = dpkFromDOB.SelectedDate,
                ToDOB = dpkToDOB.SelectedDate,
                TextSearch = txtTextSearchPAHR.Text
            };

            agency_PressAgencyHRHistory paHRHistory = new agency_PressAgencyHRHistory()
            {
                FromMeetDTG = dpkFromPAHRHistoryMeetDTG.SelectedDate,
                ToMeetDTG = dpkToPAHRHistoryMeetDTG.SelectedDate,
                TextSearch = txtTextSearchPAHRHistory.Text
            };

            agency_PressAgencyHRRelatives paHRRelatives = new agency_PressAgencyHRRelatives()
            {
                FromDOB = dpkFromPAHRRelativesDOB.SelectedDate,
                ToDOB = dpkToPAHRRelativesDOB.SelectedDate,
                TextSearch = txtTextSearchPAHRRelatives.Text
            };

            PressAgencyParam param = new PressAgencyParam(FunctionType.PressAgency.SearchItemsForViewPressAgencyHR);
            param.PagingInfo = new SoftMart.Kernel.Entity.PagingInfo()
            {
                PageSize = SMX.smx_PageMiniTen,
                PageIndex = Utility.GetInt(hidPage.Value) - 1
            };
            param.PressAgencyHR = paHR;
            param.PressAgencyHRHistory = paHRHistory;
            param.PressAgencyHRRelatives = paHRRelatives;
            MainController.Provider.Execute(param);

            rptHR.DataSource = param.ListPressAgencyHR;
            rptHR.DataBind();

            Pager.BuildPager(param.PagingInfo.RecordCount, SMX.smx_PageMiniTen, int.Parse(hidPage.Value));
        }

        private void BindObjectToRepeater(RepeaterItem rptItem)
        {
            agency_PressAgencyHR item = rptItem.DataItem as agency_PressAgencyHR;

            var url = UIUtility.BuildHyperlinkWithAnchorTag(string.Format("~/UI/PopupPages/PressAgencyHRs/Display.aspx?ID={0}", Utility.Encrypt(Profiles.MyProfile.EmployeeID, new int[] { item.PressAgencyID.GetValueOrDefault(0), item.PressAgencyHRID.GetValueOrDefault(0) })), 1000, 700);
            HtmlGenericControl divLink = rptItem.FindControl("divLink") as HtmlGenericControl;
            divLink.Attributes.Add("onclick", url);

            UIUtility.SetRepeaterItemIText(rptItem, "ltrPressAgencyName", item.PressAgencyName);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrPosition", item.Position);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrFullName", item.FullName);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrAge", item.DOB.HasValue ? Utility.GetString(DateTime.Now.Year - item.DOB.Value.Year) : string.Empty);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrDOB", Utility.GetDateString(item.DOB));
            UIUtility.SetRepeaterItemIText(rptItem, "ltrEmail", item.Email);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrMobile", item.Mobile);
            UIUtility.SetRepeaterItemIText(rptItem, "ltrAddress", item.Address);

            Label ltrAttitude = rptItem.FindControl("ltrAttitude") as Label;
            ltrAttitude.Text = Utility.GetDictionaryValue(SMX.Attitude.dicDesc, item.Attitude);
            switch (item.Attitude)
            {
                case SMX.Attitude.TichCuc:
                    ltrAttitude.Attributes["class"] = "positive";
                    break;
                case SMX.Attitude.TieuCuc:
                    ltrAttitude.Attributes["class"] = "negative";
                    break;
                case SMX.Attitude.TrungLap:
                    ltrAttitude.Attributes["class"] = "medium";
                    break;
            }

            HtmlImage img = rptItem.FindControl("img") as HtmlImage;
            BindDataImage(img, item.Attachment);
        }

        private void BindDataImage(HtmlImage imgUI, adm_Attachment imgData)
        {
            if (imgData != null)
            {
                imgUI.Alt = imgData.Description;
                imgUI.Src = GetImageURL(imgData);
            }
            else
                imgUI.Src = SMX.DefaultImage;
        }

        private string GetImageURL(adm_Attachment image)
        {
            string url = SMX.DefaultImage;

            if (image != null && image.FileContent != null)
            {
                string imageFileName = string.Format("{0}_{1}", image.AttachmentID, image.FileName);
                string imageFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Repository", "ECM");
                if (!System.IO.Directory.Exists(imageFilePath))
                    System.IO.Directory.CreateDirectory(imageFilePath);
                imageFilePath = System.IO.Path.Combine(imageFilePath, imageFileName);
                if (!System.IO.File.Exists(imageFilePath))
                    System.IO.File.WriteAllBytes(imageFilePath, image.FileContent);
                url = "~/Repository/ECM/" + imageFileName;
            }

            return ResolveUrl(url);
        }

        #endregion

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