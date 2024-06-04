<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChartStatisticUC.ascx.cs"
    Inherits="SM.SmartInfo.UI.UserControls.Default.ChartStatisticUC" %>

<%@ Register Assembly="SMCUI" Namespace="SoftMart.Core.UIControls" TagPrefix="tk" %>

<asp:HiddenField ID="hidListData" runat="server" />
<asp:HiddenField ID="hidListBackgroundColor" runat="server" />

<div style="background: rgb(255, 255, 255); margin-top: 20px;" id="chart">
    <div class="row" style="padding-left: 15px; padding-right: 15px; padding-top: 15px; padding-bottom: 15px">
        <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
            <span style="font-size: 16px; font-weight: bold; color: #464457; float: left">Thống kê sự vụ</span>
            <div class="row">
                <div class="col-lg-12 col-md-12 margin-30px-bottom sm-margin-30px-bottom" id="div-chart" style="display: grid; vertical-align: middle;">
                    <canvas id="myChart" width="852" height="405" style="margin: auto; max-width:350px;max-height:165px;"></canvas>
                </div>
            </div>
        </div>
    </div>
</div>

<script>
    new Chart(document.getElementById("myChart"), {
        type: 'doughnut',
        data: {
            labels: ["Sự vụ chưa lên báo", "Sự vụ đã lên báo"],
            datasets: [
                {
                    label: "",
                    backgroundColor: <%= hidListBackgroundColor.Value %>,
                    data: <%= hidListData.Value %>
                }
            ]
        },
        options: {
            title: {
                display: false
            },
            legend: {
                display: true,
                position: "right",
                align: "center",
                fontFamily: "SF Pro Display"
            }
        }
    });
</script>