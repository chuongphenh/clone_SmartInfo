using System.Linq;
using System.Web.UI;
using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;

namespace SM.SmartInfo.UI.UserControls.Default
{
    public partial class ChartStatisticUC : UserControl
    {
        #region Public Methods

        public void LoadData()
        {
            NewsParam param = new NewsParam(FunctionType.NegativeNew.GetAllNegativeNews);
            MainController.Provider.Execute(param);

            var lstNegativeNews = param.ListNews;

            hidListBackgroundColor.Value = string.Format("[\"{0}\", \"{1}\"]", "#597EF7", "#FF4D4F");
            hidListData.Value = string.Format("[{0}, {1}]", lstNegativeNews.Count(x => x.NegativeType == SMX.News.NegativeNews.ChuaPhatSinh), lstNegativeNews.Count(x => x.NegativeType == SMX.News.NegativeNews.DaPhatSinh));
        }

        #endregion
    }
}