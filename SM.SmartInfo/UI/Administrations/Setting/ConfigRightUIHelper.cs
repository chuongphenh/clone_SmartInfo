using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.Administration;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SM.SmartInfo.UI.Administrations.Setting
{
    public class ConfigRightUIHelper
    {
        private HiddenField _hidDynamicColumn { get; set; }

        public ConfigRightUIHelper(HiddenField hidDynamicColumn)
        {
            _hidDynamicColumn = hidDynamicColumn;
        }

        public DataGrid DefineGridStructure(RightParam param)
        {
            var newGrid = new DataGrid();
            newGrid.ID = "grdMain";

            //Setup Setting
            newGrid.Width = Unit.Percentage(100);

            newGrid.ShowFooter = false;
            newGrid.AllowPaging = false;
            newGrid.AllowCustomPaging = false;
            newGrid.AutoGenerateColumns = false;
            newGrid.CssClass = "grid-main";
            newGrid.HeaderStyle.CssClass = "grid-header";
            newGrid.ItemStyle.CssClass = "grid-item-even";
            newGrid.AlternatingItemStyle.CssClass = "grid-item-odd";


            AddColumnTempateToGrid(newGrid, "Vai trò", new ItemTemplateLiteral("ltrName"), null, HorizontalAlign.Left, colWidth: 50);
            newGrid.Columns[0].ItemStyle.HorizontalAlign = HorizontalAlign.Left;

            AddColumnTempateToGrid(newGrid, "itemID", new ItemTemplateHiddenField("hidItemID"), isDisplay: false);

            //Add Column FeatureFunctionID
            AddColumnTempateToGrid(newGrid, "hidFunctionID", new ItemTemplateHiddenField("hidFunctionID"), isDisplay: false);

            //Build SubHeader in group
            BuildDynamicHeader(newGrid, param.Functions);

            return newGrid;
        }

        private void BuildDynamicHeader(DataGrid grdData, List<Function> items)
        {
            string strIDs = string.Empty;
            foreach (Function subItem in items)
            {
                TemplateColumn col = new TemplateColumn();

                col.HeaderText = subItem.Name;
                col.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                col.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                col.ItemStyle.Width = Unit.Pixel(50);
                col.ItemTemplate = new ItemTemplateCheckBox("ck" + subItem.FunctionID);
                strIDs += subItem.FunctionID.ToString() + "-";
                grdData.Columns.Add(col);
            }

            _hidDynamicColumn.Value = strIDs;
        }

        public void AddColumnTempateToGrid(DataGrid grid, string headerText, ITemplate itemTemplate, ITemplate footerTemplate = null,
                                           HorizontalAlign headerAlign = HorizontalAlign.Center, int colWidth = 50, bool isDisplay = true)
        {
            TemplateColumn newCol = new TemplateColumn();
            newCol.HeaderText = headerText;
            newCol.HeaderStyle.HorizontalAlign = headerAlign;
            newCol.FooterStyle.Width = Unit.Pixel(colWidth);
            newCol.ItemStyle.Width = Unit.Pixel(colWidth);
            newCol.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            newCol.Visible = isDisplay;
            newCol.ItemTemplate = itemTemplate;
            newCol.FooterTemplate = footerTemplate;
            grid.Columns.Add(newCol);
        }
    }
}