using SM.SmartInfo.SharedComponent.Entities;
using System;
using System.Collections.Generic;

namespace SM.SmartInfo.SharedComponent.Params.SmartInfos
{
    public class ImageLibraryParam : BaseParam
    {
        public ImageLibraryParam(string functionType)
            : base(Constants.BusinessType.ImageLibrary, functionType)
        {
        }
        public int Id { get; set; }
        public string CatalogName { get; set; }
        public int? parentId { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime? CreatedDTG { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDTG { get; set; }
        public int refType { get; set; }
        public ImageCatalog ImageCatalog { get; set; }
        public List<ImageCatalog> listImageCatalog { get; set; }
        public List<adm_Attachment> listAttachment { get; set; }
        public adm_Attachment attachment { get; set; }
        public int CurrentUserId { get; set; }
        public List<int> listDeleteImg { get; set; }
        public List<int> listInsertImg { get; set; }
        public List<int> listPostedYears { get; set; }
        public int? year { get; set; }
        public DateTime? postedDTG { get; set; }
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
    }
}
