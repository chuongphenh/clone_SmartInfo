using System;
using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.EntityInfos;

namespace SM.SmartInfo.SharedComponent.Constants
{
    public static class SMX
    {
        public const int smx_IsDeleted = 1;
        public const int smx_IsNotDeleted = 0;
        public const int smx_Manually = 1;
        public const string smx_Key = "Key";
        public const string smx_Value = "Value";
        public const int smx_PageSize = 10;
        public const int smx_PageSmallSize = 3;
        public const int smx_PageMiniSize = 5;
        public const int smx_PageMiniNine = 9;
        public const int smx_PageMiniEight = 8;
        public const int smx_PageMiniTen = 10;
        public const int smx_PageMiniSixteen = 16;
        public const int smx_PageMiniFour = 4;
        public const int smx_PageSizeMobile = 5;
        public const int smx_PageExtraSize = 50;
        public const int smx_FirstVersion = 1;
        public const string smx_Repository = "Repository";
        public const string smx_RepositoryTemporary = "Temporary";
        public const int smx_NumberOfRequest = 1;
        public const string DefaultImage = "../../../Images/defaultimage.png";
        public const int smx_GenderB = 0;
        public const int smx_GenderG = 1;
        public const string smx_StrSave = "Save";
        public const string smx_SecretCode = "QUJDMTIzIUAjWFla";
        public const string smx_ZeroSelected = "0";
        public const int IsEmployee = 1; // using for function table
        public const int IsRole = 0; // using for function table
        public static readonly Dictionary<int?, string> dicGender = new Dictionary<int?, string>{
            {smx_GenderB,   "Nam"},
            {smx_GenderG,   "Nữ"},
        };

        public static readonly Dictionary<int?, string> dctRoleOrEmployee = new Dictionary<int?, string>()
        {
             {IsRole,       "Nhóm người dùng"},
             {IsEmployee,   "Người dùng"}
        };

        public const int LoginAuthorization_Local = 1;
        public const int LoginAuthorization_AD = 2;
        public static readonly Dictionary<int?, string> dicAuthenticationType = new Dictionary<int?, string>()
        {
            {LoginAuthorization_Local,  "Xác thực nội bộ" },
            {LoginAuthorization_AD,     "Xác thực qua AD" }
        };

        public class Mobile
        {
            public const string ISSUER = "SM_Softmart";
            public const string Timeout = "MobileTimeout";
        }

        public class Session
        {
            public const string LastError = "LastError";
        }

        public class URLParam
        {
            public const string ID = "ID";
            public const string RefID = "RefID";
            public const string OtherRefID = "OtherRefID";
            public const string RefType = "RefType";
            public const string ActivityID = "ActID";
            public const string ActionCodeID = "ActionCodeID";
            public const string Code = "Code";
            public const string Type = "Type";
            public const string RefCode = "RefCode";
        }

        public const string ActionDelete = "Delete";
        public const string ActionNew = "New";
        public const string ActionAdd = "ActionAdd";
        public const string ActionEdit = "Edit";
        public const string ActionDownload = "Download";
        public const string ActionUpload = "Upload";
        public const string ActionApproval = "ActionApproval";
        public const string ActionCancel = "ActionCancel";
        public const string ActionDisplay = "Display";
        public const string ActionSendMessage = "SendMessage";
        public const string ActionExportReport = "ExportReport";
        public const string ActionCheck = "Check";
        public const string ActionUnCheck = "UnCheck";
        public const string ActionSave = "ActionSave";

        public class Parameter
        {
            public const string ID = "ID";
            public const string RefID = "RefID";
            public const string RefType = "RefType";
            public const string Code = "Code";
            public const string TableType = "TableType";
            public const string Name = "Name";
            public const string Property = "Property";
            public const string Callback = "Callback";
            public const string Partner = "Partner";
            public const string EmployeeID = "EmployeeID";
            public const string PCDID = "PCDID";
            public const string WarehouseID = "WarehouseID";
            public const string WarehouseName = "WarehouseName";
            public const string NodeID = "NodeID";
            public const string StartDTG = "StartDTG";
            public const string EndDTG = "EndDTG";
        }

        public class AcceptFiles
        {
            public static List<string> lstCommonDocument = new List<string>()
            {
                ".doc", ".docx", ".xls", ".xlsx", ".pdf",
                ".png", ".jpg", ".gif", ".jpeg", ".bmp",
                ".DOC", ".DOCX", ".XLS", ".XLSX", ".PDF",
                ".PNG", ".JPG", ".GIF", ".JPEG", ".BMP",
            };

            public static List<string> lstOnlyDocument = new List<string>()
            {
                ".doc", ".docx", ".xls", ".xlsx", ".pdf",

                ".DOC", ".DOCX", ".XLS", ".XLSX", ".PDF",
            };

