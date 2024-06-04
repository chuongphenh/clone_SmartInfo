<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchNewsUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.Search.SearchNewsUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<style>
    .description-news {
        height: 39px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
        overflow: hidden;
    }

    .height-auto {
        height: auto !important;
    }

    .aspNetDisabled {
        background: unset !important;
        color: darkgray !important;
        border-color: unset !important;
    }
</style>

<asp:HiddenField ID="hidPage" runat="server" />
<div class="home-col-3">
    <div class="home-block" id="latest-news" style="">
        <div class="home-block-title" style="height: 60px;">
            <h3 style="font-family: SF Pro Display">Tin tức
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
                        <li class="row" style="border-bottom: none;">
                            <div class="col-md-4" style="padding-left: 0px; padding-right: 0px; text-align: left">
                                <img id="img" runat="server" src="" style="width: 90%; height: 75px" />
                            </div>
                            <div class="col-md-8" style="padding-left: 0px; padding-right: 0px; text-align: left">
                                <p style="font-size: 14px; color: #595959;">
                                    <asp:Literal ID="ltrPostingFromDTG" runat="server"></asp:Literal>
                                </p>
                                <p style="font-size: 14px; font-weight: 600; color: #262626" class="description-news">
                                    <asp:HyperLink ID="hypName" runat="server" Style="font-size: 14px; font-weight: 600; color: #262626"></asp:HyperLink>
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