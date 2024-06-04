<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Title="Danh sách tài liệu"
    Inherits="SM.SmartInfo.UI.PopupPages.ListAttachments.Edit"
    MasterPageFile="~/UI/MasterPages/Common/Popup.Master" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/ListAttachments/AttachmentUC.ascx" TagName="AttachmentUC" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidRefID" />
    <asp:HiddenField runat="server" ID="hidRefType" />

    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>

    <uc:AttachmentUC ID="ucAttachment" runat="server" />
</asp:Content>