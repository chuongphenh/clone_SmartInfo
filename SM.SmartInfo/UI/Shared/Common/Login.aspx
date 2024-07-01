<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SM.SmartInfo.UI.Shared.Common.Login" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="sm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Smart Info</title>
    <link rel="Shortcut Icon" href="../../../Images/icon-softmart.ico" />
    <link rel="stylesheet" type="text/css" href="../../../Styles/NewFonts.css" />
    <link rel="stylesheet" href="../../../Styles/login.css" />
</head>
<body style="background: #597EF7;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
            <div class="form-login">
                <div class="login-left">
                    <img src="../../../Images/login-img.jpg" alt="Image Login" />
                    <div class="image-gradient">&nbsp;</div>
                </div>
                <div class="login-right">
                    <h3>Đăng nhập</h3>
                    <div class="login-box">
                        <span>
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" clip-rule="evenodd" d="M4 4H20C21.1 4 22 4.9 22 6V18C22 19.1 21.1 20 20 20H4C2.9 20 2 19.1 2 18V6C2 4.9 2.9 4 4 4Z" stroke="#333333" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                                <path d="M22 6L12 13L2 6" stroke="#333333" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                            </svg>
                        </span>
                        <div class="login-input">
                            <p class="box-title">Tên đăng nhập</p>
                            <asp:TextBox Style="font-family: SF Pro Display;" runat="server" placeholder="Tên đăng nhập" ID="txtUserName" Width="90%"
                                autocomplete="off" AutoCompleteType="None" />
                             <input type="hidden" name="__CSRFToken" value="<%= ViewState["AntiForgeryToken"] %>" />
                        </div>
                    </div>
                    <div class="login-box" style="margin-bottom: 20px;">
                        <span>
                            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" clip-rule="evenodd" d="M3 13C3 11.8954 3.89543 11 5 11H19C20.1046 11 21 11.8954 21 13V20C21 21.1046 20.1046 22 19 22H5C3.89543 22 3 21.1046 3 20V13Z" stroke="#333333" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                                <path d="M7 11V7C7 4.23858 9.23858 2 12 2C14.7614 2 17 4.23858 17 7V11" stroke="#333333" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                            </svg>
                        </span>
                        <div class="login-input">
                            <p class="box-title">Mật khẩu</p>
                            <asp:TextBox Style="font-family: SF Pro Display;" runat="server" placeholder="Mật khẩu" ID="txtPassword" TextMode="Password"
                                Width="90%" Height="30px" autocomplete="off" AutoCompleteType="None" />
                        </div>
                    </div>
                    <div class="login-button">
                        <asp:Button runat="server" ID="btnLogin" Text="Đăng nhập" OnClick="btnLogin_Click" />
                    </div>
                    <div class="login-exception" runat="server" id="divMess" visible="false">
                        <asp:Label runat="server" ID="lblMessage" class="lblNoti" />
                    </div>
                </div>
            </div>
        </center>
    </form>
</body>
</html>