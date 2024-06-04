<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPages/Common/Popup.Master" CodeBehind="Display.aspx.cs"
    Inherits="SM.SmartInfo.UI.PopupPages.ViewImages.Display" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>
<%@ Register Src="~/UI/UserControls/Common/ErrorMessageUC.ascx" TagName="ErrorMessage" TagPrefix="uc" %>

   
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <Style>
        #image-container {
            position: relative;
            display: inline-block;
        }

        #crop-area {
            position: absolute;
            border: 2px dashed red;
            visibility:hidden;
        }
        .hidButton{
            visibility:hidden;
        }
    </Style>
    <div class="body-content" style="background: #F2F3F8">
        <div class="err">
            <uc:ErrorMessage ID="ucErr" runat="server" />
        </div>
        <%--DESIGN--%>
        <asp:HiddenField ID="hidAttID" runat="server" />
        <asp:HiddenField ID="hidCroppedImage" runat="server" />
        <asp:HiddenField ID="hidNewsID" runat="server" />
        <%--<div class="row">
            <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px; padding: 10px">
                <div id="image-container">
                    <img id="img" runat="server" src="" style="width: 100%; border-radius: 8px; z-index:2" clientidmode="Static"/>
                    <div id="crop-area" style="z-index:3"></div>
                </div>
                <canvas id="hidden-canvas"></canvas>
            </div>
        </div>--%>
        <div class="row" style="padding-top: 10px; visibility:hidden" id="docCreatedDTG">
            <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px; padding: 10px;">
                <span style="font-size: 16px"><b>Ngày đăng:</b></span> <asp:Label ID="lblCreatedDTG" runat="server"></asp:Label>
            </div>
        </div>
         <div class="row" style="padding-top: 10px; visibility:hidden" id="docDescription">
            <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px; padding: 10px;">
                <span style="font-size: 16px"><b>Mô tả tài liệu:</b></span> <asp:Label ID="lblDescription" runat="server"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12" style="background: rgb(255, 255, 255); border-radius: 5px; padding: 10px">
                <iframe id="pdfViewer" src="" runat="server" style="width: 100%; height:830px; border-radius: 8px; z-index:2" clientidmode="Static"></iframe>
                <img id="img" runat="server" src="" style="width: 100%; border-radius: 8px; z-index:2" clientidmode="Static"/>
            </div>
        </div>
       
        <%--<button id="btnCrop">Crop</button>
        <button id="btnSaveCrop" style="visibility:hidden">Save</button>
        <asp:Button ID="btnTrigger" OnClick="btnTrigger_Click" ClientIDMode="Static" runat="server" CssClass="hidButton"/>--%>
        <%--END DESIGN--%>
    </div>
    <script type="text/javascript">

        var des = document.getElementById('<%= lblDescription.ClientID%>');
        var createdDTG = document.getElementById('<%= lblCreatedDTG.ClientID%>');
        if (des.textContent != null && des.textContent != '') {
            document.getElementById('docDescription').style.visibility = 'visible';
        }
        if (createdDTG.textContent != null && createdDTG.textContent != '') {
            document.getElementById('docCreatedDTG').style.visibility = 'visible';
        }
    </script>
    <%--<script type="text/javascript">
        const image = document.getElementById('img');
        const cropArea = document.getElementById('crop-area');
        const imageContainer = document.getElementById('image-container');
        const hiddenCanvas = document.getElementById('hidden-canvas');
        const btnSave = document.getElementById('btnSaveCrop');
        let isDragging = false;
        let offsetX, offsetY;

        document.getElementById('btnCrop').addEventListener('click', function (e) {
            e.preventDefault();
            // Set initial position and dimensions of the crop area
            btnSave.style.visibility = 'visible';
            cropArea.style.visibility = 'visible';
            cropArea.style.left = '50px'; // Initial X position
            cropArea.style.top = '50px';  // Initial Y position
            cropArea.style.width = '200px'; // Initial width
            cropArea.style.height = '100px'; // Initial height
        });

        function moveCropArea(e) {
            if (isDragging) {
                const newX = e.clientX - offsetX;
                const newY = e.clientY - offsetY;

                // Calculate the new position relative to the image container
                const containerRect = imageContainer.getBoundingClientRect();
                const clampedX = Math.min(Math.max(newX, containerRect.left), containerRect.right - cropArea.offsetWidth);
                const clampedY = Math.min(Math.max(newY, containerRect.top), containerRect.bottom - cropArea.offsetHeight);

                cropArea.style.left = clampedX - containerRect.left + 'px';
                cropArea.style.top = clampedY - containerRect.top + 'px';
            }
        }

        // Event listener for mouse down
        cropArea.addEventListener('mousedown', (e) => {
            isDragging = true;
            offsetX = e.clientX - cropArea.getBoundingClientRect().left;
            offsetY = e.clientY - cropArea.getBoundingClientRect().top;

            // Prevent text selection during drag
            e.preventDefault();
        });

        // Event listener for mouse up
        document.addEventListener('mouseup', () => {
            isDragging = false;
        });

        // Event listener for moving the crop area
        document.addEventListener('mousemove', moveCropArea);

        btnSave.addEventListener('click', function (e) {
            e.preventDefault();
            const cropX = parseInt(cropArea.style.left);
            const cropY = parseInt(cropArea.style.top);
            const cropWidth = parseInt(cropArea.style.width);
            const cropHeight = parseInt(cropArea.style.height);

            // Set the canvas size to match the crop area
            hiddenCanvas.width = cropWidth;
            hiddenCanvas.height = cropHeight;

            const ctx = hiddenCanvas.getContext('2d');

            // Draw the cropped portion of the image onto the canvas
            ctx.drawImage(image, cropX, cropY, cropWidth, cropHeight, 0, 0, cropWidth, cropHeight);

            var base64Url = hiddenCanvas.toDataURL('image/jpeg');

            var hiddenField = document.getElementById('<%= hidCroppedImage.ClientID%>');

            hiddenField.value = base64Url;

            document.getElementById('<%= btnTrigger.ClientID%>').click();
        });

        function reloadEditPage() {
            window.close();
            if (window.opener && !window.opener.closed) {
                window.opener.location.reload(true);
            }
        }--%>
        
    </script>
</asp:Content>