            public static List<string> lstOnlyImage = new List<string>()
            {
                ".png", ".jpg", ".gif", ".jpeg", ".bmp",

                ".PNG", ".JPG", ".GIF", ".JPEG", ".BMP",
            };

            public static List<string> lstOnlyWordOrHTML = new List<string>()
            {
                ".doc", ".docx", ".html",

                ".DOC", ".DOCX", ".HTML",
            };

            public static List<string> lstOnlyExcel = new List<string>()
            {
                ".xls", ".xlsx", ".XLS", ".XLSX",
            };
        }

        public class Status
        {
            public class Common
            {
                /// <summary>
                /// 1
                /// </summary>
                public const int Open = SoftMart.Kernel.Constant.KernelSMX.Open;

                /// <summary>
                /// 2
                /// </summary>
                public const int Updating = SoftMart.Kernel.Constant.KernelSMX.Updating;

                /// <summary>
                /// 4
                /// </summary>
                public const int Processing = SoftMart.Kernel.Constant.KernelSMX.Processing;

                /// <summary>
                /// 8
                /// </summary>
                public const int Approving = SoftMart.Kernel.Constant.KernelSMX.Approving;

                /// <summary>
                /// 16
                /// </summary>
                public const int Approved = SoftMart.Kernel.Constant.KernelSMX.Approved;

                /// <summary>
                /// 32
                /// </summary>
                public const int Rejected = SoftMart.Kernel.Constant.KernelSMX.Rejected;

                /// <summary>
                /// 64
                /// </summary>
                public const int Final = SoftMart.Kernel.Constant.KernelSMX.Final;

                /// <summary>
                /// 128
                /// </summary>
                public const int Cancel = SoftMart.Kernel.Constant.KernelSMX.Cancel;
            }

            public const int Active = 1;
            public const int InActive = 2;
            public const int Draft = 4;
            public const int Approving = 8;

            public static readonly Dictionary<int?, string> dctStatus = new Dictionary<int?, string>{
                { Active,    "Đang sử dụng" },
                { InActive,  "Không sử dụng" }
            };

            public static readonly Dictionary<int?, string> dctStatusV2 = new Dictionary<int?, string>{
                { Active,    "Có hiệu lực" },
                { InActive,  "Hết hiệu lực" }
            };

            public static readonly Dictionary<int?, string> dctStatusV3 = new Dictionary<int?, string>{
                { Active,    "Có hiệu lực" },
                { InActive,  "Hết hiệu lực" },
                { Draft,     "Lưu tạm" },
                { Approving, "Đề xuất" }
            };

            public class EmailTemplate
            {
                public const int Updating = Common.Updating;
                public const int Final = Common.Final;
            }

            public class ReportGeneratorCommand
            {
                /// <summary>
                /// 1
                /// </summary>
                public const int Open = Common.Open;

                /// <summary>
                /// 4
                /// </summary>
                public const int Processing = Common.Processing;

                /// <summary>
                /// 32
                /// </summary>
                public const int Failed = Common.Rejected;

                /// <summary>
                /// 64
                /// </summary>
                public const int Completed = Common.Final;

                public static Dictionary<int, string> dtcDes = new Dictionary<int, string>()
                {
                    { Open,         "Chưa xử lý"       },
                    { Processing,   "Đang xử lý"       },
                    { Failed,       "Lỗi"              },
                    { Completed,    "Hoàn thành"       },
                };

                public static Dictionary<int, string> dtcColor = new Dictionary<int, string>()
                {
                    { Open,         "#000000"    },
                    { Processing,   "#FFD656"    },
                    { Failed,       "#F1764D"    },
                    { Completed,    "#0000FF"    },
                };
            }
        }

        public class FixedBusinessPermissionCode
        {
            public const string CV = "CV";
            public const string CG = "CG";
            public const string QL = "QL";
            public const string NV = "NV";
        }

