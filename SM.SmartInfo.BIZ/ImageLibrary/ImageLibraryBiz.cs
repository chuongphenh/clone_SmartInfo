using DocumentFormat.OpenXml.Office2010.ExcelAc;
using SM.SmartInfo.BIZ.CommonList;
using SM.SmartInfo.DAO.ImageLibrary;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Entities;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using SoftMart.Kernel.Exceptions;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.BIZ.ImageLibrary
{
    class ImageLibraryBiz : BizBase
    {
        private ImageLibraryDao _dao = new ImageLibraryDao();

        public void PermanentlyDelete(ImageLibraryParam param)
        {
            _dao.PermanentlyDelete(param);
        }

        public void RevertDeletedItem(ImageLibraryParam param)
        {
            _dao.RevertDeletedItem(param);
        }

        public void ViewDeletedItem(ImageLibraryParam param)
        {
            _dao.ViewDeletedItem(param);
        }

        public void GetRefTypeById(ImageLibraryParam param)
        {
            _dao.GetRefTypeById(param);
        }

        public void GetRootImageByFilter(ImageLibraryParam param)
        {
            _dao.GetRootImageByFilter(param);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();

            for (int i = 0; i < param.listAttachment.Count; i++)
            {
                try
                {
                    param.listAttachment[i] = bizECM.GetAttachmentByRefIDAndRefTypeForImageLibrary(param.listAttachment[i]);
                }
                catch
                {
                    continue;
                }
            }
        }

        public void AddImageToNode(ImageLibraryParam param)
        {
            _dao.AddImageToNode(param);
        }

        public void GetListEditableNode(ImageLibraryParam param)
        {
            _dao.GetListEditableNode(param);
        }

        public void DeleteSelectedImg(ImageLibraryParam param)
        {
            _dao.DeleteSelectedImg(param);
        }
        public void DeleteSelectedImgOriginal(ImageLibraryParam param)
        {
            _dao.DeleteSelectedImgOriginal(param);
        }

        public void GetImageByFilter(ImageLibraryParam param)
        {
            _dao.GetImageByFilter(param);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();

            for (int i = 0; i < param.listAttachment.Count; i++)
            {
                try
                {
                    param.listAttachment[i] = bizECM.GetAttachmentByRefIDAndRefTypeForImageLibrary(param.listAttachment[i]);
                }
                catch
                {
                    continue;
                }
            }
        }

        public void AddNewNode(ImageLibraryParam param)
        {
            _dao.AddNewNode(param);
        }

        public void GetListImageCatalog(ImageLibraryParam param)
        {
            _dao.GetListImageCatalog(param);
        }
        public void DeleteSelectedNode(ImageLibraryParam param)
        {
            _dao.DeleteSelectedNode(param);
        }
        public void EditNoteName(ImageLibraryParam param)
        {
            _dao.EditNoteName(param);
        }
        public void GetImageCatalogById(ImageLibraryParam param)
        {
            _dao.GetImageCatalogById(param);
        }
        public void GetListImage(ImageLibraryParam param)
        {
            _dao.GetListImage(param);

            ECMAttachmentFileBiz bizECM = new ECMAttachmentFileBiz();

            //var listAtt = new List<adm_Attachment>();

            for (int i = 0; i < param.listAttachment.Count; i++)
            {
                try
                {
                    param.listAttachment[i] = bizECM.GetAttachmentByRefIDAndRefTypeForImageLibrary(param.listAttachment[i]);
                }
                catch
                {
                    continue;
                }
            }
        }
        public void GetListPostedYears(ImageLibraryParam param)
        {
            _dao.GetListPostedYears(param);
        }
    }
}
