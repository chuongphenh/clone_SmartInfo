using System.Web.UI;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class TargetSearchBox : UserControl
    {
        #region Property

        public string SelectedValue
        {
            get
            {
                return this.ucTarget.SelectedValue;
            }
            set
            {
                this.ucTarget.SelectedValue = value;
            }
        }

        public string SelectedText
        {
            get
            {
                return this.ucTarget.SelectedText;
            }
            set
            {
                this.ucTarget.SelectedText = value;
            }
        }

        #endregion
    }
}