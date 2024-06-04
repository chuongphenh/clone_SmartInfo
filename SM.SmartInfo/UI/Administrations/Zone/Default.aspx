<%@ Page Title="Danh sách miền" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Zone.Default" %>

<%@ Register Src="~/UI/UserControls/DynamicReportUC.ascx" TagName="DynamicReportUC"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/ReportControl/ExportingPopupUC.ascx" TagName="ExportingPopup"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdID" runat="server" />
                <style>
                .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            font-weight:bold;
            color:#000;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <div class="toolbar">
        DANH SÁCH MIỀN
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">
                     <i class="fa fa-file-excel" aria-hidden="true" style="color: black; font-size: 16px;" title="Xuất Excel"></i>
                    <span>Xuất Excel</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="hypAdd" runat="server" Visible="false">
                    <i class="fa fa-plus" aria-hidden="true" style="color: black; font-size: 16px;" title="Tạo mới"></i>
                    <span>Tạo mới</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content grdDynamicReport">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <div class="table-container">
            <uc:DynamicReportUC ID="ucDynamicReport" runat="server" />
        </div>
    </div>
    <uc:ExportingPopup ID="popupExporting" runat="server" OnStartExport="popupExporting_StartExport" />

</asp:Content>
