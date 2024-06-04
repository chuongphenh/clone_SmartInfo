<%@ Page Title="Danh sách đường/phố" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Street.Default" %>

<%@ Register Src="~/UI/UserControls/DynamicReportUC.ascx" TagName="DynamicReportUC"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/ReportControl/ExportingPopupUC.ascx" TagName="ExportingPopup"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdID" runat="server" />
    <div class="toolbar">
        DANH SÁCH ĐƯỜNG/PHỐ
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" Visible="false">
                    <i class="fa fa-file-excel-o" aria-hidden="true" style="color: black; font-size: 16pt; margin-top: 5px;" title="Xuất Excel"> Xuất Excel</i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="hypAdd" runat="server" Visible="false">
                    <i class="fa fa-plus" aria-hidden="true" style="color: black; font-size: 16pt; margin-top: 5px;" title="Tạo mới" > Tạo</i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content grdDynamicReport">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <div class="table-container">
            <div class="flex_search-section">
                <table class="flex_table-search">
                    <colgroup>
                        <col width="220" />
                        <col width="250" />
                        <col width="220" />
                        <col />
                    </colgroup>
                    <tr>
                        <td align="right" style="font-weight: bold">
                            Tỉnh/Thành Phố
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlProvince" runat="server" Width="220" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <tr>
                            <td align="right" style="font-weight: bold">
                                Quận/Huyện
                            </td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlDistrict" runat="server" Width="220" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="font-weight: bold">
                                Mã hoặc Tên
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="txtCodeOrName" Width="220" runat="server"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="Tìm Kiếm" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                </table>
            </div>
            <uc:DynamicReportUC ID="ucDynamicReport" runat="server" />
        </div>
    </div>
    <uc:ExportingPopup ID="popupExporting" runat="server" OnStartExport="popupExporting_StartExport" />
</asp:Content>
