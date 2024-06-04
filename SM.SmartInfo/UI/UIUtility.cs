using System;
using System.Linq;
using System.Web.UI;
using SoftMart.Core.UIControls;
using System.Web.UI.WebControls;
using SoftMart.Kernel.Exceptions;
using System.Collections.Generic;
using SM.SmartInfo.Utils;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.CacheManager;

namespace SM.SmartInfo.UI
{
    public static class UIUtility
    {
        #region Dropdownlist
        public static void BindListToDropDownList<T>(DropDownList control, List<T> lstData, bool defautIsEmpty = true)
        {
            control.DataSource = lstData == null ? new List<T>() : lstData;
            control.DataBind();
            if (defautIsEmpty)
                control.Items.Insert(0, new ListItem(string.Empty, string.Empty));
        }

        public static void BindListToDropDownList<T>(DropDownList control, List<T> lstData, string dataValueField, string dataTextField, bool defautIsEmpty = true)
        {
            control.DataSource = lstData == null ? new List<T>() : lstData;
            control.DataValueField = dataValueField;
            control.DataTextField = dataTextField;
            control.DataBind();

            if (defautIsEmpty)
                control.Items.Insert(0, new ListItem(string.Empty, string.Empty));
        }

        public static void BindDicToDropDownList<T1, T2>(DropDownList control, Dictionary<T1, T2> dicData, bool defautIsEmpty = true)
        {
            control.Items.Clear();
            control.DataSource = dicData;
            control.DataTextField = SMX.smx_Value;
            control.DataValueField = SMX.smx_Key;
            control.DataBind();

            if (defautIsEmpty)
                control.Items.Insert(0, new ListItem(string.Empty, string.Empty));
        }

        public static void BindSPToDropDownList(DropDownList control, List<SystemParameter> lstData, bool defautIsEmpty = true)
        {
            control.DataTextField = "Name";
            control.DataValueField = "SystemParameterID";
            control.Items.Clear();
            if (defautIsEmpty)
                control.Items.Add(new ListItem("", ""));
            if (lstData != null)
            {
                foreach (SystemParameter sys in lstData)
                {
                    control.Items.Add(new ListItem(sys.Name, sys.SystemParameterID.ToString()));
                }
            }
        }

        public static int? GetDropListNullableIntValue(DropDownList drop)
        {
            return Utility.GetNullableInt(drop.SelectedValue);
        }

        public static void SetDropDownListValue(DropDownList drop, decimal? value)
        {
            foreach (ListItem item in drop.Items)
            {
                if (Utility.GetNullableDecimal(item.Value) == value)
                {
                    drop.SelectedValue = item.Value;
                }
            }
        }

        public static void SetDropDownListValue(DropDownList drop, int? value)
        {
            string strValue = Utility.GetString(value);
            SetDropDownListValue(drop, strValue);
        }

        public static void SetDropDownListValue(DropDownList drop, bool? value)
        {
            string strValue = Utility.GetString(value);
            SetDropDownListValue(drop, strValue);
        }

        public static void SetDropDownListValue(DropDownList drop, string value)
        {
            if (drop != null)
            {
                if (drop.Items.FindByValue(value) != null)
                {
                    drop.SelectedValue = value;
                }
                else
                {
                    if (drop.Items.FindByValue(string.Empty) != null)
                    {
                        drop.SelectedValue = string.Empty;
                    }
                }
            }
        }

        public static void SetRadioListValue(RadioButtonList rdo, int? value)
        {
            string strValue = Utility.GetString(value);
            SetRadioListValue(rdo, strValue);
        }

        public static void SetRadioListValue(RadioButtonList rdo, string value)
        {
            if (rdo != null)
            {
                if (rdo.Items.FindByValue(value) != null)
                {
                    rdo.SelectedValue = value;
                }
                else
                {
                    if (rdo.Items.FindByValue(string.Empty) != null)
                    {
                        rdo.SelectedValue = string.Empty;
                    }
                }
            }
        }

        public static void BindSPToRadioButtonList(RadioButtonList rdoList, List<SystemParameter> lstSysParam, int indexActive = -1)
        {
            rdoList.DataTextField = "Name";
            rdoList.DataValueField = "SystemParameterID";
            rdoList.Items.Clear();
            if (lstSysParam != null)
            {
                foreach (SystemParameter sys in lstSysParam)
                {
                    rdoList.Items.Add(new ListItem(sys.Name, sys.SystemParameterID.ToString()));
                }
            }
            if (rdoList.Items.Count >= 0 && rdoList.Items.Count >= indexActive && indexActive >= 0)
                rdoList.SelectedIndex = indexActive;
            else
                rdoList.ClearSelection();
        }
        #endregion

