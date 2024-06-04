<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListOrganizationUC.ascx.cs" Inherits="SM.CollateralManagement.UI.Administration.Users.ListOrganizationUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<tk:AjaxSearchBox runat="server" ID="ajsOrg" KeywordParam="keyword" OptionParam="optionValue"
    DataTextField="BreadCrumb" DataValueField="OrganizationID" ServiceUrl="/ClientAPI.aspx" DropDownWidth="220"
    ServiceMethod="SearchvOrgWithAll" MultiSelection="true">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <table style="width: 100%">
            <tr>
                <td>{{BreadCrumb}}
                </td>
            </tr>
        </table>
    </ItemTemplate>
</tk:AjaxSearchBox>