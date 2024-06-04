<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchNegativeNewsUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.Search.SearchNegativeNewsUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<style>
    .da-phat-sinh {
        font-size: 14px;
        color: white;
        padding: 3px;
        padding-right: 15px;
        padding-left: 15px;
        border: 1px solid #FF4D4F;
        background: #FF4D4F;
        border-radius: 100px;
        display: -webkit-inline-box;
    }

    .chua-phat-sinh {
        font-size: 14px;
        color: white;
        padding: 3px;
        padding-right: 15px;
        padding-left: 15px;
        border: 1px solid #597EF7;
        background: #597EF7;
        border-radius: 100px;
        display: -webkit-inline-box;
    }

    .fa-negative-news-done {
        padding: 2px;
        padding-left: 3px;
        padding-right: 3px;
        border: 1px solid #389E0D;
        border-radius: 50%;
        background: #389E0D;
        color: white;
        font-size: 9px;
    }

    .fa-negative-news-inprogress {
        padding: 2px;
        padding-left: 3px;
        padding-right: 3px;
        border: 1px solid #EE6400;
        border-radius: 50%;
        background: #EE6400;
        color: white;
        font-size: 9px;
    }

    ::-webkit-scrollbar {
        background: #fff;
        border-radius: 4px;
        height: 5px;
        width: 5px;
    }

    ::-webkit-scrollbar-thumb {
        background: #e8e8e8;
        border-radius: 4px;
    }

    .no-touchevents {
        overflow-y: scroll;
    }

    span {
        font-size: 13px;
    }

    .description-list {
        height: 18px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 1;
        overflow: hidden;
        text-align: left;
    }

    .done {
        color: #389E0D !important;
    }

    .inprogress {
        color: #EE6400 !important;
    }

    .div-link:hover {
        cursor: pointer;
    }

    .div-active {
        background: #dcdfe4;
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
            <h3 style="font-family: SF Pro Display; font-size: 16px; font-weight: 600; padding: 0px; margin: 5px;">Sự vụ
                <span style="float: right">
                    <asp:LinkButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click"><i class="fa fa-angle-left"></i></asp:LinkButton>
                    <span style="padding-right: 10px; padding-left: 10px">Trang <%= hidPage.Value %></span>
                    <asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click"><i class="fa fa-angle-right"></i></asp:LinkButton>
                </span>
            </h3>
        </div>

        <div class="home-block-content">
            <ul class="home-notify" style="padding: 0px">
                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <div class="div-link" id="divLink" runat="server">
                            <div class="row" style="padding-left: 25px; padding-right: 25px; padding-top: 15px; padding-bottom: 10px">
                                <div class="col-md-12" style="padding-left: 0px; padding-right: 0px; text-align: center">
                                    <span style="float: left">
                                        <i id="iStatus" runat="server"></i><span style="font-size: 13px; font-weight: 600" id="spanStatus" runat="server">&nbsp;
                                        <asp:Literal ID="ltrStatus" runat="server"></asp:Literal></span>
                                    </span>
                                    <span style="float: right">
                                        <asp:Literal ID="ltrIncurredDTG" runat="server"></asp:Literal>
                                    </span>
                                </div>
                            </div>
                            <div class="row" style="padding-left: 25px; padding-right: 25px; padding-bottom: 15px">
                                <div class="col-sm-5" style="padding-left: 0px; padding-right: 0px; text-align: center">
                                    <span style="float: left; font-size: 14px" class="description-list">
                                        <asp:HyperLink ID="hypName" runat="server" Style="color: #262626"></asp:HyperLink>
                                    </span>
                                </div>
                                <div class="col-sm-7" style="padding-left: 0px; padding-right: 0px; text-align: center">
                                    <span style="float: right; margin-top: -2px;">
                                        <asp:Label Style="padding-top: 0px; padding-bottom: 0px" ID="ltrNegativeType" runat="server"></asp:Label>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-right: 25px; margin-left: 25px;">
                            <hr style="margin-top: 0px; margin-bottom: 0px">
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>