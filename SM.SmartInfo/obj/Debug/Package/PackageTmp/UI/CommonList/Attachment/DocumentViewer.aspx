﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentViewer.aspx.cs" MasterPageFile="~/UI/MasterPages/Common/Popup.Master"
    Inherits="SM.SmartInfo.UI.CommonList.Attachment.DocumentViewer" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="toolbar">
        XEM TÀI LIỆU
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="hypExit" runat="server" CssClass="icon-exit" OnClientClick='window.close();'>Thoát</asp:LinkButton>
            </li>
        </ul>
    </div>
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="content">
    </div>
</asp:Content>