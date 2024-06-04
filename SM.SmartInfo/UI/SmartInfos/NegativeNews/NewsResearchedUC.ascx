<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsResearchedUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.NegativeNews.NewsResearchedUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    .table > tbody > tr {
        height: 50px;
    }

        .table > tbody > tr > td {
            font-size: 13px;
            vertical-align: middle;
            word-break: break-word;
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
    <div class="row blog blog-medium">
        <div class="col-md-12" style="padding-left: 0px">
            <div class="table-responsive">
                <table class="table" style="margin-top: 20px; margin-bottom: 0px; border: 1px solid #D9D9D9; box-sizing: border-box;">
                    <colgroup>
                        <col width="11%" />
                        <col width="10%" />
                        <col width="15%" />
                        <col />
                        <col width="15%"/>
                        <col width="1%" />
                    </colgroup>
                    <thead>
                        <tr>
                            <th colspan="4" style="font-size: 16px; color: #262626; text-align: left;">Quá trình tìm hiểu thông tin</th>
                            <th></th>
                            <th id="thAddNew" runat="server" style="text-align: right;">
                                <asp:LinkButton ID="btnAddNew" OnClick="btnAddNew_Click" runat="server" Style="font-size: 16pt; margin-left: 10px" Text="Thêm mới"> <i class="fa fa-plus"></i></asp:LinkButton>
                            </th>
                        </tr>
                        <tr>
                            <th style="font-size: 14px;width:13%;">Thời gian</th>
                            <th style="text-align: left; font-size: 14px;width:13%;">Hình thức</th>
                            <th style="text-align: left; font-size: 14px;width:19%;">ĐV/CN trao đổi</th>
                            <th style="text-align: left; font-size: 14px;">ND trao đổi</th>
                            <th style="text-align: left; font-size: 14px;width:20%;">ND thống nhất</th>
                            <th id="thEdit" runat="server"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptNewsResearched" runat="server" OnItemDataBound="rptNewsResearched_ItemDataBound" OnItemCommand="rptNewsResearched_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td class="center" style="font-size: 13px;">
                                        <tk:DatePicker ID="dpkCreatedDTG" runat="server" Width="100%" DateFormat="DMY" Visible="false" />
                                        <asp:Literal ID="ltrCreatedDTG" runat="server"></asp:Literal>
                                    </td>
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:Literal ID="ltrTypeOfContact" runat="server" />
                                        <asp:HiddenField ID="hidNewsResearchedID" runat="server" />
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:Literal ID="ltrObjectContact" runat="server"></asp:Literal>
                                    </td>
                                    <td style="font-size: 13px;">
                                        <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                                    </td>
                                    <td style="font-size: 13px;">
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
                <th>Thời gian <span class="star">*</span></th>
                <td>
                    <asp:HiddenField ID="hidNewsResearchedID" runat="server"></asp:HiddenField>
                    <tk:DatePicker ID="dtpCreatedDTG" runat="server" Width="100%" DateFormat="DMY" />
                </td>
            </tr>
            <tr>
                <th>
                    Hình thức trao đổi
                </th>
                <td>
                    <asp:TextBox ID="txtTypeOfContact" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    Đơn vị/ cá nhân trao đổi
                </th>
                <td>
                    <asp:TextBox ID="txtObjectContact" runat="server" Width="100%" MaxLength="256"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <th>
                    Nội dung trao đổi <span class="star">*</span>
                </th>
                <td>
                    <tk:TextArea ID="txtContent" runat="server" Width="100%" MaxLength="1024" TextMode="MultiLine" Rows="3"></tk:TextArea>
                </td>
            </tr>
            <tr>
                <th>
                    Nội dung thống nhất
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
