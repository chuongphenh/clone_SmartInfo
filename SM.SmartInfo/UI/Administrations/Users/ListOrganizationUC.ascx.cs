using SM.SmartInfo.Utils;
using System.Linq;
using System.Web.UI.WebControls;

namespace SM.CollateralManagement.UI.Administration.Users
{
    public partial class ListOrganizationUC : System.Web.UI.UserControl
    {
        public Unit Width
        {
            get { return ajsOrg.Width; }
            set { ajsOrg.Width = value; }
        }

        public System.Collections.Generic.List<SmartInfo.SharedComponent.Entities.Organization> SelectedValue
        {
            get
            {
                var lstItem = new System.Collections.Generic.List<SmartInfo.SharedComponent.Entities.Organization>();
                if (!string.IsNullOrWhiteSpace(ajsOrg.SelectedValue))
                {
                    string[] arrID = ajsOrg.SelectedValue.Split(',');
                    string[] arrText = ajsOrg.SelectedText.Split(',');
                    for (int i = 0; i < arrID.Length; i++)
                    {
                        var item = new SmartInfo.SharedComponent.Entities.Organization();
                        item.OrganizationID = Utility.GetNullableInt(arrID[i]);
                        if (i < arrText.Length)
                            item.Name = arrText[i];
                        lstItem.Add(item);
                    }
                }

                return lstItem;
            }
        }
    }
}