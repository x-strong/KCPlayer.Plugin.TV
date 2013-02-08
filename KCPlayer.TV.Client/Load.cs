namespace KCPlayer.TV.Client
{
    public class Load
    {
        #region 启动加载

        private static string ListWebPath { get; set; }

        public static void Start()
        {
            App.Style.Helper.Ui.Load += Ui_Load;
        }

        private static void Ui_Load(object sender, System.EventArgs e)
        {
            App.Style.Helper.Start();
            App.Guard.OptimizeHelper.Start();
            ListWebPath = App.Base.Global.IsLocal
                              ? @"http://localhost:25171/TvList.ashx"
                              : @"http://apk.kcplayer.com/TvList.ashx";
            App.Style.Helper.Ui.Controls.Add(new App.Style.NavPal(5, false));

            RefreshListItem();
        }

        #endregion

        #region 点击事件

        /// <summary>
        /// 菜单按钮点击事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newitemname"></param>
        /// <param name="newitemurl"></param>
        public static void HeaderBarMenuClick(string key, string newitemname, string newitemurl)
        {
            #region 菜单按钮点击事件

            switch (key)
            {
                case @"+":
                    {
                        AddListItem(newitemname, newitemurl);
                    }
                    break;
            }

            #endregion
        }

        #endregion

        #region 修改项目

        public static void ModifyListItem(string oldname, string oldurl, string newname, string newurl)
        {
            if (oldname.IsNullOrEmptyOrSpace()) return;
            if (oldurl.IsNullOrEmptyOrSpace()) return;
            if (newname.IsNullOrEmptyOrSpace()) return;
            if (newurl.IsNullOrEmptyOrSpace()) return;

            var wc = new System.Net.WebClient();
            wc.Headers.Add(@"KCPlayer", @"KCPlayer.WatchTV.Admin.Modify");
            wc.Proxy = null;
            wc.DownloadStringAsync(
                new System.Uri(string.Format("{0}?Modify={1}|{2}|{3}|{4}", ListWebPath, oldname, oldurl, newname,
                                             newurl)));
            wc.DownloadStringCompleted += wc_mod_DownloadStringCompleted;
        }

        private static void wc_mod_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result == @"KCPlayer.WatchTV.Modify.OK")
                {
                    RefreshListItem();
                }
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        #endregion

        #region 删除项目

        public static void DeleteListItem(string itemname, string itemurl)
        {
            if (itemname.IsNullOrEmptyOrSpace()) return;
            if (itemurl.IsNullOrEmptyOrSpace()) return;

            var wc = new System.Net.WebClient();
            wc.Headers.Add(@"KCPlayer", @"KCPlayer.WatchTV.Admin.Delete");
            wc.Proxy = null;
            wc.DownloadStringAsync(
                new System.Uri(string.Format("{0}?Delete={1}|{2}", ListWebPath, itemname, itemurl)));
            wc.DownloadStringCompleted += wc_Del_DownloadStringCompleted;
        }

        private static void wc_Del_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result == @"KCPlayer.WatchTV.Delete.OK")
                {
                    RefreshListItem();
                }
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        #endregion

        #region 添加项目

        public static void AddListItem(string newitemname, string newitemurl)
        {
            if (newitemname.IsNullOrEmptyOrSpace()) return;
            if (newitemurl.IsNullOrEmptyOrSpace()) return;

            var wc = new System.Net.WebClient();
            wc.Headers.Add(@"KCPlayer", @"KCPlayer.WatchTV.Admin.Add");
            wc.Proxy = null;
            wc.DownloadStringAsync(
                new System.Uri(string.Format("{0}?Add={1}|{2}", ListWebPath, newitemname, newitemurl)));
            wc.DownloadStringCompleted += wc_add_DownloadStringCompleted;
        }

        private static void wc_add_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result == @"KCPlayer.WatchTV.Add.OK")
                {
                    RefreshListItem();
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        #endregion

        #region 刷新列表

        public static void RefreshListItem()
        {
            var wc = new System.Net.WebClient();
            wc.Headers.Add(@"KCPlayer", @"KCPlayer.WatchTV.User.Get");
            wc.Proxy = null;
            wc.Encoding = System.Text.Encoding.UTF8;
            wc.DownloadStringAsync(new System.Uri(ListWebPath));
            wc.DownloadStringCompleted += wc_DownloadStringCompleted;
        }

        private static void wc_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            try
            {
                App.FileIO.WatchModel.MakeList(e.Result.ToSafeValue());
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        #endregion

        #region 切换预览

        public static void ViewThisTv(object tv)
        {
        }

        #endregion
    }
}