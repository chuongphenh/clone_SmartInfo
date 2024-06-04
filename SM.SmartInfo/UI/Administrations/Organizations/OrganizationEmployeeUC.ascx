<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationEmployeeUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Organizations.OrganizationEmployeeUC" %>
<%@ Register Src="~/UI/UserControls/EmployeeSearchBox.ascx" TagName="EmployeeSearchBox"
    TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>
<uc:ErrorMessage ID="ucErr" runat="server" Visible="false" />
<asp:DataGrid ID="grdEmp" runat="server" ShowHeader="true" ShowFooter="true" AllowPaging="false"
    AllowCustomPaging="false" AutoGenerateColumns="false" CssClass="grid-main table-staff" OnItemDataBound="grdEmp_ItemDataBound"
    OnItemCommand="grdEmp_ItemCommand">
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
        <asp:TemplateColumn HeaderText="Tên" FooterStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblName">
                </asp:Label>
                <asp:HiddenField ID="hidEmployeeID" runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <%--<uc:Employee ID="ucEmployee" runat="server" DataTextField="Name" Mode="All" />--%>
                <uc:EmployeeSearchBox ID="ucEmployee" runat="server" />
                &nbsp;
                <asp:Button ID="btnAddNew" runat="server" Text="Thêm" CommandName="New" CssClass="mouse-pointer"
                    Visible="true" />
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Mã CV">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblUserName">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Giới tính">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblGender">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Số ĐT">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblMobile">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Email">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:Label runat="server" ID="lblEmail">
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