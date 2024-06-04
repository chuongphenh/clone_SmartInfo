<%@ Page Title="Cấu hình các ngày đặc biệt" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SM.SmartInfo.UI.Administration.Anniversarys.Default" %>

<%@ Register Src="~/UI/UserControls/DynamicReportUC.ascx" TagName="DynamicReportUC"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/ReportControl/ExportingPopupUC.ascx" TagName="ExportingPopup"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
       .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            font-weight:bold;
            color: #000;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <asp:HiddenField ID="hdID" runat="server" />
    <div class="toolbar">
        CẤU HÌNH CÁC NGÀY KỶ NIỆM, TRUYỀN THỐNG
        <ul class="icon_toolbar">
<%--            <li>
                <asp:LinkButton ID="btnExport" runat="server" CssClass="icon-excel" OnClick="btnExport_Click">Xuất excel</asp:LinkButton>
            </li>--%>
            <li>
                <asp:HyperLink ID="hypAdd" runat="server">
                    <i class="fa fa-plus" aria-hidden="true" title="Tạo mới"> </i>
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
