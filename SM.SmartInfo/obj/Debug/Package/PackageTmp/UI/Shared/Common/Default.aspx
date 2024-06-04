<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SM.SmartInfo.UI.Shared.Common.Default"
    MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Src="~/UI/UserControls/Default/LatestNewsUC.ascx" TagName="LatestNewsUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Default/ChartStatisticUC.ascx" TagName="ChartStatisticUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Default/LatestNotificationUC.ascx" TagName="LatestNotificationUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Default/InprogressNegativeNewsUC.ascx" TagName="InprogressNegativeNewsUC" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .description-news {
            height: 39px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }
    </style>
    <div class="body-content" style="background: #F2F3F8">
        <%--DESIGN--%>
        <section style="padding-right: 35px; padding-left: 35px; margin-top: 10px">
            <div class="row">
                <div class="col-sm-4">
                    <uc:LatestNotificationUC runat="server" ID="ucLatestNotification" />
                </div>
                <div class="col-sm-4">
                    <uc:InprogressNegativeNewsUC runat="server" ID="ucInprogressNegativeNews" />
                    <uc:ChartStatisticUC runat="server" ID="ucChartStatistic" />
                </div>
                <div class="col-sm-4">
                    <uc:LatestNewsUC runat="server" ID="ucLatestNews" />
                </div>
            </div>
        </section>
    </div>
    <%--END DESIGN--%>

    <script>
        var height = $('#latest-news').height() - $('#negative-news').height() - 20;
        $('#chart').height(height);
        $('#div-chart').height(height - 40);
    </script>
</asp:Content>