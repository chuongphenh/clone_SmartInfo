<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBarUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.News.SideBarUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<div class="list-news-follow" style="padding-top: 15px; padding-bottom: 15px; display: none">
    <h3 style="font-size: 24px; font-weight: bold; font-family: SF Pro Display; color: #000; margin-bottom: 20px;">Tin khác</h3>
    <ul id="ulSideBar">
        <asp:Repeater ID="rptDataFollow" runat="server" OnItemDataBound="rptDataFollow_ItemDataBound">
            <ItemTemplate>
                <li class="news-follow" style="border-bottom: 1px solid #E5E5E5; padding-bottom: 10px">
                    <asp:HyperLink class="news-follow-text" ID="hypName" runat="server" style="margin-bottom: 5px"></asp:HyperLink>
                    <p style="font-family: SF Pro Display">
                        <asp:Literal ID="ltrPostingFromDTG" runat="server"></asp:Literal>
                    </p>
                    <asp:HyperLink ID="hypLink" runat="server">
                        <img id="imgRpt" runat="server" src="" alt="" style="height: 220px; object-fit: cover" />
                    </asp:HyperLink>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>