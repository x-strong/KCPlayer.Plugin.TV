namespace App.Style
{
    #region ELabel
    public class ELabel : System.Windows.Forms.Label
    {
        public ELabel()
        {
            AutoSize = false;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    } 
    #endregion

    #region EFlyPal
    public class EFlyPal : System.Windows.Forms.FlowLayoutPanel
    {
        public EFlyPal()
        {
            BackColor = Helper.Transparent;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
                );
        }
    } 
    #endregion

    #region EPanel
    public class EPanel : System.Windows.Forms.Panel
    {
        public EPanel()
        {
            BackColor = Helper.Transparent;
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
            );
        }
    } 
    #endregion

    #region EPicBox
    public class EPicBox : System.Windows.Forms.PictureBox
    {
        public EPicBox()
        {
            SetStyle(
                System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer |
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.Selectable |
                System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true
            );
        }
    } 
    #endregion

    #region ETxtBox
    public class ETxtBox : System.Windows.Forms.TextBox
    {
        public ETxtBox()
        {
            BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
    } 
    #endregion

    #region EBrowser
    public class EBrowser : System.Windows.Forms.WebBrowser
    {
        public EBrowser()
        {
            ScriptErrorsSuppressed = true;
            ScrollBarsEnabled = false;
            IsWebBrowserContextMenuEnabled = false;
        }
    } 
    #endregion
}