using System.Web;

namespace KCPlayer.TV.Server
{
    /// <summary>
    /// TVList 的摘要说明
    /// </summary>
    public class TvList : IHttpHandler
    {
        public TvList()
        {
            // 获取Server Data Path
            ListOperation.ListSavePath = HttpContext.Current.Server.MapPath(App.FileIO.WatchModel.LocalFile);
            // 生成模型
            App.FileIO.WatchModel.MakeList(System.IO.File.ReadAllText(ListOperation.ListSavePath));
        }

        public void ProcessRequest(HttpContext context)
        {
            // 检查请求来源
            var reKey = context.Request.Headers["KCPlayer"];
            if (reKey.IsNullOrEmptyOrSpace()) return;
            context.Response.ContentType = "text/plain";
            // 解析请求类型
            switch (reKey)
            {
                // 获取所有公开列表数据
                case @"KCPlayer.WatchTV.User.Get":
                    {
                        context.Response.Write(System.IO.File.ReadAllText(ListOperation.ListSavePath,
                                                                          System.Text.Encoding.UTF8));
                    }
                    break;
                // 添加新的电视项目
                case @"KCPlayer.WatchTV.Admin.Add":
                    {
                        context.Response.Write(ListDeal.KcPlayerWatchTvAdminAdd(context.Request["Add"])
                                                   ? @"KCPlayer.WatchTV.Add.OK"
                                                   : @"KCPlayer.WatchTV.Add.Failure");
                    }
                    break;
                // 删除现有的电视项目
                case @"KCPlayer.WatchTV.Admin.Delete":
                    {
                        context.Response.Write(ListDeal.KcPlayerWatchTvAdminDelete(context.Request["Delete"])
                                                   ? @"KCPlayer.WatchTV.Delete.OK"
                                                   : @"KCPlayer.WatchTV.Delete.Failure");
                    }
                    break;
                // 修改现有的电视项目
                case @"KCPlayer.WatchTV.Admin.Modify":
                    {
                        context.Response.Write(ListDeal.KcPlayerWatchTvAdminModify(context.Request["Modify"])
                                                   ? @"KCPlayer.WatchTV.Modify.OK"
                                                   : @"KCPlayer.WatchTV.Modify.Failure");
                    }
                    break;
                default:
                    {
                        context.Response.Write(@"请求成功,没有数据可返回");
                    }
                    break;
            }
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