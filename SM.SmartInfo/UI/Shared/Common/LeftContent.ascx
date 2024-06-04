<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftContent.ascx.cs"
    Inherits="SM.SmartInfo.UI.Shared.Common.LeftContent" %>

<%--<ul class="x-navigation">--%>
<ul class="x-navigation x-navigation-minimized">
    <asp:Repeater runat="server" ID="rptLevel1" OnItemDataBound="rptLevel1_ItemDataBound">
        <ItemTemplate>
            <li id="liLevel1" runat="server" class="xn-openable" visible="false">
                <asp:HyperLink runat="server" ID="hplLevel1">
                    <asp:Literal ID="ltrIconLevel1" runat="server"></asp:Literal>
                    <span class="xn-text" style="color: black; font-weight: bold">
                        <asp:Literal ID="ltrMenuNameLevel1" runat="server"></asp:Literal>
                    </span>
                </asp:HyperLink>
                <ul class="xn-submenu">
                    <asp:Repeater runat="server" ID="rptLevel2" OnItemDataBound="rptLevel2_ItemDataBound">
                        <ItemTemplate>
                            <li class="xn-submenuitem">
                                <asp:HyperLink runat="server" ID="hplLevel2" Style="margin-left: 0px">
                                    <div style="float: left" class="xn-icon">
                                        <span class="" runat="server" id="spanIconLevel2"></span>
                                    </div>
                                    <div>
                                        <span class="xn-text">
                                            <asp:Literal runat="server" ID="ltrMenuNameLevel2" />
                                        </span>
                                    </div>
                                </asp:HyperLink>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </li>
            <li id="liLevel1Alone" runat="server" visible="false">
                <asp:HyperLink runat="server" ID="hplLevel1Alone">
                    <asp:Literal runat="server" ID="ltrIconLevel1Alone" />
                    <span class="xn-text" style="color: black; font-weight: bold">
                        <asp:Literal ID="ltrMenuNameLevel1Alone" runat="server"></asp:Literal>
                    </span>
                </asp:HyperLink>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>