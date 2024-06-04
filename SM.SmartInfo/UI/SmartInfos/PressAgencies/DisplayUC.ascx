<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.SmartInfos.PressAgencies.DisplayUC" %>

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
</style>

<asp:HiddenField ID="hidCurrentTab" runat="server" />
<asp:HiddenField ID="hidPressAgencyID" runat="server" />
<div class="err">
    <uc:ErrorMessage ID="ucErr" runat="server" />
</div>
<!--Blog Post-->
<div class="press-agency-top">
    <div class="row blog blog-medium margin-bottom-40">
        <div class="col-sm-3" style="padding-left: 0px">
            <div class="holder div-link" id="divViewDetailImage" runat="server">
                <img src="" id="img" runat="server" style="width: 95%; border-radius: 12px; object-fit: contain" />
            </div>
        </div>
        <div id="text-right-display" class="col-sm-9" style="padding-right: 0px">
            <div style="display: block; border-bottom: 1px solid #597EF7;">
                <h4 style="margin-bottom: 10px; font-size: 16px; font-weight: bold; color: #262626;">
                    <asp:Literal runat="server" ID="ltrName" />
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm('Bạn có chắc chắn muốn xóa tổ chức này không?')" Style="float: right; margin-top: -5px; margin-left: 5px;background: #F2F3F8; padding-bottom: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                        <i class="fa fa-trash" aria-hidden="true" style="color: #595959; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Xóa"></i>
                        <span style="color: #595959;font-weight:600;font-size:16px;">Xóa</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" Style="float: right; margin-top: -5px; margin-left: 5px;background: #F2F3F8; padding-bottom: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;">
                        <i class="fas fa-pencil-alt" aria-hidden="true" style="color: #595959; font-size: 16px; background: #F2F3F8; padding-bottom: 5px; padding-top: 5px; padding-left: 10px; padding-right: 10px; border-radius: 4px;" title="Sửa"></i>
                        <span style="color: #595959;font-weight:600;font-size:16px;">Sửa</span>
                    </asp:LinkButton>
                </h4>
            </div>
            <%--<div style="display: block; border-bottom: 1px solid #597EF7;">
                
                <h4 style="margin: 10px 0; font-size: 16px; font-weight: bold; color: #262626;">
                    <asp:Literal runat="server" ID="ltrName" />
                </h4>
            </div>--%>
            <ul class="press-agency-info">
                <li>
                    <p>
                        <span style="font-weight: 600">Ngày thành lập:</span>
                        <asp:Literal runat="server" ID="ltrEstablishedDTG" />
                    </p>
                </li>
                <li>
                    <p>
                        <span style="font-weight: 600">Email:</span>
                        <asp:Literal runat="server" ID="ltrEmail" />
                    </p>
                </li>
                <li>
                    <p>
                        <span style="font-weight: 600">Điện thoại:</span>
                        <asp:Literal runat="server" ID="ltrPhone" />
                    </p>
                </li>
                <li>
                    <p>
                        <span style="font-weight: 600">Cơ quan chủ quản:</span>
                        <asp:Literal runat="server" ID="ltrAgency" />
                    </p>
                </li>
                <li>
                    <p>
                        <span style="font-weight: 600">Fax:</span>
                        <asp:Literal runat="server" ID="ltrFax" />
                    </p>
                </li>
                <li>
                    <p>
                        <span style="font-weight: 600">Địa chỉ:</span>
                        <asp:Literal runat="server" ID="ltrAddress" />
                    </p>
                </li>
                <li>
                    <p>
                        <span style="font-weight: 600">Đánh giá tổ chức:</span>
                        <asp:Label runat="server" ID="ltrRate" Style="display: inline-flex" />
                    </p>
                </li>
                <li>
                    <p>
                        <span style="font-weight: 600">Mức độ quan hệ:</span>
                        <asp:Label runat="server" ID="ltrRelationshipWithMB" Style="display: inline-flex" />
                    </p>
                </li>
                <li style="width: 100%">
                    <p>
                        <span style="font-weight: 600">Loại tổ chức:</span>
                        <asp:Label runat="server" ID="ltrType" Style="display: inline-flex" />
                    </p>
                </li>
                <li style="width: 100%">
                    <p>
                        <span style="font-weight: 600">Ghi chú:</span>
                        <asp:Label runat="server" ID="ltrNote" Style="display: inline-flex" />
                    </p>
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
    <div class="row" style="margin-top: 10px; padding-bottom: 10px;">
        <div class="col-md-12" style="text-align: left; padding-left: 0px">
            <i class="fa fa-comments" style="margin-right: 10px; color: #8C8C8C"></i><span style="color: #595959;font-size:16px;font-weight:600;">Bình luận</span>
        </div>
    </div>
    <uc:CommentUC ID="ucComment" runat="server" />
</div>

<script>
    $('#<%= img.ClientID %>').height($('#text-right-display').height());

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
</script>
