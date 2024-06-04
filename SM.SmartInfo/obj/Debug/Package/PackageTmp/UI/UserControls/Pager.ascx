<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="SM.SmartInfo.UI.UserControls.Pager" %>

<asp:HiddenField ID="hidCurrentIndex" runat="server" />

<asp:Repeater ID="rptPageLink" runat="server" OnItemDataBound="rptPageLink_ItemDataBound">
    <ItemTemplate>
        <li><asp:LinkButton runat="server" ID="btnPage" OnClick="btnPage_Click"></asp:LinkButton></li>
    </ItemTemplate>
</asp:Repeater>