        public class Features
        {
            public const int smx_BlockIP = 0;
            public const int smx_HomePage = 0;
            public const int smx_Country = 1201;
            public const int smx_Zone = 1202;
            public const int smx_Area = 1203; // Khu vuc: 1518
            public const int smx_Region = 1208;
            public const int smx_Province = 1204;
            public const int smx_District = 1205;
            public const int smx_Town = 1206;
            public const int smx_Street = 1207;
            public const int Country = 1201;
            public const int Organizations = 1210;
            public const int Users = 1211;
            public const int Roles = 1212;
            public const int Setting = 1213;
            public const int Sector = 1215;
            public const int EmailTemplate = 1216;
            public const int Statistic = 1217;
            public const int BlockIP = 1218;
            public const int CountryOfManufacturer = 1219; // Nuoc san xuat
            public const int smx_Segment = 1221; // doan duong
            public const int EmployeeLevel = 1258; // Cap bac nhan vien
            public const int ApprovalFlowActionCode = 1306;
            public const int RuleEngine = 1307;
            public const int Calendars = 1308;
            public const int ActivityFlowActionCode = 1309;
            public const int DocCodeEmulationAndRewarded = 1313; // Loại tài liệu
            public const int BasisValuation = 1317;
            public const int CollateralFlowStatus = 1319;// Phân luồng trạng thái
            public const int Notification = 1230; // Cấu hình gửi thông báo
            public const int Anniversary = 1231; // Cấu hình các ngày kỷ niệm
        }
        public class Feature
        {
            //14:Sự vụ 15:Tổ chức 16:Tin tức 18:Thông báo
            public const int NegativeNews = 14;
            public const int Agency_PressAgency = 15;
            public const int News = 16;
            public const int Events = 18;
        }
        public class FixedSPCode
        {
            public class Impersonate
            {
                public const string ImpersonateAccount = "ImpersonateAccount";
                public const string ImpersonatePassword = "ImpersonatePassword";
            }

            public class ZoneCode
            {
                public const string MB = "MB";
                public const string MT = "MT";
                public const string MN = "MN";
                public const string DNB = "DNB";
                public const string TNB = "TNB";
            }

            public class Notification
            {
                public const string ThongBaoSinhNhat = "ThongBaoSinhNhat";
                public const string ThongBaoNgayKyNiem = "ThongBaoNgayKyNiem";
                public const string ThongBaoNgayTruyenThong = "ThongBaoNgayTruyenThong";
                public const string ThongBaoNgayThanhLap = "ThongBaoNgayThanhLap";
                public const string ThongBaoNgayAm = "ThongBaoNgayAm";
            }
        }

        public class AutomationType
        {
            public const int Organization = 1;
            public const int Employee = 2;

            public static Dictionary<int?, string> dctAutomationTypes = new Dictionary<int?, string>()
            {
                {Organization,  "Tự động chuyển phòng"},
                {Employee,      "Tự động chuyển chuyên viên"}
            };
        }

        #region OrganizationType
        public class OrganizationType
        {
            public const int HO_BusinessUnit = 1;
            public const int ManagementUnit = 2;
            public const int ValuationUnit = 4;

            // 1, 2, 4, 8
            public static Dictionary<int?, string> dctOrganizationType = new Dictionary<int?, string>()
            {
                {HO_BusinessUnit,   "Hội sở"}, // 1
                {ManagementUnit,    "Chi nhánh"}, // 2
                {ValuationUnit,     "TTĐG"}, // 4
            };
        }
        #endregion

        #region Rule engine
        public class RuleCategory
        {
            // 0: Common
            public const int DispatchOrganization = 1;
            public const int DispatchEmployee = 2;
            public const int PermissionForData = 3;
            public const int Formula = 4;
            public const int ValidationService = 5;

            public static readonly Dictionary<int?, string> dictRuleCategory = new Dictionary<int?, string>()
            {
                // Common
                {DispatchOrganization,                      "Giao đơn vị xử lý"},
                {DispatchEmployee,                          "Giao chuyên viên xử lý"},
                {PermissionForData,                         "Phân quyền dữ liệu"},
                {Formula,                                   "Công thức tính"}, // ko có view mà chỉ chạy 1 công thức với input param rồi trả ra output
            };
        }
        #endregion

        #region Email

        public class TransformType
        {
            public const int Map = 4;
            public const int TransformByWord = 2;
            public const int TransformByXslt = 1;

            public static Dictionary<int?, string> dctName = new Dictionary<int?, string>()
            {
                {Map,               "Sinh trực tiếp"},
                {TransformByXslt,   "Sinh qua file mẫu dạng html"},
                {TransformByWord,   "Sinh qua file mẫu dạng word"},
            };
        }

        public class TriggerType
        {
            public const int Event = 1;
            public const int Daily = 2;
            public const int Weekly = 4;
            public const int Monthly = 8;

            public static readonly Dictionary<int?, string> dicDes = new Dictionary<int?, string>(){
                   {Event,      "Sự kiện"},
                   {Daily,      "Hàng ngày"},
                   {Weekly,     "Hàng tuần"},
                   {Monthly,    "Hàng tháng"}
            };
        }

        public class WeeklyActionType
        {
            public const int GiamSat = 1;
            public const int KiemKe = 2;
        }

        public class PartOfDay
        {
            public const int AM = 1;
            public const int PM = 2;
            public static Dictionary<int?, string> dicPartName = new Dictionary<int?, string>()
            {
                { AM,       "Sáng"  },
                { PM,       "Chiều" },
            };
        }

