using System;
using SM.SmartInfo.BIZ.News;
using SM.SmartInfo.BIZ.Commons;
using SM.SmartInfo.BIZ.Administration;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.Common;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private static MainController provider;

        private MainController() { }

        public static MainController Provider
        {
            get
            {
                if (provider == null)
                {
                    provider = new MainController();
                }
                return provider;
            }
        }

        public void Execute(BaseParam baseParam)
        {
            string function = string.Format("MainController: {0} - {1}", baseParam.BusinessType, baseParam.FunctionType);
            using (var logger = new SoftMart.Core.Utilities.Diagnostics.PLogger(function))
            {
                try
                {
                    switch (baseParam.BusinessType)
                    {
                        case BusinessType.Administrations:
                            ExecuteAdministration(baseParam);
                            break;
                        case BusinessType.Commons:
                            ExecuteCommon(baseParam as CommonParam);
                            break;
                        case BusinessType.Comment:
                            ExecuteComment(baseParam as CommentParam);
                            break;
                        case BusinessType.CommonList:
                            ExecuteCommonList(baseParam);
                            break;
                        case BusinessType.Notification:
                            ExecuteNotification(baseParam as NotificationParam);
                            break;
                        case BusinessType.News:
                            ExecuteNews(baseParam as NewsParam);
                            break;
                        case BusinessType.PressAgency:
                            ExecutePressAgency(baseParam as PressAgencyParam);
                            break;
                        case BusinessType.EmulationAndReward:
                            ExecuteEmulationAndReward(baseParam as EmulationAndRewardParam);
                            break;
                        case BusinessType.ImageLibrary:
                                ExecuteImageLibrary(baseParam as ImageLibraryParam);
                            break;
                        ////case BusinessType.Action:
                        ////    ExecuteAction(baseParam as ActionParam);
                        ////    break;
                        //case BusinessType.Workflow:
                        //    ExecuteApprovalFlow(baseParam as ApprovalFlowParam);
                        //    break;
                        //case BusinessType.Workflow_ApprovalManually:
                        //    ExecuteApprovalManuallyFlow(baseParam as ApprovalManuallyParam);
                        //    break;
                        //case BusinessType.Configuration:
                        //    ExecuteConfiguration(baseParam);
                        //    break;
                        //case BusinessType.Reporting:
                        //    ExecuteReporting(baseParam as SharedComponent.Params.Reporting.ReportingParam);
                        //    break;
                        default:
                            throw new Exception("BusinessType is not supported");
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("MainController.Execute({0}, {1})", baseParam.BusinessType, baseParam.FunctionType);
                    Utils.LogManager.WebLogger.LogDebug(message, ex);
                    throw ex;
                }
            }
        }

        private void ExecuteCommon(CommonParam param)
        {
            switch (param.FunctionType)
            {
                #region Employee

                case FunctionType.Common.SearchShortEmployee:
                    {
                        var biz = new UserBiz();
                        biz.SearchShortEmployee(param);
                        break;
                    }
                case FunctionType.Common.EmployeeSelectorSearch:
                    {
                        var biz = new UserBiz();
                        biz.EmployeeSelectorSearch(param);
                        break;
                    }
                case FunctionType.Common.SearchUserByName:
                    {
                        var biz = new UserBiz();
                        biz.SearchUserByName(param);
                        break;
                    }
                case FunctionType.Common.GetShortUserByID:
                    {
                        var biz = new UserBiz();
                        biz.GetShortUserByID(param);
                        break;
                    }
                case FunctionType.Common.GetListEmployeeByOrganizationID:
                    {
                        var biz = new UserBiz();
                        biz.GetListEmployeeByOrganizationID(param);
                        break;
                    }
                case FunctionType.Common.GetListEmployeeByListStringOrganizationID:
                    {
                        var biz = new UserBiz();
                        biz.GetListEmployeeByListStringOrganizationID(param);
                        break;
                    }
                case FunctionType.Common.NewsSearch:
                    {
                        var bizNews = new NewsBiz();
                        int? type = param.SearchType;
                        if (type == null || type == SMX.TypeSearch.News)
                            bizNews.SearchNews(param);
                        break;
                    }
                case FunctionType.Common.NegativeNewsSearch:
                    {
                        var bizNews = new NewsBiz();
                        int? type = param.SearchType;
                        if (type == null || type == SMX.TypeSearch.NegativeNews)
                            bizNews.SearchNegativeNews(param);
                        break;
                    }
                case FunctionType.Common.PressAgencySearch:
                    {
                        var bizPressAgency = new PressAgency.PressAgencyBiz();
                        int? type = param.SearchType;
                        if (type == null || type == SMX.TypeSearch.PressAgency)
                            bizPressAgency.SearchPressAgency(param);
                        break;
                    }
                case FunctionType.Common.NotificationSearch:
                    {
                        var bizNoti = new Notification.NotificationBiz();
                        int? type = param.SearchType;
                        if (type == null || type == SMX.TypeSearch.Notification)
                            bizNoti.SearchNotification(param);
                        break;
                    }
                case FunctionType.Common.GetCommon:
                    {
                        var biz = new NewsBiz();
                        var bizNoti = new Notification.NotificationBiz();
                        param.ListNotification = bizNoti.GetAllNotification(param.EmployeeId.GetValueOrDefault(0));
                        param.ListTinTuc = biz.Get5TinTuc();
                        param.ListSuVu = biz.Get4TinSuVu();
                        break;
                    }
                case FunctionType.Common.FilterNotification:
                    {
                        var bizNoti = new Notification.NotificationBiz();
                        bizNoti.FilterNotification(param);
                        break;
                    }
                #endregion

                #region Organization
                case FunctionType.Common.OrganizationSelectorSearch:
                    {
                        var biz = new OrganizationBiz();
                        biz.OrganizationSelectorSearch(param);
                        break;
                    }
                case FunctionType.Common.GetZoneIDByOrganizationID:
                    {
                        var biz = new OrganizationBiz();
                        biz.GetZoneIDByOrganizationID(param);
                        break;
                    }
                case FunctionType.Common.GetOrganizationByType:
                    {
                        var biz = new OrganizationBiz();
                        biz.GetOrganizationByType(param);
                        break;
                    }
                case FunctionType.Common.GetOrganizationByID:
                    {
                        var biz = new OrganizationBiz();
                        biz.GetOrganizationByID(param);
                        break;
                    }

                case FunctionType.Common.GetOrganizationByListDirectManagingOrganizationID:
                    {
                        var biz = new OrganizationBiz();
                        biz.GetOrganizationByListDirectManagingOrganizationID(param);
                        break;
                    }

                case FunctionType.Common.GetListOrganizationByProvinceId:
                    {
                        var biz = new OrganizationBiz();
                        biz.GetListItemByProvinceId(param);
                        break;
                    }

                case FunctionType.Common.GetListOrganizationByZoneId:
                    {
                        var biz = new OrganizationBiz();
                        biz.GetListOrganizationByZoneId(param);
                        break;
                    }
                case FunctionType.Common.GetOrganizationByTypeAndCommitteeCode:
                    {
                        var biz = new OrganizationBiz();
                        biz.GetOrganizationByTypeAndCommitteeCode(param);
                        break;
                    }
                case FunctionType.Common.GetOrganizationByParentID:
                    {
                        var biz = new OrganizationBiz();
                        biz.GetOrganizationByParentID(param);
                        break;
                    }
                case FunctionType.Common.SearchBranchByName:
                    {
                        var biz = new OrganizationBiz();
                        biz.SearchBranchByName(param);
                        break;
                    }
                case FunctionType.Common.SearchOrganization:
                    {
                        var biz = new OrganizationBiz();
                        biz.SearchOrganization(param);
                        break;
                    }

                    #endregion
            }
        }
    }
}