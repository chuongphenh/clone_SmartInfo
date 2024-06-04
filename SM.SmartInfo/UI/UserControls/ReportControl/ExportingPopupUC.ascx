<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportingPopupUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.ReportControl.ExportingPopupUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="sm" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<sm:PopupPanel ID="popupExporting" runat="server" Width="550" Title="Xuất báo cáo"
    OnPopupClosed="popupExporting_PopupClosed">
    <popuptemplate>
        <asp:Panel runat="server" DefaultButton="btnExport">
            <div class="err">
                <uc:ErrorMessage ID="ucErr" runat="server" ValidationGroup="PopupExportingPopup" />
            </div>
            <div class="popup-data-content">
                <div class="row form-group">
                    <label class="col-md-3 control-label">
                        Tên báo cáo<span class="star">*</span></label>
                    <div class="col-md-9">
                        <asp:TextBox ID="txtDisplayName" runat="server" Width="400" MaxLength="128" ValidationGroup="PopupExportingPopup"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDisplayName" ErrorMessage="[Tên báo cáo] không được phép rỗng"
                            Display="None" ValidationGroup="PopupExportingPopup"></asp:RequiredFieldValidator>
                        <asp:CheckBox ID="chkRedirectToDownloadPage" runat="server" Text="Tự động chuyển hướng đến trang tải báo cáo." />
                    </div>
                </div>
            </div>
            <div class="popup-footer">
                <ul>
                    <li>
                        <asp:Button CssClass="btn-done" ID="btnExport" runat="server" Text="Đồng ý" OnClick="btnExport_Click"
                            ValidationGroup="PopupExportingPopup" /></li>
                    <li>
                        <asp:Button CssClass="btn-cancel" ID="btnCancel" runat="server" Text="Bỏ qua" OnClick="btnCancel_Click"
                            CausesValidation="false" /></li>
                </ul>
            </div>
        </asp:Panel>
    </popuptemplate>
</sm:PopupPanel>