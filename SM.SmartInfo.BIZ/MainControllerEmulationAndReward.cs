using SM.SmartInfo.BIZ.EmulationAndRewards;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private void ExecuteEmulationAndReward(EmulationAndRewardParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.EmulationAndReward.SetupFormDisplay:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.SetupFormDisplay(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.BuildTreeListEmulationAndRewards:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.BuildTreeListEmulationAndRewards(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListEmulationAndRewardByFilter:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListEmulationAndRewardByFilter(param);
                        break;
                    }

                case FunctionType.EmulationAndReward.SaveEmulationAndRewardSubject:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.SaveEmulationAndRewardSubject(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.DeleteEmulationAndRewardSubject:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.DeleteEmulationAndRewardSubject(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListEmulationAndRewardHistory:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListEmulationAndRewardHistory(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.AddNewEmulationAndReward:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.AddNewEmulationAndReward(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.AddNewAwardingCatalog:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.AddNewAwardingCatalog(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.EditAwardingCatalog:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.EditAwardingCatalog(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListAwardingCatalog:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListAwardingCatalog(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.DeleteSelectedAwardingCatalog:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.DeleteSelectedAwardingCatalog(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetAwardingCatalogById:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetAwardingCatalogById(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListAwardingLevel:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListAwardingLevel(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.CreateAwardingLevel:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.CreateAwardingLevel(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetAwardingLevelById:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetAwardingLevelById(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.EditAwardingLevel:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.EditAwardingLevel(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.DeleteSelectedAwardingLevel:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.DeleteSelectedAwardingLevel(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.CreateAwardingPeriod:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.CreateAwardingPeriod(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListAwardingPeriod:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListAwardingPeriod(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetAwardingPeriodById:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetAwardingPeriodById(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.DeleteSelectedAwardingPeriod:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.DeleteSelectedAwardingPeriod(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.EditAwardingPeriod:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.EditAwardingPeriod(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListAwardingType:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListAwardingType(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.CreateAwardingType:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.CreateAwardingType(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.EditAwardingType:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.EditAwardingType(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetAwardingTypeById:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetAwardingTypeById(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.DeleteSelectedAwardingType:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.DeleteSelectedAwardingType(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetAwardingTypeCount:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetAwardingTypeCount(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListAwardingPeriodNoPaging:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListAwardingPeriodNoPaging(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListAwardingLevelNoPaging:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListAwardingLevelNoPaging(param);
                        break;
                    }
                case FunctionType.EmulationAndReward.GetListAwardingTypeNoPaging:
                    {
                        var biz = new EmulationAndRewardBiz();
                        biz.GetListAwardingTypeNoPaging(param);
                        break;
                    }
            }
        }
    }
}