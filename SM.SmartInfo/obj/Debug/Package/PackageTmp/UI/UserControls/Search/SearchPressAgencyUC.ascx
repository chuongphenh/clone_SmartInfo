<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPressAgencyUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.Search.SearchPressAgencyUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<style>
    .col-md-10 > p {
        margin-bottom: 5px;
    }

    tab1 {
        padding-left: 10px;
    }

    .negative {
        color: white;
        padding: 0 10px;
        border: 1px solid red;
        border-radius: 50px;
        background: red;
        font-size: 13px;
    }

    .medium {
        color: black;
        padding: 0 10px;
        border: 1px solid orange;
        border-radius: 50px;
        background: orange;
        font-size: 13px;
    }

    .positive {
        color: white;
        padding: 0 10px;
        border: 1px solid limegreen;
        border-radius: 50px;
        background: limegreen;
        font-size: 13px;
    }

    table.table > thead > tr > th {
        text-align: center;
    }

    table.table > tbody > tr > td.center {
        text-align: center;
    }

    .holder_default {
        border: 3px dashed #ccc;
    }

    #holder.hover {
        border: 3px dashed #0c0 !important;
    }

    .hidden {
        visibility: hidden;
    }

    .visible {
        visibility: visible;
    }

    .custom-file-input {
        opacity: 0;
        z-index: 2;
        position: absolute;
    }

    .custom-file-label {
        z-index: 1;
        padding: .375rem .75rem;
        font-weight: 400;
        line-height: 1.5;
        color: #495057;
        background-color: #fff;
        border: 1px solid #ced4da;
        border-radius: .25rem;
        margin-bottom: 10px;
        width: 100%;
    }

    .div-link:hover {
        cursor: pointer;
    }

    .div-active {
        background: #dcdfe4;
        color: #1D39C4;
    }

        .div-active > li > #text-right > .notify-employee {
            color: #1D39C4;
        }

            .div-active > li > #text-right > .notify-employee > span {
                background: #1D39C4;
            }

            .div-active > li > #text-right > .notify-employee > .fa-user {
                color: white;
            }

        .div-active > li > #text-right > h2 > a {
            color: #1D39C4 !important;
            font-weight: 600 !important;
        }

        .div-active > li > #text-right > h2 > span {
            color: #1D39C4 !important;
            font-weight: 600 !important;
        }

        .div-active > li > #text-right > p {
            color: #1D39C4 !important;
        }

    .address-detail {
        height: 31px;
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
            <h3 style="font-family: SF Pro Display; font-size: 16px; font-weight: 600; padding: 0px; margin: 5px;">Danh sách tổ chức
                <span style="float: right">
                    <asp:LinkButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click"><i class="fa fa-angle-left"></i></asp:LinkButton>
                    <span style="padding-right: 10px; padding-left: 10px">Trang <%= hidPage.Value %></span>
                    <asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click"><i class="fa fa-angle-right"></i></asp:LinkButton>
                </span>
            </h3>
        </div>
        <div class="home-block-content">
            <ul class="home-notify" style="padding: 0px">
                <asp:Repeater ID="rptPressAgencies" runat="server" OnItemDataBound="rptPressAgencies_ItemDataBound">
                    <ItemTemplate>
                        <!--Blog Post-->
                        <div class="div-link" id="divLink" runat="server">
                            <li class="row blog blog-medium margin-bottom-40" style="padding-right: 20px; padding-left: 20px;">
                                <div class="col-md-2">
                                    <img id="img" runat="server" class="img-responsive image-ava" src="" alt="" style="width: 100%;">
                                </div>
                                <div id="text-right" class="col-md-10" style="padding-right: 0px">
                                    <div class="notify-employee">
                                        <asp:Literal ID="ltrCountHR" runat="server"></asp:Literal>
                                        nhân sự
                                        <span>
                                            <i class="fa fa-user"></i>
                                        </span>
                                    </div>
                                    <h2 style="font-weight: normal; font-size: 14px; line-height: 22px; background: transparent; margin: 0; border: none; padding: 0; color: #262626;">
                                        <span style="color: #434a54; font-family: SF Pro Display">
                                            <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                        </span>
                                    </h2>
                                    <p class="address-detail" style="font-size: 12px; color: #8C8C8C; margin-top: 5px">
                                        <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                                    </p>
                                </div>
                            </li>
                        </div>
                        <!--End Blog Post-->
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>