        public class DayOfWeekName
        {
            public static Dictionary<int?, string> dicDOWName = new Dictionary<int?, string>()
            {
                { (int)DayOfWeek.Sunday,        "Chủ nhật"  },
                { (int)DayOfWeek.Monday,        "Thứ hai"   },
                { (int)DayOfWeek.Tuesday,       "Thứ ba"    },
                { (int)DayOfWeek.Wednesday,     "Thứ tư"    },
                { (int)DayOfWeek.Thursday,      "Thứ năm"   },
                { (int)DayOfWeek.Friday,        "Thứ sáu"   },
                { (int)DayOfWeek.Saturday,      "Thứ bảy"   },
            };
        }

        public class TemplateType
        {
            public const int Email = 1;
            public const int SMS = 2;

            public static Dictionary<int?, string> dctTemplateTypes = new Dictionary<int?, string>()
            {
                { Email, "Email" },
                { SMS,   "SMS" }
            };
        }
        public class TargetType
        {
            public const int SUM = 1;
            public const int AVG = 2;

            public static Dictionary<int?, string> dctTargetTypes = new Dictionary<int?, string>()
            {
                { SUM, "Tính tổng" },
                { AVG,   "Tính trung bình" }
            };
        }
        
        public class PlanType
        {
            public const int DAY = 1;
            public const int WEEK = 2;
            public const int MONTH = 3;
            public const int QUARTER = 4;
            public const int YEAR = 5;

            public static Dictionary<int?, string> dctReportCycle = new Dictionary<int?, string>()
            {
                { DAY, "Ngày" },
                { WEEK, "Tuần" },
                { MONTH, "Tháng" },
                { QUARTER, "Quý" },
                { YEAR, "Năm" },
            };
        }

        #endregion

        public class ConnectionString
        {
            public const string ApplicationData = "ApplicationDatabase";
        }

        public class BatchProcessingService
        {
            public static Dictionary<int, BatchProcessingInfo> DicConfig = new Dictionary<int, BatchProcessingInfo>()
            {

            };

            public static Dictionary<int, string> DicTemplate = new Dictionary<int, string>()
            {

            };

            public static List<string> ListImportExcelFile = new List<string> { AllExcelFiles, AllExcel2007Files };
            public const string AllExcelFiles = "*.xls";
            public const string AllExcel2007Files = "*.xlsx";
        }
        public class DataTracking
        {
            public class Feature
            {
                public const int LogIn = 1;
                public const int LogOut = 2;
                public const int Valuation = 4;
                public const int StorePrice = 5;
                public const int FramePrice = 6;
                public const int Report = 8;
                public const int Administrator = 9;
                public const int Configuration = 10;
                public const int SupportBusines = 11;
                public static readonly Dictionary<int?, string> dicDescFeatureType = new Dictionary<int?, string>()
                {
                    {LogIn,             "Đăng nhập"},
                    {LogOut,            "Đăng xuất"},
                    {Valuation,         "Định giá"},
                    {StorePrice,        "Kho giá riêng"},
                    {FramePrice,        "Kho giá chung"},
                    {Report,            "Báo cáo"},
                    {Administrator,     "[QTHT] /Cơ cấu tổ chức và Phân quyền"},
                    {Configuration,     "Cấu hình"},
                    {SupportBusines,    "[QTHT] /Hỗ trợ nghiệp vụ"},
                };
            }

            public class ActionType
            {
                public const int View = 1;
                public const int AddNew = 2;
                public const int Display = 3;
                public const int Edit = 4;
                public const int Delete = 5;
                public const int RequestApproval = 6;
                public const int Approved = 7;
                public const int RejectApproval = 8;
                public const int Export = 9;
                public const int ConfigRight = 10;

                public static readonly Dictionary<int?, string> dicDescActionType = new Dictionary<int?, string>()
                {
                    {View,              "Tra cứu"},
                    {AddNew,            "Thêm mới"},
                    {Display,           "Hiển thị"},
                    {Edit,              "Chỉnh sửa"},
                    {Delete,            "Xóa"},
                    {RequestApproval,   "Gửi yêu cầu phê duyệt"},
                    {Approved,          "Phê duyệt"},
                    {RejectApproval,    "Từ chối phê duyệt"},
                    {Export,            "Xuất dữ liệu"},
                    {ConfigRight,       "Phân quyền"}
                };
            }