        #region ComboCheckBox
        public static void BindDicToComboBox<T1, T2>(ComboCheckBox control, Dictionary<T1, T2> dicData, bool isSelectedAll = true)
        {
            control.DataSource = dicData;
            control.DataTextField = SMX.smx_Value;
            control.DataValueField = SMX.smx_Key;
            control.DataBind();

            if (isSelectedAll)
            {
                foreach (ListItem lstITem in control.Items)
                    lstITem.Selected = true;
            }
        }

        public static void SetComboboxChecked(ComboCheckBox control, int? number)
        {
            if (number.HasValue)
            {
                foreach (ListItem lstITem in control.Items)
                {
                    if (string.IsNullOrEmpty(lstITem.Value))
                        continue;
                    int itemValue = int.Parse(lstITem.Value);
                    lstITem.Selected = (itemValue & number.Value) == itemValue;
                }
            }
        }

        public static int? GetSumSelectedComboCheckbox(ComboCheckBox control)
        {
            if (control.CheckedItems.Count == 0)
                return null;

            int value = 0;
            foreach (ListItem checkedItem in control.CheckedItems)
            {
                value |= int.Parse(checkedItem.Value);
            }
            return value;
        }

        public static List<int> GetListSelectedComboCheckbox(ComboCheckBox control)
        {
            List<int> lstValue = new List<int>();
            if (control.CheckedItems.Count == 0)
                return lstValue;

            foreach (ListItem checkedItem in control.CheckedItems)
                lstValue.Add(int.Parse(checkedItem.Value));

            return lstValue;
        }

        public static void BindComboBox<T>(DropDownList cboControl, List<T> values, bool defautIsEmpty = true)
        {
            cboControl.DataSource = values;
            cboControl.DataBind();

            if (defautIsEmpty)
                cboControl.Items.Insert(0, new ListItem(string.Empty, string.Empty));
        }

        public static void BindSystemComboBox(DropDownList rcbCombo, int featureID, bool defautIsEmpty = true)
        {
            var lst = GlobalCache.GetListSystemParameterByFeatureID(featureID, true);
            lst = lst.OrderBy(c => c.DisplayOrder).ToList();
            BindSPToDropDownList(rcbCombo, lst, defautIsEmpty);
        }

        public static void BindSystemComboBox(DropDownList rcbCombo, List<SystemParameter> lstSysParam, bool defautIsEmpty = true)
        {
            rcbCombo.DataTextField = "Name";
            rcbCombo.DataValueField = "SystemParameterID";
            rcbCombo.Items.Clear();
            if (defautIsEmpty)
                rcbCombo.Items.Add(new ListItem("", ""));
            if (lstSysParam != null)
            {
                foreach (SystemParameter sys in lstSysParam)
                {
                    rcbCombo.Items.Add(new ListItem(sys.Name, sys.SystemParameterID.ToString()));
                }
            }
        }

        public static void BindDicToRadioButtonList<T1, T2>(RadioButtonList rdoControl, Dictionary<T1, T2> dicValues, int indexActive = 0)
        {
            rdoControl.DataSource = dicValues;
            rdoControl.DataTextField = SMX.smx_Value;
            rdoControl.DataValueField = SMX.smx_Key;
            rdoControl.DataBind();

            if (rdoControl.Items.Count >= 0 && rdoControl.Items.Count >= indexActive && indexActive >= 0)
                rdoControl.SelectedIndex = indexActive;
        }
        #endregion

        #region DataGrid
        public static void BindDataGrid<T>(DataGrid grdMain, List<T> lstItem, int? recordCount = null)
        {
            if (lstItem == null)
                lstItem = new List<T>();

            if (recordCount == null)
                recordCount = lstItem.Count;

            grdMain.VirtualItemCount = recordCount.Value;
            grdMain.DataSource = lstItem;
            if (grdMain.CurrentPageIndex > (recordCount / grdMain.PageSize)) // truong hop thay doi filter va click vao pageIndex
                grdMain.CurrentPageIndex = 0;

            grdMain.DataBind();
        }

        public static void SetGridItemHidden(Control item, string hiddenID, object value)
        {
            HiddenField hidden = (HiddenField)item.FindControl(hiddenID);

            if (hidden == null)
            {
                throw new Exception("HiddenField: " + hiddenID + " is not existed.");
            }

            if (value == null)
                hidden.Value = string.Empty;
            else
                hidden.Value = value.ToString();
        }

