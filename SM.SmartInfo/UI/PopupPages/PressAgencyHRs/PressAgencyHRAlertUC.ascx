<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PressAgencyHRAlertUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.PressAgencyHRs.PressAgencyHRAlertUC" %>

<%@ Register Src="~/UI/UserControls/Pager.ascx" TagName="PagerUC" TagPrefix="uc" %>
<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<style>
    ::-webkit-scrollbar-track {
        /*-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);*/
        border-radius: 4px;
        background-color: #fff;
    }

    ::-webkit-scrollbar {
        width: 5px;
        height: 5px;
        background-color: #F5F5F5;
    }

    ::-webkit-scrollbar-thumb {
        border-radius: 10px;
        /*-webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);*/
        background-color: #E8E8E8;
    }
</style>

<asp:HiddenField ID="hidPage" runat="server" />
<asp:HiddenField ID="hidIsEdit" runat="server" />
<asp:HiddenField ID="hidPressAgencyHRID" runat="server" />
<asp:HiddenField ID="hidPressAgencyHRPosition" runat="server" />

<div class="err">
    <uc:ErrorMessage ID="ucErr" runat="server" />
</div>

<div class="pa-popup">
    <div class="pa-popup-title">
        <span>&nbsp;</span>
        Lịch nhắc nhở
        <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="font-size: 16pt; float: right; padding-right: 5px"><i class="fa fa-plus"></i></asp:LinkButton>
    </div>
    <ul style="max-height: 303px; min-height: 303px; overflow-y: scroll;">
        <asp:Repeater ID="rptHistory" runat="server" OnItemDataBound="rptHistory_ItemDataBound" OnItemCommand="rptHistory_ItemCommand">
            <ItemTemplate>
                <li style="padding-right: 5px;">
                    <p class="pa-date-red">
                        <asp:HiddenField ID="hidPressAgencyHRAlertID" runat="server" />
                        <asp:HiddenField ID="fieldTypeDate" runat="server" />
                        <asp:Literal ID="ltrAlertDTG" runat="server"></asp:Literal>
                        <tk:DatePicker ID="dpkAlertDTG" runat="server" Width="100%" DateFormat="DMY" Visible="false" /> - <asp:Literal ID="lrtDate" runat="server"></asp:Literal>
                    </p>
                    <asp:LinkButton style="float: right" ID="btnDelete" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')"><i class="fa fa-trash" title="Xóa"></i></asp:LinkButton>
                    <asp:LinkButton style="float: right; margin-right: 10px" ID="btnEdit" runat="server"><i class="fas fa-pencil-alt" title="Sửa"></i></asp:LinkButton>
                    <p>
                        <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                    </p>        </script>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<%--<!--Pagination-->
<div style="text-align: center">
    <div class="text-center" style="display: inline-block">
        <ul class="pagination">
            <uc:PagerUC runat="server" ID="Pager" OnPageIndexChanged="ucPager_PageIndexChanged" />
        </ul>
    </div>
</div>
<!--End Pagination-->--%>

<tk:PopupPanel ID="popEdit" runat="server" Title="THÊM MỚI/CẬP NHẬT LỊCH NHẮC" Width="800" CancelButton="btnCancel" OnPopupClosed="popEdit_PopupClosed">
    <PopupTemplate>
        <table class="table" style="width: 100%; margin-bottom: 0px">
            <colgroup>
                <col width="150" />
                <col />
            </colgroup>
             <tr>
                <th>Kiểu lịch
                </th>
                <td>
                   <asp:DropDownList runat="server"  ID="dataTypeDate" Width="100%"/>
                </td>
            </tr>
            <tr>
                <th>Ngày nhắc
                </th>
                <td>
                    <asp:HiddenField ID="hidPressAgencyHRAlertID" runat="server"></asp:HiddenField>
                    <tk:DatePicker ID="dpkAlertDTG" runat="server" Width="100%" DateFormat="DMY"  Visible="true"/>
                     <span><i>(Chỉ chọn ngày dương diễn ra sự kiện)</i></span>
                </td>
            </tr>
            <tr>
                <th>Nội dung
                </th>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" Width="100%" MaxLength="512"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="popup-toolbar" style="text-align: center">
            <asp:Button runat="server" ID="btnSave" Text="Lưu" OnClick="btnSave_Click" class="btn btn-primary" Style="background: #434a54" />
            <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>