            public class RefType
            {
                public const int LogIn = 1;
                public const int LogOut = 2;
                public const int Valuation = 4;
                public const int StorePrice = 5;
                public const int FramePrice = 6;
                public const int Report = 8;
                public const int Administrator = 9;
                public const int Configuration = 10;
                public const int ValuationDocument = 11;
                public const int ItSelfValuation = 12;
                public const int ThirdValuation = 13;
                public const int AdminUser = 20;
                public const int AdminOrganizationStructure = 21;
                public const int AdminRightConfigsUser = 22;
                public const int AdminRightConfigsRole = 23;

                public static readonly Dictionary<int?, string> dicDescRefType = new Dictionary<int?, string>()
                {
                    {LogIn,                         "Đăng nhập"},
                    {LogOut,                        "Đăng xuất"},
                    {Valuation,                     "Định giá"},
                    {StorePrice,                    "Kho giá riêng"},
                    {FramePrice,                    "Kho giá chung"},
                    {Report,                        "Báo cáo"},
                    {Administrator,                 "Quản trị hệ thống"},
                    {ValuationDocument,             "Gửi đề nghị định giá"},
                    {ItSelfValuation,               "Tự định giá"},
                    {ValuationDocument,             "Gửi định giá bên thứ 3"},
                    {AdminUser,                     "QTHT (Người dùng)" },
                    {AdminOrganizationStructure,    "QTHH (Cơ cấu tổ chức)" },
                    {AdminRightConfigsUser,         "QTHT (Phân quyền người dùng)" },
                    {AdminRightConfigsRole,         "QTHT (Phân quyền người role)" }
                };
            }
        }

        #region Dynamic Report
        public class DynamicReport
        {
            public const string AdministrationDistrict = "AdministrationDistrict";
            public const string AdministrationSegment = "AdministrationSegment";
            public const string AdministrationProvince = "AdministrationProvince";
            public const string AdministrationStreet = "AdministrationStreet";
            public const string AdministrationTown = "AdministrationTown";
            public const string AdministrationZone = "AdministrationZone";
            public const string AdministrationEmails = "AdministrationEmails";
            public const string AdministrationRoles = "AdministrationRoles";
            public const string AdministrationCommittees = "AdministrationCommittees";
            public const string AdministrationUsersLog = "AdministrationUsersLogView";
            public const string AdministrationCountry = "AdministrationCountry";
            public const string AdministrationNotification = "AdministrationNotification";
            public const string AdministrationAnniversary = "AdministrationAnniversary";
            public const string AdministrationTargets = "AdministrationTargets";
            public const string AdministrationDocuments = "AdministrationDocuments";
            public const string AdministrationCategories = "AdministrationCategories";
            // Lich su truy cap
            public const string REPORT_LOGINHISTORYSUMMARY = "REPORT_LOGINHISTORYSUMMARY";

            public static readonly Dictionary<string, DynamicReportUserInputInfo> dicReports = new Dictionary<string, DynamicReportUserInputInfo>()
            {
                //-------------------Administration-----------------------------
                {AdministrationDistrict                 , new DynamicReportUserInputInfo("Danh sách quận huyện"                                     , "Administration_District.xml")},
                {AdministrationProvince                 , new DynamicReportUserInputInfo("Danh sách tỉnh thành phố"                                 , "Administration_Province.xml")},
                {AdministrationStreet                   , new DynamicReportUserInputInfo("Danh sách đường phố"                                      , "Administration_Street.xml")},
                {AdministrationTown                     , new DynamicReportUserInputInfo("Danh sách xã/phường"                                      , "Administration_Town.xml")},
                {AdministrationZone                     , new DynamicReportUserInputInfo("Danh sách miền"                                           , "Administration_Zone.xml")},
                {AdministrationEmails                   , new DynamicReportUserInputInfo("Danh sách thư điện tử"                                    , "Administration_Emails.xml")},
                {AdministrationRoles                    , new DynamicReportUserInputInfo("Danh sách nhóm người dùng"                                , "Administration_Roles.xml")},
                {AdministrationCommittees               , new DynamicReportUserInputInfo("Danh sách cấp phê duyệt"                                  , "Administration_Committees.xml")},
                {AdministrationUsersLog                 , new DynamicReportUserInputInfo("Danh sách lịch sử truy cập"                               , "Administration_UsersLog.xml")},
                {AdministrationCountry                  , new DynamicReportUserInputInfo("Danh sách nước sản xuất"                                  , "Administration_Country.xml")},
                // Lich su truy cap
                {REPORT_LOGINHISTORYSUMMARY             , new DynamicReportUserInputInfo("Thống kê lịch sử truy cập"                                , "Report_LoginHistorySummary.xml")},
                {AdministrationNotification             , new DynamicReportUserInputInfo("Danh sách cấu hình gửi thông báo"                         , "Administration_Notification.xml")},
                {AdministrationAnniversary             , new DynamicReportUserInputInfo("Danh sách cấu hình các ngày kỷ niệm truyền thống"          , "Administration_Anniversary.xml")},
                //
                {AdministrationTargets             , new DynamicReportUserInputInfo("Danh sách các chỉ tiêu"          , "Administration_Targets.xml")},
                {AdministrationDocuments             , new DynamicReportUserInputInfo("Danh mục văn bản"          , "Administration_Documents.xml")},
                {AdministrationCategories             , new DynamicReportUserInputInfo("Danh mục phân loại"          , "Administration_Categories.xml")},
            };

        }
        #endregion

