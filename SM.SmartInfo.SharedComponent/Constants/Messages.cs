namespace SM.SmartInfo.SharedComponent.Constants
{
    public class Messages
    {
        public const string LinkNotExisted = "Trang không tồn tại";
        public const string ItemNotExisted = "Dữ liệu bạn đang cập nhật không tồn tại trong hệ thống.";
        public const string ChangedVersionInfo = "Dữ liệu đã bị thay đổi từ phiên bản {0} sang phiên bản {1} bởi [{2}] tại thời điểm [{3}] (ID = {4}). Vui lòng tải lại trang và thực hiện lại";
        public const string ItemNotExitOrChanged = "Dữ liệu đã bị thay đổi bởi người dùng khác hoặc tự động bởi hệ thống. Vui lòng tải lại trang và thực hiện lại";
        public const string CodeItemExisted = "Mã nhập vào đã có trong hệ thống.";
        public const string CodeItemEmptyCommodityType1 = "Mã loại hàng hóa không được để trống.";
        public const string NameItemEmptyCommodityType1 = "Tên loại hàng hóa không được để trống";
        public const string StatusItemEmpty = "Trạng thái không được để trống";
        public const string ErrorLoading = "Đã xảy ra lỗi trong quá trình tải trang. Vui lòng tải lại trang";
        public const string ProductedExpiredDateError = "Ngày sản xuất phải trước Hạn sử dụng";
        public const string NotOtherProduct = "Đây không phải hàng hóa khác";
        public const string FieldNotEmpty = "[{0}] Không được để trống";
        public const string FieldDateNotLessThanNow = "[{0}] Không được bé hơn ngày hôm nay";
        public const string WrongFormat = "[{0}] Không đúng định dạng";
        public const string ErrorNotExistField = "Không có dữ liệu của [{0}] tương ứng với yêu cầu";
        public const string ErrorInChooseService = "Xảy ra lỗi trong đề xuất dịch vụ yêu cầu thẩm định";
        public const string FileNotChoose = "Chưa chọn file tải lên";
        public const string FileSizeOverLoad = "File tải lên phải nhỏ hơn [{0}MB]";
        public const string CancelSuccessfull = "Hủy yêu cầu thành công";
        public const string WorkfieldStartAndEndDate = "Thời điểm kết thúc khảo sát phải sau thời điểm bắt đầu khảo sát";
        public const string WarehouseTypeNotFound = "Không tìm thấy loại tài sản (kho/bãi) tương ứng";
        public const string DocumentNotEnough = "Chưa đủ tài liệu bắt buộc";
        public const string ImageNotEnough = "Chưa đủ ảnh";
        public const string InfoNotFound = "Không tìm thấy thông tin yêu cầu";
        public const string WarehouseMapTypeError = "[Loại] thêm vào phải thấp hơn 1 cấp so với [Cấp trước] đang chọn";
        public const string CannotDeleteIsParent = "Đang có cấp con. Không được phép xóa";
        public const string CannotDeleteIsUse = "Đang được sử dụng trong bảng thông tin hàng hóa. Không được phép xóa";
        public const string CannotDone = "Không thể [Hoàn thành] khi còn hàng hóa [Không đạt]";
        public const string GoodInWarehouseNotEnough = "Số lượng hàng trong kho không đủ";
        public const string SaveSuccessfully = "Lưu thành công";
        public const string RequestedSuccessful = "Trình duyệt thành công";
        public const string ApprovedSuccessful = "Phê duyệt thành công";
        public const string RejectedSuccessful = "Từ chối duyệt thành công";
        public const string DoNotSupport = "Không hỗ trợ";
        public const string MeetingPlanDTGHaveToInsideWeek = "[Thời gian giao dịch] với [{0}] phải từ ngày {1} đến ngày {2}";

        public class AcceptFiles
        {
            public const string CommonDocument = "Chỉ tải được file (Ảnh, Videos, Word, Excel, Slide và Văn bản).";
            public const string OnlyDocument = "Chỉ tải được file (Word, Excel, Slide và Văn bản).";
            public const string OnlyImage = "Chỉ tải được file ảnh.";
            public const string OnlyWordOrHTML = "Chỉ cho phép upload file word or html";
            public const string OnlyExcel = "Chỉ tải được file Excel.";
        }

        public class Users
        {
            public const string UserIsUing = "Không được xóa người dùng đang sử dụng.";
            public const string UserNotSelected = "Bạn chưa chọn chuyên viên";
        }
        public class Organization
        {
            public const string RequiredOrgCode = "Bạn chưa nhập [Mã đơn vị]";
            public const string RequiredOrgName = "Bạn chưa nhập [Tên đơn vị]";
            public const string DuplicateCode = "Mã phòng ban đã tồn tại trong hệ thống. Vui lòng nhập mã khác.";
            public const string CannotDeleteIsParent = "Tổ chức đang có đơn vị con trực thuộc. Không được phép xóa.";
            public const string DuplicateEmpInGrid = "Danh sách chuyên viên không được phép trùng chuyên viên.";
        }


        public class WorkFlow
        {
            public const string WFNotExistedOrEnd = "Luồng phê duyệt đã kết thúc hoặc không tồn tại.";
        }

        public class Emailtemplate
        {
            public const string NoItemEmailtemplate = "Không có Mẫu email/sms nào để thực thi.";
            public const string NoTransformType = "Bạn cần phải chọn [Cách thức sinh].";
            public const string NoTemplateType = "Bạn cần phải chọn [Loại mẫu].";
            public const string NoCode = "Bạn chưa nhập [Mã mẫu].";
            public const string NoName = "Bạn chưa nhập [Tên mẫu].";
            public const string IsChanged = "Mẫu Email/SMS đã bị thay đổi. Hãy refresh và thử lại.";
        }
        public class Target
        {
            public const string NoItemTarget = "Không có Chỉ tiêu nào để thực thi.";
            public const string NoTransformType = "Bạn cần phải chọn [Cách thức sinh].";
            public const string NoTemplateType = "Bạn cần phải chọn [Loại mẫu].";
            public const string NoCode = "Bạn chưa nhập [Mã chỉ tiêu].";
            public const string NoName = "Bạn chưa nhập [Tên chỉ tiêu].";
            public const string IsChanged = "Chỉ tiêu đã bị thay đổi. Hãy refresh và thử lại.";
            public const string DuplicateTargetInGrid = "Danh sách chỉ tiêu không được phép trùng chỉ tiêu.";
        }
        public class Category
        {
            public const string NoItemCategory = "Không có Chỉ tiêu nào để thực thi.";
            public const string NoName = "Bạn chưa nhập [Tên phân loại].";
            public const string IsChanged = "Chỉ tiêu đã bị thay đổi. Hãy refresh và thử lại.";
            public const string DuplicateCategoryInGrid = "Danh sách phân loại không được phép trùng phân loại.";
        }
        public class Plan
        {
            public const string NoItemPlan = "Không có Kế hoạch nào để thực thi.";
            public const string NoTransformType = "Bạn cần phải chọn [Cách thức sinh].";
            public const string NoTemplateType = "Bạn cần phải chọn [Loại mẫu].";
            public const string NoCode = "Bạn chưa nhập [Mã kế hoạch].";
            public const string NoName = "Bạn chưa nhập [Tên tên kế hoạch].";
            public const string IsChanged = "Kế hoạch đã bị thay đổi. Hãy refresh và thử lại.";
            public const string DuplicateCode = "Mã kế hoạch đã tồn tại trong hệ thống. Vui lòng nhập mã khác.";
        }
    }
}
