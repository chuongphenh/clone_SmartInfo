<%@ Page Title="Sửa người dùng" Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Users.Edit" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/Administrations/Users/ListOrganizationUC.ascx" TagPrefix="uc" TagName="ListOrganizationUC" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <asp:HiddenField runat="server" ID="hdId" />
    <asp:HiddenField runat="server" ID="hdVersion" />
    <asp:HiddenField runat="server" ID="hidDeletedOrgEmpID" />
    <asp:HiddenField runat="server" ID="hidDeletedOrgMgrID" />
    <div class="toolbar">
        SỬA NGƯỜI DÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server"
                    Visible="false"><i class="far fa-save" aria-hidden="true" style="color:black; font-size: 16px;" title="Lưu"></i>
                    <span>Lưu</span>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server"> <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; font-size: 16px;" title="Thoát"></i> 
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
                <td>
                </td>
                <td>Tên đầy đủ <span class="star">*</span>
                </td>
                <td></td>
            </tr>
            <tr>
               <td></td>
                <td>
                    <b>Tên đăng nhập:</b>
                    <asp:Label runat="server" ID="lblUserName" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtFullName" Width="99%" MaxLength="64" />
                </td>
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
                <td></td>
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
                <td></td>
                <td class="cus-FixMultiselect">
                    <div>
                        <asp:Literal runat="server" ID="ltrOrganizationEmployee" />
                    </div>
                    <uc:ListOrganizationUC runat="server" ID="ucOrganizationEmployee" Width="99%" />
                </td>
                <td>
                    <div>
                        <asp:Literal runat="server" ID="ltrOrganizationManager" />
                    </div>
                    <uc:ListOrganizationUC runat="server" ID="ucOrganizationManager" Width="99%" />
                </td>
                <td></td>
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
                <td></td>
                <td>
                    <tk:DatePicker ID="rdpDOB" runat="server" Width="99%" DateFormat="DMY" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlGender" runat="server" Width="99%">
                    </asp:DropDownList>
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
                    <asp:TextBox runat="server" ID="txtEmail" Width="99%" MaxLength="32" />
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlStatus" Width="99%">
                    </asp:DropDownList>
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
                    <asp:Image runat="server" ID="imgSignImage" Height="100px" />
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
    <script type="text/javascript">
        function removeItemInPanel(obj, hidLogID) {
            objValue = obj.id.substring(1);
            document.getElementById(hidLogID).value = document.getElementById(hidLogID).value + objValue + ';';
            obj.parentNode.removeChild(obj);
        }
    </script>
</asp:Content>