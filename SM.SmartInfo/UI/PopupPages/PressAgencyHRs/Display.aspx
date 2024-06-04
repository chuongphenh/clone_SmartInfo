<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Title="Chi tiết nhân sự tổ chức"
    Inherits="SM.SmartInfo.UI.PopupPages.PressAgencyHRs.Display"
    MasterPageFile="~/UI/MasterPages/Common/Popup.Master" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRUC.ascx" TagName="PressAgencyHRUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRAlertUC.ascx" TagName="PressAgencyHRAlertUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRHistoryUC.ascx" TagName="PressAgencyHRHistoryUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/PopupPages/PressAgencyHRs/PressAgencyHRRelativesUC.ascx" TagName="PressAgencyHRRelativesUC" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidPressAgencyID" />
    <asp:HiddenField runat="server" ID="hidPressAgencyHRID" />
    <style>
        .negative {
            color: white;
            padding: 7px;
            padding-bottom: 2px;
            padding-top: 2px;
            border: 1px solid red;
            border-radius: 50px;
            background: red;
            font-size: 17px;
        }

        .medium {
            color: black;
            padding: 7px;
            padding-bottom: 2px;
            padding-top: 2px;
            border: 1px solid orange;
            border-radius: 50px;
            background: orange;
            font-size: 14px;
        }

        .positive {
            color: white;
            padding: 7px;
            padding-bottom: 2px;
            padding-top: 2px;
            border: 1px solid limegreen;
            border-radius: 50px;
            background: limegreen;
            font-size: 17px;
        }

        table.table > thead > tr > th {
            text-align: center;
        }

        table.table > tbody > tr > td.center {
            text-align: center;
        }

        h3 {
            border-bottom: 1px solid #eb0028;
        }

        .p-content {
            margin-left: 30px;
        }

        .holder_default {
            border: 3px dashed #ccc;
        }

        #holder.hover {
            border: 3px dashed #0c0 !important;
        }

        .hidden {
            visibility: hidden;
        }

        .div-link:hover {
            cursor: pointer;
        }

        .view-all {
            font-size: 12px;
            color: white;
            float: right;
            padding: 1px;
            padding-right: 15px;
            padding-left: 15px;
            border: 1px solid #597EF7;
            background: #597EF7;
            border-radius: 100px;
        }

            .view-all:hover {
                cursor: pointer;
            }

        .custom-file-input {
            opacity: 0;
            z-index: 2;
            position: relative;
        }

        .custom-file-label {
            z-index: 1;
            padding: .375rem .75rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            margin-bottom: 10px;
            position: absolute;
            top: 85px;
            width: 490px;
        }

        .fa-times {
            color: #595959;
            margin-top: 4px;
            font-size: 12px;
            background: #F2F3F8;
            padding-bottom: 3px;
            padding-top: 3px;
            padding-left: 5px;
            padding-right: 5px;
            border-radius: 4px;
        }

            .fa-times:hover {
                color: #597EF7 !important;
                margin-top: 4px;
                font-size: 12px;
                background: #F2F3F8;
                padding-bottom: 3px;
                padding-top: 3px;
                padding-left: 5px;
                padding-right: 5px;
                border-radius: 4px;
            }
    </style>
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>

    <div class="toolbar">
        NHÂN SỰ TỔ CHỨC
        <asp:Label ID="lblPressAgencyName" runat="server"></asp:Label>
        <asp:Label ID="lblHRTypeName" runat="server"></asp:Label>
        <ul class="icon_toolbar">
            <li>
                <asp:HyperLink ID="lnkEdit" runat="server" Text="Sửa" Style="float: right; margin-left: 5px"> 
                    <i class="fas fa-pencil-alt" aria-hidden="true" style="color: #595959; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Sửa"> Sửa</i>
                </asp:HyperLink>
            </li>
            <li>
                <asp:LinkButton ID="btnExit" runat="server" OnClientClick='refreshParent()' Style="float: right; margin-left: 5px">
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thoát"> Thoát</i>
                </asp:LinkButton>
            </li>
        </ul>
    </div>
    <div style="margin-top: 50px; padding: 10px; width: 100%">
        <div class="body-content">
            <div class="row blog-page">
                <div class="col-sm-12 md-margin-bottom-40">
                    <div class="press-agency-top" style="border: 1px solid #D9D9D9; display: flex;">
                        <div style="width: 120px; margin-right: 30px;" id="divViewDetailImage" runat="server" class="div-link">
                            <img runat="server" id="img" class="img-responsive" alt="" style="width: 120px; height: 120px; object-fit: contain">
                        </div>
                        <div style="flex: 1;">
                            <h4>
                                <span style="margin-right: 15px; font-size: 16px; font-weight: bold; color: #262626;">
                                    <asp:Literal runat="server" ID="ltrFullName" />
                                </span>
                                <span>
                                    <asp:Label runat="server" ID="ltrAttitude" />
                                </span>
                            </h4>
                            <p style="font-size: 12px; color: #595959; border-bottom: 1px solid #597EF7; padding-bottom: 12px; margin-bottom: 18px;">
                                <asp:Literal runat="server" ID="ltrPosition" />
                            </p>
                            <div style="width: 100%; display: flex; justify-content: flex-start;">
                                <div style="display: flex; margin-right: 40px; max-width: 200px;">
                                    <span style="background: #F5F5F7; border-radius: 50%; width: 40px; height: 40px; text-align: center; margin-right: 12px;">
                                        <i class="far fa-user" style="font-size: 20px; line-height: 40px; color: #8C8C8C;"></i>
                                    </span>
                                    <div>
                                        <p style="margin: 4px 0 0;">Tuổi</p>
                                        <p style="font-weight: bold;">
                                            <asp:Literal runat="server" ID="ltrAge" />
                                        </p>
                                    </div>
                                </div>
                                <div style="display: flex; margin-right: 40px; max-width: 200px;">
                                    <span style="background: #F5F5F7; border-radius: 50%; width: 40px; height: 40px; text-align: center; margin-right: 12px;">
                                        <i class="fa fa-birthday-cake" style="font-size: 20px; line-height: 40px; color: #8C8C8C;"></i>
                                    </span>
                                    <div>
                                        <p style="margin: 4px 0 0;">Ngày sinh</p>
                                        <p style="font-weight: bold;">
                                            <asp:Literal runat="server" ID="ltrDOB" />
                                        </p>
                                    </div>
                                </div>
                                <div style="display: flex; margin-right: 40px; max-width: 200px;">
                                    <span style="background: #F5F5F7; border-radius: 50%; width: 40px; height: 40px; text-align: center; margin-right: 12px;">
                                        <i class="fas fa-mobile-alt" style="font-size: 20px; line-height: 40px; color: #8C8C8C;"></i>
                                    </span>
                                    <div>
                                        <p style="margin: 4px 0 0;">Điện thoại</p>
                                        <p style="font-weight: bold;">
                                            <asp:Literal runat="server" ID="ltrMobile" />
                                        </p>
                                    </div>
                                </div>
                                <div style="display: flex; margin-right: 40px; max-width: 200px;">
                                    <span style="background: #F5F5F7; border-radius: 50%; width: 40px; height: 40px; text-align: center; margin-right: 12px;">
                                        <i class="far fa-envelope" style="font-size: 20px; line-height: 40px; color: #8C8C8C;"></i>
                                    </span>
                                    <div>
                                        <p style="margin: 4px 0 0;">Email</p>
                                        <p style="font-weight: bold;">
                                            <asp:Literal runat="server" ID="ltrEmail" />
                                        </p>
                                    </div>
                                </div>
                                <div style="display: flex; margin-right: 40px; max-width: 200px;">
                                    <span style="background: #F5F5F7; border-radius: 50%; width: 40px; height: 40px; text-align: center; margin-right: 12px;">
                                        <i class="fas fa-map-marker-alt" style="font-size: 20px; line-height: 40px; color: #8C8C8C;"></i>
                                    </span>
                                    <div>
                                        <p style="margin: 4px 0 0;">Địa chỉ</p>
                                        <p style="font-weight: bold;">
                                            <asp:Literal runat="server" ID="ltrAddress" />
                                        </p>
                                    </div>
                                </div>
                                <div style="display: flex; margin-right: 40px; max-width: 200px;">
                                    <span style="background: #F5F5F7; border-radius: 50%; width: 40px; height: 40px; text-align: center; margin-right: 12px;">
                                        <i class="fas fa-swimmer" style="font-size: 20px; line-height: 40px; color: #8C8C8C;"></i>
                                    </span>
                                    <div>
                                        <p style="margin: 4px 0 0;">Sở thích</p>
                                        <p style="font-weight: bold;">
                                            <asp:Literal runat="server" ID="ltrHobby" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row blog-page">
                <div class="col-sm-3 md-margin-bottom-40">
                    <div class="press-agency-top" style="border: 1px solid #D9D9D9; display: flex;">
                        <uc:PressAgencyHRHistoryUC ID="ucPressAgencyHRHistory" runat="server"></uc:PressAgencyHRHistoryUC>
                    </div>
                </div>
                <div class="col-sm-3 md-margin-bottom-40">
                    <div class="press-agency-top" style="border: 1px solid #D9D9D9; display: flex;">
                        <uc:PressAgencyHRAlertUC ID="ucPressAgencyHRAlert" runat="server"></uc:PressAgencyHRAlertUC>
                    </div>
                </div>
                <div class="col-sm-3 md-margin-bottom-40">
                    <div class="press-agency-top" style="border: 1px solid #D9D9D9; display: flex;">
                        <div class="pa-popup" style="max-height: 325px; min-height: 325px; overflow-y: scroll;">
                            <div class="pa-popup-title">
                                <span>&nbsp;</span>
                                Thông tin liên quan
                            </div>
                            <p style="margin-top: 20px">
                                <asp:Literal runat="server" ID="ltrRelatedInformation" />
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3 md-margin-bottom-40">
                    <div class="press-agency-top" style="border: 1px solid #D9D9D9; display: flex;">
                        <uc:PressAgencyHRRelativesUC ID="ucPressAgencyHRRelatives" runat="server"></uc:PressAgencyHRRelativesUC>
                    </div>
                </div>
            </div>
            <div class="row blog-page">
                <div class="col-sm-12 md-margin-bottom-40">
                    <div class="press-agency-bottom" style="margin-bottom: 40px;">
                        <div class="list-table-title">
                            <span style="font-size: 16px; font-weight: bold; color: #464457; padding: 0 15px 15px 0; display: inline-block;">Danh sách cùng nhóm</span>
                            <asp:Label ID="ltrAttitude1" runat="server"></asp:Label>
                        </div>
                        <div class="list-table-content pa-popup-same-group">
                            <uc:PressAgencyHRUC ID="ucPressAgencyHR" runat="server"></uc:PressAgencyHRUC>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row blog-page">
                <div class="col-sm-12 md-margin-bottom-40">
                    <div class="press-agency-bottom" style="margin-bottom: 40px;">
                        <div class="list-table-title">
                            <span style="font-size: 16px; font-weight: bold; color: #464457; padding: 0 15px 15px 0; display: inline-block;">
                                <i class="fa fa-image" style="margin-right: 10px; color: #8C8C8C"></i>Hình ảnh khác
                            </span>
                            <asp:Button ID="btnShowPopupUpload" OnClick="btnShowPopupUpload_Click" runat="server" class="view-all" Text="Upload files" Style="margin-right: 5px; margin-top: 5px"></asp:Button>
                        </div>
                        <div class="list-table-content pa-popup-same-group row" id="divImage" runat="server">
                            <asp:Repeater ID="rptImage" runat="server" OnItemDataBound="rptImage_ItemDataBound" OnItemCommand="rptImage_ItemCommand">
                                <ItemTemplate>
                                    <div class="col-md-3" style="padding-left: 0px; margin-top: 5px">
                                        <div id="divViewDetailImage" runat="server" class="div-link">
                                            <img id="img" runat="server" src="" style="width: 99%; height: 120px; border-radius: 8px; object-fit: cover" />
                                        </div>
                                        <asp:LinkButton ID="btnDeleteImage" runat="server" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa ảnh này?')" Style="position: absolute; right: 17px; top: 0px;"><i class="fa fa-times" title="Xóa ảnh"></i></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row blog-page">
                <div class="col-sm-12 md-margin-bottom-40">
                    <div class="press-agency-bottom" style="margin-bottom: 40px;">
                        <div class="list-table-title">
                            <span style="font-size: 16px; font-weight: bold; color: #464457; padding: 0 15px 15px 0; display: inline-block;">
                                <i class="fa fa-comments" style="margin-right: 10px; color: #8C8C8C"></i>Bình luận
                            </span>
                        </div>
                        <div class="list-table-content pa-popup-same-group" style="width:100% !important">
                            <uc:CommentUC ID="ucComment" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <tk:PopupPanel runat="server" ID="popUpload" Title="Tải tài liệu" Width="500px" Height="100px" CancelButton="btnCancel">
        <PopupTemplate>
            <div class="popup-content" style="margin-top: 0px; width: 100%; height: 100%">
                <table class="tabLogin" style="width: 100%">
                    <colgroup>
                        <col width="50%" />
                        <col />
                    </colgroup>
                    <tr style="height: 30px; font-weight: bold;">
                        <td colspan="2">Chọn tài liệu
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="custom-file mb-3">
                                <asp:FileUpload runat="server" ID="fileUpload" class="custom-file-input" Style="width: 100%"></asp:FileUpload>
                                <div id="errUpload" class="err" style="display: none;">
                                    <ul>
                                        <li>Chỉ tải được file (Ảnh)</li>
                                    </ul>
                                </div>
                                <label id="lblCustom" class="custom-file-label" for="customFile">Choose file</label>
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 30px; font-weight: bold">
                        <td colspan="2" style="padding-top: 15px; padding-bottom: 5px;">Mô tả tài liệu
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <tk:TextArea runat="server" ID="txtDescription" Width="100%" MaxLength="512" TextMode="MultiLine" Rows="3" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="popup-toolbar" style="text-align: center">
                <asp:Button runat="server" ID="btnPopupUpload" Text="Tải lên" OnClick="btnPopupUpload_Click" class="btn btn-primary" Style="background: #434a54" />
                <asp:Button runat="server" ID="btnCancel" Text="Bỏ qua" class="btn btn-primary" Style="background: #434a54; margin-left: 15px" />
            </div>
        </PopupTemplate>
    </tk:PopupPanel>

    <script type="text/javascript">
        $(".holder_default").height($("#text-right").height() - 45);

        $("#<%= fileUpload.ClientID %>").height($("#lblCustom").height() + 10);

        $(".custom-file-input").on("change", function (event) {
            var file = event.target.files[0];
            if (validateFile(file)) {
                var fileName = $(this).val().split("\\").pop();
                $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
            }
        });

        function validateFile(file) {
            var extension = getExtension(file.name);
            if (extension != "jpg"
                && extension != "JPG"
                && extension != "jpeg"
                && extension != "JPEG"
                && extension != "png"
                && extension != "PNG") {
                $('#<%= fileUpload.ClientID %>').val("");
                setTimeout(function () {
                    $('#errUpload').hide('blind', {}, 500)
                }, 15000)
                $('#errUpload').show();
                return false;
            }

            return true;
        }

        function getExtension(filename) {
            var parts = filename.split('.');
            return parts[parts.length - 1];
        }
    </script>
</asp:Content>