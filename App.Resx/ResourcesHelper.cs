namespace App.Resx
{
    public class ResourcesHelper
    {
        /// <summary>
        /// 返回Image
        /// </summary>
        /// <param name="resxname"></param>
        /// <returns></returns>
        public System.Drawing.Image GetImage(string resxname)
        {
            if (resxname.IsNullOrEmptyOrSpace()) return null;
            var stream = GetType().Assembly.GetManifestResourceStream("App.Resx.Image." + resxname);
            if (stream == null)
            {
                return null;
            }
            var image = System.Drawing.Image.FromStream(stream);
            return image.IsEmptyImage() ? null : image;
        }

        /// <summary>
        /// 返回ICO
        /// </summary>
        /// <param name="resxname"></param>
        /// <returns></returns>
        public System.Drawing.Icon GetIco(string resxname)
        {
            if (resxname.IsNullOrEmptyOrSpace()) return null;
            var stream = GetType().Assembly.GetManifestResourceStream("App.Resx.ICO." + resxname);
            if (stream == null)
            {
                return null;
            }
            var icon = new System.Drawing.Icon(stream);
            return icon.IsEmptyIcon() ? null : icon;
        }
    }
}