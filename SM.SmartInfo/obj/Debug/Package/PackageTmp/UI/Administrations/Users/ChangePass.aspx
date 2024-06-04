<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Users.ChangePass" 
    MasterPageFile="~/UI/MasterPages/Common/SmartInfo.Master" %>

<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="toolbar">
        THAY ĐỔI MẬT KHẨU
        <ul class="icon_toolbar">
            <li>
                <asp:LinkButton ID="btnChange" OnClick="btnChange_Click" runat="server" ><i class="far fa-save" aria-hidden="true" style="color:black; margin-top: 4px; font-size: 16pt;" title="Lưu thay đổi"></i></asp:LinkButton>
            </li>
        </ul>
    </div>
    <div class="content">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <table width="100%">
            <tr>
                <td style="width: 10%"></td>
                <td>
                    <table cellpadding="0" cellspacing="0" class="tableDisplay">
                        <tr>
                            <th align="right">Tên đăng nhập
                            </th>
                            <td align="left">
                                <asp:Literal runat="server" ID="ltrUserName" />
                            </td>
                        </tr>
                        <tr>
                            <th align="right">Mật khẩu cũ <span class="star">(*)</span>
                            </th>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtOldPassword" TextMode="Password" Width="200" autocomplete="off"
                                    AutoCompleteType="None" />
                            </td>
                        </tr>
                        <tr>
                            <th align="right">Mật khẩu mới <span class="star">(*)</span>
                            </th>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtNewPassword1" TextMode="Password" Width="200"
                                    autocomplete="off" AutoCompleteType="None" />
                            </td>
                        </tr>
                        <tr>
                            <th align="right">Nhập lại mật khẩu mới <span class="star">(*)</span>
                            </th>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtNewPassword2" TextMode="Password" Width="200"
                                    autocomplete="off" AutoCompleteType="None" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10%"></td>
            </tr>
        </table>
    </div>
</asp:Content>
