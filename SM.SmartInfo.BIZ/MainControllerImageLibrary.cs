using SM.SmartInfo.BIZ.ImageLibrary;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private void ExecuteImageLibrary(ImageLibraryParam param)
        {
            switch (param.FunctionType)
            {
                case FunctionType.ImageLibrary.AddNewNode:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.AddNewNode(param);
                        break;
                    }
                case FunctionType.ImageLibrary.GetRefTypeById:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.GetRefTypeById(param);
                        break;
                    }
                case FunctionType.ImageLibrary.GetListImageCatalog:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.GetListImageCatalog(param);
                        break;
                    }
                case FunctionType.ImageLibrary.DeleteSelectedNode:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.DeleteSelectedNode(param);
                        break;
                    }
                case FunctionType.ImageLibrary.EditNoteName:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.EditNoteName(param);
                        break;
                    }
                case FunctionType.ImageLibrary.GetImageCatalogById:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.GetImageCatalogById(param);
                        break;
                    }
                case FunctionType.ImageLibrary.GetListImage:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.GetListImage(param);
                        break;
                    }
                case FunctionType.ImageLibrary.GetImageByFilter:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.GetImageByFilter(param);
                        break;
                    }
                case FunctionType.ImageLibrary.DeleteSelectedImg:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.DeleteSelectedImg(param);
                        break;
                    }
                case FunctionType.ImageLibrary.DeleteSelectedImgOriginal:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.DeleteSelectedImgOriginal(param);
                        break;
                    }
                case FunctionType.ImageLibrary.GetListEditableNode:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.GetListEditableNode(param);
                        break;
                    }
                case FunctionType.ImageLibrary.AddImageToNode:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.AddImageToNode(param);
                        break;
                    }
                case FunctionType.ImageLibrary.GetRootImageByFilter:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.GetRootImageByFilter(param);
                        break;
                    }
                case FunctionType.ImageLibrary.ViewDeletedItem:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.ViewDeletedItem(param);
                        break;
                    }
                case FunctionType.ImageLibrary.RevertDeletedItem:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.RevertDeletedItem(param);
                        break;
                    }
                case FunctionType.ImageLibrary.PermanentlyDelete:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.PermanentlyDelete(param);
                        break;
                    }
                case FunctionType.ImageLibrary.GetListPostedYears:
                    {
                        var biz = new ImageLibraryBiz();
                        biz.GetListPostedYears(param);
                        break;
                    }

            }
        }
    }
}
