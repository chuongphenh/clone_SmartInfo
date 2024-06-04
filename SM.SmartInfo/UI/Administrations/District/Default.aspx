<%@ Page Title="Danh sách quận/huyện" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.District.Default" %>

<%@ Register Src="~/UI/UserControls/DynamicReportUC.ascx" TagName="DynamicReportUC"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/ReportControl/ExportingPopupUC.ascx" TagName="ExportingPopup"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdID" runat="server" />
    <div class="toolbar">
        DANH SÁCH QUẬN/HUYỆN
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">
                    <i class="fa fa-file-excel-o" aria-hidden="true" style="color: black; font-size: 16pt; margin-top: 5px;" title="Xuất Excel"> Xuất Excel</i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="hypAdd" runat="server" Visible="false">
                    <i class="fa fa-plus" aria-hidden="true" style="color: black; font-size: 16pt; margin-top: 5px;" title="Tạo mới"> Tạo</i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content grdDynamicReport">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <div class="table-container">
            <uc:DynamicReportUC runat="server" id="ucDynamicReport" />
        </div>
    </div>
    <uc:ExportingPopup ID="popupExporting" runat="server" OnStartExport="popupExporting_StartExport" />
</asp:Content>
