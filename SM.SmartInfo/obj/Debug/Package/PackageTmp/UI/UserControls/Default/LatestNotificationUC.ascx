<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestNotificationUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.Default.LatestNotificationUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<style>
    .span-date {
        padding-right: 10px;
        padding-left: 10px;
        padding-top: 9px;
        padding-bottom: 9px;
        border: 1px solid #F0F5FF;
        background: #F0F5FF;
        display: block;
        border-radius: 4px;
    }

    .description-text {
        height: 17px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 1;
        overflow: hidden;
    }

    .notification-description {
        max-height: 30px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
        overflow: hidden;
    }
</style>

<div class="home-col-3">
    <div class="home-block">
        <div class="home-block-title bg-home-block-title">
            <h3>
                <span class="title-right" style="margin-top: -5px;">
                    <span class="title-right-icon">
                        <i class="far fa-calendar-alt"></i>
                    </span>
                    <asp:DropDownList ID="ddlFilterTime" runat="server" Style="border: unset" OnSelectedIndexChanged="ddlFilterTime_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </span>
                Thông báo
            </h3>
        </div>
        <%--CONTENT--%>
        <div class="home-block-content home-block-content-custom">
            <ul class="home-notify">
                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <li>
                            <div id="divDoDTG" runat="server">
                                <asp:Label style="font-size: 13px;color:#979797" ID="ltrDoDTG" runat="server"></asp:Label></div>
                            <div class="notify-note" style="color: #242424">Ghi nhớ <span style="margin-left: 5px" class="circle-note">&nbsp;</span></div>
                            <div class="home-notify-content">
                                <h4 class="no-margin title-notifi">
                                    <asp:HyperLink CssClass="notification-description" ID="hypContent" runat="server"></asp:HyperLink></h4>
                                <p style="color: #242424" class="description-text">
                                    <asp:Literal ID="ltrNote" runat="server"></asp:Literal></p>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <%--END CONTENT--%>
    </div>
</div>