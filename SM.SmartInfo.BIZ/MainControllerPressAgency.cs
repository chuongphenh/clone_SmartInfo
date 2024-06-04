using SM.SmartInfo.BIZ.PressAgency;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private void ExecutePressAgency(PressAgencyParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.PressAgency.SetupFormDefault:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SetupFormDefault(param);
                        break;
                    }
                case FunctionType.PressAgency.GetItemsForView:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgency(param);
                        break;
                    }
                case FunctionType.PressAgency.SearchItemsForView:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SearchItemsForView(param);
                        break;
                    }
                case FunctionType.PressAgency.LoadDataDisplay:
                    {
                        var biz = new PressAgencyBiz();
                        biz.LoadDataDisplay(param);
                        break;
                    }
                case FunctionType.PressAgency.SavePressAgency:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SavePressAgency(param);
                        break;
                    }
                case FunctionType.PressAgency.DeletePressAgency:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeletePressAgency(param);
                        break;
                    }

                case FunctionType.PressAgency.GetPressAgencySelector:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SearchPressAgencySelector(param);
                        break;
                    }
                case FunctionType.PressAgency.GetListAgencyType:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListAgencyType(param);
                        break;
                    }
                case FunctionType.PressAgency.GetListPressAgencyByType:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyByType(param);
                        break;
                    }
                case FunctionType.PressAgency.AddNewPressAgency:
                    {
                        var biz = new PressAgencyBiz();
                        biz.AddNewPressAgency(param);
                        break;
                    }
                case FunctionType.PressAgency.GetListSharedUser:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListSharedUser(param);
                        break;
                    }
                case FunctionType.PressAgency.DeleteShare:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeleteShare(param);
                        break;
                    }


                #region Press Agency HR
                case FunctionType.PressAgency.ImportOrUpdateListPressAgencyHRFromExcel:
                    {
                        var biz = new PressAgencyBiz();
                        biz.ImportOrUpdateListPressAgencyHRFromExcel(param);
                        break;
                    }
                case FunctionType.PressAgency.AddNewAgencyType:
                    {
                        var biz = new PressAgencyBiz();
                        biz.AddNewAgencyType(param);
                        break;
                    }
                case FunctionType.PressAgency.SavePressAgencyHR:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SavePressAgencyHR(param);
                        break;
                    }

                case FunctionType.PressAgency.DeletePressAgencyHR:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeletePressAgencyHR(param);
                        break;
                    }
                case FunctionType.PressAgency.GetPressAgencyHR_ByID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetPressAgencyHR_ByID(param);
                        break;
                    }
                case FunctionType.PressAgency.GetListPressAgencyHR_ByPressAgencyID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHR_ByPressAgencyID(param);
                        break;
                    }
                case FunctionType.PressAgency.GetListPressAgencyHR:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHR(param);
                        break;
                    }
                case FunctionType.PressAgency.GetListPressAgencyHR_ByFilter:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHR_ByFilter(param);
                        break;
                    }
                case FunctionType.PressAgency.GetListPressAgencyHR_ByFilterNoPaging:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHR_ByFilterNoPaging(param);
                        break;
                    }
                case FunctionType.PressAgency.GetPressAgencyByName:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetPressAgencyByName(param);
                        break;
                    }
                case FunctionType.PressAgency.GetPressAgencyHRByName:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetPressAgencyHRByName(param);
                        break;
                    }

                case FunctionType.PressAgency.GetListPressAgencyHRByName:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHRByName(param);
                        break;
                    }

                case FunctionType.PressAgency.GetPressAgencyTypeByCode:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetPressAgencyTypeByCode(param);
                        break;
                    }
                    


                #region Press Agency HR History
                case FunctionType.PressAgency.GetListPressAgencyHRHistory_ByPressAgencyHRID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHRHistory_ByPressAgencyHRID(param);
                        break;
                    }
                case FunctionType.PressAgency.SavePressAgencyHRHistory:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SavePressAgencyHRHistory(param);
                        break;
                    }
                case FunctionType.PressAgency.DeletePressAgencyHRHistory:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeletePressAgencyHRHistory(param);
                        break;
                    }
                #endregion

                #region Press Agency HR Relatives
                case FunctionType.PressAgency.GetListPressAgencyHRRelatives_ByPressAgencyHRID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHRRelatives_ByPressAgencyHRID(param);
                        break;
                    }
                case FunctionType.PressAgency.SavePressAgencyHRRelatives:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SavePressAgencyHRRelatives(param);
                        break;
                    }
                case FunctionType.PressAgency.DeletePressAgencyHRRelatives:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeletePressAgencyHRRelatives(param);
                        break;
                    }
                #endregion

                #region Press Agency HR Alert
                case FunctionType.PressAgency.GetListPressAgencyHRAlert_ByPressAgencyHRID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHRAlert_ByPressAgencyHRID(param);
                        break;
                    }
                case FunctionType.PressAgency.SavePressAgencyHRAlert:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SavePressAgencyHRAlert(param);
                        break;
                    }
                case FunctionType.PressAgency.DeletePressAgencyHRAlert:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeletePressAgencyHRAlert(param);
                        break;
                    }
                case FunctionType.PressAgency.DeletePressAgencyHRAlertByPressAgenctyHrID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeletePressAgencyHRAlertByPressAgenctyHrID(param);
                        break;
                    }
                    
                #endregion
                #endregion

                #region Press Agency History
                case FunctionType.PressAgency.GetListPressAgencyHistory_ByPressAgencyID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyHistory_ByPressAgencyID(param);
                        break;
                    }
                case FunctionType.PressAgency.SavePressAgencyHistory:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SavePressAgencyHistory(param);
                        break;
                    }
                case FunctionType.PressAgency.DeletePressAgencyHistory:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeletePressAgencyHistory(param);
                        break;
                    }
                #endregion

                #region Press Agency Meeting
                case FunctionType.PressAgency.GetListPressAgencyMeeting_ByPressAgencyID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListPressAgencyMeeting_ByPressAgencyID(param);
                        break;
                    }
                case FunctionType.PressAgency.SavePressAgencyMeeting:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SavePressAgencyMeeting(param);
                        break;
                    }
                case FunctionType.PressAgency.DeletePressAgencyMeeting:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeletePressAgencyMeeting(param);
                        break;
                    }
                #endregion

                #region Relations Press Agency
                case FunctionType.PressAgency.GetListRelationsPressAgency_ByPressAgencyID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListRelationsPressAgency_ByPressAgencyID(param);
                        break;
                    }
                case FunctionType.PressAgency.SaveRelationsPressAgency:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SaveRelationsPressAgency(param);
                        break;
                    }
                case FunctionType.PressAgency.DeleteRelationsPressAgency:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeleteRelationsPressAgency(param);
                        break;
                    }
                #endregion

                #region Relationship With MB
                case FunctionType.PressAgency.GetListRelationshipWithMB_ByPressAgencyID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListRelationshipWithMB_ByPressAgencyID(param);
                        break;
                    }
                case FunctionType.PressAgency.SaveRelationshipWithMB:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SaveRelationshipWithMB(param);
                        break;
                    }
                case FunctionType.PressAgency.DeleteRelationshipWithMB:
                    {
                        var biz = new PressAgencyBiz();
                        biz.DeleteRelationshipWithMB(param);
                        break;
                    }
                #endregion

                case FunctionType.PressAgency.GetListOtherImage_ByPressAgencyID:
                    {
                        var biz = new PressAgencyBiz();
                        biz.GetListOtherImage_ByPressAgencyID(param);
                        break;
                    }
                case FunctionType.PressAgency.LoadDataOtherImage_PressAgencyHR:
                    {
                        var biz = new PressAgencyBiz();
                        biz.LoadDataOtherImage_PressAgencyHR(param);
                        break;
                    }
                case FunctionType.PressAgency.SearchItemsForViewPressAgencyHR:
                    {
                        var biz = new PressAgencyBiz();
                        biz.SearchItemsForViewPressAgencyHR(param);
                        break;
                    }
            }
        }
    }
}