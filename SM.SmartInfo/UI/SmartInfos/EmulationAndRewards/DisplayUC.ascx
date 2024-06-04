<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.EmulationAndRewards.DisplayUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<style>
    table .grid-header td{
        font-size:14px;
        font-weight:600;
    }
        table tr td{
        font-size:13px;
    }
</style>

<div style="width: 100%; text-align: left">
    <%--CONTENT--%>
    <div class="col-sm-12" style="text-align: left;padding:0;">
        <div class="home-col-3">
            <div class="home-block" style="padding: 15px">
                <div class="home-block-title" style="padding-left: 0px">
                    <h3 style="font-family: SF Pro Display; font-size: 16px; font-weight: 600; padding: 0px; margin: 0px;">
                        <span class="list-table-title" style="border: unset; padding: unset">
                            <asp:LinkButton CssClass="title-active" Style="margin-right: 10px; color: #464457; font-family: SF Pro Display; font-size: 14px; font-weight: 600;" ID="btnSwitchEmployee" runat="server" OnClick="btnSwitchEmployee_Click">Cá nhân</asp:LinkButton>
                            <asp:LinkButton Style="margin-right: 10px; color: #464457; font-family: SF Pro Display; font-size: 14px; font-weight: 600;" ID="btnSwitchOrganization" runat="server" OnClick="btnSwitchOrganization_Click">Tập thể</asp:LinkButton>
                        </span>
                    </h3>
                </div>
                <asp:Button ID="btnReloadAppendix" runat="server" Text="Tải lại trang" OnClick="btnReloadAppendix_Click" Style="display: none;" />
                <div class="home-block-content">
                    <ul class="home-notify" style="padding: 0px">
                        <div class="row">
                            <div class="col-sm-12" style="background: #fff; padding: 20px 0 0;">
                                <div style="width: 100%;">
                                    <div class="new-grid">
                                        <asp:DataGrid ID="grdData" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="true"
                                            AllowCustomPaging="true" AutoGenerateColumns="false" OnItemDataBound="grdData_ItemDataBound"
                                            OnItemCommand="grdData_ItemCommand" OnPageIndexChanged="grdData_PageIndexChanged" GridLines="None">
                                            <HeaderStyle CssClass="grid-header" />
                                            <ItemStyle CssClass="grid-even" />
                                            <AlternatingItemStyle CssClass="grid-odd" />
                                            <FooterStyle CssClass="grid-footer" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="STT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%# grdData.CurrentPageIndex * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageMiniTen + (Container.DataSetIndex + 1) %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Mã đối tượng được khen thưởng" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnCode" runat="server" Width="100%"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Tên đối tượng được khen thưởng" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Danh hiệu gần nhất" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltrLatestTitle" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Đơn vị khen thưởng gần nhất" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltrLatestEmulationAndRewardUnit" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Email" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Điện thoại" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle Mode="NextPrev" CssClass="new-grid-page" PageButtonCount="10" />
                                        </asp:DataGrid>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>