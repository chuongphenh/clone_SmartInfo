<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.PressAgencies.EditUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/CommentUC.ascx" TagName="CommentUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/OtherImageUC.ascx" TagName="OtherImageUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/PressAgencyHRUC.ascx" TagName="PressAgencyHRUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/PressAgencyHistoryUC.ascx" TagName="PressAgencyHistoryUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/PressAgencyMeetingUC.ascx" TagName="PressAgencyMeetingUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/RelationshipWithMBUC.ascx" TagName="RelationshipWithMBUC" TagPrefix="uc" %>
<%@ Register Src="~/UI/SmartInfos/PressAgencies/RelationsPressAgencyUC.ascx" TagName="RelationsPressAgencyUC" TagPrefix="uc" %>

<style>
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
</style>

<div style="position: fixed; right: 10px; top: 50%; opacity: 1; z-index: 9">
    <asp:LinkButton ID="btnSaveContinues" class="btnSaveContinue" OnClick="btnSaveContinues_Click" runat="server">
        <i class="far fa-save" style="color: white; margin-top: 4px; font-size: 16px; border-radius: 4px;"></i>
        <span style="font-size: 10px; display: inherit;">&nbsp; Lưu & Tiếp tục</span>
    </asp:LinkButton>
</div>
<asp:HiddenField ID="hidCurrentTab" runat="server" />
<asp:HiddenField ID="hidPressAgencyID" runat="server" />
<div class="err">
    <uc:ErrorMessage ID="ucErr" runat="server" />
