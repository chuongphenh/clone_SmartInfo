using SM.SmartInfo.BIZ;
using SM.SmartInfo.SharedComponent.Constants;
using SM.SmartInfo.SharedComponent.Params.SmartInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SM.SmartInfo.UI.SmartInfos.News
{
    /// <summary>
    /// Summary description for TagAJax
    /// </summary>
    public class TagAJax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string key = context.Request.QueryString["searchValue"];
            List<string> searchResults = new List<string>();
            if (string.IsNullOrEmpty(key))
            {
                NewsParam param = new NewsParam(FunctionType.News.GetListHastag);
                MainController.Provider.Execute(param);
                foreach (var item in param.ListHastag)
                {
                    searchResults.Add(item.Name.ToString());
                }

            }
            else
            {
                NewsParam param = new NewsParam(FunctionType.News.GetListHastag);
                param.Hastag = key;
                MainController.Provider.Execute(param);
                foreach (var item in param.ListHastag)
                {
                    searchResults.Add(item.Name.ToString());
                }

            }
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string json = jsSerializer.Serialize(searchResults);
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}