        #region Import theo lo
        public class ImportingType
        {
            public static List<string> ListImportExcelFile = new List<string> { "*.xls", "*.xlsx" };

            public const int ImportDiaChi = 1;
            public const int ImportUserInformation = 2;
            public const int ImportUserState = 3;


            public const int ImportCaNhanKhenThuong = 4;
            public const int ImportDonViKhenThuong = 5;

            public const int ImportUserContact = 6;

            public static Dictionary<int?, BatchProcessingInfo> dic = new Dictionary<int?, BatchProcessingInfo>()
            {
                {ImportDiaChi,              new BatchProcessingInfo(ImportDiaChi,               "Địa chỉ",                                  "Import_DiaChi.xml")},
                {ImportUserInformation ,    new BatchProcessingInfo(ImportUserInformation,      "Nguời dùng",                               "Import_UserInformation.xml") },
                {ImportUserState,           new BatchProcessingInfo(ImportUserState,            "Đổi trạng thái",                           "Import_UserState.xml") },
                {ImportCaNhanKhenThuong,    new BatchProcessingInfo(ImportCaNhanKhenThuong,     "Import cá nhân được khen thưởng",          "Import_CaNhanKhenThuong.xml") },
                {ImportDonViKhenThuong,     new BatchProcessingInfo(ImportDonViKhenThuong,      "Import đơn vị được khen thưởng",           "Import_DonViKhenThuong.xml") },
            };

            public static Dictionary<int, string> DicTemplate = new Dictionary<int, string>()
            {
                {ImportDiaChi                   ,   "Import_DiaChi.xlsx"},
                {ImportUserInformation          ,   "Import_UserInformation.xlsx"},
                {ImportUserState                ,   "Import_UserState.xlsx"},
                {ImportCaNhanKhenThuong         ,   "Import_CaNhanKhenThuong.xlsx"},
                {ImportDonViKhenThuong          ,   "Import_DonViKhenThuong.xlsx"},
                {ImportUserContact              ,   "Import_UserContact.xlsx" }
            };
        }
        #endregion

        public class ManuallyReport
        {
            /// <summary>
            /// Báo cáo nhóm người dùng
            /// </summary>
            public const int AdministrationRoles = 9;
            public const int BC_ThongKeTinTuc = 100;

            public static readonly Dictionary<int?, ReportInfo> dicReport = new Dictionary<int?, ReportInfo>()
            {
                {
                    AdministrationRoles,
                    new ReportInfo()
                    {
                        ReportTitle = "Báo cáo nhóm người dùng",
                        ReportType = AdministrationRoles,
                        TemplateName = "Excel_BaoCaoNhomNguoiDung.xlsx"
                    }
                },
                {
                    BC_ThongKeTinTuc,
                    new ReportInfo()
                    {
                        ReportTitle = "Báo cáo thống kê tin tức",
                        ReportType = AdministrationRoles,
                        TemplateName = "Excel_BaoCaoThongKeTinTuc.xlsx"
                    }
                },
            };
        }

        #region Committee
        public class CommitteeType
        {
            #region Type
            /// <summary>
            /// Cơ cấu tổ chức
            /// </summary>
            public const int TypeOrganization = 1;

            /// <summary>
            /// Chuyên gia
            /// </summary>
            public const int TypeSpecialist = 2;

            /// <summary>
            /// Nhóm người dùng
            /// </summary>
            public const int TypeRole = 4;

            /// <summary>
            /// Chuyên viên
            /// </summary>
            public const int TypeEmployee = 8;

            public static readonly Dictionary<int?, string> dctTypeName = new Dictionary<int?, string>()
            {
                {TypeOrganization,      "Cơ cấu tổ chức"},
                {TypeSpecialist,        "Chuyên gia, hội đồng"},
                {TypeRole,              "Nhóm người dùng"},
                {TypeEmployee,          "Chuyên viên"}
            };
            #endregion
        }

        public class CommitteeCode
        {
            public const string CKS_Nhom = "CKS-Nhom";
            public const string CKS_Phong = "CKS-Phong";
            public const string CKS_TrungTam = "CKS-TrungTam";
            public const string CKS_Khoi = "CKS-Khoi";

