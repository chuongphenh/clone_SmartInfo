<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDetailUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.Administrations.Users.UserDetailUC" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage"
    TagPrefix="uc" %>

<div class="list-table-content" style="padding-top: 50px; padding-bottom: 40px">
    <div class="err">
        <uc:ErrorMessage ID="ucErr" runat="server" />
    </div>
    <div class="row">
        <div class="col-sm-12">
            <div style="display: flex; align-items: center; margin-bottom: 20px;">
                <div style="width: 120px; margin-right: 30px; position: relative; border-radius: 50%;">
                    <%--<img src="../../../Images/avatar.png" alt="image user detail" style="width: 120px; height: 120px;" />--%>
                    <asp:Image ID="UserImage" runat="server" AlternateText="image user detail" Width="120px" Height="120px" Style="object-fit: cover; border-radius:50%;" />
                    <input type="file" id="fileInput"  accept=".jpg, .jpeg, .png" style="display: none;" />
                    <label for="fileInput" style="position: absolute; bottom: -10px; left: 97%; transform: translateX(-50%);">                    <svg width="20px" height="20px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--!Font Awesome Free 6.5.1 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.--><path d="M149.1 64.8L138.7 96H64C28.7 96 0 124.7 0 160V416c0 35.3 28.7 64 64 64H448c35.3 0 64-28.7 64-64V160c0-35.3-28.7-64-64-64H373.3L362.9 64.8C356.4 45.2 338.1 32 317.4 32H194.6c-20.7 0-39 13.2-45.5 32.8zM256 192a96 96 0 1 1 0 192 96 96 0 1 1 0-192z"/></svg>
</label>
                </div>
                <div style="flex: 1;">
                    <h4>
                        <span style="margin-right: 15px; font-size: 16px; font-weight: bold; color: #262626;">
                            <asp:Literal runat="server" ID="ltrName" />
                        </span>
                    </h4>
                    <p style="font-size: 14px; color: #595959; margin: 0;">
                        <asp:Literal runat="server" ID="ltrNote" />
                    </p>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6">
            <table class="tabLogin" style="width: 100%" cellspacing="0" cellpadding="0">
                <colgroup>
                    <col width="20%" />
                    <col width="30%" />
                    <col width="20%" />
                    <col width="30%" />
                </colgroup>
                <tr style="height: 40px; line-height: 1.34;">
                    <td style="text-align: left; font-weight: bold">Tên đăng nhập 
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrUserName" />
                    </td>
                    <td style="text-align: left; font-weight: bold">Tên đầy đủ
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrFullName" />
                    </td>
                </tr>
                <tr style="height: 40px;">
                    <td style="text-align: left; font-weight: bold">Mã nhân viên
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrEmployeeCode" />
                    </td>
                    <td style="text-align: left; font-weight: bold">Khối quản lý
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrSector" />
                    </td>
                    <%-- <th>Số CIF</th>
            <td>
                <asp:Literal runat="server" ID="ltrCIFCode" />
            </td>--%>
                </tr>
                <tr style="height: 40px">
                    <td style="text-align: left; font-weight: bold">Chuyên viên phòng
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrOrganizationEmployee" />
                    </td>
                    <td style="text-align: left; font-weight: bold">Quản lý phòng
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrOrganizationManager" />
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td style="text-align: left; font-weight: bold">Ngày sinh
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrDOB" />
                    </td>
                    <td style="text-align: left; font-weight: bold">Giới tính
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrGender" />
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td style="text-align: left; font-weight: bold">Điện thoại nhà riêng
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrHomePhone" />
                    </td>
                    <td style="text-align: left; font-weight: bold">Số Mobile
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrMobilePhone" />
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td style="text-align: left; font-weight: bold">Email
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrEmail" />
                    </td>
                    <td style="text-align: left; font-weight: bold">Trạng thái
                    </td>
                    <td>
                        <asp:Literal runat="server" ID="ltrStatus" />
                    </td>
                </tr>

                <%--<th>Cấp bậc
            </th>
            <td>
                <asp:Literal runat="server" ID="ltrLevel" />
            </td>--%>

                <%--<tr>
            <th>Mã chi nhánh</th>
            <td>
                <asp:Literal runat="server" ID="ltrListBranchCode" />
            </td>
            <th>Là quản lý</th>
            <td>
                <asp:CheckBox runat="server" ID="chkIsManager" Text="Quản lý" Enabled="false" />
            </td>
        </tr>--%>
                <tr style="height: 40px">
                    <td style="text-align: left; font-weight: bold">Phương thức xác thực</td>
                    <td>
                        <asp:Literal runat="server" ID="ltrAuthorizationType" />
                    </td>
                    <td style="text-align: left; font-weight: bold">Cấu hình AD</td>
                    <td>
                        <asp:Literal runat="server" ID="ltrLdapCnnName" />
                    </td>
                </tr>
                <tr style="height: 40px">
                    <td style="text-align: left; font-weight: bold">Hình ảnh chữ ký</td>
                    <td colspan="3">
                        <asp:Image runat="server" ID="imgSignImage" Height="100px" />
                    </td>
                </tr>
            </table>

        </div>

        <div class="col-sm-6">
            <h3>Vai trò</h3>
            <div class="new-grid">
                <asp:DataGrid ID="grdRole" runat="server" ShowHeader="true" ShowFooter="false" AllowPaging="false"
                    AllowCustomPaging="false" AutoGenerateColumns="false" GridLines="None"
                    BackColor="White" OnItemDataBound="grdRole_ItemDataBound">
                    <HeaderStyle CssClass="grid-header" />
                    <ItemStyle CssClass="grid-even" />
                    <AlternatingItemStyle CssClass="grid-odd" />
                    <FooterStyle CssClass="grid-footer" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="#">
                            <HeaderStyle Width="60px" Font-Bold="true" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HiddenField ID="hiRoleID" runat="server" />
                                <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" Checked='<%# Eval("IsSelect") %>'></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="Name" HeaderText="Tên chức năng">
                            <HeaderStyle Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Description" HeaderText="Mô tả">
                            <HeaderStyle Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages" CssClass="grid-pager" PageButtonCount="10" />
                </asp:DataGrid>
            </div>
        </div>
    </div>
</div>
<%--<script>
    function checkFileExtension() {
        var fileInput = document.getElementById('fileInput');
        var filePath = fileInput.value;
        var allowedExtensions = /(\.jpg|\.jpeg|\.png)$/i;
        if (!allowedExtensions.exec(filePath)) {
            alert('Chỉ được phép tải lên các tệp tin có đuôi .jpg, .jpeg hoặc .png.');
            fileInput.value = '';
            return false;
        }
        return true;
    }
</script>--%>
<%--<script type="text/javascript">
    function uploadFile() {
        var fileInput = document.getElementById('fileInput');
        var file = fileInput.files[0];
        if (file) {
            var reader = new FileReader();
            reader.onload = function (e) {
                // Call server-side function to upload file
                PageMethods.UploadImage(file.name, e.target.result, onUploadSuccess, onUploadFailure);
            };
            reader.readAsDataURL(file);
        }
    }

    function onUploadSuccess(result) {
        // Display the uploaded image
        document.getElementById('UserImage').src = result;
    }

    function onUploadFailure(error) {
        alert(error.get_message());
    }
</script>--%>
