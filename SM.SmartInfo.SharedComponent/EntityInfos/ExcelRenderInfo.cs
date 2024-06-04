using System.Xml.Serialization;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.EntityInfos
{
    public class ExcelRenderInfo
    {
        /// <summary>
        /// Tên file chứa code class C#.
        /// Các method bên trong để thực thi các logic mà không phải build lại code.
        /// </summary>
        [XmlElement("cs_file_name")]
        public string CSFileName { get; set; }

        /// <summary>
        /// Cấu hình cho việc render
        /// </summary>
        [XmlArray("render_infos")]
        [XmlArrayItem("render_info")]
        public List<RenderInfo> Renders { get; set; }

        #region Support classes
        /// <summary>
        /// Cấu hình render cho 1 vùng độc lập
        /// </summary>
        public class RenderInfo
        {
            /// <summary>
            /// Cách khai báo SourceDynamic
            /// </summary>
            [XmlElement("source_style")]
            public DeclarationStyle? SourceStyle { get; set; }

            /// <summary>
            /// Nguồn dữ liệu để set cho các vùng thêm động.
            /// Cách khai báo phụ thuộc vào SourceDeclarationType.
            /// Nếu SourceDeclarationType = null thì dùng coi như khai báo trực tiếp.
            /// </summary>
            [XmlElement("source_dynamic")]
            public string SourceDynamic { get; set; }

            /// <summary>
            /// Active sheet. Giá trị là tên sheet cần active
            /// </summary>
            [XmlElement("active_sheet")]
            public string ActiveSheet { get; set; }

            /// <summary>
            /// Dùng dữ liệu query được từ Database để set vào các DefinedName.
            /// Mapping dựa trên tên cột và tên DefinedName.
            /// </summary>
            [XmlElement("set_defined_name")]
            public bool SetDefinedName { get; set; }

            /// <summary>
            /// Cấu hình render Barcode và set vào template
            /// </summary>
            [XmlArray("barcodes")]
            [XmlArrayItem("barcode")]
            public List<Barcode> Barcodes { get; set; }

            /// <summary>
            /// Cấu hình render QRcode và set vào template
            /// </summary>
            [XmlArray("qrcodes")]
            [XmlArrayItem("qrcode")]
            public List<QRcode> QRcodes { get; set; }

            /// <summary>
            /// True: Chuẩn hóa tên cột để set được thành Bookmark name của excel
            /// Value: Tên Prefix của tên cột. Chỉ các cột có prefix = Value thì mới được normalize
            /// </summary>
            [XmlArray("normalize_table_column_names")]
            public List<string> NormalizeTableColumnNames { get; set; }

            /// <summary>
            /// DefinedName để tạo cột động
            /// </summary>
            [XmlArray("auto_create_columns")]
            [XmlArrayItem("auto_create_column")]
            public List<AutoCreateColumn> AutoCreateColumns { get; set; }

            /// <summary>
            /// Tương đương sử dụng method SetDataForVirtualTable
            /// </summary>
            [XmlElement("virtual_table")]
            public SetDataForVirtualTable VirtualTable { get; set; }

            /// <summary>
            /// Tương đương sử dụng method SetDataForVirtualTableGroup
            /// </summary>
            [XmlElement("virtual_table_group")]
            public SetDataForVirtualTableGroup VirtualTableGroup { get; set; }

            /// <summary>
            /// Tương đương sử dụng method FillTemplateAndCopyDown
            /// </summary>
            [XmlElement("fill_template")]
            public FillTemplateAndCopyDown FillTemplate { get; set; }

            /// <summary>
            /// Tương đương sử dụng method FillFromDataTable
            /// </summary>
            [XmlElement("fill_datatable")]
            public FillFromDataTable FillDataTable { get; set; }

            /// <summary>
            /// Set dữ liệu từ DataTable sử dụng cột Name/Value thay vì ColumnName/RowValue
            /// </summary>
            [XmlElement("fill_name_value")]
            public FillNameValueDataTable FillNameValue { get; set; }

            /// <summary>
            /// Xoay dữ liệu trong DataTable. 
            /// Tạo cột động và fill dữ liệu theo cột vừa tạo
            /// </summary>
            [XmlElement("fill_dynamic_column")]
            public FillDynamicColumn FillDynamicColumn { get; set; }

            /// <summary>
            /// Chuyển số sang dạng chữ với các DefinedName
            /// </summary>
            [XmlArray("number_to_texts")]
            public List<string> NumberToTexts { get; set; }

            /// <summary>
            /// Gọi Merge
            /// </summary>
            [XmlArray("merge_ranges")]
            public List<string> MergeRanges { get; set; }

            /// <summary>
            /// Merge cột theo nhóm x cột với nhau thành 1
            /// </summary>
            [XmlArray("merge_column_groups")]
            [XmlArrayItem("merge_column_group")]
            public List<MergeColumnGroup> MergeColumnGroups { get; set; }

            [XmlArray("merge_row_by_column_values")]
            [XmlArrayItem("merge_row_by_column_value")]
            public List<MergeRowByColumnValue> MergeRowByColumnValues { get; set; }

            /// <summary>
            /// Danh sách các dòng tĩnh sẽ gọi AutoFit. Là tên của 1 DefinedName xác định dòng đó
            /// (Các dòng thêm động đã tự động được gọi rồi)
            /// </summary>
            [XmlArray("auto_fit_rows")]
            public List<string> AutoFitRows { get; set; }

            /// <summary>
            /// Cấu hình các row bị xóa nếu có dấu hiệu đặc biệt
            /// </summary>
            [XmlArray("delete_rows")]
            [XmlArrayItem("delete_row")]
            public List<DeleteRow> DeleteRows { get; set; }
        }

        public class SetDataForVirtualTable
        {
            /// <summary>
            /// Bắt đầu đổ dữ liệu từ đây.
            /// Tên DefinedName
            /// </summary>
            [XmlElement("template_row_name")]
            public string TemplateRowName { get; set; }

            /// <summary>
            /// Có ẩn đi nếu không có dữ liệu? 
            /// </summary>
            [XmlElement("hide_when_empty")]
            public bool HideWhenEmpty { get; set; }
        }

        public class SetDataForVirtualTableGroup
        {
            /// <summary>
            /// Defined name, Tên Prefix mặc định của Group.
            /// VD: AUTO_GROUP_
            /// </summary>
            [XmlElement("group_defined_name")]
            public string GroupDefinedName { get; set; }
            /// <summary>
            /// Có ẩn đi nếu không có dữ liệu? 
            /// </summary>
            [XmlElement("hide_when_empty")]
            public bool HideWhenEmpty { get; set; }
            /// <summary>
            /// Danh sách cột trong DataTable Sql trả ra để nhóm dữ liệu
            /// </summary>
            [XmlArray("group_columns")]
            public List<string> GroupColumns { get; set; }
        }

        public class FillTemplateAndCopyDown
        {
            /// <summary>
            /// Tên vùng teamplate. Có thể chứa nhiều row (Khác với SetDataForVirtualTable chỉ set cho từng row)
            /// </summary>
            [XmlElement("region_name")]
            public string RegionName { get; set; }
        }

        public class FillFromDataTable
        {
            [XmlElement("from_range")]
            public string FromRange { get; set; }
        }

        public class FillNameValueDataTable
        {
            [XmlElement("name_column")]
            public string NameColumn { get; set; }

            [XmlElement("value_column")]
            public string ValueColumn { get; set; }
        }

        public class AutoCreateColumn
        {
            /// <summary>
            /// DefinedName của 1 vùng trên Excel dùng để làm template tạo cột động.
            /// Là các cột có tên bắt đầu bắt đầu bằng giá trị của DefinedName này
            /// Required.
            /// </summary>
            [XmlAttribute("prefix_name")]
            public string PrefixName { get; set; }

            /// <summary>
            /// Nếu defined, set giá trị tên cột vào. Null: Bỏ qua
            /// Optional.
            /// </summary>
            [XmlAttribute("header_name")]
            public string HeaderName { get; set; }
        }

        /// <summary>
        /// Merge theo nhóm, mỗi nhóm Count cột
        /// </summary>
        public class MergeColumnGroup
        {
            /// <summary>
            /// Vị trí bắt đầu merge
            /// </summary>
            [XmlAttribute("start_range")]
            public string StartRange { get; set; }

            /// <summary>
            /// Vị trí chứa tổng số cột cần xử lý
            /// </summary>
            [XmlAttribute("total_column_count_range")]
            public string TotalColumnCountRange { get; set; }

            /// <summary>
            /// Số cột được merge thành 1 nhóm
            /// </summary>
            [XmlAttribute("column_count")]
            public int ColumnCount { get; set; }
        }

        /// <summary>
        /// Merge các row liền nhau nếu chúng có cùng giá trị trên 1 cột nào đó.
        /// </summary>
        public class MergeRowByColumnValue
        {
            /// <summary>
            /// Vùng tính toán để merge
            /// </summary>
            [XmlAttribute("merge_range")]
            public string MergeRange { get; set; }

            /// <summary>
            /// Cột dữ liệu để so sánh
            /// </summary>
            [XmlAttribute("value_column")]
            public string ValueColumn { get; set; }
        }

        public class DeleteRow
        {
            /// <summary>
            /// Tên để xác định cột chứa giá trị dùng xác định dòng bị xóa
            /// VD: "G10:G30" hoặc "DeleteValueRange" hoặc "AB:AB"
            /// </summary>
            [XmlAttribute("column_range_name")]
            public string ColumnRangName { get; set; }

            /// <summary>
            /// Giá trị xác định dòng bị xóa, Nếu cell được xác định bởi dòng và cột ColumnDefinedName chứa giá trị này
            /// </summary>
            [XmlAttribute("cell_value")]
            public string CellValue { get; set; }
        }

        /// <summary>
        /// Cấu hình để render Barcode và thay thế 1 hình trên template
        /// </summary>
        public class Barcode
        {
            /// <summary>
            /// Tên cột chứa dữ liệu của SourceStatic để gen Barcode
            /// </summary>
            [XmlAttribute("column_name")]
            public string ColumnName { get; set; }

            /// <summary>
            /// Tên ảnh/textbox/... trên Template để thay Barcode vào
            /// </summary>
            [XmlAttribute("shape_name")]
            public string ShapeName { get; set; }

            /// <summary>
            /// Loại Barcode. Mặc định dùng Code39
            /// </summary>
            [XmlAttribute("barcode_type")]
            public string BarcodeType { get; set; }
        }

        /// <summary>
        /// Cấu hình để render QRcode và thay thế 1 hình trên template
        /// </summary>
        public class QRcode
        {
            /// <summary>
            /// Tên cột chứa dữ liệu của SourceStatic để gen QRcode
            /// </summary>
            [XmlAttribute("column_name")]
            public string ColumnName { get; set; }

            /// <summary>
            /// Tên ảnh/textbox/... trên Template để thay QRcode vào
            /// </summary>
            [XmlAttribute("shape_name")]
            public string ShapeName { get; set; }
        }

        /// <summary>
        /// Xoay dữ liệu trong DataTable. 
        /// Tạo cột động và fill dữ liệu theo cột vừa tạo
        /// </summary>
        public class FillDynamicColumn
        {
            /// <summary>
            /// Tên cell chứa title. Giá trị title là tiếng việt thoải mái. VD: Tên loại thẻ
            /// </summary>
            [XmlElement("template_column")]
            public string TemplateColumn { get; set; }
        }

        /// <summary>
        /// Cách dữ liệu được khai báo, trực tiếp hay qua 1 phương pháp khác
        /// </summary>
        public enum DeclarationStyle
        {
            /// <summary>
            /// Dữ liệu được khai báo trực tiếp.
            /// </summary>
            Direct,
            /// <summary>
            /// Dữ liệu lấy được bằng cách chạy 1 method Csharp.
            /// Khai báo tên class + method. ex: Report.GenerateStaticSqlQuery
            /// Không dùng trực tiếp lệnh sql vì nhiều lý do như giảm tải cho Sql, build lệnh sql động...
            /// </summary>
            CSharp,
        }

        #endregion
    }
}