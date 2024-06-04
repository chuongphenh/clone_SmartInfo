<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InprogressNegativeNewsUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.Default.InprogressNegativeNewsUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    .even {
        float: left;
    }

    .odd {
        float: right;
    }

    .second-row {
        margin-top: 15px;
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

    .description-negative-news {
        height: 39px;
        display: -webkit-box;
        -webkit-box-orient: vertical;
        -webkit-line-clamp: 2;
        overflow: hidden;
    }

    .done {
        color: #389E0D !important;
    }

    .inprogress {
        color: #EE6400 !important;
    }
</style>

<div style="background: rgb(255, 255, 255);" id="negative-news">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="row" style="padding-left: 25px; padding-right: 25px; padding-top: 20px;background-color:#e0f0ff;padding-bottom:15px;">
        <span style="font-size: 16px; font-weight: bold; color: #141ed2; float: left;">Sự vụ</span>
        <a href="/UI/SmartInfos/NegativeNews/Default.aspx"><span style="font-size: 14px; font-weight: 500; color: #141ed2; float: right;">Xem tất cả</span></a>
    </div>
    <%--CONTENT--%>
    <div class="row" style="padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px;">
        <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
            <ItemTemplate>
                <div class="col-md-6" style="padding-left: 0px; padding-right: 0px; text-align: left; width: 48%;" id="divData" runat="server">
                    <div style="background: #F2F3F8;">
                        <div class="row" style="padding: 15px">
                            <p style="font-size: 13px; margin-bottom: 3px" id="pTime" runat="server">
                                <asp:Literal ID="ltrIncurredDTG" runat="server"></asp:Literal>
                            </p>
                            <p style="font-size: 14px; font-weight: 600;" class="description-negative-news" id="pName" runat="server">
                                <asp:HyperLink ID="hypName" runat="server"></asp:HyperLink>
                            </p>
                            <br />
                            <br />
                            <p>
                                <i id="iStatus" runat="server"></i><span style="font-size: 13px; font-weight: 600" id="spanStatus" runat="server">&nbsp;
                                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal></span>
                            </p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <%--END CONTENT--%>
</div>