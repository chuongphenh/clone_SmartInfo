<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestNewsUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.Default.LatestNewsUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<div class="home-col-3">
    <div class="home-block" id="latest-news">
        <div class="home-block-title bg-home-block-title">
            <h3>Tin tức
                <a href="/UI/SmartInfos/News/Default.aspx"><span style="font-size: 14px; font-weight: 500; color: #141ed2; float: right;">Xem tất cả</span></a>
            </h3>
        </div>
        <%--CONTENT--%>
        <div class="home-block-content home-block-content-news">
            <ul class="home-notify">
                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <li class="row" style="border-bottom: none;">
                            <div class="col-md-4" style="padding-left: 0px; padding-right: 0px; text-align: left">
                                <img id="img" runat="server" src="" style="width: 90%; height: 95px; object-fit: cover;" />
                            </div>
                            <div class="col-md-8" style="padding-left: 0px; padding-right: 0px; text-align: left">
                                <p style="font-size: 13px; color: #979797;">
                                    <asp:Literal ID="ltrPostingFromDTG" runat="server"></asp:Literal>
                                </p>
                                <p style="font-size: 14px; font-weight: 600; color: #262626" class="description-news">
                                    <asp:HyperLink ID="hypName" runat="server" ></asp:HyperLink>
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