            public static readonly Dictionary<string, List<string>> dctRequestCommitteeLevel = new Dictionary<string, List<string>>()
            {
                {CKS_Nhom, new List<string>(){CKS_Nhom, CKS_Phong, CKS_TrungTam, CKS_Khoi}},
                {CKS_Phong, new List<string>(){CKS_Phong, CKS_TrungTam, CKS_Khoi}},
                {CKS_TrungTam, new List<string>(){CKS_TrungTam, CKS_Khoi}},
                {CKS_Khoi, new List<string>(){CKS_Khoi}},
            };
        }
        #endregion

        public class AttachmentRefType
        {
            public const int NegativeNews = 1;
            public const int PressAgency = 2;
            public const int PressAgencyHR = 3;
            public const int NegativeNewsDetail = 4;
            public const int News = 5;

            public const int AllNegativeNews = 6;
            public const int AllNews = 7;
            public const int PressAgencyOtherImage = 8;
            public const int PressAgencyMeeting = 9;
            public const int PositiveNews = 10;
            public const int PressAgencyHROtherImage = 11;
            public const int EmulationAndReward = 12;

            public const int PressAgencyHRHistory = 13;
            public const int SingleNews = 14;
            public const int CampaignNews = 15;
        }

        public class CommentRefType
        {
            public const int Notification = 0;
            public const int Birthday = 1;
            public const int Anniversary = 2;
            public const int Holiday = 3;
            public const int Establishday = 4;
            public const int Other = 5;
            public const int NegativeNews = 10;
            public const int PressAgency = 20;
            public const int News = 30;
            public const int PressAgencyHR = 40;

            //public const int HRChangingHistory = 6;
            //public const int PressAgencyMeeting = 7;
            //public const int RelationsPressAgency = 8;
            //public const int RelationShipWithMB = 9;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {NegativeNews,          "Sự vụ"},
                {PressAgency,           "Tổ chức"},
                {News,                  "Tin tức"},
                {Notification,          "Thông báo"},
                {PressAgencyHR,         "Nhân sự tổ chức"},
            };
        }

        public class News
        {
            public class Type
            {
                public const int NegativeNews = 1;
                public const int News = 2;
            }

            public class PositiveNews
            {
                public const int TruyenHinh = 1;
                public const int BaoMang = 2;
                public const int BaoGiay = 3;
                public const int MangXaHoi = 4;
                public const int DienDan = 5;
                public static readonly Dictionary<int?, string> dicType = new Dictionary<int?, string>()
                {
                    {TruyenHinh,            "Truyền hình"},
                    {BaoMang,               "Báo mạng"},
                    {BaoGiay,               "Báo giấy"},
                    {MangXaHoi,             "Mạng xã hội"},
                    {DienDan,               "Diễn đàn"},
                };
            }

            public class NegativeNews
            {
                public const int ChuaPhatSinh = 1;
                public const int DaPhatSinh = 2;
                public static readonly Dictionary<int?, string> dicType = new Dictionary<int?, string>()
                {
                    {ChuaPhatSinh,              "Chưa lên báo"},
                    {DaPhatSinh,                "Đã lên báo"},
                };
            }

            public class Status
            {
                public const int MoiTao = 1;
                public const int HoanThanh = 2;
                public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
                {
                    {MoiTao,        "Đang xử lý"},
                    {HoanThanh,     "Hoàn thành"},
                };
            }

            public class Classification
            {
                public const int QuanTrong = 1;
                public const int TrungBinh = 2;
                public const int BinhThuong = 3;

                public static readonly Dictionary<int?, string> dicClassification = new Dictionary<int?, string>()
                {
                    {QuanTrong,                 "Quan trọng"},
                    {TrungBinh,                 "Trung bình"},
                    {BinhThuong,                "Bình thường"}
                };
            }
        }

