<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TargetSearchBox.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.TargetSearchBox" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="flex" %>

<flex:AjaxSearchBox ID="ucTarget" runat="server" Width="200" ServiceUrl="/ClientAPI.aspx"
    ServiceMethod="SearchTarget" KeywordParam="keyword" OptionParam="optionValue"
    DataTextField="Name" DataValueField="TargetID" DropDownWidth="300"  >
    <headertemplate>
        <table width="100%">
            <tr>
                <td width="100">
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
                <td width="100">
                    {{TargetCode}}
                </td>
                <td>
                    {{Name}}
                </td>
            </tr>
        </table>
    </itemtemplate>
</flex:AjaxSearchBox>



