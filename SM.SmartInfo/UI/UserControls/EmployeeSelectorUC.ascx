<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSelectorUC.ascx.cs" Inherits="SM.SmartInfo.UI.UserControls.EmployeeSelectorUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<asp:HiddenField runat="server" ID="hidMode" />
<asp:HiddenField runat="server" ID="hidOrganizationID" />
<asp:HiddenField runat="server" ID="hidOrganizationType" />
<asp:HiddenField runat="server" ID="hidRoleIDs" />

<tk:SearchBox runat="server" ID="rcbEmployee" Width="200" OnTextChanged="rcbEmployee_TextChanged"
    OnItemDataBound="rcbEmployee_ItemDataBound" DropDownWidth="400" DataTextField="Username"
    DataValueField="EmployeeID" OnSelectedIndexChanged="rcbEmployee_SelectedIndexChanged">
    <headertemplate>
        <table width="100%">
            <tr>
                <td width="200">
                    <b>Tên đăng nhập</b>
                </td>
                <td>
                    <b>Tên người dùng</b>
                </td>
            </tr>
        </table>
    </headertemplate>
    <itemtemplate>
        <table width="100%">
            <tr>
                <td width="200">
                    <asp:Literal ID="ltrUsername" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="ltrName" runat="server" />
                </td>
            </tr>
        </table>
    </itemtemplate>
</tk:SearchBox>