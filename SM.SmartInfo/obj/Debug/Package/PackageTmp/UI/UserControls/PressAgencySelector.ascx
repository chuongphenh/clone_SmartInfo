<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressAgencySelector.ascx.cs" Inherits="SM.SmartInfo.UI.UserControls.PressAgencySelector" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<tk:SearchBox runat="server" ID="rcbPressAgency" Width="200" OnTextChanged="rcbPressAgency_TextChanged"
    OnItemDataBound="rcbPressAgency_ItemDataBound" DropDownWidth="500" DataTextField="Name"
    DataValueField="PressAgencyID" OnSelectedIndexChanged="rcbPressAgency_SelectedIndexChanged">
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td>
                    <asp:Literal ID="ltrName" runat="server" />
                </td>
            </tr>
        </table>
    </ItemTemplate>
</tk:SearchBox>