        public class Attitude
        {
            public const int TichCuc = 1;
            public const int TieuCuc = 2;
            public const int TrungLap = 3;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {TichCuc,           "Tích cực"},
                {TieuCuc,           "Tiêu cực"},
                {TrungLap,          "Trung lập"},
            };
        }
        public class TypeDate
        {
            public const int solarCalendar = 1;
            public const int lunarCalendar = 2;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {solarCalendar,           "Lịch Dương"},
                {lunarCalendar,           "Lịch Âm"},
            };
        }

        public class Notification
        {
            public class CauHinhGuiThongBao
            {
                public const int SinhNhat = 1;
                public const int KyNiem = 2;
                public const int TruyenThong = 3;
                public const int NgayThanhLap = 4;
                public const int NgayGio = 5;

                public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
                {
                    {SinhNhat,          "Sinh nhật"},
                    {KyNiem,            "Kỷ niệm"},
                    {TruyenThong,       "Truyền thống"},
                    {NgayThanhLap,      "Ngày thành lập"},
                    {NgayGio,           "Ngày giỗ"}
                };
            }
        }

        public class TypeSearch
        {
            public const int Notification = 1;
            public const int NegativeNews = 2;
            public const int News = 3;
            public const int PressAgency = 4;
        }

        public class FilterTime
        {
            public const int Week = 1;
            public const int Month = 2;
            public const int All = 3;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {Week,          "Tuần"},
                {Month,         "Tháng"},
                {All,           "Tất cả"},
            };
        }

        public class Rate
        {
            public const int BinhThuong = 1;
            public const int QuanTrong = 2;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {BinhThuong,                "Bình thường"},
                {QuanTrong,                 "Quan trọng"},
            };
        }

        public class PressAgencyType
        {
            public const int All = 0;
            public const int PressAgency = 1;
            public const int StateAgencies = 2;
            public const int ResCofShopOther = 3;
            public const int Other = 4;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {PressAgency,       "Cơ quan báo chí"},
                {StateAgencies,     "Cơ quan nhà nước"},
                {ResCofShopOther,   "Nhà hàng/cafe/shop/hoa"},
                {Other,             "Tổ chức khác"},
            };

            public static readonly Dictionary<int?, string> dicDescFull = new Dictionary<int?, string>()
            {
                {All,               "Tất cả"},
                {PressAgency,       "Cơ quan báo chí"},
                {StateAgencies,     "Cơ quan nhà nước"},
                {ResCofShopOther,   "Nhà hàng/cafe/shop/hoa"},
                {Other,             "Tổ chức khác"},
            };
        }

        public class RelationshipWithMB
        {
            public const int NongAm = 1;
            public const int ThietLap = 2;
            public const int HieuBiet = 3;
            public const int ThanThiet = 4;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {NongAm,        "Nồng ấm"},
                {ThietLap,      "Thiết lập"},
                {HieuBiet,      "Hiểu biết"},
                {ThanThiet,     "Thân thiết"},
            };
        }

        public class TemplateExcel
        {
            public const string ExcelExport_News = "ExcelExport_News.xml";
            public const string ExcelExport_ListNews = "ExcelExport_ListNews.xml";
            public const string ExcelExport_NegativeNews = "ExcelExport_NegativeNews.xml";
            public const string ExcelExport_ListNegativeNews = "ExcelExport_ListNegativeNews.xml";

            public const string ExcelExport_ListPressAgency = "ExcelExport_ListPressAgency.xml";
            public const string ExcelExport_ListEmulationAndReward = "ExcelExport_ListEmulationAndReward.xml";
            public const string ExcelExport_ListEmployeeInfo = "ExcelExport_ListEmployeeInfo.xml";

            public const string ExcelExport_ListPressAgencyHR = "ExcelExport_ListPressAgencyHR.xml";

            public const string ExcelExport_ListAwardingCatalog = "ExcelExport_ListAwardingCatalog.xml";
            public const string ExcelExport_ListAwardingPeriod = "ExcelExport_ListAwardingPeriod.xml";
            public const string ExcelExport_ListAwardingLevel = "ExcelExport_ListAwardingLevel.xml";
            public const string ExcelExport_ListAwardingType = "ExcelExport_ListAwardingType.xml";
            public const string ExcelExport_ListAwardingPeriodResult = "ExcelExport_ListAwardingPeriodResult.xml";
        }

        public class TemplateWord
        {
            public const string WordExport_PhieuTrinhChiTietSuVu = "WordExport_PhieuTrinhChiTietSuVu.xml";
        }

        public class NewsCategory
        {
            public const int ThongCaoBaoChi = 1;
            public const int BaiPR = 2;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {ThongCaoBaoChi,    "Thông cáo báo chí"},
                {BaiPR,             "Bài PR"},
            };
        }

        public class EmulationAndRewardSubjectRewarded
        {
            public const int CaNhan = 1;
            public const int DonViToChuc = 2;
            public const int All = 3;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {CaNhan,            "Cá nhân"},
                {DonViToChuc,       "Đơn vị/Tổ chức"},
                {All,               "Đơn vị và cá nhân"},
            };
        }

        public class EmulationAndRewardSubjectType
        {
            public const int CaNhan = 1;
            public const int DonViToChuc = 2;

            public static readonly Dictionary<int?, string> dicDesc = new Dictionary<int?, string>()
            {
                {CaNhan,            "Cá nhân"},
                {DonViToChuc,       "Đơn vị/Tổ chức"},
            };
        }
    }
}