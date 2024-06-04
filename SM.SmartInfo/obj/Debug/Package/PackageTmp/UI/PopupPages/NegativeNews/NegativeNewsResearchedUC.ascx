<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NegativeNewsResearchedUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.NegativeNews.NegativeNewsResearchedUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    .table > tbody > tr {
        height: 50px;
    }

        .table > tbody > tr > td {
            font-size: 13px;
            vertical-align: middle;
        }

    .table > thead > tr {
        height: 50px;
    }

        .table > thead > tr > th {
            color: #595959;
            background: transparent !important;
            font-size: 14px;
            font-weight: 600;
            text-align: center;
            vertical-align: middle;
        }

    .center {
        font-size: 14px;
        text-align: center;
    }
</style>

<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidPage" runat="server" />
<asp:HiddenField ID="hidIsEdit" runat="server" />
<div class="row">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="headline">
        <span style="color: #262626; font-size: 14px; font-weight: 600;">Quá trình tìm hiểu thông tin
            <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="font-size: 16pt; float: right; padding-right: 5px"><i class="fa fa-plus"></i></asp:LinkButton></span>
    </div>
    <div class="row blog blog-medium">
        <div class="col-md-12" style="padding-left: 0px">
            <div class="table-responsive">
                <table class="table" style="margin-top: 20px; margin-bottom: 0px; border: 1px solid #D9D9D9; box-sizing: border-box;">
                    <colgroup>
                        <col width="10%" />
                        <col width="20%" />
                        <col width="20%" />
                        <col width="20%" />
                        <col width="20%" />
                        <col />
                    </colgroup>
                    <thead>
                        <tr>
                            <th>Thời gian</th>
                            <th style="text-align: left">Hình thức trao đổi</th>
                            <th style="text-align: left">Đơn vị/ cá nhân trao đổi</th>
                            <th style="text-align: left">Nội dung trao đổi</th>
                            <th style="text-align: left">Nội dung thống nhất</th>
                            <th id="thEdit" runat="server"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptNegativeNewsResearched" runat="server" OnItemDataBound="rptNegativeNewsResearched_ItemDataBound" OnItemCommand="rptNegativeNewsResearched_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="center">
                                        <tk:DatePicker ID="dpkCreatedDTG" runat="server" Width="100%" DateFormat="DMY" Visible="false" />
                                        <asp:Literal ID="ltrCreatedDTG" runat="server"></asp:Literal></td>
                                    </td>
                                <td>
                                    <asp:Literal ID="ltrTypeOfContact" runat="server" />
                                    <asp:HiddenField ID="hidNegativeNewsResearchedID" runat="server" />
                                </td>
                                    <td>
                                        <asp:Literal ID="ltrObjectContact" runat="server"></asp:Literal></td>
                                    <td>
                                        <asp:Literal ID="ltrContent" runat="server"></asp:Literal></td>
                                    <td>
                                        <asp:Literal runat="server" ID="ltrResult"></asp:Literal>
                                    <td id="tdEdit" runat="server" class="center">
                                        <asp:LinkButton ID="btnEdit" runat="server" Style="margin-right: 5px"><i class="fas fa-pencil-alt" title="Sửa"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')"><i class="fa fa-trash" title="Xóa"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>

<tk:PopupPanel ID="popEdit" runat="server" Title="THÊM MỚI/CẬP NHẬT" Width="800" CancelButton="btnCancel" OnPopupClosed="popEdit_PopupClosed">
    <PopupTemplate>
        <table class="table" style="width: 100%; margin-bottom: 0px">
            <colgroup>
                <col width="150" />
                <col />
            </colgroup>
            <tr>
                <th>Thời gian</th>
                <td>
                    <asp:HiddenField ID="hidNegativeNewsResearchedID" runat="server"></asp:HiddenField>
                    <tk:DatePicker ID="dtpCreatedDTG" runat="server" Width="100%" DateFormat="DMY" />
                </td>
            </tr>
            <tr>
                <th>Hình thức trao đổi
                </th>
                <td>
                    <asp:TextBox ID="txtTypeOfContact" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Đơn vị/ cá nhân trao đổi
                </th>
                <td>
                    <asp:TextBox ID="txtObjectContact" runat="server" Width="100%" MaxLength="256"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <th>Nội dung trao đổi
                </th>
                <td>
                    <tk:TextArea ID="txtContent" runat="server" Width="100%" MaxLength="1024" TextMode="MultiLine" Rows="3"></tk:TextArea>
                </td>
            </tr>
            <tr>
                <th>Nội dung thống nhất
                </th>
                <td>
                    <tk:TextArea ID="txtResult" runat="server" Width="100%" MaxLength="1024" TextMode="MultiLine" Rows="3"></tk:TextArea>
                </td>
            </tr>
        </table>
        <div class="popup-toolbar" style="text-align: center">
            <asp:Button runat="server" ID="btnSave" Text="Lưu" OnClick="btnSave_Click" class="btn btn-primary" Style="background: #434a54" />
            <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>