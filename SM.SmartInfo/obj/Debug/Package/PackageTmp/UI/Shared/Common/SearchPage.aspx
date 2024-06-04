<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="SM.SmartInfo.UI.Shared.Common.SearchPage"
    MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Src="~/UI/UserControls/Search/SearchNewsUC.ascx" TagName="SearchNewsUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Search/SearchPressAgencyUC.ascx" TagName="SearchPressAgencyUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Search/SearchNotificationUC.ascx" TagName="SearchNotificationUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Search/SearchNegativeNewsUC.ascx" TagName="SearchNegativeNewsUC" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidType" runat="server" />
    <asp:HiddenField ID="hidTextSearch" runat="server" />
    <div class="body-content" style="background: #F2F3F8">
        <%--DESIGN--%>
        <section style="padding-right: 35px; padding-left: 35px; margin-top: 10px">
            <div class="row">
                <div class="col-sm-3" id="divNotification" runat="server">
                    <uc:SearchNotificationUC runat="server" ID="ucSearchNotification" />
                </div>
                <div class="col-sm-3" id="divNegativeNews" runat="server">
                    <uc:SearchNegativeNewsUC runat="server" ID="ucSearchNegativeNews" />
                </div>
                <div class="col-sm-3" id="divNews" runat="server">
                    <uc:SearchNewsUC runat="server" ID="ucSearchNews" />
                </div>
                <div class="col-sm-3" id="divPressAgency" runat="server">
                    <uc:SearchPressAgencyUC runat="server" ID="ucSearchPressAgency" />
                </div>
            </div>
        </section>
    </div>
    <%--END DESIGN--%>
</asp:Content>