<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CampaignNewsUC.ascx.cs" Inherits="SM.SmartInfo.UI.SmartInfos.News.CampaignNewsUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/News/PostsUC.ascx" TagPrefix="uc" TagName="PostsUC" %>

<style>
    .table > tbody > tr {
        height: 50px;
    }

        .table > tbody > tr > td {
            vertical-align: middle;
            font-size: 13px
        }
        .table th, .table td {
        min-width: 50px;
        word-break: break-word !important;
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
        font-size: 13px;
        text-align: center;
    }

    .aspNetDisabled {
        background: unset !important;
        color: darkgray !important;
        border-color: unset !important;
    }
</style>

<asp:HiddenField ID="hidPage" runat="server" />
<asp:HiddenField ID="hidIsEdit" runat="server" />
<asp:HiddenField ID="hidNewsID" runat="server" />
<div class="row">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="row blog blog-medium">

        <div class="col-md-12 table-responsive" style="padding-left: 0px; padding-right: 0px">
            <table class="table" style="border: 1px solid #D9D9D9; box-sizing: border-box;">
                 <colgroup>
                    <col style="width: 5%" />
                    <col style="width: 50%" />
                    <col style="width: 15%" />
                    <col style="width: 15%" />
                    <col style="width: 10%"/>
                    <col style="width: 5%"/>
                </colgroup>
                <thead>
                    <tr>
                        <th colspan="2" style="font-size: 16px; color: #262626; text-align: left; padding-left: 25px;">Tuyến bài</th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th id="thAddNew" runat="server" style="text-align: right; padding-right: 25px;">
                            <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click" Style="font-size: 16pt; margin-left: 10px"><i class="fa fa-plus"></i></asp:LinkButton>
                        </th>
                    </tr>
                    <tr>
                        <th>#</th>
                        <th style="text-align: left">Tuyến bài</th>
                        <th>Số bài đăng</th>
                        <th>Ngày đăng</th>
                        <th>Upload</th>
                        <th id="thEdit" runat="server"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCampaignNews" runat="server" OnItemDataBound="rptCampaignNews_ItemDataBound" OnItemCommand="rptCampaignNews_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td class="center">
                                    <%# (Container.ItemIndex + 1) + ((SM.SmartInfo.Utils.Utility.GetNullableInt(hidPage.Value) - 1) * SM.SmartInfo.SharedComponent.Constants.SMX.smx_PageMiniSize) %>
                                    <asp:HiddenField ID="hidCampaignNewsID" runat="server" />
                                </td>
                                <td>
                                    <asp:Literal ID="ltrCampaign" runat="server"></asp:Literal></td>
                                <td class="center">
                                    <asp:Literal ID="ltrNumberOfNews" runat="server"></asp:Literal></td>
                                <td class="center">
                                    <asp:Literal ID="ltrPostedDate" runat="server"></asp:Literal>
                                </td>
                                <td class="center">
                                    <asp:Literal ID="ltrNumOfFilesUploaded" runat="server"></asp:Literal>
                                </td>
                                <td id="tdEdit" runat="server" class="center">
                                    <asp:LinkButton ID="btnEdit" runat="server" Style="margin-right: 5px"><i class="fas fa-pencil-alt" title="Sửa"></i></asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa dòng này?')"><i class="fa fa-trash" title="Xóa"></i></asp:LinkButton>
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
                        <td id="tdPaging" runat="server" style="text-align: right" class="center"><span style="font-size: 13px; color: #262626"></span></td>

                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>

<tk:PopupPanel ID="popEdit" runat="server" Title="THÊM MỚI/CẬP NHẬT TUYẾN BÀI" Width="1100" CancelButton="btnCancel" OnPopupClosed="popEdit_PopupClosed">
    <PopupTemplate>
        <table class="table" style="width: 100%; margin-bottom: 0px">
            <colgroup>
                <col width="150" />
                <col />
            </colgroup>
            <tr>
                <th>Tuyến bài <span class="star">*</span></th>
                <td>
                    <asp:HiddenField ID="hidCampaignNewsID" runat="server"></asp:HiddenField>
                    <asp:TextBox ID="txtCampaign" runat="server" Width="100%" MaxLength="256"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>Số bài đăng
                </th>
                <td>
                    <tk:NumericTextBox ID="txtNumberOfNews" runat="server" AllowThousandDigit="false" NumberDecimalDigit="0"></tk:NumericTextBox>
                </td>
            </tr>
            <tr>
                <th>Ngày đăng
                </th>
                <td>
                    <tk:DatePicker ID="dpkPostedDate" runat="server" />
                </td>
            </tr>
            <tr>
                <th>Upload tài liệu
                </th>
                <td>
                    <div class="custom-file mb-3">
                        <asp:FileUpload runat="server" ID="fupDocument" class="custom-file-input-att" Style="width: 220px" AllowMultiple="true" accept=".doc,.docx,.xls,.xlsx,.pdf,.png,.jpg,.gif,.jpeg,.bmp" />
                        <label id="lblCustomAtt" class="custom-file-label-att" for="customFile">Choose file</label>
                    </div>
                    <asp:RegularExpressionValidator ID="revfulDocument"
                        Display="Dynamic" ValidationGroup="groupPopupAttachment"
                        ErrorMessage="Chỉ tải được file(Ảnh, Word, Excel và PDF)."
                        ControlToValidate="fupDocument" runat="server" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.gif|.GIF|.jpg|.JPG|.jpeg|.JPEG|.png|PNG|.doc|.DOC|.docx|.DOCX|.xls|.XLS|.xlsx|.XLSX|.pdf|.PDF)$" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <th>Tài liệu
                </th>
                <td>
                    <div class="row image-detail" style="text-align: center; margin-top: 10px; min-height: 102px" id="divDoc" runat="server">
                        <asp:Repeater ID="rptDoc" runat="server" OnItemDataBound="rptDoc_ItemDataBound" OnItemCommand="rptDoc_ItemCommand">
                            <ItemTemplate>
                                <div class="col-md-3 img-width" style="padding-left: 0px; margin-top: 5px; float: unset; display: -webkit-inline-box;">
                                    <div id="divViewDetailImage" runat="server" class="div-link">
                                        <img id="img" runat="server" src="" style="width: 99%; height: 220px; border-radius: 8px; object-fit: cover" />
                                    </div>
                                    <asp:LinkButton ID="btnDeleteDoc" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này?')" Style="position: absolute; right: 17px; top: 0px;"><i class="fa fa-times" title="Xóa ảnh"></i></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </td>
            </tr>
            <tr>
                <th>Danh sách bài viết
                </th>
                <td>
                    <uc:PostsUC ID="PostsUC" runat="server" />
                </td>
            </tr>
        </table>
        <div class="popup-toolbar" style="text-align: center">
            <asp:Button runat="server" ID="btnSaveAndContinue" Text="Lưu & Tiếp tục" OnClick="btnSaveAndContinue_Click" class="btn btn-primary" Style="background: #434a54" />
            <asp:Button runat="server" ID="btnSave" Text="Lưu" OnClick="btnSave_Click" class="btn btn-primary" Style="background: #434a54" />
            <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
        </div>
    </PopupTemplate>
</tk:PopupPanel>