</div>
<!--Blog Post-->
<div class="press-agency-top">
    <div class="row blog blog-medium margin-bottom-40">
        <div class="col-sm-3" style="padding-left: 0px">
            <div id="divViewDetailImage" runat="server" class="holder_default div-link">
                <asp:HiddenField runat="server" ID="hidAttID" />
                <img src="" id="img" runat="server" style="border: 3px dashed #7A97FC; width: 100%; height: 100%; object-fit: contain;" class=" hidden" />
            </div>
            <asp:FileUpload runat="server" ID="fileUpload" class="custom-file-input" Style="width: 97%"></asp:FileUpload>
            <div id="errUpload" class="err" style="display: none;">
                <ul>
                    <li>Chỉ tải được file (Ảnh)</li>
                </ul>
            </div>
            <label id="lblCustom" class="custom-file-label" for="customFile">Choose file</label>
        </div>
        <div id="text-right-display" class="col-sm-9" style="padding-right: 0px">
            <div style="display: block; border-bottom: 1px solid #597EF7;">
                <div class="row" style="margin-top: -5px; margin-bottom: 5px;">
                    <div class="col-sm-7" style="padding-left: 0px; padding-right: 0px; font-size: 14px; font-weight: 600; color: #595959">
                        Tên tổ chức <span class="star">*</span>
                    </div>
                    <div class="col-sm-5" style="padding-right: 0px">
                        <asp:LinkButton ID="btnExit" runat="server" OnClick="btnExit_Click" Style="float: right; margin-top: -5px; margin-left: 5px;background: #F2F3F8; padding-bottom: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                            <i class="fas fa-sign-out-alt" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Thoát"></i>
                            <span style="color: #595959;font-weight:600;font-size:16px;">Thoát</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" Style="float: right; margin-top: -5px; margin-left: 5px;background: #F2F3F8; padding-bottom: 5px;padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                            <i class="far fa-save" aria-hidden="true" style="color: #595959; margin-top: 4px; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Lưu"></i>
                            <span style="color: #595959;font-weight:600;font-size:16px;">Lưu</span>
                        </asp:LinkButton>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 10px">
                    <div class="col-sm-12" style="padding-left: 0px; padding-right: 0px;">
                        <asp:TextBox runat="server" ID="txtName" MaxLength="256" Width="100%" placeholder="Tên tổ chức" />
                    </div>
                </div>
                <div class="row" style="margin-bottom: 5px;">
                    <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px; font-size: 14px; font-weight: 600; color: #595959">
                        Loại tổ chức <span class="star">*</span>
                    </div>
                    <div id="divTitleTypeName" runat="server" visible="false" class="col-sm-6" style="padding-left: 0px; padding-right: 0px; font-size: 14px; font-weight: 600; color: #595959">
                        Tổ chức <span class="star">*</span>
                    </div>
                </div>
                <div class="row" style="margin-bottom: 10px">
                    <div class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                        <asp:DropDownList ID="ddlType" runat="server" Width="96%" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div id="divTypeName" runat="server" visible="false" class="col-sm-6" style="padding-left: 0px; padding-right: 0px;">
                        <asp:TextBox runat="server" ID="txtTypeName" MaxLength="256" Width="100%" placeholder="Tổ chức" />
                    </div>
                </div>
            </div>
            <ul class="press-agency-info">
                <li>
                    <div class="row">
                        <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Ngày thành lập:</span>
                        </div>
                        <div class="col-sm-8" style="padding-right: 0px;">
                            <tk:DatePicker ID="dpkEstablishedDTG" runat="server" DateFormat="DMY" Width="100%" />
                        </div>
                    </div>
                </li>
                <li>
                    <div class="row">
                        <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Email: <span class="star">*</span></span>
                        </div>
                        <div class="col-sm-8" style="padding-right: 0px;">
                            <asp:TextBox runat="server" ID="txtEmail" Width="100%" MaxLength="256" />
                        </div>
                    </div>
                </li>
                <li>
                    <div class="row">
                        <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Điện thoại: <span class="star">*</span></span>
                        </div>
                        <div class="col-sm-8" style="padding-right: 0px;">
                            <asp:TextBox runat="server" ID="txtPhone" Width="100%" MaxLength="256" />
                        </div>
                    </div>
                </li>
                <li>
                    <div class="row">
                        <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Cơ quan chủ quản: <span class="star">*</span></span>
                        </div>
                        <div class="col-sm-8" style="padding-right: 0px;">
                            <asp:TextBox runat="server" ID="txtAgency" Width="100%" MaxLength="256" />
                        </div>
                    </div>
                </li>
                <li>
                    <div class="row">
                        <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">FAX:</span>
                        </div>
                        <div class="col-sm-8" style="padding-right: 0px;">
                            <asp:TextBox runat="server" ID="txtFax" Width="100%" MaxLength="256" />
                        </div>
                    </div>
                </li>
                <li>
                    <div class="row">
                        <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Địa chỉ: <span class="star">*</span></span>
                        </div>
                        <div class="col-sm-8" style="padding-right: 0px;">
                            <asp:TextBox runat="server" ID="txtAddress" Width="100%" MaxLength="1024" />
                        </div>
                    </div>
                </li>
                <li>
                    <div class="row">
                        <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Đánh giá:</span>
                        </div>
                        <div class="col-sm-8" style="padding-right: 0px;">
                            <tk:TextArea runat="server" ID="txtRate" Width="100%" TextMode="MultiLine" Rows="1" />
                        </div>
                    </div>
                </li>
                <li>
                    <div class="row">
                        <div class="col-sm-4" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Mức độ quan hệ:</span>
                        </div>
                        <div class="col-sm-8" style="padding-right: 0px;">
                            <asp:DropDownList ID="ddlRelationshipWithMB" runat="server" Width="100%"></asp:DropDownList>
                        </div>
                    </div>
                </li>
                <li style="width: 100%">
                    <div class="row">
                        <div class="col-sm-2" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Số thứ tự hiển thị:</span>
                        </div>
                        <div class="col-sm-10" style="padding-right: 0px; padding-left: 5px">
                            <tk:NumericTextBox ID="numDisplayOrder" runat="server" Width="100%" AllowThousandDigit="false" NumberDecimalDigit="0"></tk:NumericTextBox>
                        </div>
                    </div>
                </li>
                <li style="width: 100%">
                    <div class="row">
                        <div class="col-sm-2" style="padding-left: 0px; display: flex;">
                            <span style="font-weight: 600">Ghi chú:</span>
                        </div>
                        <div class="col-sm-10" style="padding-right: 0px; padding-left: 5px">
                            <tk:TextArea runat="server" ID="txtNote" Width="100%" TextMode="MultiLine" Rows="1" />
                        </div>
                    </div>
                </li>
            </ul>
        </div>
    </div>
