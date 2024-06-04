<%@ Page Title="Chi tiết người dùng" Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Users.Display" MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/Administrations/Users/UserDetailUC.ascx" TagPrefix="uc" TagName="UserDetail" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="toolbar">
        CHI TIẾT NGƯỜI DÙNG
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnOpenPassword" runat="server" OnClick="btnOpenPassword_Click">
                     <i class="fa fa-unlock" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Mở khóa"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" Text="Sửa" Visible="false"> 
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: black; margin-top: 4px; font-size: 16pt;" title="Sửa"></i>
                </asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                    OnClientClick="return confirm('Bạn có chắc chắn muốn xóa người dùng không?')" Visible="false">
                    <i class="fa fa-trash" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Xóa"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btnSetPassword" runat="server" OnClick="btnSetPassword_Click" Visible="false">
                     <i class="fa fa-key" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Cấp mật khẩu"></i>
                </asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="hypExit" runat="server">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Thoát"></i>
                </asp:HyperLink>
            </li>
        </ul>
    </div>
    <input type="hidden" runat="server" id="hdId" />
    <input type="hidden" runat="server" id="hdVersion" />
    <uc:UserDetail runat="server" ID="ucUserDetail" />

    <tk:PopupPanel runat="server" ID="popSetPassword" Title="Cấp mật khẩu" Width="500px">
        <PopupTemplate>
            <table class="tableDisplay" width="100%">
                <col width="200" />
                <col />
                <tr>
                    <th>Mật khẩu mới</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtNewPassword" MaxLength="256" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <asp:Button runat="server" ID="btnSetPasswordOK" OnClick="btnSetPasswordOK_Click" Text="OK" 
                            OnClientClick="return confirm('Bạn có chắc chắn muốn cấp mật khẩu mới không?')" />
                    </td>
                </tr>
            </table>
        </PopupTemplate>
    </tk:PopupPanel>
</asp:Content>