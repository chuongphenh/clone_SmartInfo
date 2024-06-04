<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNagativeNewsUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.NegativeNews.ListNagativeNewsUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

<style>
    .fa-negative-news-done {
        padding: 2px;
        padding-left: 3px;
        padding-right: 3px;
        border: 1px solid #389E0D;
        border-radius: 50%;
        background: #389E0D;
        color: white;
        font-size: 9px;
    }

    .fa-negative-news-inprogress {
        padding: 2px;
        padding-left: 3px;
        padding-right: 3px;
        border: 1px solid #EE6400;
        border-radius: 50%;
        background: #EE6400;
        color: white;
        font-size: 9px;
    }

    .done {
        color: #389E0D !important;
    }

    .inprogress {
        color: #EE6400 !important;
    }

    .aspNetDisabled {
        background: unset !important;
        color: darkgray !important;
        border-color: unset !important;
    }

    .linkPopup:hover {
        cursor: pointer;
    }

    .table > tbody > tr {
        height: 50px;
    }

        .table > tbody > tr > td {
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

<asp:HiddenField ID="hidPage" runat="server" />
<asp:HiddenField ID="hidIsEdit" runat="server" />
<asp:HiddenField ID="hidNewsID" runat="server" />
<div class="err">
    <uc:ErrorMessage ID="ucErr" runat="server" />
</div>
<asp:Button ID="btnReloadAppendix" runat="server" Text="Tải lại trang" OnClick="btnReloadAppendix_Click" Style="display: none;" />
<div class="row" style="margin-top: 20px;">
    <div class="col-md-12" style="text-align: left; padding-left: 0px">
        <div class="table-responsive">
            <table class="table" style="border: 1px solid #D9D9D9; box-sizing: border-box;">
                <colgroup>
                    <col />
                    <col width="175" />
                    <col width="140" />
                    <col />
                    <col width="100" />
                    <col />
                </colgroup>
                <thead>
                    <tr>
                        <th colspan="2" style="font-size: 16px; color: #262626; text-align: left; padding-left: 25px;">Chi tiết sự vụ</th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th id="thAddNew" runat="server" style="text-align: right; padding-right: 25px;">
                            <asp:LinkButton ID="btnAddNew" OnClick="btnAddNew_Click" runat="server" Style="font-size: 16pt; margin-left: 10px" Text="Thêm mới sự vụ"> <i class="fa fa-plus"></i></asp:LinkButton>
                        </th>
                    </tr>
                    <tr>
                        <th>#</th>
                        <th>Loại tin</th>
                        <th>Trạng thái xử lý</th>
                        <th style="text-align: left">Kênh thông tin</th>
                        <th>Kết quả</th>
                        <th id="thEdit" runat="server"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound" OnItemCommand="rptData_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td class="center">
                                    <%# (Container.ItemIndex + 1) + ((SM.SmartInfo.Utils.Utility.GetNullableInt(hidPage.Value) - 1) * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageMiniTen) %>
                                </td>
                                <td class="center">
                                    <span id="spanType" runat="server">
                                        <asp:Literal ID="ltrType" runat="server"></asp:Literal></span>
                                </td>
                                <td class="center">
                                    <i id="iStatus" runat="server"></i><span style="font-size: 13px; font-weight: 600" id="spanStatus" runat="server">&nbsp;
                                    <asp:Literal ID="ltrStatus" runat="server"></asp:Literal></span>
                                </td>
                                <td><span style="font-size: 14px; color: #262626">
                                    <asp:Literal ID="ltrName" runat="server"></asp:Literal></span></td>
                                <td class="center"><span style="font-size: 13px; color: #262626">
                                    <a id="aLink" runat="server" style="color: #262626" class="linkPopup">Xem chi tiết</a></span></td>
                                <td class="center" id="tdEdit" runat="server">
                                    <asp:LinkButton ID="btnDelete" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')" runat="server" Style="font-size: 16pt; margin-left: 10px" Text="Xóa"> <i class="fa fa-trash"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <asp:LinkButton ID="btnPrevious" runat="server" OnClick="btnPrevious_Click"><i class="fa fa-angle-left"></i></asp:LinkButton>
                            <span style="padding-right: 20px; padding-left: 20px">Trang <%= hidPage.Value %></span>
                            <asp:LinkButton ID="btnNext" runat="server" OnClick="btnNext_Click"><i class="fa fa-angle-right"></i></asp:LinkButton>
                        </td>
                        <td></td>
                        <td id="footerEdit" runat="server"></td>
                        <td colspan="2" style="text-align: right" class="center"><span style="font-size: 13px; color: #262626"></span></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>