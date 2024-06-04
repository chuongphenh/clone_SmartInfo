using System.Collections.Generic;
using SM.SmartInfo.SharedComponent.Entities;

namespace SM.SmartInfo.SharedComponent.Params.SmartInfos
{
    public class NewsParam : BaseParam
    {
        public NewsParam(string functionType)
            : base(Constants.BusinessType.News, functionType)
        {
        }

        public News News { get; set; }

        public News QuickFilterNews { get; set; }
        public bool IsSingleCamp { get; set; }
        public List<News> ListNews { get; set; }
        public bool IsNameExists { get; set; }
        public SingleNews SingleNews { get; set; }
        public HastagManagement HastagManagement { get; set; }
        public List<SingleNews> ListSingleNews { get; set; }
        public int? SingleNewsId { get; set; }

        public int? NegativeNewsID { get; set; }

        public NegativeNews NegativeNews { get; set; }

        public List<NegativeNews> ListNegativeNews { get; set; }

        public CatalogNews CatalogNews { get; set; }

        public int? CatalogNewsID { get; set; }

        public List<CatalogNews> ListCatalogNews { get; set; }
        public List<NegativeNewsResearched> ListNegativeNewsResearched { get; set; }
        public NegativeNewsResearched NegativeNewsResearched { get; set; }

        public List<NewsResearched> ListNewsResearched { get; set; }
        public NewsResearched NewsResearched { get; set; }

        public List<PositiveNews> ListPositiveNews { get; set; }
        public PositiveNews PositiveNews { get; set; }
        public int? PositiveNewsID { get; set; }

        public List<CampaignNews> ListCampaignNews { get; set; }
        public CampaignNews CampaignNews { get; set; }
        public int? CampaignNewsID { get; set; }
        public int? NegativeNewsResearchedID { get; set; }
        public int? NewsResearchedID { get; set; }

        public int? NewsID { get; set; }
        public int? AttachmentID { get; set; }
        public adm_Attachment Attachment { get; set; }
        public List< adm_Attachment>  ListAttachment { get; set; }

        public int? TypeTime { get; set; }

        public bool IsSaveComplete { get; set; }

        public int? attRefID { get; set; }
        public int? attRefType { get; set; }
        public int attCount { get; set; }
        public List<HastagManagement> ListHastag { get; set; }
        public string Hastag { get; set; }
    }
}