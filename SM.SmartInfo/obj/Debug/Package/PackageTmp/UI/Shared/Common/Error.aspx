<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="SM.SmartInfo.UI.Shared.Common.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quản lý Tài liệu</title>
    <style type="text/css">
        .error {
            background: #FDDE80 none repeat scroll 0 0;
            padding: 10px;
        }

        .errorContent {
            border: 1px dashed #282828;
            padding: 10px;
        }

            .errorContent a {
                font-weight: bold;
                font-size: 110%;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="height: 6px;">
            </div>
            <div class="error">
                <div class="errorContent">
                    <table>
                        <tr>
                            <td colspan="2">
                                <h2>Có lỗi xảy ra trong quá trình xử lý.</h2>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px" align="right">Thông báo lỗi:
                            </td>
                            <td align="left">
                                <asp:Label runat="server" ID="lblMesage" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Trang phát sinh lỗi:
                            </td>
                            <td align="left">
                                <asp:HyperLink ID="hplUrl" runat="server"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">Quay trở lại&nbsp;<asp:HyperLink runat="server" NavigateUrl="~/UI/Shared/Common/Default.aspx" ID="hplHomePage">Trang chủ</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </center>
    </form>
</body>
</html>