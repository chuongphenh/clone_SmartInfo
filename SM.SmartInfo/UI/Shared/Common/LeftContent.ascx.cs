using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SM.SmartInfo.CacheManager;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.Shared.Common
{
    public partial class LeftContent : UserControl
    {
        private int? _selectedFeature = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BuildMenu1();
        }

        protected void rptLevel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Feature menu1 = e.Item.DataItem as Feature;
                List<Feature> lstMenu2 = Profiles.MyProfile.Features.Where(
                        c => c.ParentID == menu1.FeatureID &&
                        c.Deleted == SMX.smx_IsNotDeleted &&
                        c.IsVisible == SMX.Status.Active)
                        .OrderBy(c => c.DisplayOrder).ToList();

                if (lstMenu2.Count > 0)
                {
                    HtmlControl liLevel1 = e.Item.FindControl("liLevel1") as HtmlGenericControl;
                    liLevel1.Visible = true;

                    HyperLink hplLevel1 = (HyperLink)e.Item.FindControl("hplLevel1");
                    hplLevel1.NavigateUrl = string.IsNullOrWhiteSpace(menu1.URL) ? "#" : menu1.URL;

                    Literal ltrIcon = (Literal)e.Item.FindControl("ltrIconLevel1");
                    ltrIcon.Text = string.Format("<span class=\"{0}\"></span>", menu1.CreatedOn);

                    Literal ltrMenuNameLevel1 = (Literal)e.Item.FindControl("ltrMenuNameLevel1");
                    ltrMenuNameLevel1.Text = menu1.Name;

                    // menu cap 3
                    Repeater rptLevel2 = (Repeater)e.Item.FindControl("rptLevel2");

                    rptLevel2.DataSource = lstMenu2;
                    rptLevel2.DataBind();
                }
                else
                {
                    HtmlControl liLevel1Alone = e.Item.FindControl("liLevel1Alone") as HtmlGenericControl;
                    liLevel1Alone.Visible = true;

                    HyperLink hplLevel1Alone = (HyperLink)e.Item.FindControl("hplLevel1Alone");
                    hplLevel1Alone.NavigateUrl = string.IsNullOrWhiteSpace(menu1.URL) ? "#" : menu1.URL;

                    Literal ltrIconLevel1Alone = (Literal)e.Item.FindControl("ltrIconLevel1Alone");
                    ltrIconLevel1Alone.Text = string.Format("<span class=\"{0}\"></span>", menu1.CreatedOn);

                    Literal ltrMenuNameLevel1Alone = (Literal)e.Item.FindControl("ltrMenuNameLevel1Alone");
                    ltrMenuNameLevel1Alone.Text = menu1.Name;

                    if (menu1.FeatureID == _selectedFeature)
                    {
                        hplLevel1Alone.CssClass = "active";
                    }
                }
            }
        }

        protected void rptLevel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Feature menu2 = e.Item.DataItem as Feature;

                HyperLink hplLevel2 = e.Item.FindControl("hplLevel2") as HyperLink;
                hplLevel2.NavigateUrl = menu2.URL;

                Literal ltrMenuNameLevel2 = e.Item.FindControl("ltrMenuNameLevel2") as Literal;
                ltrMenuNameLevel2.Text = menu2.Name;

                HtmlGenericControl spanIconLevel2 = e.Item.FindControl("spanIconLevel2") as HtmlGenericControl;
                spanIconLevel2.Attributes["class"] = menu2.CreatedOn;

                if (menu2.FeatureID == _selectedFeature)
                {
                    hplLevel2.CssClass = "active";
                }
            }
        }

        private void BuildMenu1()
        {
            List<Feature> lstMenu1 = GetListMenu1();

            rptLevel1.DataSource = lstMenu1;
            rptLevel1.DataBind();
        }

        private List<Feature> GetListMenu1()
        {
            _selectedFeature = FindActivingFeatureID();
            if (_selectedFeature == null)
            {
                return new List<Feature>();
            }

            List<Feature> lstFeature = Profiles.MyProfile.Features;

            lstFeature = lstFeature.Where(c =>
                c.ParentID == null &&
                c.IsVisible == SMX.Status.Active)
                .OrderBy(c => c.DisplayOrder).ToList();

            return lstFeature;
        }

        private int? FindActivingFeatureID()
        {
            UserProfile userProfile = Profiles.MyProfile;
            if (userProfile == null)
                return null;

            string url = Request.Url.PathAndQuery.ToLower();
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
    }
}