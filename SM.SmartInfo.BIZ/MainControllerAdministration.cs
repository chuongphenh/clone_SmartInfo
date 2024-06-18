using SM.SmartInfo.BIZ.Administration;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Administration;
using SM.SmartInfo.SharedComponent.Params.CommonList;
using System;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private void ExecuteAdministration(SharedComponent.Params.BaseParam param)
        {
            string name = param.GetType().Name;
            switch (name)
            {
                case "UserParam":
                    ExecuteAdministration(param as UserParam);
                    break;
                case "OrganizationParam":
                    ExecuteAdministration(param as OrganizationParam);
                    break;
                case "RoleParam":
                    ExecuteAdministration(param as RoleParam);
                    break;
                case "SettingParam":
                    ExecuteAdministration(param as SettingParam);
                    break;
                case "RightParam":
                    ExecuteAdministration(param as RightParam);
                    break;
                case "SystemParameterParam":
                    ExecuteAdministration(param as SystemParameterParam);
                    break;
                //case "ConfigDocumentParam":
                //    ExecuteAdministration(param as ConfigDocumentParam);
                //    break;
                case "EmailTemplateParam":
                    ExecuteAdministration(param as EmailTemplateParam);
                    break;
                case "TargetParam":
                    ExecuteAdministration(param as TargetParam);
                    break; 
                case "PlanParam":
                    ExecuteAdministration(param as PlanParam);
                    break;
                //case "CommitteeParam":
                //    ExecuteAdministration(param as CommitteeParam);
                //    break;

                case "RegionParam":
                    ExecuteAdministration(param as RegionParam);
                    break;
                default:
                    throw new Exception(string.Format("Param not supported: {0}", name));
            }
        }

        //private void ExecuteAdministration(CommitteeParam param)
        //{
        //    CommitteeBiz biz = new CommitteeBiz();

        //    switch (param.FunctionType)
        //    {
        //        case FunctionType.Administration.CommitteeFunctionType.Committee_AddNewCommittee:
        //            {
        //                biz.AddNewCommittee((CommitteeParam)param);
        //                break;
        //            }
        //        case FunctionType.Administration.CommitteeFunctionType.Committee_UpdateCommittee:
        //            {
        //                biz.UpdateCommittee((CommitteeParam)param);
        //                break;
        //            }
        //        case FunctionType.Administration.CommitteeFunctionType.Committee_DeleteCommittee:
        //            {
        //                biz.DeleteCommittee((CommitteeParam)param);
        //                break;
        //            }
        //        //case FunctionType.Administration.CommitteeFunctionType.Committee_SearchCommittee:
        //        //    {
        //        //        biz.SearchCommittee((CommitteeParam)param);
        //        //        break;
        //        //    }

        //        case FunctionType.Administration.CommitteeFunctionType.GetCommitteeByID:
        //            {
        //                biz.GetCommitteeByID((CommitteeParam)param);
        //                break;
        //            }

        //        case FunctionType.Administration.CommitteeFunctionType.Committee_GetAllCommitteeName:
        //            {
        //                biz.GetAllCommitteeName((CommitteeParam)param);
        //                break;
        //            }

        //        case FunctionType.Administration.CommitteeFunctionType.Committee_SetupDisplayForm:
        //            {
        //                biz.SetupDisplayForm((CommitteeParam)param);
        //                break;
        //            }
        //        case FunctionType.Administration.CommitteeFunctionType.Committee_SetupEditForm:
        //            {
        //                biz.SetupEditForm((CommitteeParam)param);
        //                break;
        //            }
        //        case FunctionType.Administration.CommitteeFunctionType.Committee_GetCommitteeEmployeeByEmployeeID:
        //            {
        //                biz.GetCommitteeEmployeeByEmployeeID((CommitteeParam)param);
        //                break;
        //            }

        //        default:
        //            throw new Exception(string.Format("FunctionType is not supported: {0}", param.FunctionType));
        //    }
        //}

        private void ExecuteAdministration(RightParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.Administration.Right.SetupViewForm:
                    {
                        var biz = new RightBiz();
                        biz.SetupViewForm(param);
                        break;
                    }
                case FunctionType.Administration.Right.GetItemsForView:
                    {
                        var biz = new RightBiz();
                        biz.GetItemsForView(param);
                        break;
                    }
                case FunctionType.Administration.Right.SaveItem:
                    {
                        var biz = new RightBiz();
                        biz.SaveItem(param);
                        break;
                    }
                default:
                    break;
            }
        }
        private void ExecuteAdministration(SettingParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.Administration.Setting.GetSettingFirst:
                    {
                        var biz = new SettingBiz();
                        biz.GetSettingFirst(param);
                        break;
                    }
                case FunctionType.Administration.Setting.UpdateDataSetting:
                    {
                        var biz = new SettingBiz();
                        biz.UpdateDataSetting(param);
                        break;
                    }
                default:
                    break;
            }
        }
        private void ExecuteAdministration(RoleParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.Administration.Role.GetAllRole:
                    {
                        var biz = new RoleConfigBiz();
                        biz.GetAllRole(param);
                        break;
                    }
                case FunctionType.Administration.Role.GetAllActiveRoleExceptQTHT:
                    {
                        var biz = new RoleConfigBiz();
                        biz.GetAllActiveRoleExceptQTHT(param);
                        break;
                    } 
                case FunctionType.Administration.Role.GetListRoleIDByPressAgencyHRID:
                    {
                        var biz = new RoleConfigBiz();
                        biz.GetListRoleIDByPressAgencyHRID(param);
                        break;
                    }
                case FunctionType.Administration.Role.GetItemsForView:
                    {
                        var biz = new RoleConfigBiz();
                        biz.SearchItemsForView(param);
                        break;
                    }
                case FunctionType.Administration.Role.AddNewItem:
                    {
                        var biz = new RoleConfigBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Role.SetupEditForm:
                    {
                        var biz = new RoleConfigBiz();
                        biz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.Role.UpdateItem:
                    {
                        var biz = new RoleConfigBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Role.DeleteItems:
                    {
                        var biz = new RoleConfigBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Role.LoadDataDisplayRole:
                    {
                        var biz = new RoleConfigBiz();
                        biz.LoadDataDisplay(param);
                        break;
                    }
                case FunctionType.Administration.Role.ExportExcel:
                    {
                        var biz = new RoleConfigBiz();
                        biz.ExportExcel(param);
                        break;
                    }

            }
        }

        private void ExecuteAdministration(SystemParameterParam param)
        {
            switch (param.FunctionType)
            {
                //#region Common
                //case FunctionType.Configuration.Common.DeleteItem:
                //    {
                //        SystemParameterBiz biz = new SystemParameterBiz();
                //        biz.DeleteItem(param);
                //        break;
                //    }
                //case FunctionType.Administration.GetSystemByFeatureIDAndExt1i:
                //    {
                //        SystemParameterBiz biz = new SystemParameterBiz();
                //        biz.GetSystemByFeatureIDAndExt1i(param);
                //        break;
                //    }
                //case FunctionType.Administration.GetAllActiveSystemParametersByFeatureID:
                //    {
                //        SystemParameterBiz biz = new SystemParameterBiz();
                //        biz.GetAllActiveSystemParametersByFeatureID(param);
                //        break;
                //    }

                //#endregion
                #region District

                case FunctionType.Administration.District.SetupAddFormDistrict:
                    {
                        DistrictBiz biz = new DistrictBiz();
                        biz.SetupAddNewForm(param);
                        break;
                    }
                case FunctionType.Administration.District.AddNewDistrict:
                    {
                        DistrictBiz biz = new DistrictBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.District.SetupEditFormDistrict:
                    {
                        DistrictBiz biz = new DistrictBiz();
                        biz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.District.UpdateDistrict:
                    {
                        DistrictBiz biz = new DistrictBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.District.SetupViewFormDistrict:
                    {
                        DistrictBiz biz = new DistrictBiz();
                        biz.SetupViewForm(param);
                        break;
                    }
                case FunctionType.Administration.District.DeleteItemsDistrict:
                    {
                        DistrictBiz biz = new DistrictBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.District.LoadDataEditDistrict:
                    {
                        DistrictBiz biz = new DistrictBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.District.GetItemsForViewDistrict:
                    {
                        DistrictBiz biz = new DistrictBiz();
                        biz.SearchItemsForView(param);
                        break;
                    }
                #endregion
                #region Country

                case FunctionType.Administration.Country.LoadDataEditCountry:
                    {
                        CountryBiz biz = new CountryBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.Country.DeleteItemsCountry:
                    {
                        CountryBiz biz = new CountryBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Country.UpdateCountry:
                    {
                        CountryBiz biz = new CountryBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Country.AddNewCountry:
                    {
                        CountryBiz biz = new CountryBiz();
                        biz.AddNewItem(param);
                        break;
                    }

                #endregion
                #region Province

                case FunctionType.Administration.Province.SetupAddFormProvince:
                    {
                        ProvinceBiz biz = new ProvinceBiz();
                        biz.SetupAddNewForm(param);
                        break;
                    }
                case FunctionType.Administration.Province.AddNewProvince:
                    {
                        ProvinceBiz biz = new ProvinceBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Province.SetupEditFormProvince:
                    {
                        ProvinceBiz biz = new ProvinceBiz();
                        biz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.Province.UpdateProvince:
                    {
                        ProvinceBiz biz = new ProvinceBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Province.SetupViewFormProvince:
                    {
                        ProvinceBiz biz = new ProvinceBiz();
                        biz.SetupViewForm(param);
                        break;
                    }
                case FunctionType.Administration.Province.DeleteItemsProvince:
                    {
                        ProvinceBiz biz = new ProvinceBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Province.LoadDataEditProvince:
                    {
                        ProvinceBiz biz = new ProvinceBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.Province.GetItemsForViewProvince:
                    {
                        ProvinceBiz biz = new ProvinceBiz();
                        biz.SearchItemsForView(param);
                        break;
                    }
                #endregion
                #region Street

                case FunctionType.Administration.Street.SetupAddFormStreet:
                    {
                        StreetBiz biz = new StreetBiz();
                        biz.SetupAddNewForm(param);
                        break;
                    }
                case FunctionType.Administration.Street.AddNewStreet:
                    {
                        StreetBiz biz = new StreetBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Street.SetupEditFormStreet:
                    {
                        StreetBiz biz = new StreetBiz();
                        biz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.Street.UpdateStreet:
                    {
                        StreetBiz biz = new StreetBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Street.SetupViewFormStreet:
                    {
                        StreetBiz biz = new StreetBiz();
                        biz.SetupViewForm(param);
                        break;
                    }
                case FunctionType.Administration.Street.DeleteItemsStreet:
                    {
                        StreetBiz biz = new StreetBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Street.LoadDataEditStreet:
                    {
                        StreetBiz biz = new StreetBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.Street.GetItemsForViewStreet:
                    {
                        StreetBiz biz = new StreetBiz();
                        biz.SearchItemsForView(param);
                        break;
                    }
                #endregion
                #region Town

                case FunctionType.Administration.Town.SetupAddFormTown:
                    {
                        TownBiz biz = new TownBiz();
                        biz.SetupAddNewForm(param);
                        break;
                    }
                case FunctionType.Administration.Town.AddNewTown:
                    {
                        TownBiz biz = new TownBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Town.SetupEditFormTown:
                    {
                        TownBiz biz = new TownBiz();
                        biz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.Town.UpdateTown:
                    {
                        TownBiz biz = new TownBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Town.SetupViewFormTown:
                    {
                        TownBiz biz = new TownBiz();
                        biz.SetupViewForm(param);
                        break;
                    }
                case FunctionType.Administration.Town.DeleteItemsTown:
                    {
                        TownBiz biz = new TownBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Town.LoadDataEditTown:
                    {
                        TownBiz biz = new TownBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.Town.GetItemsForViewTown:
                    {
                        TownBiz biz = new TownBiz();
                        biz.SearchItemsForView(param);
                        break;
                    }
                #endregion
                #region Zone

                case FunctionType.Administration.Zone.SetupAddFormZone:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.SetupAddNewForm(param);
                        break;
                    }
                case FunctionType.Administration.Zone.AddNewZone:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Zone.SetupEditFormZone:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.Zone.UpdateZone:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Zone.SetupViewFormZone:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.SetupViewForm(param);
                        break;
                    }
                case FunctionType.Administration.Zone.DeleteItemsZone:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Zone.LoadDataEditZone:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.Zone.GetItemsForViewZone:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.SearchItemsForView(param);
                        break;
                    }
                #endregion

                #region Notification
                case FunctionType.Administration.Notification.AddNewNotification:
                    {
                        NotificationBiz biz = new NotificationBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Notification.UpdateNotification:
                    {
                        NotificationBiz biz = new NotificationBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Notification.DeleteItemsNotification:
                    {
                        NotificationBiz biz = new NotificationBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Notification.LoadDataEditNotification:
                    {
                        NotificationBiz biz = new NotificationBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                #endregion

                #region Anniversary
                case FunctionType.Administration.Anniversary.AddNewAnniversary:
                    {
                        AnniversaryBiz biz = new AnniversaryBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Anniversary.UpdateAnniversary:
                    {
                        AnniversaryBiz biz = new AnniversaryBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Anniversary.DeleteItemsAnniversary:
                    {
                        AnniversaryBiz biz = new AnniversaryBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Anniversary.LoadDataEditAnniversary:
                    {
                        AnniversaryBiz biz = new AnniversaryBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                #endregion
                //#region RentOrganization
                //case FunctionType.Configuration.RentOrganization.AddNewRentOrganization:
                //    {
                //        RentOrganizationBiz biz = new RentOrganizationBiz();
                //        biz.AddNewItem(param);
                //        break;
                //    }
                //case FunctionType.Configuration.RentOrganization.UpdateRentOrganization:
                //    {
                //        RentOrganizationBiz biz = new RentOrganizationBiz();
                //        biz.UpdateItem(param);
                //        break;
                //    }
                //case FunctionType.Configuration.RentOrganization.DeleteRentOrganization:
                //    {
                //        RentOrganizationBiz biz = new RentOrganizationBiz();
                //        biz.DeleteItems(param);
                //        break;
                //    }
                //case FunctionType.Configuration.RentOrganization.LoadDataEditRentOrganization:
                //    {
                //        RentOrganizationBiz biz = new RentOrganizationBiz();
                //        biz.LoadDataEdit(param);
                //        break;
                //    }
                //case FunctionType.Configuration.RentOrganization.LoadDataDisplayRentOrganization:
                //    {
                //        RentOrganizationBiz biz = new RentOrganizationBiz();
                //        biz.LoadDataDisplay(param);
                //        break;
                //    }
                //#endregion
                #region Segment
                case FunctionType.Administration.Segment.SetupAddFormSegment:
                    {
                        SegmentBiz biz = new SegmentBiz();
                        biz.SetupAddNewForm(param);
                        break;
                    }
                case FunctionType.Administration.Segment.AddNewSegment:
                    {
                        SegmentBiz biz = new SegmentBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Segment.SetupEditFormSegment:
                    {
                        SegmentBiz biz = new SegmentBiz();
                        biz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.Segment.UpdateSegment:
                    {
                        SegmentBiz biz = new SegmentBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Segment.SetupViewFormSegment:
                    {
                        ZoneBiz biz = new ZoneBiz();
                        biz.SetupViewForm(param);
                        break;
                    }
                case FunctionType.Administration.Segment.DeleteItemsSegment:
                    {
                        SegmentBiz biz = new SegmentBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Segment.LoadDataEditSegment:
                    {
                        SegmentBiz biz = new SegmentBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.Segment.GetItemsForViewSegment:
                    {
                        SegmentBiz biz = new SegmentBiz();
                        biz.SearchItemsForView(param);
                        break;
                    }
                #endregion
            }
        }

        private void ExecuteAdministration(EmailTemplateParam param)
        {
            EmailTemplateBiz biz = new EmailTemplateBiz();

            switch (param.FunctionType)
            {
                case FunctionType.Administration.EmailTemplate.SetupAddNewForm:
                    biz.SetupAddNewForm(param);
                    break;
                case FunctionType.Administration.EmailTemplate.AddNewItem:
                    biz.AddNewItem(param);
                    break;
                case FunctionType.Administration.EmailTemplate.LoadDataDisplay:
                    biz.LoadDataDisplay(param);
                    break;
                case FunctionType.Administration.EmailTemplate.SetupEditForm:
                    biz.SetupEditForm(param);
                    break;
                case FunctionType.Administration.EmailTemplate.LoadDataEdit:
                    biz.LoadDataEdit(param);
                    break;
                case FunctionType.Administration.EmailTemplate.UpdateItem:
                    biz.UpdateItem(param);
                    break;
                case FunctionType.Administration.EmailTemplate.SetupViewForm:
                    biz.SetupViewForm(param);
                    break;
                case FunctionType.Administration.EmailTemplate.DeleteItem:
                    biz.DeleteItems(param);
                    break;
                case FunctionType.Administration.EmailTemplate.GetItemsForView:
                    biz.SearchItemsForView(param);
                    break;
                case FunctionType.Administration.EmailTemplate.ApproveRejectEmailTemplate:
                    biz.ApproveRejectEmailTemplate(param);
                    break;
            }
        }
        private void ExecuteAdministration(TargetParam param)
        {
            TargetBiz biz = new TargetBiz();

            switch (param.FunctionType)
            {
                case FunctionType.Administration.Target.SetupAddNewForm:
                    biz.SetupAddNewForm(param);
                    break;
                case FunctionType.Administration.Target.AddNewItem:
                    biz.AddNewItem(param);
                    break;
                case FunctionType.Administration.Target.LoadDataDisplay:
                    biz.LoadDataDisplay(param);
                    break;
                case FunctionType.Administration.Target.SetupEditForm:
                    biz.SetupEditForm(param);
                    break;
                case FunctionType.Administration.Target.LoadDataEdit:
                    biz.LoadDataEdit(param); 
                    break;
                case FunctionType.Administration.Target.UpdateItem:
                    biz.UpdateItem(param);
                    break;
                case FunctionType.Administration.Target.SetupViewForm:
                    biz.SetupViewForm(param);
                    break;
                case FunctionType.Administration.Target.DeleteItem:
                    biz.DeleteItems(param);
                    break;
                case FunctionType.Administration.Target.GetItemsForView:
                    biz.SearchItemsForView(param);
                    break;
            }
        }
        private void ExecuteAdministration(PlanParam param)
        {
            PlanBiz biz = new PlanBiz();

            switch (param.FunctionType)
            {
                case FunctionType.Administration.Plan.SetupAddNewForm:
                    biz.SetupAddNewForm(param);
                    break;
                case FunctionType.Administration.Plan.AddNewItem:
                    biz.AddNewItem(param);
                    break;
                case FunctionType.Administration.Plan.LoadDataDisplay:
                    biz.LoadDataDisplay(param);
                    break;
                case FunctionType.Administration.Plan.SetupEditForm:
                    biz.SetupEditForm(param);
                    break;
                case FunctionType.Administration.Plan.LoadDataEdit:
                    biz.LoadDataEdit(param);
                    break;
                case FunctionType.Administration.Plan.UpdateItem:
                    biz.UpdateItem(param);
                    break;
                case FunctionType.Administration.Plan.SetupViewForm:
                    biz.SetupViewForm(param);
                    break;
                case FunctionType.Administration.Plan.DeleteItem:
                    biz.DeleteItems(param);
                    break;
                case FunctionType.Administration.Plan.GetItemsForView:
                    biz.SearchItemsForView(param);
                    break;
            }
        }

        private void ExecuteAdministration(OrganizationParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.Administration.Organization.GetOrganizationTreeData:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.GetOrganizationTreeData(param);
                        break;
                    }
                case FunctionType.Administration.Organization.GetListEmployeeByOrganizationID:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.GetListEmployeeByOrganizationID(param);
                        break;
                    }
                case FunctionType.Administration.Organization.SetupAddNewForm:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.SetupAddNewForm(param);
                        break;
                    }
                case FunctionType.Administration.Organization.AddNewOrganization:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.Organization.DeleteOrganizations:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.Organization.SetupEditForm:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.Organization.UpdateOrganization:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.Organization.LoadDataDisplayOrganization:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.LoadDataDisplay(param);
                        break;
                    }
                case FunctionType.Administration.Organization.LoadDataEditOrganization:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.LoadDataEdit(param);
                        break;
                    }

                case FunctionType.Administration.Organization.ValidateEmployeeIsInOtherOrganization:
                    {
                        OrganizationConfigBiz biz = new OrganizationConfigBiz();
                        biz.ValidateEmployeeIsInOtherOrganization(param);
                        break;
                    }

                default:
                    throw new NotSupportedException();
            }
        }

        private void ExecuteAdministration(UserParam param)
        {
            Commons.UserBiz biz = new Commons.UserBiz();

            switch (param.FunctionType)
            {
                #region CRUD
                case FunctionType.Administration.User.DeleteItems:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.DeleteItems(param);
                        break;
                    }
                case FunctionType.Administration.User.AddNewItem:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.AddNewItem(param);
                        break;
                    }
                case FunctionType.Administration.User.SetupAddNewForm:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.SetupAddNewForm(param);
                        break;
                    }
                case FunctionType.Administration.User.SetupViewForm:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.SetupViewForm(param);
                        break;
                    }
                case FunctionType.Administration.User.GetItemsForView:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.SearchItemsForView(param);
                        break;
                    }
                case FunctionType.Administration.User.GetEmployeeByID:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.GetEmployeeByID(param);
                        break;
                    }
                case FunctionType.Administration.User.GetEmployeeByUserName:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.GetEmployeeByUserName(param);
                        break;
                    }
                case FunctionType.Administration.User.SetupEditForm:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.SetupEditForm(param);
                        break;
                    }
                case FunctionType.Administration.User.UpdateItem:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.UpdateItem(param);
                        break;
                    }
                case FunctionType.Administration.User.LoadDataDisplayUser:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.LoadDataDisplay(param);
                        break;
                    }
                case FunctionType.Administration.User.UpdateIsLocked:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.UpdateIsLockedOpen(param);
                        break;
                    }
                case FunctionType.Administration.User.LoadDataEditUser:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.User.LoadDataDisplayForReport:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.LoadDataDisplayForReport(param);
                        break;
                    }
                case FunctionType.Administration.User.SearchUserForSharing:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.SearchUserForSharing(param);
                        break;
                    }

                case FunctionType.Administration.User.SearchListUserForSharing:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.SearchListUserForSharing(param);
                        break;
                    }
                case FunctionType.Administration.User.ShareToStaff:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.ShareToStaff(param);
                        break;
                    }
                case FunctionType.Administration.User.ImportOrUpdateListEmployeeFromExcel:
                    {
                        var cBiz = new UserConfigBiz();
                        cBiz.ImportOrUpdateListEmployeeFromExcel(param);
                        break;
                    }

                #endregion

                //#region Ho tro nghiep vu
                //case FunctionType.Administration.User.SupportBusiness_ReExportPDF:
                //    {
                //        SupportBusinessBiz supBiz = new SupportBusinessBiz();
                //        supBiz.ReExportPDF(param);
                //        break;
                //    }
                //case FunctionType.Administration.User.SupportBusiness_ChangeVDDRequestDoc:
                //    {
                //        SupportBusinessBiz supBiz = new SupportBusinessBiz();
                //        supBiz.ChangeVDDRequestDoc(param);
                //        break;
                //    }
                //#endregion

                default:
                    throw new Exception(string.Format("FunctionType is not supported: {0}", param.FunctionType));
            }
        }

        #region Vùng
        private void ExecuteAdministration(RegionParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.Administration.Region.GetItemsForView:
                    {
                        var regionBiz = new RegionBiz();
                        regionBiz.SearchItemsForView(param);
                        break;
                    }
                case FunctionType.Administration.Region.LoadDataEdit:
                    {
                        var regionBiz = new RegionBiz();
                        regionBiz.LoadDataEdit(param);
                        break;
                    }
                case FunctionType.Administration.Region.AddNewItem:
                    {
                        var regionBiz = new RegionBiz();
                        regionBiz.AddNewData(param);
                        break;
                    }
                case FunctionType.Administration.Region.SaveDataEdit:
                    {
                        var regionBiz = new RegionBiz();
                        regionBiz.SaveData(param);
                        break;
                    }
                case FunctionType.Administration.Region.DeleteRegion:
                    {
                        var regionBiz = new RegionBiz();
                        regionBiz.DeleteRegion(param);
                        break;
                    }
                default:
                    break;
            }

        }
        #endregion
    }
}