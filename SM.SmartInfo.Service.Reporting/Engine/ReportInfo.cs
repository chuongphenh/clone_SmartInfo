using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.EntityInfos;

namespace SM.SmartInfo.Service.Reporting.Engine
{
    [XmlRoot("report")]
    public class ReportInfo : ExcelRenderInfo
    {
        [XmlElement("report_name")]
        public string ReportName { get; set; }

        /// <summary>
        /// Tên file khi lưu
        /// </summary>
        [XmlElement("save_as_file_name")]
        public string SaveAsFileName { get; set; }

        /// <summary>
        /// Danh sách template ứng với mỗi khổ giấy
        /// </summary>
        [XmlArray("templates")]
        [XmlArrayItem("template")]
        public List<Template> Templates { get; set; }

        /// <summary>
        /// Tên method trong C# Script dùng để xác định PaperSize, từ đó xác định template.
        /// Nếu kết quả trả về Null thì sẽ dùng template mặc định.
        /// </summary>
        [XmlElement("custom_paper_size_method")]
        public string CustomPaperSizeMethod { get; set; }

        /// <summary>
        /// Tên method trong C# Script dùng để xác định trực tiếp tên Template.
        /// Nếu kết quả trả về Null thì sẽ dùng template mặc định.
        /// </summary>
        [XmlElement("custom_template_method")]
        public string CustomTemplateMethod { get; set; }

        /// <summary>
        /// Danh sách các options của form param AllParams được hỗ trợ. Là enum EAllParamsOption cách nhau dấu ','
        /// Không khai báo thì tương ứng với dùng toàn bộ options
        /// VD: Báo cáo này chỉ hỗ trợ các params: TuNgay, DenNgay.
        ///     Khi đó form AllParams chỉ cho chọn TuNgay, DenNgay.
        ///     Các options khác bị disable
        /// </summary>
        [XmlElement("all_params_option")]
        public string AllParams_Options { get; set; }

        /// <summary>
        /// Danh sách các tiêu chí báo cáo của form param AllParams được hỗ trợ. Là enum TieuChiBaoCao cách nhau dấu ','
        /// Không khai báo thì tương ứng với dùng toàn tiêu chí
        /// VD: Báo cáo này chỉ hỗ trợ các params: BenhNhan_Tuoi, BenhNhan_GioiTinh
        ///     Khi đó form AllParams chỉ cho chọn TuNgay, DenNgay.
        ///     Các tiêu chí khác bị ẩn đi không hiển thị
        /// </summary>
        [XmlElement("all_params_tieu_chi_bao_cao")]
        public string AllParams_TieuChiBaoCaos { get; set; }

        /// <summary>
        /// Cấm dùng export ra excel khi đang preview hay không.
        /// "true": Cấm. "false": Cho phép
        /// Mặc định nếu không định nghĩa: "false" = cho phép xuất excel
        /// </summary>
        [XmlElement("lock_export")]
        public bool LockExport { get; set; }

        /// <summary>
        /// Danh sách template ứng với mỗi khổ giấy
        /// </summary>
        [XmlArray("controls")]
        [XmlArrayItem("control")]
        public List<DynamicReportControl> Controls { get; set; }

        #region Support classes
        public class Template
        {
            [XmlAttribute("file_name")]
            public string FileName { get; set; }

            [XmlAttribute("paper_size")]
            public PaperSize PaperSize { get; set; }

            /// <summary>
            /// Template được chọn mặc định nếu không set Manually
            /// </summary>
            [XmlAttribute("is_default")]
            public bool IsDefault { get; set; }
        }

        public class DynamicReportControl
        {
            [XmlAttribute("param")]
            public string Param { get; set; }

            [XmlAttribute("caption")]
            public string Caption { get; set; }

            [XmlElement("textbox")]
            public ReportTextBox TextBox { get; set; }

            [XmlElement("numerictextbox")]
            public ReportNumericTextBox NumericTextBox { get; set; }

