
using System.Linq;

namespace App.Style
{
    public class Helper
    {
        public static System.Windows.Forms.Form Ui { get; set; }
        public static System.Windows.Forms.ToolTip Tip { get; set; }
        public static System.Windows.Forms.NotifyIcon Notify { get; set; }
        public static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Drawing.Color>> All { get; set; }
        public static System.Windows.Forms.Cursor HCursors { get; set; }
        public static System.Drawing.ContentAlignment Align { get; set; }
        public static System.Drawing.ContentAlignment Blign { get; set; }
        public static System.Drawing.Color Transparent = System.Drawing.Color.Transparent;
        public static string FontNormal { get; set; }
        public static string FontWeb { get; set; }
        public static string FontWing { get; set; }
        public static string FontSymbol { get; set; }
        public static string FontFang { get; set; }
        public static System.Drawing.Size NavSize { get; set; }
        public static float NavFontSize { get; set; }

        
        public static void Start()
        {
            Ui.BackgroundImage = new Resx.ResourcesHelper().GetImage("golden_beach.jpg");
            Ui.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Ui.Icon = new Resx.ResourcesHelper().GetIco("Application.ico");
            Notify = new System.Windows.Forms.NotifyIcon { Icon = Ui.Icon, Visible = true, };
            Notify.MouseClick += Notify_MouseClick;
            Tip = new System.Windows.Forms.ToolTip();

            HCursors = System.Windows.Forms.Cursors.Hand;
            Align = System.Drawing.ContentAlignment.MiddleCenter;
            Blign = System.Drawing.ContentAlignment.BottomCenter;
            FontNormal = HasThisFonts(@"微软雅黑") ? @"微软雅黑" : @"仿宋";
            FontWeb = HasThisFonts(@"Webdings")? @"Webdings": @"仿宋";
            FontWing = HasThisFonts(@"Wingdings") ? @"Wingdings" : @"仿宋";
            FontSymbol = HasThisFonts(@"Symbol") ? @"Symbol" : @"仿宋";
            FontFang = HasThisFonts(@"方正舒体") ? @"方正舒体" : @"仿宋";
            NavSize = new System.Drawing.Size(30,24);
            NavFontSize = 11F;
            All =new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Drawing.Color>>();
            var navBar = new System.Collections.Generic.List<System.Drawing.Color>
                {
                    // Normal Font
                    System.Drawing.Color.Goldenrod,
                    // Normal Back
                    Transparent,
                    // Hover Font
                    System.Drawing.Color.FromArgb(234,234,234),
                    // Hover Back
                    System.Drawing.Color.FromArgb(195 ,0, 122, 204),
                    // Custom Hover Font
                    System.Drawing.Color.FromArgb(254,254,254),
                    // Custom Hover Back
                    System.Drawing.Color.FromArgb(195,255, 80, 80),
                };
            All.Add("NavBar", navBar);
            var listNavBar = new System.Collections.Generic.List<System.Drawing.Color>
                {
                    // Normal Font
                    System.Drawing.Color.WhiteSmoke,
                    // Normal Back
                    System.Drawing.Color.DodgerBlue,
                    // Hover Font
                    System.Drawing.Color.White,
                    // Hover Back
                    System.Drawing.Color.FromArgb(255, 128, 0),
                };
            All.Add("ListNavBar", listNavBar);

            var txtPalItem = new System.Collections.Generic.List<System.Drawing.Color>
                {
                    // Normal Font
                    System.Drawing.Color.FromArgb(60,60,60),
                    // Normal Back
                    System.Drawing.Color.White,
                };
            All.Add("TxtPal", txtPalItem);

            var listMango = new System.Collections.Generic.List<System.Drawing.Color>
                {
                    // title Font
                    System.Drawing.Color.WhiteSmoke,
                    // title Back
                    System.Drawing.Color.Transparent,
                    // score Font
                    System.Drawing.Color.Goldenrod,
                    // score Back
                    System.Drawing.Color.Transparent,
                    // url Font 
                    System.Drawing.Color.FromArgb(144,144,144),
                    // url Back
                    System.Drawing.Color.Transparent,
                };
            All.Add("ListMango", listMango);
        }

        private static void Notify_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            #region 托盘鼠标点击事件

            if (e.Button != System.Windows.Forms.MouseButtons.Left || e.Clicks < 0) return;
            Ui.Show();

            #endregion
        }

        private static bool HasThisFonts(string fontname)
        {
            return !fontname.IsNullOrEmptyOrSpace() && new System.Drawing.Text.InstalledFontCollection().Families.Any(fontFamily => fontFamily.Name == fontname);
        }
    }
}