</div>
<div class="press-agency-bottom">
    <div class="list-table-title">
        <a class="title-active" id="aDanhSachLienHe" onclick="showTab(0)">Nhân sự</a>
        <a id="aLichSuThayDoiNhanSu" onclick="showTab(1)">Lịch sử thay đổi nhân sự</a>
        <a id="aLichSuHopTacGapGo" onclick="showTab(2)">Hợp đồng</a>
        <a id="aQuanHeGiuaCacCoQuanBaoChi" onclick="showTab(3)">Quan hệ các tổ chức</a>
        <a id="aLichSuQuanHe" onclick="showTab(4)">Lịch sử quan hệ</a>
        <a id="aAnhKhac" onclick="showTab(5)">Ảnh khác</a>
    </div>
    <div class="list-table-content" id="divDanhSachLienHe">
        <uc:PressAgencyHRUC ID="ucPressAgencyHR" runat="server"></uc:PressAgencyHRUC>
    </div>
    <div class="list-table-content" id="divLichSuThayDoiNhanSu" style="display: none">
        <uc:PressAgencyHistoryUC ID="ucPressAgencyHistory" runat="server"></uc:PressAgencyHistoryUC>
    </div>
    <div class="list-table-content" id="divLichSuHopTacGapGo" style="display: none">
        <uc:PressAgencyMeetingUC ID="ucPressAgencyMeeting" runat="server"></uc:PressAgencyMeetingUC>
    </div>
    <div class="list-table-content" id="divQuanHeGiuaCacCoQuanBaoChi" style="display: none">
        <uc:RelationsPressAgencyUC ID="ucRelationsPressAgency" runat="server"></uc:RelationsPressAgencyUC>
    </div>
    <div class="list-table-content" id="divLichSuQuanHe" style="display: none">
        <uc:RelationshipWithMBUC ID="ucRelationshipWithMB" runat="server"></uc:RelationshipWithMBUC>
    </div>
    <div class="list-table-content" id="divAnhKhac" style="display: none">
        <uc:OtherImageUC ID="ucOtherImage" runat="server"></uc:OtherImageUC>
    </div>
</div>
<!--End Blog Post-->
<div class="press-agency-bottom" style="padding-left: 20px; padding-right: 20px;">
    <hr style="margin-bottom: 10px; margin-top: 0px;" />
    <div class="row" style="margin-top: 10px; padding-bottom: 10px;" id="divComment" runat="server">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fa fa-comments" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959">Bình luận</span>
        </div>
    </div>
    <uc:CommentUC ID="ucComment" runat="server" />
</div>

