<%@ Page Title="Danh sách tỉnh/thành phố" Language="C#" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SM.SmartInfo.UI.Administrations.Province.Default" %>

<%@ Register Src="~/UI/UserControls/DynamicReportUC.ascx" TagName="DynamicReportUC"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%--<%@ Register Src="~/UI/UserControls/ReportControl/ExportingPopupUC.ascx" TagName="ExportingPopup"
    TagPrefix="uc" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdID" runat="server" />
    <div class="toolbar">
        DANH SÁCH TỈNH/THÀNH PHỐ
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click">
                    <i class="fas fa-sync-alt new-button" aria-hidden="true" title="Làm mới"> Tạo</i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btnImport" runat="server" OnClick="btnImport_Click" UseSubmitBehavior="false">
                    <i class="fa fa-upload new-button" aria-hidden="true" title="Import"> Import</i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="hypAdd" runat="server" Visible="false">
                    <i class="fa fa-plus new-button" aria-hidden="true" title="Tạo mới"> Tạo</i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content grdDynamicReport">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <uc:DynamicReportUC ID="ucDynamicReport" runat="server" />
    </div>

    <tk:PopupPanel ID="popupUploadFile" runat="server" Title="IMPORT ĐỊA CHỈ"
        Width="500">
        <PopupTemplate>
            <div class="popup-data-content">
                <table class="tableDisplay" width="100%">
                    <colgroup>
                        <col width="150" />
                        <col />
                    </colgroup>
                    <tr>
                        <th>Mẫu import</th>
                        <td>
                            <asp:HyperLink ID="hplDownload" runat="server">Tải về tại đây</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <th>File Upload<span class="star">*</span>
                        </th>
                        <td>
                            <asp:FileUpload ID="fuImportExcel" runat="server" />
                            <i>(*.xls, *.xlsx)</i>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-footer">
                <ul>
                    <li>
                        <asp:Button ID="btnUploadFile" CssClass="btn-done" runat="server" Text="Import" OnClick="btnUploadFile_Click" UseSubmitBehavior="false" />
                    </li>
                </ul>
            </div>
        </PopupTemplate>
    </tk:PopupPanel>
    <%--<uc:ExportingPopup ID="popupExporting" runat="server" OnStartExport="popupExporting_StartExport" />--%>
</asp:Content>
