<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TargetPlanUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Plans.TargetUC" %>
<%@ Register Src="~/UI/UserControls/TargetSearchBox.ascx" TagName="TargetSearchBox"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<uc:ErrorMessage ID="ucErr" runat="server" Visible="false" />
<asp:DataGrid ID="grdTarget" runat="server" ShowHeader="true" ShowFooter="true" AllowPaging="false"
    AllowCustomPaging="false" AutoGenerateColumns="false" CssClass="grid-main table-staff" OnItemDataBound="grdTarget_ItemDataBound"
    OnItemCommand="grdTarget_ItemCommand">
    <HeaderStyle CssClass="grid-header" />
    <ItemStyle CssClass="grid-item-even" />
    <AlternatingItemStyle CssClass="grid-item-odd" />
    <FooterStyle CssClass="grid-footer" />
    <Columns>
        <asp:TemplateColumn HeaderText="#">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <%# Container.DataSetIndex+1 %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Mã chỉ tiêu" FooterStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblTargetCode">
                </asp:Label>
                <asp:HiddenField ID="hidTargetID" runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <%--<uc:Employee ID="ucEmployee" runat="server" DataTextField="Name" Mode="All" />--%>
                <uc:TargetSearchBox ID="ucTarget" runat="server" />
                &nbsp;
                <asp:Button ID="btnAddNew" runat="server" Text="Thêm" CommandName="New" CssClass="mouse-pointer"
                    Visible="true" />
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Tên chỉ tiêu">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblTargetName">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Mô tả">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblDescribe">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Loại chỉ tiêu">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblTargetType">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Chức năng">
            <HeaderStyle Width="40" HorizontalAlign="Center" Wrap="false" />
            <ItemStyle HorizontalAlign="Center" />
            <FooterStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:LinkButton ID="btnDelete" runat="server" CssClass="grid-delete" />
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle Mode="NumericPages" CssClass="grid-pager" PageButtonCount="10" />
</asp:DataGrid>