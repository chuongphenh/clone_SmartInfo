using System;
using SM.SmartInfo.SharedComponent.Constants;

namespace SM.SmartInfo.UI.UserControls.ReportControl
{
    public partial class ExportingPopupUC : BaseUserControl
    {
        public event EventHandler<EventArgs> StartExport;

        #region Public property

        public string ReportDisplayName
        {
            get
            {
                return (string)ViewState["ReportDisplayName"];
            }
            set
            {
                ViewState["ReportDisplayName"] = value;
            }
        }

        #endregion

        #region Events

        protected override void OnPreRender(EventArgs e)
        {
            txtDisplayName.Text = ReportDisplayName;

            base.OnPreRender(e);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (StartExport != null)
            {
                ReportDisplayName = txtDisplayName.Text;
                StartExport(sender, e);
            }

            if (chkRedirectToDownloadPage.Checked)
            {
                Response.Redirect(PageURL.ReportPages.DownloadReport);
            }
            else
            {
                popupExporting.Hide();
                //Response.Redirect(Request.Url.AbsoluteUri);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popupExporting.Hide();
            //Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void popupExporting_PopupClosed(object sender, EventArgs e)
        {
            //Response.Redirect(Request.Url.AbsoluteUri);
        }

        #endregion

        #region Public methods

        public void Show()
        {
            popupExporting.Show();
        }

        #endregion
    }
}