<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Title="Cập nhật nhân sự tổ chức"
    Inherits="SM.SmartInfo.UI.PopupPages.PressAgencyHRs.Edit"
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
        }

        .medium {
            color: black;
            padding: 7px;
            padding-bottom: 2px;
            padding-top: 2px;
            border: 1px solid orange;
            border-radius: 50px;
            background: orange;
        }

        .positive {
            color: white;
            padding: 7px;
            padding-bottom: 2px;
            padding-top: 2px;
            border: 1px solid limegreen;
            border-radius: 50px;
            background: limegreen;
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

            .holder_default.hover {
                border: 3px dashed #0c0 !important;
            }

        .hidden {
            visibility: hidden;
        }

        .visible {
            visibility: visible;
        }

        .custom-file-input {
            opacity: 0;
            z-index: 2;
            position: absolute;
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
            width: 100%;
        }

        a:hover {
            color: aquamarine;
        }

        .div-link:hover {
            cursor: pointer;
        }

        .btnSaveContinue {
            float: right;
            margin-top: -5px;
            padding-right: 15px;
            padding-left: 15px;
            padding-bottom: 2px;
            padding-top: 0px;
            border: 1px solid #597EF7;
            background: #597EF7;
            border-radius: 100px;
            color: white;
            font-size: 10px;
            text-align: center;
        }

            .btnSaveContinue:hover {
                text-decoration: unset;
                color: white;
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

        .custom-file-input-other-image {
            opacity: 0;
            z-index: 2;
            position: relative;
        }

        .custom-file-label-other-image {
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
            top: 6px;
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

            #MainContent_cmbGroups_cmbGroupspnlData {
                width: 12% !important; /* Adjust the width of the dropdown menu */
                position: absolute;
                left: 0; /* Align dropdown menu with the ComboCheckBox */
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
                <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" runat="server" Style="float: right; margin-top: -5px; margin-left: 5px">
                    <i class="far fa-save" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Lưu"> Lưu</i></asp:LinkButton>
            </li>
            <li>
                <asp:HyperLink ID="lnkExit" runat="server" Style="float: right; margin-top: -5px; margin-left: 5px"> 
                    <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thoát"> Thoát</i> </asp:HyperLink>
            </li>
        </ul>
    </div>
    <div style="position: fixed; right: 10px; top: 50%; opacity: 1; z-index: 9">
        <asp:LinkButton ID="btnSaveContinues" class="btnSaveContinue" OnClick="btnSaveContinues_Click" runat="server">
            <i class="far fa-save" style="color: white; margin-top: 4px; font-size: 16px; border-radius: 4px;"></i>
            <span style="font-size: 10px; display: inherit;">&nbsp; Lưu & Tiếp tục</span>
        </asp:LinkButton>
    </div>
    <div style="margin-top: 50px; padding: 35px; width: 100%">
        <div class="content">
            <div class="row">
                <div class="col-md-12 md-margin-bottom-40" style="padding-left: 0px; padding-right: 0px">
                    <!--Blog Post-->
                    <div class="row blog blog-medium margin-bottom-40">
                        <div class="col-md-4">
                            <div id="divViewDetailImage" runat="server" class="holder_default div-link">
                                <asp:HiddenField runat="server" ID="hidAttID" />
                                <img alt="" src="" id="img" runat="server" class="img-responsive" style="height: 100%; object-fit: contain" />
                            </div>
                            <asp:FileUpload runat="server" ID="fileUpload" ClientIDMode="Static" class="custom-file-input" Style="width: 95%"></asp:FileUpload>
                            <div id="errUpload" class="err" style="display: none;">
                                <ul>
                                    <li>Chỉ tải được file (Ảnh)</li>
                                </ul>
                            </div>
                            <label id="lblCustom" class="custom-file-label" for="fileUpload">Choose file</label>
                        </div>
                        <div id="text-right" class="col-md-4">
                            <p>
                                Họ và tên: <span class="star">*</span>
                                <asp:TextBox runat="server" ID="txtFullName" Width="100%" MaxLength="256" />
                            </p>
                            <p>
                                Chức danh: <span class="star">*</span>
                                <asp:TextBox runat="server" ID="txtPosition" Width="100%" MaxLength="256" />
                            </p>
                            <p>
                                Thái độ: <span class="star">*</span>
                                <asp:DropDownList runat="server" ID="ddlAttitude" Width="100%" />
                            </p>
                            <h3>Thông tin cơ bản</h3>
                            <p>
                                Tuổi:
                                    <asp:Literal runat="server" ID="ltrAge" />
                            </p>
                            <p>
                                Ngày sinh: <span class="star">*</span>
                                <tk:DatePicker ID="dpkDOB" runat="server" Width="100%" DateFormat="DMY" />
                            </p>
                            <p>
                                SĐT:
                                    <asp:TextBox runat="server" ID="txtMobile" Width="100%" MaxLength="256" />
                            </p>
                            <p>
                                Email:
                                    <asp:TextBox runat="server" ID="txtEmail" Width="100%" MaxLength="256" />
                            </p>
                            <p>
                                Địa chỉ:
                                    <asp:TextBox runat="server" ID="txtAddress" Width="100%" MaxLength="1024" />
                            </p>
                            <p>
                                Sở thích:
                                    <tk:TextArea runat="server" ID="txtHobby" Width="100%" MaxLength="512" TextMode="MultiLine" Rows="1" />
                            </p>
                            <p>
                                Nhóm:
                                    <tk:ComboCheckBox ID="cmbGroups" runat="server" Width="100%" CssClass="custom-combobox" />
                            </p>
                        </div>
                    </div>
                    <!--End Blog Post-->
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
                            <tk:TextArea Style="margin-bottom: 10px" runat="server" ID="txtRelatedInformation" Width="100%" TextMode="MultiLine" Rows="13" />
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
                                        <img alt="" id="img" runat="server" src="" style="width: 99%; height: 120px; border-radius: 8px; object-fit: cover" />
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
                    <div class="list-table-content pa-popup-same-group">
                        <uc:CommentUC ID="ucComment" runat="server" />
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
                                <asp:FileUpload runat="server" ID="fileUploadOtherImage" ClientIDMode="Static" class="custom-file-input-other-image" Style="width: 100%"></asp:FileUpload>
                                <div id="errUploadOtherImage" class="err" style="display: none;">
                                    <ul>
                                        <li>Chỉ tải được file (Ảnh)</li>
                                    </ul>
                                </div>
                                <label id="lblCustomOtherImage" class="custom-file-label-other-image" for="fileUploadOtherImage">Choose file</label>
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
        $("#<%= fileUpload.ClientID %>").height($("#lblCustom").height() + 10);

        $(".holder_default").height($("#text-right").height() - 45);

        $(".custom-file-input").on("change", function (event) {
            var file = event.target.files[0];
            if (validateFile(file)) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    document.getElementById('<%= img.ClientID %>').className = 'img-responsive visible'
                    $('#<%= img.ClientID %>').attr('src', event.target.result);
                }
                reader.readAsDataURL(file);
            }
        });

        $(document).ready(function () {
            var holder = document.getElementById('<%= divViewDetailImage.ClientID %>');
            holder.ondragover = function () { this.className = 'hover'; return false; };
            holder.ondrop = function (e) {
                e.preventDefault();
                var file = e.dataTransfer.files[0];
                if (validateFile(file)) {
                    var reader = new FileReader();
                    reader.onload = function (event) {
                        document.getElementById('<%= img.ClientID %>').className = 'img-responsive visible'
                        $('#<%= img.ClientID %>').attr('src', event.target.result);
                    }
                    reader.readAsDataURL(file);
                }
            };

            let target = document.documentElement;
            let body = document.body;
            var inputFile = document.getElementById('<%= fileUpload.ClientID %>');

            target.addEventListener('dragover', (e) => {
                e.preventDefault();
                body.classList.add('dragging');
            });

            target.addEventListener('dragleave', () => {
                body.classList.remove('dragging');
            });

            target.addEventListener('drop', (e) => {
                e.preventDefault();
                body.classList.remove('dragging');

                inputFile.files = e.dataTransfer.files;
            });
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

        $("#<%= fileUploadOtherImage.ClientID %>").height($("#lblCustom").height() + 10);

        $(".custom-file-input-other-image").on("change", function (event) {
            var file = event.target.files[0];
            if (validateFile(file)) {
                var fileName = $(this).val().split("\\").pop();
                $(this).siblings(".custom-file-label-other-image").addClass("selected").html(fileName);
            }
        });
    </script>
</asp:Content>
