<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TargetSelectorUC.ascx.cs" Inherits="SM.SmartInfo.UI.UserControls.TargetSelectorUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<asp:HiddenField runat="server" ID="hidMode" />
<asp:HiddenField runat="server" ID="hidOrganizationID" />
<asp:HiddenField runat="server" ID="hidOrganizationType" />
<asp:HiddenField runat="server" ID="hidRoleIDs" />

<tk:SearchBox runat="server" ID="rcbTarget" Width="200" OnTextChanged="rcbTarget_TextChanged"
    OnItemDataBound="rcbTarget_ItemDataBound" DropDownWidth="400" DataTextField="Name"
    DataValueField="TargetID" OnSelectedIndexChanged="rcbTarget_SelectedIndexChanged">
    <headertemplate>
        <table width="100%">
            <tr>
                <td width="200">
                    <b>Mã chỉ tiêu</b>
                </td>
                <td>
                    <b>Tên chỉ tiêu</b>
                </td>
            </tr>
        </table>
    </headertemplate>
    <itemtemplate>
        <table width="100%">
            <tr>
                <td width="200">
                    <asp:Literal ID="ltrTargetCode" runat="server" />
                </td>
                <td>
                    <asp:Literal ID="ltrTargetName" runat="server" />
                </td>
            </tr>
        </table>
    </itemtemplate>
</tk:SearchBox>