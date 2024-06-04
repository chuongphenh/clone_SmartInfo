<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchNotificationUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.Search.SearchNotificationUC" %>

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

    .aspNetDisabled {
        background: unset !important;
        color: darkgray !important;
        border-color: unset !important;
    }
</style>

<asp:HiddenField ID="hidPage" runat="server" />
<div class="home-col-3">
    <div class="home-block" style="">
        <div class="home-block-title" style="height: 60px;">
            <h3>Thông báo
                <span style="float: right">
                    <asp:LinkButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click"><i class="fa fa-angle-left"></i></asp:LinkButton>
                    <span style="padding-right: 10px; padding-left: 10px">Trang <%= hidPage.Value %></span>
                    <asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click"><i class="fa fa-angle-right"></i></asp:LinkButton>
                </span>
            </h3>
        </div>
        <%--CONTENT--%>
        <div class="home-block-content">
            <ul class="home-notify">
                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <li>
                            <div id="divDoDTG" runat="server" style="margin-right: 5px; padding-left: 10px; padding-right: 10px; width: auto">
                                <asp:Label ID="ltrDoDTG" runat="server"></asp:Label>
                            </div>
                            <div class="notify-note" style="color: #1D39C4">Ghi nhớ <span style="margin-left: 5px" class="circle-note">&nbsp;</span></div>
                            <div class="home-notify-content">
                                <h4 class="no-margin">
                                    <asp:HyperLink CssClass="notification-description" ID="hypContent" runat="server" Style="color: #1D39C4"></asp:HyperLink></h4>
                                <p style="color: #1D39C4" class="description-text">
                                    <asp:Literal ID="ltrNote" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <%--END CONTENT--%>
    </div>
</div>