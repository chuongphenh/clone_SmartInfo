<%@ Page Title="Danh sách nuớc sản xuất" Language="C#"   MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Country.Default" %>

<%@ Register Src="~/UI/UserControls/DynamicReportUC.ascx" TagName="DynamicReportUC"
    TagPrefix="uc" %>
<%--<%@ Register Src="~/UI/UserControls/ReportControl/ExportingPopupUC.ascx" TagName="ExportingPopup"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdID" runat="server" />
    <div class="toolbar">
        DANH SÁCH NƯỚC SẢN XUẤT
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="hypAdd" runat="server" CssClass="icon-new" Visible="false">Tạo mới</asp:HyperLink>
            </li>
            <%--<li>
                <asp:LinkButton ID="btnExport" runat="server" CssClass="icon-excel" OnClick="btnExport_Click">Xuất excel</asp:LinkButton>
            </li>--%>
        </ul>
    </div>
    <div class="content grdDynamicReport">
        <%--<div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>--%>
        <div class="table-container">
            <uc:DynamicReportUC runat="server" id="ucDynamicReport" />
        </div>
    </div>
    <%--<uc:ExportingPopup ID="popupExporting" runat="server" OnStartExport="popupExporting_StartExport" />--%>
</asp:Content>
