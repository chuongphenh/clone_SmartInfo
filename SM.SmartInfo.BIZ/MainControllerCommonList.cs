using System;
using SM.SmartInfo.BIZ.CommonList;
using SM.SmartInfo.SharedComponent.Params;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.CommonList;

namespace SM.SmartInfo.BIZ
{
    public partial class MainController
    {
        private void ExecuteCommonList(BaseParam param)
        {
            string name = param.GetType().Name;
            switch (name)
            {
                case "AttachmentParam":
                    ExecuteCommonList(param as AttachmentParam);
                    break;
                default:
                    throw new Exception(string.Format("Param not supported: {0}", name));
            }
        }

        private void ExecuteCommonList(AttachmentParam param)
        {
            ECMAttachmentFileBiz bizAtt = new ECMAttachmentFileBiz();
            //ExportTemporaryFileBiz bizExport = new ExportTemporaryFileBiz();

            switch (param.FunctionType)
            {
                case FunctionType.CommonList.Attachment.GetAttachmentByID:
                    bizAtt.GetAttachmentByID(param);
                    break;
                case FunctionType.CommonList.Attachment.GetListAttachment:
                    bizAtt.GetListAttachment(param);
                    break;
                case FunctionType.CommonList.Attachment.GetListAttachmentByRefType:
                    bizAtt.GetListAttachmentByRefType(param);
                    break;
                case FunctionType.CommonList.Attachment.Upload:
                    bizAtt.UploadFile(param.adm_Attachment);
                    break;
                case FunctionType.CommonList.Attachment.UploadOther:
                    bizAtt.UploadOtherFile(param.adm_Attachment);
                    break;
                case FunctionType.CommonList.Attachment.Replace:
                    bizAtt.ReplaceExistingFile(param);
                    break;
                case FunctionType.CommonList.Attachment.DeleteDocument:
                    bizAtt.DeleteDocument(param.adm_Attachment);
                    break;
                case FunctionType.CommonList.Attachment.Download:
                    bizAtt.Download(param.adm_Attachment);
                    break;
                case FunctionType.CommonList.Attachment.DownloadAll:
                    break;
                case FunctionType.CommonList.Attachment.ViewDocument:
                    bizAtt.ViewDocument(param.adm_Attachment);
                    break;
                case FunctionType.CommonList.Attachment.GetByteArrayForMobile:
                    bizAtt.GetByteArrayForMobile(param);
                    break;
            }
        }
    }
}