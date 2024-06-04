using System.Web.UI;

namespace SM.SmartInfo.UI.UserControls
{
    public partial class EmployeeSearchBox : UserControl
    {
        #region Property

        public string SelectedValue
        {
            get
            {
                return this.ucEmployee.SelectedValue;
            }
            set
            {
                this.ucEmployee.SelectedValue = value;
            }
        }

        public string SelectedText
        {
            get
            {
                return this.ucEmployee.SelectedText;
            }
            set
            {
                this.ucEmployee.SelectedText = value;
            }
        }

        #endregion
    }
}