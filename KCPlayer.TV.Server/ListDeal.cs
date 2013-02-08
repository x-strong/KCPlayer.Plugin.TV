using System.Linq;

namespace KCPlayer.TV.Server
{
    public class ListDeal
    {
        public static bool KcPlayerWatchTvAdminModify(string modifykey)
        {
            if (string.IsNullOrEmpty(modifykey)) return false;
            var urlDecode = System.Web.HttpUtility.UrlDecode(modifykey);
            if (urlDecode != null)
            {
                var modifyString = urlDecode.Split('|');
                if (modifyString.Length != 4) return false;
                if (string.IsNullOrEmpty(modifyString[0]) || string.IsNullOrEmpty(modifyString[1]) ||
                    string.IsNullOrEmpty(modifyString[2]) || string.IsNullOrEmpty(modifyString[3])) return false;
                var olditem = new App.FileIO.TvModel { Name = modifyString[0], Url = modifyString[1] };
                var newitem = new App.FileIO.TvModel { Name = modifyString[2], Url = modifyString[3] };

                if (!ListOperation.ModifyItem(olditem, newitem)) return false;

                ListOperation.SaveListFile();
            }
            return true;
        }
        public static bool KcPlayerWatchTvAdminDelete(string deletekey)
        {
            if (string.IsNullOrEmpty(deletekey)) return false;
            var urlDecode = System.Web.HttpUtility.UrlDecode(deletekey);
            if (urlDecode != null)
            {
                var deleteString = urlDecode.Split('|');
                if (deleteString.Length != 2) return false;
                if (string.IsNullOrEmpty(deleteString[0]) || string.IsNullOrEmpty(deleteString[1])) return false;
                var needdel = new App.FileIO.TvModel { Name = deleteString[0], Url = deleteString[1] };

                if (!ListOperation.DelListItem(needdel)) return false;

                ListOperation.SaveListFile();
            }
            return true;
        }

        public static bool KcPlayerWatchTvAdminAdd(string addkey)
        {
            if (string.IsNullOrEmpty(addkey)) return false;
                        var urlDecode = System.Web.HttpUtility.UrlDecode(addkey);
            if (urlDecode != null)
            {
                var newString = urlDecode.Split('|');
                if (newString.Length != 2) return false;
                if (string.IsNullOrEmpty(newString[0]) || string.IsNullOrEmpty(newString[1])) return false;
                var needadd = new App.FileIO.TvModel { Name = newString[0], Url = newString[1] };

                if (App.FileIO.WatchModel.TvLists.IsEmpty() || App.FileIO.WatchModel.TvLists.Count<=0) return false;
                if (ListOperation.HasThisItem(needadd)) return false;
                App.FileIO.WatchModel.TvLists.Add(needadd);

                ListOperation.SaveListFile();
            }
            return true;
        }

    }

    public class ListOperation
    {
        public static string ListSavePath;


        public static bool ModifyItem(App.FileIO.TvModel olditem, App.FileIO.TvModel newitem)
        {
            for (var i = 0; i < App.FileIO.WatchModel.TvLists.Count; i++)
            {
                if (App.FileIO.WatchModel.TvLists[i].Name != olditem.Name && App.FileIO.WatchModel.TvLists[i].Url != olditem.Url) continue;
                App.FileIO.WatchModel.TvLists.RemoveAt(i);
                App.FileIO.WatchModel.TvLists.Add(newitem);
                return true;
            }
            return false;
        }

        public static void SaveListFile()
        {
            var resultString = App.FileIO.WatchModel.TvLists.Select(tvList => tvList.Name + "@" + tvList.Url)
                                      .Aggregate("", (current, ssresult) => current + ssresult + "|");
            if (string.IsNullOrEmpty(resultString)) return;
            App.FileIO.ReadWriteHelper.EncryptFileFromString(ListSavePath, resultString, App.FileIO.WatchModel.OpenGift, App.FileIO.WatchModel.OpenFloa);
        }

        public static bool DelListItem(App.FileIO.TvModel item)
        {
            for (var i = 0; i < App.FileIO.WatchModel.TvLists.Count; i++)
            {
                if (App.FileIO.WatchModel.TvLists[i].Name != item.Name) continue;
                if (App.FileIO.WatchModel.TvLists[i].Url != item.Url) continue;
                App.FileIO.WatchModel.TvLists.RemoveAt(i);
                return true;
            }
            return false;
        }

        public static bool HasThisItem(App.FileIO.TvModel item)
        {
            return App.FileIO.WatchModel.TvLists.Where(tvList => tvList.Name == item.Name).Any(tvList => tvList.Url == item.Url);
        }
    }
}