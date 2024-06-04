<%@ Page Title="Lịch sử truy cập" Language="C#" AutoEventWireup="true" CodeBehind="LogView.aspx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Users.LogView" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>
<%@ Register Src="~/UI/UserControls/ReportControl/ExportingPopupUC.ascx" TagName="ExportingPopup"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/DynamicReportUC.ascx" TagName="DynamicReportUC"
    TagPrefix="uc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="toolbar">
        LỊCH SỬ TRUY CẬP
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click"><i class="fa fa-file-excel-o" aria-hidden="true" style="color: black; font-size: 16pt; margin-top: 5px;" title="Xuất Excel"></i></asp:LinkButton>
            </li>
        </ul>
    </div>
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>

    <div class="content grdDynamicReport">
        <uc:DynamicReportUC ID="ucDynamicReport" runat="server" />
    </div>
    <uc:ExportingPopup ID="popupExporting" runat="server" OnStartExport="popupExporting_StartExport" />
</asp:Content>