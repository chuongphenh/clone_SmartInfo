namespace SM.SmartInfo.SharedComponent.Constants
{
    public partial class FunctionType
    {
        public class Authentication
        {
            public const string AutoLogin = "AutoLogin";
            public const string Login = "Login";
            public const string LoginBySinhTracHoc = "LoginBySinhTracHoc";
            public const string LoginAPI = "LoginAPI";
            public const string Logout = "Logout";
            public const string ChangePassword = "ChangePassword";
            public const string ResetPassword = "ResetPassword";
            public const string GetLog = "GetLog";
            public const string DeleteLog = "DeleteLog";
            public const string LockUser = "LockUser";
            public const string UnlockUser = "UnlockUser";
            public const string UpdateLoggingAttemp = "UpdateLoggingAttemp";
            public const string CheckValidUser = "CheckValidUser";
            public const string GetLoggingAttemptByUsername = "GetLoggingAttemptByUsername";
        }

        #region Common

        public class Common
        {
            public const string GetCommon = "GetCommon";
            public const string NewsSearch = "NewsSearch";
            public const string PressAgencySearch = "PressAgencySearch";
            public const string NegativeNewsSearch = "NegativeNewsSearch";
            public const string NotificationSearch = "NotificationSearch";
            public const string FilterNotification = "FilterNotification";
            public const string GetOrganizationByParentID = "GetOrganizationByParentID";
            public const string GetOrganizationByTypeAndCommitteeCode = "GetOrganizationByTypeAndCommitteeCode";
            public const string SearchShortEmployee = "SearchShortEmployee";
            public const string SearchUserByName = "SearchUserByName";
            public const string EmployeeSelectorSearch = "EmployeeSelectorSearch";
            public const string GetShortUserByID = "GetShortUserByID";
            public const string GetShortEmployeeInOrg = "GetShortEmployeeInOrg";

            public const string OrganizationSelectorSearch = "OrganizationSelectorSearch";
            public const string GetZoneIDByOrganizationID = "GetZoneIDByOrganizationID";
            public const string GetOrganizationByType = "GetOrganizationByType";
            public const string GetOrganizationByID = "GetOrganizationByID";
            public const string GetOrganizationByListDirectManagingOrganizationID = "GetOrganizationByListDirectManagingOrganizationID";
            public const string GetListOrganizationByProvinceId = "GetListOrganizationByProvinceId";

            public const string SearchServiceCommand = "SearchServiceCommand";
            public const string DeleteServiceCommand = "DeleteServiceCommand";
            public const string AddBlacklistPriceServiceCommand = "AddBlacklistPriceServiceCommand";
            public const string GetListValuationManager = "GetListValuationManager";

            public const string GetFeatureDefinitionByFeatureID = "GetFeatureDefinitionByFeatureID";

            public const string GetListOrganizationByZoneId = "GetListOrganizationByZoneId";
            public const string GetListEmployeeByOrganizationID = "GetListEmployeeByOrganizationID";
            public const string SearchBranchByName = "SearchBranchByName";

            public const string SearchOrganization = "SearchOrganization";

            public const string GetListEmployeeByListStringOrganizationID = "GetListEmployeeByListStringOrganizationID";

            public const string SearchSQLFunctionDefinition = "SearchSQLFunctionDefinition";
            public const string GetSQLFunctionDefinitionByID = "GetSQLFunctionDefinitionByID";
            public const string UpdateSQLFunctionDefinition = "UpdateSQLFunctionDefinition";

            public const string SearchImporting = "SearchImporting";
            public const string ImportingRequest_GetApprovingEmployee = "ImportingRequest_GetApprovingEmployee";
            public const string ImportingRequest_AddNew = "ImportingRequest_AddNew";
            public const string ImportingRequest_Download = "ImportingRequest_Download";
            public const string ImportingRequest_Approve = "ImportingRequest_Approve";
            public const string ImportingRequest_Reject = "ImportingRequest_Reject";
            public const string ImportingRequest_Search = "ImportingRequest_Search";
        }

        #endregion

        // thông báo
        public class Notification
        {
            
            public const string DeleteNotificationByPressAgencyID = "DeleteNotificationByPressAgencyID";
            public const string GetAllNotification = "GetAllNotification";
            public const string SearchNotification = "SearchNotification";
            public const string LoadDataDisplay = "LoadDataDisplay";
            public const string PushNotification = "PushNotification";
            public const string AddOrUpdateItemNotification = "AddOrUpdateItemNotification";
            public const string UpdateItem = "UpdateItem";
            public const string AddOrUpdateItem = "AddOrUpdateItem";
            public const string DeleteNotificationByHRAlertID = "DeleteNotificationByHRAlertID";
            public const string InsertFirebase = "InsertFirebase";
            public const string GetListPushNotificationHistory = "GetListPushNotificationHistory";
        }

        // sự vụ
        public class NegativeNew
        {
            public const string GetAllNegativeNews = "GetAllNegativeNews";
            public const string LoadDataDisplay = "LoadDataDisplay";
            public const string LoadDataImages = "LoadDataImages";
            public const string LoadDataImagesDetail = "LoadDataImagesDetail";

            public const string AddNewItem = "AddNewItem";
            public const string UpdateItem = "UpdateItem";
            public const string UpdateStatusHoanThanh = "UpdateStatusHoanThanh";
            public const string DeleteItem = "DeleteItem";
            public const string DeleteNewNegativeNews = "DeleteNewNegativeNews";

            public const string GetItemsForView = "GetItemsForView";
            public const string SearchNegativeNews = "SearchNegativeNews";
            public const string GetItemsNegativeNews = "GetItemsNegativeNews";
            public const string UpdateNegativeNews = "UpdateNegativeNews";
            public const string FinishNegativeNews = "FinishNegativeNews";
            public const string AddNewNegativeNews = "AddNewNegativeNews";
            public const string LoadDataNegativeNews = "LoadDataNegativeNews";
        }

        // tin tức
        public class News
        {
            public const string GetNewsForView = "GetNewsForView";
            public const string SearchNewsForView = "SearchNewsForView";
            public const string GetItemsNews = "GetItemsNews";
            public const string UpdateNews = "UpdateNews";
            public const string AddNewNews = "AddNewNews";
            public const string LoadDataNews = "LoadDataNews";
            public const string LoadDataImagesNews = "LoadDataImagesNews";
            public const string LoadDataImagesPositiveNews = "LoadDataImagesPositiveNews";
            public const string DeleteNewsAndPositiveNewsAndCampaignNews = "DeleteNewsAndPositiveNewsAndCampaignNews";
            public const string AddDocumentCampaignNews = "AddDocumentCampaignNews";
            public const string LoadDataDocumentCampaignNews = "LoadDataDocumentCampaignNews";

            public const string BuildTreeListNews = "BuildTreeListNews";
            public const string GetListHastag = "GetListHastag";
            public const string IsNameExists = "IsNameExists";
            public const string CreateHastag = "CreateHastag";
            public const string IsSingleCamp = "IsSingleCamp";
            public const string SetIsSingleCamp = "SetIsSingleCamp";

            // single news
            public const string SaveSingleNews = "SaveSingleNews";
            public const string GetListSingleNewsByNewsID = "GetListSingleNewsByNewsID";
            public const string DeleteSingleNews = "DeleteSingleNews";
            public const string LoadDataImagesSingleNews = "LoadDataImagesSingleNews";
            public const string GetListSingleNewsByNewsIDAndCampaignID = "GetListSingleNewsByNewsIDAndCampaignID";
            public const string AddNewCampaignNews = "AddNewCampaignNews";
        }

        // bình luận
        public class Comment
        {
            public const string GetAllCommentByRefIDAndRefType = "GetAllCommentByRefIDAndRefType";
            public const string InsertUpdateCommentByRefIDAndRefType = "InsertUpdateCommentByRefIDAndRefType";
            public const string DeleteCommentByID = "DeleteCommentByID";
        }

        public class NegativeNewsResearched
        {
            public const string SaveNegativeNewsResearched = "SaveNegativeNewsResearched";
            public const string GetListNegativeNewsResearchedByNegativeNewsID = "GetListNegativeNewsResearchedByNegativeNewsID";
            public const string DeleteNegativeNewsResearched = "DeleteNegativeNewsResearched";
        }

        public class NewsResearched
        {
            public const string SaveNewsResearched = "SaveNewsResearched";
            public const string GetListNewsResearchedByNewsID = "GetListNewsResearchedByNewsID";
            public const string DeleteNewsResearched = "DeleteNewsResearched";
        }

        public class PositiveNews
        {
            public const string SavePositiveNews = "SavePositiveNews";
            public const string PrepareDataCampaign = "PrepareDataCampaign";
            public const string GetListPositiveNewsByNewsID = "GetListPositiveNewsByNewsID";
            public const string DeletePositiveNews = "DeletePositiveNews";
        }

        public class CampaignNews
        {
            public const string SaveCampaignNews = "SaveCampaignNews";
            public const string GetListCampaignNewsByNewsID = "GetListCampaignNewsByNewsID";
            public const string getAtt = "getAtt";
            public const string DeleteCampaignNews = "DeleteCampaignNews";
            public const string getCountUploadedDocument = "getCountUploadedDocument";
        }

        public class PressAgency
        {
            public const string SetupFormDefault = "SetupForm";
            public const string GetItemsForView = "GetItemsForView";
            public const string SearchItemsForView = "SearchItemsForView";
            public const string LoadDataDisplay = "LoadDataDisplay";
            public const string SavePressAgency = "SavePressAgency";
            public const string DeletePressAgency = "DeletePressAgency";

            public const string SavePressAgencyHR = "SavePressAgencyHR";
            public const string DeletePressAgencyHR = "DeletePressAgencyHR";
            public const string GetPressAgencyHR_ByID = "GetPressAgencyHR_ByID";
            public const string GetListPressAgencyHR_ByPressAgencyID = "GetListPressAgencyHR_ByPressAgencyID";
            public const string GetListPressAgencyHR = "GetListPressAgencyHR";
            public const string GetListPressAgencyHR_ByFilter = "GetListPressAgencyHR_ByFilter";
            public const string GetListPressAgencyHR_ByFilterNoPaging = "GetListPressAgencyHR_ByFilterNoPaging";
            public const string ImportOrUpdateListPressAgencyHRFromExcel = "ImportOrUpdateListPressAgencyHRFromExcel";
            
            public const string GetListPressAgencyHRRelatives_ByPressAgencyHRID = "GetListPressAgencyHRRelatives_ByPressAgencyHRID";
            public const string SavePressAgencyHRRelatives = "SavePressAgencyHRRelatives";
            public const string DeletePressAgencyHRRelatives = "DeletePressAgencyHRRelatives";

            public const string GetListPressAgencyHRHistory_ByPressAgencyHRID = "GetListPressAgencyHRHistory_ByPressAgencyHRID";
            public const string SavePressAgencyHRHistory = "SavePressAgencyHRHistory";
            public const string DeletePressAgencyHRHistory = "DeletePressAgencyHRHistory";

            public const string GetListPressAgencyHRAlert_ByPressAgencyHRID = "GetListPressAgencyHRAlert_ByPressAgencyHRID";
            public const string SavePressAgencyHRAlert = "SavePressAgencyHRAlert";
            public const string DeletePressAgencyHRAlert = "DeletePressAgencyHRAlert";

            public const string GetListPressAgencyHistory_ByPressAgencyID = "GetListPressAgencyHistory_ByPressAgencyID";
            public const string SavePressAgencyHistory = "SavePressAgencyHistory";
            public const string DeletePressAgencyHistory = "DeletePressAgencyHistory";

            public const string GetListPressAgencyMeeting_ByPressAgencyID = "GetListPressAgencyMeeting_ByPressAgencyID";
            public const string SavePressAgencyMeeting = "SavePressAgencyMeeting";
            public const string DeletePressAgencyMeeting = "DeletePressAgencyMeeting";

            public const string GetListRelationsPressAgency_ByPressAgencyID = "GetListRelationsPressAgency_ByPressAgencyID";
            public const string SaveRelationsPressAgency = "SaveRelationsPressAgency";
            public const string DeleteRelationsPressAgency = "DeleteRelationsPressAgency";

            public const string GetListRelationshipWithMB_ByPressAgencyID = "GetListRelationshipWithMB_ByPressAgencyID";
            public const string SaveRelationshipWithMB = "SaveRelationshipWithMB";
            public const string DeleteRelationshipWithMB = "DeleteRelationshipWithMB";

            public const string GetPressAgencySelector = "GetPressAgencySelector";

            public const string GetListOtherImage_ByPressAgencyID = "GetListOtherImage_ByPressAgencyID";
            public const string LoadDataOtherImage_PressAgencyHR = "LoadDataOtherImage_PressAgencyHR";
            public const string SearchItemsForViewPressAgencyHR = "SearchItemsForViewPressAgencyHR";

            public const string AddNewAgencyType = "AddNewAgencyType";
            public const string GetListAgencyType = "GetListAgencyType";
            public const string GetListPressAgencyByType = "GetListPressAgencyByType";
            public const string GetPressAgencyByName = "GetPressAgencyByName";
            public const string GetPressAgencyHRByName = "GetPressAgencyHRByName";
            public const string GetListPressAgencyHRByName = "GetListPressAgencyHRByName";
            public const string AddNewPressAgency = "AddNewPressAgency";
            public const string GetPressAgencyTypeByCode = "GetPressAgencyTypeByCode";
            public const string GetPressAgencyTypeByID = "GetPressAgencyTypeByID";
            
            public const string GetListSharedUser = "GetListSharedUser";
            public const string DeleteShare = "DeleteShare";
            public const string DeletePressAgencyHRAlertByPressAgenctyHrID = "DeletePressAgencyHRAlertByPressAgenctyHrID";
        }

        public class CatalogNews
        {
            public const string GetCatalogNewsTreeData = "GetCatalogNewsTreeData";
            public const string DeleteCatalogNews = "DeleteCatalogNews";
            public const string UpdateCatalogNews = "UpdateCatalogNews";
            public const string LoadDataDisplayCatalogNews = "LoadDataDisplayCatalogNews";
            public const string LoadDataEditCatalogNews = "LoadDataEditCatalogNews";
            public const string AddNewCatalogNews = "AddNewCatalogNews";
        }

        public class Administration
        {
            public class Setting
            {
                public const string GetSettingFirst = "GetSettingFirst";
                public const string UpdateDataSetting = "UpdateDataSetting";
            }

            public class Right
            {
                public const string SetupAddNewForm = "SetupAddNewForm";
                public const string AddNewItem = "AddNewItem";

                public const string SetupDisplayForm = "SetupDisplayForm";
                public const string DeleteItem = "DeleteItem";
                public const string SaveItem = "SaveItem";

                public const string SetupEditForm = "SetupEditForm";
                public const string UpdateItem = "UpdateItem";

                public const string SetupViewForm = "SetupViewForm";
                public const string DeleteItems = "DeleteItems";
                public const string GetItemsForView = "GetItemsForView";
            }

            public class Organization
            {
                //CRUD
                public const string GetOrganizationTreeData = "GetOrganizationTreeData";
                public const string SetupAddNewForm = "SetupAddNewForm";
                public const string AddNewOrganization = "AddNewOrganization";
                public const string SetupEditForm = "SetupEditForm";
                public const string UpdateOrganization = "UpdateOrganizationByID";
                public const string DeleteOrganizations = "DeleteOrganization";
                public const string LoadDataDisplayOrganization = "LoadDataDisplayOrganization";
                public const string LoadDataEditOrganization = "LoadDataEditOrganization";
                public const string ValidateEmployeeIsInOtherOrganization = "ValidateEmployeeIsInOtherOrganization";

                public const string GetListEmployeeByOrganizationID = "GetListEmployeeByOrganizationID";
            }

            public class Country
            {
                public const string AddNewCountry = "AddNewCountry";
                public const string LoadDataEditCountry = "LoadDataEditCountry";
                public const string DeleteItemsCountry = "DeleteItemsCountry";
                public const string UpdateCountry = "UpdateCountry";
            }

            public class Region
            {
                public const string GetItemsForView = "GetItemsForView";
                public const string AddNewItem = "AddNewItem";
                public const string SaveDataEdit = "SaveDataEdit";
                public const string LoadDataEdit = "LoadDataEdit";
                public const string DeleteRegion = "DeleteRegion";
            }

            public class Province
            {
                public const string SetupAddFormProvince = "SetupAddFormProvince";
                public const string SetupViewFormProvince = "SetupViewFormProvince";
                public const string SetupEditFormProvince = "SetupEditFormProvince";
                public const string LoadDataEditProvince = "LoadDataEditProvince";
                public const string GetItemsForViewProvince = "GetItemsForViewProvince";

                public const string AddNewProvince = "AddNewProvince";
                public const string UpdateProvince = "UpdateProvince";
                public const string DeleteItemsProvince = "DeleteItemsProvince";
            }
            public class Notification
            {
                public const string LoadDataEditNotification = "LoadDataEditNotification";
                public const string AddNewNotification = "AddNewNotification";
                public const string UpdateNotification = "UpdateNotification";
                public const string DeleteItemsNotification = "DeleteItemsNotification";
            }

            public class Anniversary
            {
                public const string LoadDataEditAnniversary = "LoadDataEditAnniversary";
                public const string AddNewAnniversary = "AddNewAnniversary";
                public const string UpdateAnniversary = "UpdateAnniversary";
                public const string DeleteItemsAnniversary = "DeleteItemsAnniversary";
            }


            public class Street
            {
                public const string SetupAddFormStreet = "SetupAddFormStreet";
                public const string SetupViewFormStreet = "SetupViewFormStreet";
                public const string SetupEditFormStreet = "SetupEditFormStreet";
                public const string LoadDataEditStreet = "LoadDataEditStreet";
                public const string GetItemsForViewStreet = "GetItemsForViewStreet";

                public const string AddNewStreet = "AddNewStreet";
                public const string UpdateStreet = "UpdateStreet";
                public const string DeleteItemsStreet = "DeleteItemsStreet";
            }

            public class User
            {
                public const string SetupAddNewForm = "SetupAddNewForm";
                public const string AddNewItem = "AddNewItem";

                public const string SetupEditForm = "SetupEditForm";
                public const string UpdateItem = "UpdateItem";

                public const string SetupViewForm = "SetupViewForm";
                public const string DeleteItems = "DeleteItems";
                public const string GetItemsForView = "GetItemsForView";

                public const string LoadDataDisplayUser = "LoadDataDisplayUser";
                public const string LoadDataEditUser = "LoadDataEditUser";
                public const string LoadDataDisplayForReport = "LoadDataDisplayForReport";

                public const string GetEmployeeByID = "GetEmployeeByID";
                public const string GetEmployeeByUserName = "GetEmployeeByUserName";
                public const string UpdateIsLocked = "UpdateIsLocked";

                // ho tro nghiep vu
                public const string ImportOrUpdateListEmployeeFromExcel = "ImportOrUpdateListEmployeeFromExcel";
                public const string SupportBusiness_ReExportPDF = "SupportBusiness_ReExportPDF";
                public const string SupportBusiness_ChangeVDDRequestDoc = "SupportBusiness_ChangeVDDRequestDoc";

                // event sharing
                public const string SearchUserForSharing = "SearchUserForSharing";
                // event sharing
                public const string SearchListUserForSharing = "SearchListUserForSharing";
                public const string ShareToStaff = "ShareToStaff";
            }


            public class Town
            {
                public const string SetupAddFormTown = "SetupAddFormTown";
                public const string SetupViewFormTown = "SetupViewFormTown";
                public const string SetupEditFormTown = "SetupEditFormTown";
                public const string LoadDataEditTown = "LoadDataEditTown";
                public const string GetItemsForViewTown = "GetItemsForViewTown";

                public const string AddNewTown = "AddNewTown";
                public const string UpdateTown = "UpdateTown";
                public const string DeleteItemsTown = "DeleteItemsTown";
            }

            public class Zone
            {
                public const string SetupAddFormZone = "SetupAddFormZone";
                public const string SetupViewFormZone = "SetupViewFormZone";
                public const string SetupEditFormZone = "SetupEditFormZone";
                public const string LoadDataEditZone = "LoadDataEditZone";
                public const string GetItemsForViewZone = "GetItemsForViewZone";

                public const string AddNewZone = "AddNewZone";
                public const string UpdateZone = "UpdateZone";
                public const string DeleteItemsZone = "DeleteItemsZone";
            }

            public class Role
            {
                public const string AddNewItem = "AddNewItem";
                public const string SetupEditForm = "SetupEditForm";
                public const string UpdateItem = "UpdateItem";
                public const string DeleteItems = "DeleteItems";
                public const string GetItemsForView = "GetItemsForView";
                public const string LoadDataDisplayRole = "LoadDataDisplayRole";
                public const string ExportExcel = "ExportExcel";

                public const string GetAllRole = "GetAllRole";
                public const string GetAllActiveRoleExceptQTHT = "GetAllActiveRoleExceptQTHT";
                public const string GetListRoleIDByPressAgencyHRID = "GetListRoleIDByPressAgencyHRID";
            }

            public class District
            {
                public const string SetupAddFormDistrict = "SetupAddFormDistrict";
                public const string SetupViewFormDistrict = "SetupViewFormDistrict";
                public const string SetupEditFormDistrict = "SetupEditFormDistrict";
                public const string LoadDataEditDistrict = "LoadDataEditDistrict";
                public const string GetItemsForViewDistrict = "GetItemsForViewDistrict";

                public const string AddNewDistrict = "AddNewDistrict";
                public const string UpdateDistrict = "UpdateDistrict";
                public const string DeleteItemsDistrict = "DeleteItemsDistrict";
            }

            public class Segment
            {
                public const string AddNewSegment = "AddNewSegment";
                public const string UpdateSegment = "UpdateSegment";
                public const string DeleteItemsSegment = "DeleteItemsSegment";

                public const string SetupAddFormSegment = "SetupAddFormSegment";
                public const string SetupViewFormSegment = "SetupViewFormSegment";
                public const string SetupEditFormSegment = "SetupEditFormSegment";
                public const string LoadDataEditSegment = "LoadDataEditSegment";
                public const string GetItemsForViewSegment = "GetItemsForViewSegment";
            }

            public class EmailTemplate
            {
                public const string SetupAddNewForm = "SetupAddNewForm";
                public const string AddNewItem = "AddNewItem";

                public const string LoadDataDisplay = "LoadDataDisplay";

                public const string SetupEditForm = "SetupEditForm";
                public const string LoadDataEdit = "LoadDataEdit";
                public const string UpdateItem = "UpdateItem";

                public const string SetupViewForm = "SetupViewForm";
                public const string DeleteItem = "DeleteItem";
                public const string GetItemsForView = "GetItemsForView";

                public const string ApproveRejectEmailTemplate = "ApproveRejectEmailTemplate";
            }
        }

        public class CommonList
        {
            public class Attachment
            {
                public const string Upload = "Upload";
                public const string UploadOther = "UploadOther";
                public const string Replace = "Replace";
                public const string Download = "Download";
                public const string DownloadAll = "DownloadAll";
                public const string ViewDocument = "ViewDocument";
                public const string DeleteDocument = "DeleteDocument";
                public const string GetAttachmentByID = "Attachment_GetAttachmentByID";
                public const string GetListAttachment = "Attachment_GetListAttachment";
                public const string GetListAttachmentByRefType = "Attachment_GetListAttachmentByRefType";

                public const string GetByteArrayForMobile = "GetByteArrayForMobile";
            }
        }

        public class EmulationAndReward
        {
            public const string SetupFormDisplay = "SetupFormDisplay";
            public const string BuildTreeListEmulationAndRewards = "BuildTreeListEmulationAndRewards";
            public const string GetListEmulationAndRewardByFilter = "GetListEmulationAndRewardByFilter";

            public const string AddNewEmulationAndReward = "AddNewEmulationAndReward";
            public const string SaveEmulationAndRewardSubject = "SaveEmulationAndRewardSubject";
            public const string DeleteEmulationAndRewardSubject = "DeleteEmulationAndRewardSubject";
            public const string GetListEmulationAndRewardHistory = "GetListEmulationAndRewardHistory";

            public const string GetListAwardingCatalog = "GetListAwardingCatalog";
            public const string GetListAwardingPeriod = "GetListAwardingPeriod";
            public const string GetListAwardingLevel = "GetListAwardingLevel";
            public const string GetListAwardingPeriodResult = "GetListAwardingPeriodResult";

            public const string AddNewAwardingCatalog = "AddNewAwardingCatalog";
            public const string EditAwardingCatalog = "EditAwardingCatalog";
            public const string DeleteSelectedAwardingCatalog = "DeleteSelectedAwardingCatalog";
            public const string GetAwardingCatalogById = "GetAwardingCatalogById";

            public const string CreateAwardingLevel = "CreateAwardingLevel";
            public const string GetAwardingLevelById = "GetAwardingLevelById";
            public const string EditAwardingLevel = "EditAwardingLevel";
            public const string DeleteSelectedAwardingLevel = "DeleteSelectedAwardingLevel";
            public const string GetListAwardingLevelNoPaging = "GetListAwardingLevelNoPaging";

            public const string GetListAwardingType = "GetListAwardingType";
            public const string GetAwardingTypeCount = "GetAwardingTypeCount";
            public const string GetAwardingTypeById = "GetAwardingTypeById";
            public const string CreateAwardingType = "CreateAwardingType";
            public const string EditAwardingType = "EditAwardingType";
            public const string DeleteSelectedAwardingType = "DeleteSelectedAwardingType";
            public const string GetListAwardingTypeNoPaging = "GetListAwardingTypeNoPaging";

            public const string CreateAwardingPeriod = "CreateAwardingPeriod";
            public const string GetAwardingPeriodById = "GetAwardingPeriodById";
            public const string DeleteSelectedAwardingPeriod = "DeleteSelectedAwardingPeriod";
            public const string EditAwardingPeriod = "EditAwardingPeriod";
            public const string GetListAwardingPeriodNoPaging = "GetListAwardingPeriodNoPaging";
        }

        public class ImageLibrary
        {
            public const string AddNewNode = "AddNewNode";
            public const string GetListImageCatalog = "GetListImageCatalog";
            public const string DeleteSelectedNode = "DeleteSelectedNode";
            public const string EditNoteName = "EditNoteName";
            public const string GetImageCatalogById = "GetImageCatalogById";
            public const string GetListImage = "GetListImage";
            public const string GetImageByFilter = "GetImageByFilter";
            public const string DeleteSelectedImg = "DeleteSelectedImg";
            public const string DeleteSelectedImgOriginal = "DeleteSelectedImgOriginal";
            public const string GetListEditableNode = "GetListEditableNode";
            public const string AddImageToNode = "AddImageToNode";
            public const string GetRootImageByFilter = "GetRootImageByFilter";
            public const string ViewDeletedItem = "ViewDeletedItem";
            public const string RevertDeletedItem = "RevertDeletedItem";
            public const string PermanentlyDelete = "PermanentlyDelete";
            public const string GetRefTypeById = "GetRefTypeById";
            public const string GetListPostedYears = "GetListPostedYears";
        }
    }
}