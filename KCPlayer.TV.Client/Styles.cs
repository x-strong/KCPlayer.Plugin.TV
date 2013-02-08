namespace KCPlayer.TV.Client
{
    public class Styles
    {
        public const float FontSize = 9F;
        //public const float FontSize = 18F;
        public const int SizeWidth = 21;
        public const int SizeHeight = 19;
        public const int SsWidth = 21;
        public static readonly System.Drawing.Size NavSize = new System.Drawing.Size(SizeWidth, SizeHeight);
        public static readonly System.Drawing.Font NavFontWeb = new System.Drawing.Font("Webdings", FontSize);
        public static readonly System.Drawing.Font NavFontWing = new System.Drawing.Font("Wingdings", FontSize);

        public static readonly System.Collections.Generic.List<System.Drawing.Color> NavColor = new System.Collections.
            Generic.List<System.Drawing.Color>
            {
                //0 A - Normal - BackColor
                System.Drawing.Color.Transparent,
                //1 A - Normal - ForeColor
                System.Drawing.Color.FromArgb(100, 144, 173, 126),
                //2 B - MouseHover - BackColor
                System.Drawing.Color.Transparent,
                //3 B - MouseHover - ForeColor
                System.Drawing.Color.FromArgb(100, 88, 151, 132),
                //4 A - MouseHover - BackColor
                System.Drawing.Color.Transparent,
                //5
                System.Drawing.Color.YellowGreen,
                //6
                System.Drawing.Color.WhiteSmoke,
                //7
                System.Drawing.Color.White,
            };

        public static readonly System.Collections.Generic.List<System.Drawing.Color> SectionColor = new System.
            Collections.
            Generic.List<System.Drawing.Color>
            {
                // Section Normal BackColor
                System.Drawing.Color.Transparent,
                // ListItem BackColor
                //System.Drawing.Color.FromArgb(51, 153, 255),
                System.Drawing.Color.FromArgb(60, 60, 60, 60),
                // ListLine BackColor
                System.Drawing.Color.White,
                // Listtitle ForeColor
                System.Drawing.Color.WhiteSmoke,
                // ListHover ForeColor
                System.Drawing.Color.White,
                // ListHover ForeColor Waring
                System.Drawing.Color.Brown,
                // ListHover BackColor 
                //System.Drawing.Color.FromArgb(0, 122, 204),
                System.Drawing.Color.FromArgb(160, 60, 60, 60),
                // BtnItem BackColor
                //System.Drawing.Color.FromArgb(214,214,214),
                System.Drawing.Color.FromArgb(150, 88, 151, 132),
                // BtnItem ForeColor
                System.Drawing.Color.FromArgb(233, 233, 233),
                // BtnItemHover BackColor
                System.Drawing.Color.FromArgb(200, 88, 151, 132),
                // InItem BackColor
                System.Drawing.Color.FromArgb(230, 230, 230),
            };
    }
}