        public static void SetGridItemIText(Control item, string controlID, object value)
        {
            ITextControl ctr = (ITextControl)item.FindControl(controlID);

            if (ctr == null)
            {
                throw new Exception("Control: " + controlID + " is not existed.");
            }

            if (value != null)
            {
                Type type = value.GetType();

                if (type == typeof(DateTime))
                {
                    ctr.Text = Utils.Utility.GetDateString((DateTime)value);
                    return;
                }

                if (type == typeof(decimal))
                {
                    ctr.Text = Utils.Utility.GetString((decimal)value);
                    return;
                }

                // Other type
                ctr.Text = value.ToString();
            }
            else
                ctr.Text = string.Empty;
        }

        public static void SetGridItemDatePicker(Control item, string controlID, DateTime? value)
        {
            DatePicker ctr = (DatePicker)item.FindControl(controlID);
            ctr.SelectedDate = value;
        }

        public static void SetGridItemNumeric(Control item, string controlID, decimal? value)
        {
            NumericTextBox ctr = (NumericTextBox)item.FindControl(controlID);
            ctr.Value = (double?)value;
        }

        public static string GetGridItemHiddenValue(Control item, string hiddenID)
        {
            HiddenField hidden = (HiddenField)item.FindControl(hiddenID);
            if (hidden == null)
            {
                throw new Exception("HiddenField is not found: " + hiddenID);
            }
            return hidden.Value;
        }

        public static int? GetGridItemHiddenInt(Control item, string hiddenID)
        {
            string value = GetGridItemHiddenValue(item, hiddenID);

            return Utility.GetNullableInt(value);
        }

        public static string GetGridItemITextValue(DataGridItem item, string controlID)
        {
            ITextControl ctr = (ITextControl)item.FindControl(controlID);
            if (ctr == null)
            {
                throw new Exception("ITextControl is not found: " + controlID);
            }
            return ctr.Text;
        }

        public static DateTime? GetGridItemDatePicker(DataGridItem item, string controlID)
        {
            DatePicker ctr = (DatePicker)item.FindControl(controlID);
            if (ctr == null)
            {
                throw new Exception("DatePicker is not found: " + controlID);
            }
            return ctr.SelectedDate;
        }

        public static decimal? GetGridItemNumeric(Control item, string controlID)
        {
            NumericTextBox ctr = (NumericTextBox)item.FindControl(controlID);
            if (ctr == null)
            {
                throw new Exception("NumericTextBox is not found: " + controlID);
            }
            return (decimal?)ctr.Value;
        }