<script>
    function showTab(index) {
        $("#<%= hidCurrentTab.ClientID %>").val(index);

        if (index == 0) {
            $('#aDanhSachLienHe').attr("class", "title-active");
            $('#aLichSuThayDoiNhanSu').attr("class", "");
            $('#aLichSuHopTacGapGo').attr("class", "");
            $('#aQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
            $('#aLichSuQuanHe').attr("class", "");
            $('#aAnhKhac').attr("class", "");

            $('#divDanhSachLienHe').show();
            $('#divLichSuThayDoiNhanSu').hide();
            $('#divLichSuHopTacGapGo').hide();
            $('#divQuanHeGiuaCacCoQuanBaoChi').hide();
            $('#divLichSuQuanHe').hide();
            $('#divAnhKhac').hide();
        }

        if (index == 1) {
            $('#aDanhSachLienHe').attr("class", "");
            $('#aLichSuThayDoiNhanSu').attr("class", "title-active");
            $('#aLichSuHopTacGapGo').attr("class", "");
            $('#aQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
            $('#aLichSuQuanHe').attr("class", "");
            $('#aAnhKhac').attr("class", "");

            $('#divDanhSachLienHe').hide();
            $('#divLichSuThayDoiNhanSu').show();
            $('#divLichSuHopTacGapGo').hide();
            $('#divQuanHeGiuaCacCoQuanBaoChi').hide();
            $('#divLichSuQuanHe').hide();
            $('#divAnhKhac').hide();
        }

        if (index == 2) {
            $('#aDanhSachLienHe').attr("class", "");
            $('#aLichSuThayDoiNhanSu').attr("class", "");
            $('#aLichSuHopTacGapGo').attr("class", "title-active");
            $('#aQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
            $('#aLichSuQuanHe').attr("class", "");
            $('#aAnhKhac').attr("class", "");

            $('#divDanhSachLienHe').hide();
            $('#divLichSuThayDoiNhanSu').hide();
            $('#divLichSuHopTacGapGo').show();
            $('#divQuanHeGiuaCacCoQuanBaoChi').hide();
            $('#divLichSuQuanHe').hide();
            $('#divAnhKhac').hide();
        }

        if (index == 3) {
            $('#aDanhSachLienHe').attr("class", "");
            $('#aLichSuThayDoiNhanSu').attr("class", "");
            $('#aLichSuHopTacGapGo').attr("class", "");
            $('#aQuanHeGiuaCacCoQuanBaoChi').attr("class", "title-active");
            $('#aLichSuQuanHe').attr("class", "");
            $('#aAnhKhac').attr("class", "");

            $('#divDanhSachLienHe').hide();
            $('#divLichSuThayDoiNhanSu').hide();
            $('#divLichSuHopTacGapGo').hide();
            $('#divQuanHeGiuaCacCoQuanBaoChi').show();
            $('#divLichSuQuanHe').hide();
            $('#divAnhKhac').hide();
        }

        if (index == 4) {
            $('#aDanhSachLienHe').attr("class", "");
            $('#aLichSuThayDoiNhanSu').attr("class", "");
            $('#aLichSuHopTacGapGo').attr("class", "");
            $('#aQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
            $('#aLichSuQuanHe').attr("class", "title-active");
            $('#aAnhKhac').attr("class", "");

            $('#divDanhSachLienHe').hide();
            $('#divLichSuThayDoiNhanSu').hide();
            $('#divLichSuHopTacGapGo').hide();
            $('#divQuanHeGiuaCacCoQuanBaoChi').hide();
            $('#divLichSuQuanHe').show();
            $('#divAnhKhac').hide();
        }

        if (index == 5) {
            $('#aDanhSachLienHe').attr("class", "");
            $('#aLichSuThayDoiNhanSu').attr("class", "");
            $('#aLichSuHopTacGapGo').attr("class", "");
            $('#aQuanHeGiuaCacCoQuanBaoChi').attr("class", "");
            $('#aLichSuQuanHe').attr("class", "");
            $('#aAnhKhac').attr("class", "title-active");

            $('#divDanhSachLienHe').hide();
            $('#divLichSuThayDoiNhanSu').hide();
            $('#divLichSuHopTacGapGo').hide();
            $('#divQuanHeGiuaCacCoQuanBaoChi').hide();
            $('#divLichSuQuanHe').hide();
            $('#divAnhKhac').show();
        }

        var keepscroll = window.setTimeout(function () {
            var cs = document.cookie ? document.cookie.split(';') : [];
            var i = 0, cslen = cs.length;
            for (; i < cs.length; i++) {
                var c = cs[i].split('=');
                if (c[0].trim() == "keepscroll") {
                    window.scrollTo(0, parseInt(c[1]));
                    break;
                }
            }
            window.clearTimeout(keepscroll);
            keepscroll = null;
        }, 0);
    }

    $("#<%= fileUpload.ClientID %>").height($("#lblCustom").height() + 10);
    $(".holder_default").height($("#text-right-display").height() - 40);

    $(".custom-file-input").on("change", function (event) {
        var file = event.target.files[0];
        if (validateFile(file)) {
            var reader = new FileReader();
            reader.onload = function (event) {
                document.getElementById('<%= img.ClientID %>').className = 'visible'
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
                    document.getElementById('<%= img.ClientID %>').className = 'visible'
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
</script>
