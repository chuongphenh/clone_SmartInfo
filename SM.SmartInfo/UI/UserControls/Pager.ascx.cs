using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace SM.SmartInfo.UI.UserControls
{
    public class PageItem
    {
        public int PageIndex { get; set; }
        public int Position { get; set; }
    }

    public delegate void PageIndexChangedEventHandler(object source, DataGridPageChangedEventArgs e);

    public partial class Pager : UserControl
    {
        private const int PageIn = 0;
        private const int First = 1;
        private const int Before = 2;
        private const int After = 3;
        private const int Last = 4;

        public event PageIndexChangedEventHandler PageIndexChanged;

        public int? CurrentIndex
        {
            get { return Utils.Utility.GetNullableInt(hidCurrentIndex.Value); }
            set { hidCurrentIndex.Value = value.ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BuildPager(int VitualItemCount, int PageSize, int PageIndex, int NumberOfPage = 10)
        {
            this.CurrentIndex = PageIndex;

            int PageNumber = VitualItemCount / PageSize;
            if (VitualItemCount % PageSize > 0) PageNumber++;

            bool HaveBefore = false;
            bool HaveAfter = false;
            bool ShowFirst = false;
            bool ShowLast = false;

            int fromIndex = ((PageIndex - 1) / NumberOfPage) * NumberOfPage + 1;
            int toIndex = ((PageIndex - 1) / NumberOfPage) * NumberOfPage + NumberOfPage;

            if (fromIndex > 1)
            {
                HaveBefore = true;
                ShowFirst = true;
            }

            if (toIndex >= PageNumber)
            {
                toIndex = PageNumber;
            }
            else
            {
                HaveAfter = true;
                ShowLast = true;
            }

            List<PageItem> lstPage = new List<PageItem>();
            if (ShowFirst)
                lstPage.Add(new PageItem() { PageIndex = 1, Position = First });

            if (HaveBefore)
                lstPage.Add(new PageItem() { PageIndex = fromIndex - 1, Position = Before });

            for (int i = fromIndex; i <= toIndex; i++)
                lstPage.Add(new PageItem() { PageIndex = i, Position = PageIn });

            if (HaveAfter && (toIndex + 1 != PageNumber))
                lstPage.Add(new PageItem() { PageIndex = toIndex + 1, Position = After });

            if (ShowLast)
                lstPage.Add(new PageItem() { PageIndex = PageNumber, Position = Last });

            rptPageLink.DataSource = lstPage;
            rptPageLink.DataBind();
        }

        protected void rptPageLink_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PageItem page = (PageItem)e.Item.DataItem;
                LinkButton btnPage = e.Item.FindControl("btnPage") as LinkButton;
                btnPage.CommandArgument = page.PageIndex.ToString();
                switch (page.Position)
                {
                    case PageIn:
                        btnPage.Text = page.PageIndex.ToString();
                        break;
                    case First:
                        btnPage.Text = "<<";
                        btnPage.ToolTip = "Trang đầu";
                        break;
                    case Before:
                        btnPage.Text = "<";
                        btnPage.ToolTip = "Trang trước";
                        break;
                    case After:
                        btnPage.Text = ">";
                        btnPage.ToolTip = "Trang sau";
                        break;
                    case Last:
                        btnPage.Text = ">>";
                        btnPage.ToolTip = "Trang cuối";
                        break;
                }

                if (page.PageIndex == this.CurrentIndex)
                    btnPage.Enabled = false;
            }
        }

        protected void btnPage_Click(object sender, EventArgs e)
        {
            LinkButton btnPage = (LinkButton)sender;
            if (PageIndexChanged != null)
                PageIndexChanged(btnPage, new DataGridPageChangedEventArgs(btnPage, int.Parse(btnPage.CommandArgument)));
        }
    }
}