using SM.SmartInfo.BIZ.NegativeNew;
using SM.SmartInfo.BIZ.CatalogNews;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SM.SmartInfo.BIZ.News;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private void ExecuteNews(NewsParam param)
        {
            switch (param.FunctionType)
            {
                #region su vu

                case FunctionType.NegativeNew.GetItemsForView:
                    {
                        var biz = new NegativeNewBiz();
                        biz.GetListNews(param);
                        break;
                    }
                case FunctionType.NegativeNew.SearchNegativeNews:
                    {
                        var biz = new NegativeNewBiz();
                        biz.SearchNegativeNews(param);
                        break;
                    }
                case FunctionType.NegativeNew.AddNewItem:
                    {
                        var biz = new NegativeNewBiz();
                        biz.AddNewData(param);
                        break;
                    }
                case FunctionType.NegativeNew.UpdateItem:
                    {
                        var biz = new NegativeNewBiz();
                        biz.UpdateData(param);
                        break;
                    }
                case FunctionType.NegativeNew.UpdateStatusHoanThanh:
                    {
                        var biz = new NegativeNewBiz();
                        biz.UpdateStatusHoanThanh(param);
                        break;
                    }
                case FunctionType.NegativeNew.GetAllNegativeNews:
                    {
                        var biz = new NegativeNewBiz();
                        biz.GetAllNegativeNews(param);
                        break;
                    }
                case FunctionType.NegativeNew.LoadDataDisplay:
                    {
                        var biz = new NegativeNewBiz();
                        biz.LoadDataDisplay(param);
                        break;
                    }
                case FunctionType.NegativeNew.LoadDataImages:
                    {
                        var biz = new NegativeNewBiz();
                        biz.LoadDataImages(param);
                        break;
                    }
                case FunctionType.NegativeNew.LoadDataImagesDetail:
                    {
                        var biz = new NegativeNewBiz();
                        biz.LoadDataImagesDetail(param);
                        break;
                    }
                case FunctionType.NegativeNew.GetItemsNegativeNews:
                    {
                        var biz = new NegativeNewBiz();
                        biz.GetListNegativeNews(param);
                        break;
                    }    
                case FunctionType.NegativeNew.DeleteItem:
                    {
                        var biz = new NegativeNewBiz();
                        biz.DeleteNewsAndNegativeNews(param);
                        break;
                    }
                case FunctionType.NegativeNew.DeleteNewNegativeNews:
                    {
                        var biz = new NegativeNewBiz();
                        biz.DeleteNegativeNewsByNegativeNewsID(param);
                        break;
                    }

                #region Tin tuc

                case FunctionType.News.GetNewsForView:
                    {
                        var biz = new NewsBiz();
                        biz.GetListNews(param);
                        break;
                    }
                case FunctionType.News.SearchNewsForView:
                    {
                        var biz = new NewsBiz();
                        biz.SearchNewsForView(param);
                        break;
                    }
                case FunctionType.News.IsNameExists:
                    {
                        var biz = new NewsBiz();
                        biz.IsNameExists(param);
                        break;
                    }
                case FunctionType.News.IsSingleCamp:
                    {
                        var biz = new NewsBiz();
                        biz.IsSingleCamp(param);
                        break;
                    }
                case FunctionType.News.SetIsSingleCamp:
                    {
                        var biz = new NewsBiz();
                        biz.SetIsSingleCamp(param);
                        break;
                    }
                case FunctionType.News.AddNewNews:
                    {
                        var biz = new NewsBiz();
                        biz.AddNewData(param);
                        break;
                    }
                case FunctionType.News.UpdateNews:
                    {
                        var biz = new NewsBiz();
                        biz.UpdateData(param);
                        break;
                    }
                case FunctionType.News.LoadDataNews:
                    {
                        var biz = new NewsBiz();
                        biz.LoadDataDisplay(param);
                        break;
                    }
                case FunctionType.News.LoadDataImagesNews:
                    {
                        var biz = new NewsBiz();
                        biz.LoadDataImagesNews(param);
                        break;
                    }
                case FunctionType.News.LoadDataImagesPositiveNews:
                    {
                        var biz = new NewsBiz();
                        biz.LoadDataImagesPositiveNews(param);
                        break;
                    }
                case FunctionType.News.DeleteNewsAndPositiveNewsAndCampaignNews:
                    {
                        var biz = new NewsBiz();
                        biz.DeleteNewsAndPositiveNewsAndCampaignNews(param);
                        break;
                    }
                case FunctionType.News.BuildTreeListNews:
                    {
                        var biz = new NewsBiz();
                        biz.BuildTreeListNews(param);
                        break;
                    }
                case FunctionType.News.GetListSingleNewsByNewsID:
                    {
                        var biz = new NewsBiz();
                        biz.GetListSingleNewsByNewsID(param);
                        break;
                    }
                case FunctionType.News.GetListSingleNewsByNewsIDAndCampaignID:
                    {
                        var biz = new NewsBiz();
                        biz.GetListSingleNewsByNewsIDAndCampaignID(param);
                        break;
                    }
                
                case FunctionType.News.SaveSingleNews:
                    {
                        var biz = new NewsBiz();
                        biz.SaveSingleNews(param);
                        break;
                    }
                case FunctionType.News.DeleteSingleNews:
                    {
                        var biz = new NewsBiz();
                        biz.DeleteSingleNews(param);
                        break;
                    }
                case FunctionType.News.LoadDataImagesSingleNews:
                    {
                        var biz = new NewsBiz();
                        biz.LoadDataImagesSingleNews(param);
                        break;
                    }
                case FunctionType.News.LoadDataDocumentCampaignNews:
                    {
                        var biz = new NewsBiz();
                        biz.LoadDataDocumentCampaignNews(param);
                        break;
                    }
                case FunctionType.News.AddNewCampaignNews:
                    {
                        var biz = new NewsBiz();
                        biz.AddNewCampaignData(param);
                        break;
                    }
                case FunctionType.News.AddDocumentCampaignNews:
                    {
                        var biz = new NewsBiz();
                        biz.AddDocumentCampaignNews(param);
                        break;
                    }
                case FunctionType.News.GetListHastag:
                    {
                        var biz = new NewsBiz();
                        biz.GetListHastag(param);
                        break;
                    }
                case FunctionType.News.CreateHastag:
                    {
                        var biz = new NewsBiz();
                        biz.CreateHastag(param);
                        break;
                    }
                    
                #endregion

                #endregion NegativeNews
                case FunctionType.NegativeNew.LoadDataNegativeNews:
                    {
                        var biz = new NegativeNewBiz();
                        biz.LoadDataNegativeNews(param);
                        break;
                    }

                case FunctionType.NegativeNew.UpdateNegativeNews:
                    {
                        var biz = new NegativeNewBiz();
                        biz.UpdateDataNegativeNews(param);
                        break;
                    }
                case FunctionType.NegativeNew.FinishNegativeNews:
                    {
                        var biz = new NegativeNewBiz();
                        biz.FinishNegativeNews(param);
                        break;
                    }
                case FunctionType.NegativeNew.AddNewNegativeNews:
                    {
                        var biz = new NegativeNewBiz();
                        biz.AddNewsDataNegativeNews(param);
                        break;
                    }
                #region

                #endregion

                #region CatalogNews
                case FunctionType.CatalogNews.GetCatalogNewsTreeData:
                    {
                        var biz = new CatalogNewsBiz();
                        biz.GetCatalogNewsTreeData(param);
                        break;
                    }

                case FunctionType.CatalogNews.LoadDataDisplayCatalogNews:
                    {
                        var biz = new CatalogNewsBiz();
                        biz.LoadDataDisplay(param);
                        break;
                    }
                case FunctionType.CatalogNews.DeleteCatalogNews:
                    {
                        var biz = new CatalogNewsBiz();
                        biz.DeleteItems(param);
                        break;
                    }
                case FunctionType.CatalogNews.AddNewCatalogNews:
                    {
                        var biz = new CatalogNewsBiz();
                        biz.AddNewItem(param);
                        break;
                    }
                case FunctionType.CatalogNews.UpdateCatalogNews:
                    {
                        var biz = new CatalogNewsBiz();
                        biz.UpdateItem(param);
                        break;
                    }
                case FunctionType.CatalogNews.LoadDataEditCatalogNews:
                    {
                        var biz = new CatalogNewsBiz();
                        biz.LoadDataDisplay(param);
                        break;
                    }

                #endregion
                #region NegativeNewsResearched
                case FunctionType.NegativeNewsResearched.SaveNegativeNewsResearched:
                    {
                        var biz = new NegativeNewBiz();
                        biz.SaveNegativeNewsResearched(param);
                        break;
                    }
                case FunctionType.NegativeNewsResearched.GetListNegativeNewsResearchedByNegativeNewsID:
                    {
                        var biz = new NegativeNewBiz();
                        biz.GetListNegativeNewsResearched_ByNegativeNewsID(param);
                        break;
                    }
                case FunctionType.NegativeNewsResearched.DeleteNegativeNewsResearched:
                    {
                        var biz = new NegativeNewBiz();
                        biz.DeleteNegativeNewsResearched(param);
                        break;
                    }
                #endregion
                #region NewsResearched
                case FunctionType.NewsResearched.SaveNewsResearched:
                    {
                        var biz = new NegativeNewBiz();
                        biz.SaveNewsResearched(param);
                        break;
                    }
                case FunctionType.NewsResearched.GetListNewsResearchedByNewsID:
                    {
                        var biz = new NegativeNewBiz();
                        biz.GetListNewsResearched_ByNewsID(param);
                        break;
                    }
                case FunctionType.NewsResearched.DeleteNewsResearched:
                    {
                        var biz = new NegativeNewBiz();
                        biz.DeleteNewsResearched(param);
                        break;
                    }
                #endregion

                #region NegativeNewsResearched
                case FunctionType.PositiveNews.SavePositiveNews:
                    {
                        var biz = new NewsBiz();
                        biz.SavePositiveNews(param);
                        break;
                    }
                case FunctionType.PositiveNews.PrepareDataCampaign:
                    {
                        var biz = new NewsBiz();
                        biz.PrepareDataCampaign(param);
                        break;
                    }
                case FunctionType.PositiveNews.GetListPositiveNewsByNewsID:
                    {
                        var biz = new NewsBiz();
                        biz.GetListPositiveNews_ByNewsID(param);
                        break;
                    }
                case FunctionType.PositiveNews.DeletePositiveNews:
                    {
                        var biz = new NewsBiz();
                        biz.DeleteNegativeNewsResearched(param);
                        break;
                    }
                #endregion

                #region CampaignNews
                case FunctionType.CampaignNews.SaveCampaignNews:
                    {
                        var biz = new NewsBiz();
                        biz.SaveCampaignNews(param);
                        break;
                    }
                case FunctionType.CampaignNews.GetListCampaignNewsByNewsID:
                    {
                        var biz = new NewsBiz();
                        biz.GetListCampaignNews_ByNewsID(param);
                        break;
                    }
                case FunctionType.CampaignNews.DeleteCampaignNews:
                    {
                        var biz = new NewsBiz();
                        biz.DeleteCampaignNews(param);
                        break;
                    }
                case FunctionType.CampaignNews.getCountUploadedDocument:
                    {
                        var biz = new NewsBiz();
                        biz.getCountUploadedDocument(param);
                        break;
                    }
                case FunctionType.CampaignNews.getAtt:
                    {
                        var biz = new NewsBiz();
                        biz.getAtt(param);
                        break;
                    }
                    #endregion
            }
        }
    }
}