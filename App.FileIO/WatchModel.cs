using System.Linq;

namespace App.FileIO
{
    public class WatchModel
    {
        public const string Companyname = @"KCPlayer";
        public const string OpenGift = @"KCPlayer.WatchTV";
        public const string OpenFloa = @"KCPlayer.Admin";
        public const string LocalFile = @"WatchTV";
        public static System.Collections.Generic.List<TvModel> TvLists { get; set; }
        private const char ItemLine = ';';
        private const char ParaLine = ',';

        public static void MakeList(string datastr)
        {
            // Decrypt Data
            datastr = ReadWriteHelper.DecryptStringFromString(datastr, OpenGift, OpenFloa);
            if (datastr.IsNullOrEmptyOrSpace()) return;

            TvLists = new System.Collections.Generic.List<TvModel>();
            // Resolve Data
            var listItems = datastr.Split(ItemLine);
            if (listItems.IsEmptyStrings()) return;
            foreach (var paras in listItems.Select(list => list.Split(ParaLine)))
            {
                if (paras.IsEmptyStrings() || paras.Length < 4) continue;
                var tvitem = new TvModel();
                // Name - paras[0]
                if (!paras[0].IsNullOrEmptyOrSpace())
                {
                    tvitem.Name = paras[0].Trim();
                }
                // Url - paras[1]
                if (!paras[1].IsNullOrEmptyOrSpace())
                {
                    var oooo = paras[1].Trim();
                    tvitem.Url = oooo.Substring(1,oooo.Length-2);
                }
                // Width - paras[2]
                if (!paras[2].IsNullOrEmptyOrSpace())
                {
                    int iWidth;
                    if (System.Int32.TryParse(paras[2].Trim(), out iWidth))
                    {
                        tvitem.Width = iWidth;
                    }
                }
                // Height - paras[3]
                if (!paras[3].IsNullOrEmptyOrSpace())
                {
                    int iHeight;
                    if (System.Int32.TryParse(paras[3].Trim(), out iHeight))
                    {
                        tvitem.Height = iHeight;
                    }
                }
                TvLists.Add(tvitem);
            }
        }
    }
}
