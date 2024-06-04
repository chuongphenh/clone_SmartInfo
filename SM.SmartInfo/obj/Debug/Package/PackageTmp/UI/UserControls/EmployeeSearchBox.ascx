<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSearchBox.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.EmployeeSearchBox" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="flex" %>

<flex:AjaxSearchBox ID="ucEmployee" runat="server" Width="200" ServiceUrl="/ClientAPI.aspx"
    ServiceMethod="SearchEmployee" KeywordParam="keyword" OptionParam="optionValue"
    DataTextField="Name" DataValueField="EmployeeID" DropDownWidth="300">
    <headertemplate>
        <table width="100%">
            <tr>
                <td width="100">
                    <b>Tên đăng nhập</b>
                </td>
                <td>
                    <b>Tên hiển thị</b>
                </td>
            </tr>
        </table>
    </headertemplate>
    <itemtemplate>
        <table width="100%">
            <tr>
                <td width="100">
                    {{UserName}}
                </td>
                <td>
                    {{Name}}
                </td>
            </tr>
        </table>
    </itemtemplate>
</flex:AjaxSearchBox>