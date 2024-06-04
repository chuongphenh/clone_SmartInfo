<%@ Page Title="Tạo mới người dùng" Language="C#" AutoEventWireup="true" CodeBehind="AddNew.aspx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Users.AddNew" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/Administrations/Users/ListOrganizationUC.ascx" TagPrefix="uc" TagName="ListOrganizationUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
                .icon_toolbar li a{
            background:#F2F3F8;
            padding:5px 10px;
            border-radius:4px;
            color: #595959;
            font-weight:bold;
        }
        .icon_toolbar li a i{
            padding-top:0;
        }
    </style>
    <div class="toolbar">
        TẠO MỚI NGƯỜI DÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server"
                    Visible="false">
                    <i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;" title="Lưu"></i>
                    <span>Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server"><i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; font-size: 16px;" title="Thoát"></i>
                    <span>Thoát</span>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div class="content">
        <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
            <colgroup>
                <col width="20%" />
                <col width="30%" />
                <col width="30%" />
                <col />
            </colgroup>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Tên đăng nhập <span class="star">*</span>
                </td>
                <td>Tên đầy đủ <span class="star">*</span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="64" Width="99%"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtFullName" Width="99%" MaxLength="64" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Mã nhân viên<span class="star">*</span>
                </td>
                <td>Khối quản lý
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:TextBox runat="server" ID="txtEmployeeCode" Width="99%" MaxLength="256" />
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSector" Width="99%" />
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Chuyên viên phòng
                </td>
                <td>Quản lý phòng
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="cus-FixMultiselect">
                    <uc:ListOrganizationUC runat="server" ID="ucOrganizationEmployee" Width="99%" />
                </td>
                <td>
                    <uc:ListOrganizationUC runat="server" ID="ucOrganizationManager" Width="99%" />
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Ngày sinh
                </td>
                <td>Giới tính
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <tk:DatePicker ID="rdpDOB" runat="server" Width="99%" DateFormat="DMY" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlGender" runat="server" Width="99%" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Điện thoại nhà riêng
                </td>
                <td>Số Mobile <span class="star">*</span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:TextBox runat="server" ID="txtHomePhone" Width="99%" MaxLength="32" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMobilePhone" Width="99%" MaxLength="32" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Email<span class="star">*</span>
                </td>
                <td>Trạng thái
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:TextBox runat="server" ID="txtEmail" Width="99%" MaxLength="64" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="99%" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Chức danh<span class="star">*</span>
                </td>
                <td>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txtNote" Width="37%" MaxLength="512" />
                </td>
            </tr>
            <%--<tr>
                <th>Mã chi nhánh</th>
                <td>
                    <tk:ComboCheckBox runat="server" ID="cboBranchCode" Width="100%" />
                </td>
                <th>Là quản lý</th>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsManager" Text="Quản lý" />
                </td>
            </tr>--%>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Phương thức xác thực
                </td>
                <td>Cấu hình AD
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlAuthorizationType" Width="99%" />
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlLdapCnnName" Width="99%" />
                </td>
                <td></td>
            </tr>
            <tr style="height: 30px; font-weight: bold;">
                <td></td>
                <td>Hình ảnh chữ ký
                </td>
                <td>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <asp:FileUpload runat="server" ID="fileSignImage" Width="100%" />
                </td>
            </tr>
        </table>
        <h3>Vai trò</h3>
        <asp:DataGrid ID="grdRole" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="false"
            AllowCustomPaging="false" AutoGenerateColumns="false" CssClass="grid-main" GridLines="None"
            BackColor="White" OnItemDataBound="grdRole_ItemDataBound">
            <HeaderStyle CssClass="grid-header" />
            <ItemStyle CssClass="grid-item-even" />
            <AlternatingItemStyle CssClass="grid-item-odd" />
            <FooterStyle CssClass="grid-footer" />
            <Columns>
                <asp:TemplateColumn HeaderText="#">
                    <HeaderStyle Width="60px" Font-Bold="true" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:HiddenField ID="hiRoleID" runat="server" />
                        <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("IsSelect") %>'></asp:CheckBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="Name" HeaderText="Tên chức năng">
                    <HeaderStyle Font-Bold="true" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Description" HeaderText="Mô tả">
                    <HeaderStyle Font-Bold="true" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" CssClass="grid-pager" PageButtonCount="10" />
        </asp:DataGrid>
    </div>
</asp:Content>