            [XmlElement("dropdownlist")]
            public ReportDropDownList DropDownList { get; set; }

            [XmlElement("datepicker")]
            public ReportDatePicker DatePicker { get; set; }

            [XmlElement("searchbox")]
            public ReportSearchBox SearchBox { get; set; }
        }

        public class ReportTextBox
        {
            [XmlAttribute("width")]
            public string Width { get; set; }
        }

        public class ReportNumericTextBox
        {
            [XmlAttribute("width")]
            public string Width { get; set; }

            [XmlAttribute("allowthousanddigit")]
            [DefaultValue("true")]
            public string AllowThousandDigit { get; set; }

            [XmlAttribute("numberdecimaldigit")]
            [DefaultValue("0")]
            public string NumberDecimalDigit { get; set; }
        }

        public class ReportDropDownList
        {
            [XmlAttribute("width")]
            public string Width { get; set; }

            [XmlAttribute("datatextfield")]
            [DefaultValue("Text")]
            public string DataTextField { get; set; }

            [XmlAttribute("datavaluefield")]
            [DefaultValue("Value")]
            public string DataValueField { get; set; }

            [XmlAttribute("valuetype")]
            public ReportControlDataType ValueType { get; set; }

            [XmlElement("source")]
            public ReportControlSource DataSource { get; set; }
        }

        public class ReportControlSource
        {
            [XmlAttribute("width")]
            public string Width { get; set; }

            [XmlAttribute("type")]
            public ReportControlSourceType SourceType { get; set; }

            [XmlText(Type = typeof(string))]
            public string Source { get; set; }
        }

        public class ReportDatePicker
        {
            [XmlAttribute("width")]
            public string Width { get; set; }

            [XmlAttribute("dateformat")]
            public string DateFormat { get; set; }
        }

        public class ReportSearchBox
        {
            [XmlAttribute("width")]
            public string Width { get; set; }

            [XmlAttribute("datatextfield")]
            [DefaultValue("Text")]
            public string DataTextField { get; set; }

            [XmlAttribute("datavaluefield")]
            [DefaultValue("Value")]
            public string DataValueField { get; set; }

            [XmlAttribute("valuetype")]
            public ReportControlDataType ValueType { get; set; }

            [XmlElement("source")]
            public ReportControlSource DataSource { get; set; }

            [XmlArray("columns")]
            [XmlArrayItem("column")]
            public List<ReportSearchBoxColumn> Columns { get; set; }
        }

        public class ReportSearchBoxColumn
        {
            public string Width { get; set; }
            public string HeaderText { get; set; }
            public string DataField { get; set; }
        }
        #endregion

        #region Enums
        /// <summary>
        /// Khổ giấy
        /// </summary>
        public enum PaperSize
        {
            A3 = System.Drawing.Printing.PaperKind.A3,
            A4 = System.Drawing.Printing.PaperKind.A4,
            A5 = System.Drawing.Printing.PaperKind.A5,
            /// <summary>
            /// Máy in nhiệt khổ to
            /// </summary>
            A6 = System.Drawing.Printing.PaperKind.A6,
            /// <summary>
            /// Khổ giấy của máy in nhiệt, không có size cố định.
            /// Vd: Máy lấy số, phiếu in siêu thị
            /// </summary>
            A7 = System.Drawing.Printing.PaperKind.Custom,
        }

        public enum ReportControlSourceType
        {
            [XmlEnum("None")]
            None,

            [XmlEnum("List")]
            List,

            [XmlEnum("Sql")]
            Sql
        }

        public enum ReportControlDatePickerDisplayFormat
        {
            [XmlEnum("YMD")]
            YMD,

            [XmlEnum("MDY")]
            MDY,

            [XmlEnum("DMY")]
            DMY
        }

        public enum ReportControlDataType
        {
            [XmlEnum("String")]
            String,

            [XmlEnum("Integer")]
            Integer,

            [XmlEnum("Decimal")]
            Decimal
        }

        #endregion
    }
}