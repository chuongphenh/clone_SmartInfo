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
                   <%-- <svg class="logo-login" width="140" height="22" viewBox="0 0 140 22" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M45.0562 18.1078C43.6873 18.1078 42.5401 17.8079 41.6144 17.2082C40.7018 16.6085 40.0565 15.7807 39.6784 14.7247L41.986 13.3753C42.5205 14.7703 43.57 15.4678 45.1344 15.4678C45.8906 15.4678 46.4447 15.3309 46.7967 15.0571C47.1487 14.7833 47.3247 14.4378 47.3247 14.0207C47.3247 13.5383 47.1096 13.1667 46.6793 12.906C46.2491 12.6322 45.4799 12.3389 44.3718 12.026C43.759 11.8435 43.2376 11.6609 42.8073 11.4784C42.3901 11.2959 41.9664 11.0547 41.5362 10.7549C40.6758 10.1161 40.2456 9.17739 40.2456 7.93887C40.2456 6.68732 40.6823 5.70302 41.5558 4.98598C42.4293 4.24287 43.4918 3.87132 44.7433 3.87132C45.8515 3.87132 46.8227 4.13858 47.6571 4.67309C48.5045 5.20761 49.1629 5.96376 49.6322 6.94154L47.3638 8.25176C46.8162 7.07843 45.9427 6.49176 44.7433 6.49176C44.1827 6.49176 43.7395 6.62213 43.4136 6.88287C43.1007 7.13058 42.9442 7.4565 42.9442 7.86065C42.9442 8.29087 43.1202 8.64287 43.4722 8.91665C43.8633 9.19043 44.5543 9.47724 45.5451 9.77709L46.4838 10.09C46.5881 10.1161 46.7119 10.1617 46.8553 10.2269C47.0118 10.279 47.1813 10.3442 47.3638 10.4224C47.7549 10.5658 48.0417 10.7027 48.2242 10.8331C48.4328 10.9635 48.6675 11.1395 48.9282 11.3611C49.189 11.5827 49.3845 11.8109 49.5149 12.0455C49.6583 12.2802 49.7756 12.567 49.8669 12.906C49.9712 13.2319 50.0233 13.5904 50.0233 13.9815C50.0233 15.2461 49.5605 16.25 48.6349 16.9931C47.7093 17.7362 46.5164 18.1078 45.0562 18.1078ZM65.0836 4.14509V17.834H62.4044V8.95576L58.552 15.3113H58.2391L54.3867 8.97532V17.834H51.688V4.14509H54.4649L58.3956 10.618L62.3067 4.14509H65.0836ZM76.4342 17.834L75.6128 15.37H70.1568L69.3355 17.834H66.4217L71.2128 4.14509H74.5568L79.3675 17.834H76.4342ZM71.0173 12.8473H74.772L72.8946 7.25443L71.0173 12.8473ZM88.206 17.834L85.4487 13.082H83.3953V17.834H80.6967V4.14509H86.1722C87.4368 4.14509 88.5124 4.58835 89.3989 5.47487C90.2854 6.36139 90.7287 7.43043 90.7287 8.68198C90.7287 9.54243 90.4875 10.3312 90.0051 11.0482C89.5227 11.7652 88.8839 12.3063 88.0887 12.6713L91.1198 17.834H88.206ZM83.3953 6.66776V10.7158H86.1722C86.4199 10.7158 86.6546 10.6636 86.8762 10.5593C87.1109 10.455 87.313 10.3116 87.4825 10.1291C87.6519 9.93354 87.7823 9.71191 87.8736 9.46421C87.9779 9.2165 88.03 8.95576 88.03 8.68198C88.03 8.40821 87.9779 8.14747 87.8736 7.89976C87.7823 7.65206 87.6519 7.43695 87.4825 7.25443C87.313 7.07191 87.1109 6.9285 86.8762 6.82421C86.6546 6.71991 86.4199 6.66776 86.1722 6.66776H83.3953ZM101.477 4.14509V6.72643H97.7806V17.834H95.0819V6.72643H91.4055V4.14509H101.477Z" fill="white" />
                        <path d="M106.578 3.81111H108.397V17.5H106.578V3.81111ZM116.027 7.468C116.6 7.468 117.115 7.56578 117.571 7.76133C118.041 7.94385 118.438 8.21111 118.764 8.56311C119.103 8.90207 119.364 9.31926 119.547 9.81467C119.729 10.3101 119.82 10.8707 119.82 11.4964V17.5H118.119V11.5942C118.119 10.799 117.904 10.1862 117.474 9.756C117.043 9.31274 116.45 9.09111 115.694 9.09111C114.847 9.09111 114.162 9.35837 113.641 9.89289C113.119 10.4144 112.859 11.2227 112.859 12.3178V17.5H111.157V7.72222H112.859V9.13022C113.536 8.02207 114.592 7.468 116.027 7.468ZM127.221 5.19956C125.5 5.06918 124.64 5.8123 124.64 7.42889V7.72222H127.221V9.36489H124.64V17.5H122.939V9.36489H121.374V7.72222H122.939V7.42889C122.939 6.09911 123.304 5.10178 124.034 4.43689C124.777 3.75896 125.839 3.46563 127.221 3.55689V5.19956ZM136.769 16.268C135.778 17.2588 134.559 17.7542 133.112 17.7542C131.665 17.7542 130.446 17.2588 129.455 16.268C128.464 15.2772 127.969 14.0582 127.969 12.6111C127.969 11.164 128.464 9.94504 129.455 8.95422C130.446 7.96341 131.665 7.468 133.112 7.468C134.559 7.468 135.778 7.96341 136.769 8.95422C137.773 9.95807 138.275 11.177 138.275 12.6111C138.275 14.0452 137.773 15.2641 136.769 16.268ZM133.112 16.092C134.09 16.092 134.911 15.7596 135.576 15.0947C136.241 14.4298 136.573 13.6019 136.573 12.6111C136.573 11.6203 136.241 10.7924 135.576 10.1276C134.911 9.46267 134.09 9.13022 133.112 9.13022C132.147 9.13022 131.333 9.46267 130.668 10.1276C130.003 10.7924 129.67 11.6203 129.67 12.6111C129.67 13.6019 130.003 14.4298 130.668 15.0947C131.333 15.7596 132.147 16.092 133.112 16.092Z" fill="white" />
                        <defs>
                            <clipPath id="clip0">
                                <rect width="22" height="22" fill="white" />
                            </clipPath>
                            <clipPath id="clip1">
                                <rect width="22" height="9.77778" fill="white" />
                            </clipPath>
                            <clipPath id="clip2">
                                <rect width="22" height="9.77778" fill="white" transform="translate(0 12.2227)" />
                            </clipPath>
                        </defs>
                    </svg>--%>
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