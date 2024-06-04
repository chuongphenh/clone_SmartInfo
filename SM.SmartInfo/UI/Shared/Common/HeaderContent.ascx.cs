using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SM.SmartInfo.CacheManager;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class HeaderContent : UserControl
    {
        public delegate void SearchAll(string searchText);

        public event SearchAll Search;

        private int? currentFeatureID = null;

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserProfile userProfile = Profiles.MyProfile;
                if (userProfile == null)
                {
                    string oldPage = Request.Url.PathAndQuery;
                    string newPage = ResolveUrl(string.Format(PageURL.LoginPageWithReturn, Server.UrlEncode(oldPage)));
                    Response.Redirect(newPage);
                }

                lnkLogout.NavigateUrl = ResolveUrl(PageURL.LogoutPage);
                ltrUserName.Text = userProfile.FullName;
                hrefChangePass.Visible = (!string.IsNullOrWhiteSpace(userProfile.Employee.Email) && userProfile.Employee.Email.EndsWith("@mbamc.com.vn"));

                BuildMenu();

                string searchText = Request.Params["q"];
                if (!string.IsNullOrWhiteSpace(searchText))
                    txtSearch.Text = searchText;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Search != null)
                Search(txtSearch.Text);
        }

        protected void rptMenu1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Feature feature = (Feature)e.Item.DataItem;

                // link
                HyperLink hypMenu1Link = (HyperLink)e.Item.FindControl("hypMenu1Link");

                // set active
                if (feature.FeatureID == currentFeatureID)
                {
                    string oldClass = hypMenu1Link.Attributes["class"];
                    if (string.IsNullOrWhiteSpace(oldClass))
                        hypMenu1Link.Attributes["class"] = "menu-active";
                    else
                        hypMenu1Link.Attributes["class"] = oldClass + " menu-active";
                }

                string menuLink = ResolveUrl(feature.URL);
                if (string.IsNullOrWhiteSpace(menuLink) || "#".Equals(menuLink.Trim()))
                {
                    menuLink = string.Empty;
                    List<Feature> lstFeature2 = Profiles.MyProfile.Features.Where(c => c.ParentID == feature.FeatureID &&
                        c.Deleted == SMX.smx_IsNotDeleted && c.IsVisible == SMX.Status.Active).OrderBy(c => c.DisplayOrder).ToList();
                    foreach (Feature feature2 in lstFeature2)
                    {
                        menuLink = ResolveUrl(feature2.URL);
                        if (string.IsNullOrWhiteSpace(menuLink) || "#".Equals(menuLink.Trim()))
                        {
                            Feature feature3 = Profiles.MyProfile.Features.Where(c => c.ParentID == feature2.FeatureID &&
                                c.Deleted == SMX.smx_IsNotDeleted && c.IsVisible == SMX.Status.Active &&
                                !string.IsNullOrWhiteSpace(c.URL) &&
                                !"#".Equals(c.URL.Trim())).OrderBy(c => c.DisplayOrder).FirstOrDefault();
                            if (feature3 != null)
                            {
                                menuLink = ResolveUrl(feature3.URL);
                                break;
                            }
                        }
                        else
                            break;
                    }
                }
                if (!string.IsNullOrWhiteSpace(menuLink) && menuLink.Trim().ToLower().EndsWith(".pdf"))
                    menuLink = "#";

                if (menuLink != "#")
                    hypMenu1Link.NavigateUrl = ResolveUrl(menuLink);
                else
                    hypMenu1Link.NavigateUrl = "javascript:void(0);";

                // icon
                if (!string.IsNullOrWhiteSpace(feature.CreatedOn))
                {
                    Literal ltrMenu1Icon = (Literal)hypMenu1Link.FindControl("ltrMenu1Icon");
                    ltrMenu1Icon.Text = string.Format("<i class='{0}' title='{1}' style='color: #2F54EB; font-size: 15px;'></i>", feature.CreatedOn, feature.Name);

                    Literal ltrMenu1Name = (Literal)e.Item.FindControl("ltrMenu1Name");
                    ltrMenu1Name.Text = feature.Name;
                }

                Repeater rptMenu2 = (Repeater)e.Item.FindControl("rptMenu2");
                List<Feature> lstMenu2 = GetListMenu2(feature.FeatureID);


                if (lstMenu2 != null && lstMenu2.Count > 0)
                {
                    rptMenu2.DataSource = lstMenu2;
                    rptMenu2.DataBind();
                }
                else
                {
                    HtmlGenericControl ulMenu2 = (HtmlGenericControl)e.Item.FindControl("ulMenu2");
                    ulMenu2.Visible = false;
                }
            }
        }

        protected void rptMenu2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Feature menu2 = e.Item.DataItem as Feature;

                HyperLink hypMenu2Link = (HyperLink)e.Item.FindControl("hypMenu2Link");
                hypMenu2Link.NavigateUrl = ResolveUrl(string.IsNullOrWhiteSpace(menu2.URL) ? "#" : menu2.URL);

                if (!string.IsNullOrWhiteSpace(menu2.CreatedOn))
                {
                    Literal ltrMenu2Icon = (Literal)e.Item.FindControl("ltrMenu2Icon");
                    ltrMenu2Icon.Text = string.Format("<i class='{0}' style='color: #2F54EB; font-size: 15px;'></i>", menu2.CreatedOn);
                }

                Literal ltrMenu2Name = (Literal)e.Item.FindControl("ltrMenu2Name");
                ltrMenu2Name.Text = menu2.Name;

                if (menu2.FeatureID == FindActivingFeatureID())
                {
                    hypMenu2Link.CssClass = "sub-menu-active";
                }
            }
        }

        #endregion

        #region Helper

        private void BuildMenu()
        {
            int? featureID = FindActivingFeatureID();
            if (featureID != null)
            {
                currentFeatureID = featureID;

                // find root featureID (level 1)
                Feature feature = Profiles.MyProfile.Features.FirstOrDefault(c => c.FeatureID == featureID);
                Feature parentFeature = Profiles.MyProfile.Features.FirstOrDefault(c => c.FeatureID == feature.ParentID);
                while (parentFeature != null)
                {
                    currentFeatureID = parentFeature.FeatureID;
                    parentFeature = Profiles.MyProfile.Features.FirstOrDefault(c => c.FeatureID == parentFeature.ParentID);
                }
            }

            // Bind level 1 menu
            List<Feature> lstMenu1 = Profiles.MyProfile.Features.Where(item => item.ParentID == null && item.IsVisible == SMX.Status.Active).OrderBy(c => c.DisplayOrder).ToList();

            rptMenu1.DataSource = lstMenu1;
            rptMenu1.DataBind();
        }

        private List<Feature> GetListMenu2(int? featureMenu1)
        {
            List<Feature> lstFeature = Profiles.MyProfile.Features;
            Feature feature = lstFeature.FirstOrDefault(c => c.FeatureID == featureMenu1);
            while (feature != null && feature.ParentID != null)
            {
                feature = lstFeature.FirstOrDefault(c => c.FeatureID == feature.ParentID);
            }

            if (feature == null)
            {
                return new List<Feature>();
            }

            lstFeature = lstFeature.Where(c =>
                c.ParentID == feature.FeatureID &&
                c.IsVisible == SMX.Status.Active)
                .OrderBy(c => c.DisplayOrder).ToList();

            return lstFeature;
        }

        private int? FindActivingFeatureID()
        {
            UserProfile userProfile = Profiles.MyProfile;

            string url = Request.Url.ToString().ToLower();
            Regex rg = new Regex(@"/ui/.*\.aspx(\?SPTID=\d+)?(\?type=\w+)?", RegexOptions.IgnoreCase);
            Match match = rg.Match(url);
            if (match == null)
                return null;
            string normalizesUrl = match.Value;

            // find by feature
            Feature feature = userProfile.Features.FirstOrDefault(c => normalizesUrl.Equals(c.URL, StringComparison.OrdinalIgnoreCase));
            if (feature != null)
                return feature.FeatureID;

            // find by FeatureFunction (FeatureFunction -> Feature)
            FeatureFunction featureFunction = userProfile.FeatureFunctions.FirstOrDefault(
                c => normalizesUrl.Equals(c.URL, StringComparison.OrdinalIgnoreCase));
            if (featureFunction == null)
                return null;

            feature = userProfile.Features.FirstOrDefault(c => c.FeatureID == featureFunction.FeatureID);
            if (feature != null)
                return feature.FeatureID;

            return null;
        }

        #endregion
    }
}