        /// <summary>
        /// Tìm ra dòng datagrid chứa control child
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public static DataGridItem FindDataGridItemByChild(Control child)
        {
            if (child == null)
            {
                return null;
            }

            Control parent = child.Parent;
            while (true)
            {
                if (parent != null)
                {
                    if (parent is DataGridItem)
                    {
                        return (DataGridItem)parent;
                    }
                    else
                    {
                        parent = parent.Parent;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public static Control FindControlInDataGridFooter(DataGrid dgv, string controlName)
        {
            Control ctrl = new Control();
            foreach (Control c in dgv.Controls)
            {
                foreach (Control ct in c.Controls)
                {
                    if (ct is DataGridItem)
                    {
                        DataGridItem di = (DataGridItem)ct;
                        if (di.ItemType == ListItemType.Footer)
                        {
                            ctrl = di.FindControl(controlName);
                            return ctrl;
                        }
                    }
                }
            }
            return ctrl;
        }
        #endregion

        #region Repeater
        public static void SetRepeaterItemIText(RepeaterItem item, string controlID, object value)
        {
            ITextControl ctr = (ITextControl)item.FindControl(controlID);

            if (value != null)
            {
                Type type = value.GetType();

                if (type == typeof(DateTime))
                {
                    ctr.Text = Utils.Utility.GetDateString((DateTime)value);
                    return;
                }

                if (type == typeof(decimal))
                {
                    ctr.Text = Utils.Utility.GetString((decimal)value);
                    return;
                }

                // Other type
                ctr.Text = value.ToString();
            }
            else
            {
                ctr.Text = string.Empty;
            }
        }
        #endregion

        #region Param
        public static string GetParamId(string paramName = SMX.Parameter.ID)
        {
            string encodedVal = System.Web.HttpContext.Current.Request.Params[paramName];
            if (string.IsNullOrEmpty(encodedVal))
                throw new SMXException(Messages.LinkNotExisted);

            return HtmlUtils.DecodeHtml(encodedVal);
        }

        public static int GetIntIdParam(string paramName = SMX.Parameter.ID)
        {
            string value = System.Web.HttpContext.Current.Request.Params[paramName];
            int id;
            if (int.TryParse(value, out id))
            {
                return id;
            }
            else
            {
                throw new SMXException(Messages.LinkNotExisted);
            }
        }

        public static string GetNullParamId(string paramName = SMX.Parameter.ID)
        {
            string encodedVal = System.Web.HttpContext.Current.Request.Params[paramName];
            if (string.IsNullOrEmpty(encodedVal))
                return null;

            return HtmlUtils.DecodeHtml(encodedVal);
        }
        #endregion

        #region Convert breakline
        public static string ConvertBreakLine2Html(string text, bool isIncludeEncode = true)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            string html = text;
            if (isIncludeEncode)
                html = HtmlUtils.EncodeHtml(html);
            html = html.Replace("\r\n", "<br/>");
            html = html.Replace("\n", "<br/>");

            return html;
        }

        public static string ConvertHtml2BreakLine(string html, bool isIncludeDecode = true)
        {
            if (string.IsNullOrWhiteSpace(html))
                return html;

            string text = html;
            if (isIncludeDecode)
                text = HtmlUtils.DecodeHtml(text);
            text = text.Replace("<br/>", "\r\n");

            return text;
        }
        #endregion

        #region Popup
        public static void OpenPopupWindow(Page page, string url, int? width = 800, int? height = 600)
        {
            string script = string.Format("window.open('{0}', '', 'toolbar=no, location=no, directories=no, status=no, menubar=yes, scrollbars=yes, resizable=yes, copyhistory=no, width={1}, height={2}');", url, width, height);
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "OpenPopupAttachment", script, true);
        }

        public static string GetPopupLink(string link)
        {
            Control ctr = new Control();
            link = ctr.ResolveUrl(link);
            return string.Format("PopupCenter('{0}', '', 1000, 700);", link);
        }

        public static string GetPopupLink(string link, int? width = 1000, int? height = 700)
        {
            Control ctr = new Control();
            link = ctr.ResolveUrl(link);
            return string.Format("PopupCenter('{0}', '', {1}, {2});", link, width, height);
        }

        public static string GetPopupLinkClientClick(string link)
        {
            Control ctr = new Control();
            link = ctr.ResolveUrl(link);
            return string.Format("javascript:PopupCenter('{0}', '', 1000, 500);return false;", link);
        }

        public static string GetPopupLinkClientClick(string link, int? width = 1000, int? height = 500)
        {
            Control ctr = new Control();
            link = ctr.ResolveUrl(link);
            return string.Format("javascript:PopupCenter('{0}', '', {1}, {2});return false;", link, width, height);
        }

        public static string BuildHyperlinkWithPopup(string text, string nativeUrl, int? width = 1000, int? height = 700, string css = "")
        {
            string url = GetPopupLink(nativeUrl, width, height);
            string anchorClientID = string.Format("linkPop{0}", Guid.NewGuid().ToString());
            string cssClass = "";
            if (!string.IsNullOrWhiteSpace(css))
                cssClass = "class=\"" + css + "\"";

            return string.Format("<a href=\"#{2}\" onclick=\"javascript:{0}\" {3}>{1}</a>", url, text, anchorClientID, cssClass);
        }

        public static string BuildHyperlinkWithAnchorTag(string nativeUrl, int? width = 1000, int? height = 700)
        {
            string url = GetPopupLink(nativeUrl, width, height);
            return string.Format("javascript:{0}", url);
        }
        #endregion

        #region Script manager
        public static List<string> GetListLdapCnnName()
        {
            List<string> lstLdapCnnName = new List<string>();
            var lstCnn = System.Configuration.ConfigurationManager.ConnectionStrings;
            foreach (System.Configuration.ConnectionStringSettings cnn in lstCnn)
            {
                if (cnn.ConnectionString.StartsWith("LDAP", StringComparison.OrdinalIgnoreCase))
                    lstLdapCnnName.Add(cnn.Name);
            }

            return lstLdapCnnName;
        }

        public static void RegisterPostBackControl(Page page, params Control[] arrControl)
        {
            if (arrControl == null || arrControl.Length == 0 || page == null)
                return;

            ScriptManager scriptManager = ScriptManager.GetCurrent(page);
            if (scriptManager != null)
            {
                foreach (Control control in arrControl)
                    scriptManager.RegisterPostBackControl(control);
            }
        }
        #endregion

        #region Date
        public static void SetValueForDatePicker(DatePicker control, DateTime? dt)
        {
            DateTime? maxDate = control.MaxDate;
            DateTime? minDate = control.MinDate;

            if (dt < minDate || dt > maxDate)
                control.SelectedDate = null;
            else
                control.SelectedDate = dt;
        }

        public static bool IsOverSLA(DateTime slaEndDTG, int plusMinute = 0)
        {
            var remainTime = SoftMart.Core.Workflow.CalendarApi.GetActualProcessingTime(DateTime.Now,
                slaEndDTG, SoftMart.Core.Workflow.SharedComponent.Constants.WF_SMX.DurationType.Hour);
            remainTime = remainTime * 60;

            return remainTime < plusMinute;
        }
        #endregion
    }
}