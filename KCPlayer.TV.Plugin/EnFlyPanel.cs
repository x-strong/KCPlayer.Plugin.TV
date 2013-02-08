using System.Windows.Forms;

namespace KCPlayer.TV.Plugin
{
    public class EnFlyPanel:FlowLayoutPanel
    {
        public EnFlyPanel()
        {
            SetStyle(
                    ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw |
                    ControlStyles.Selectable |
                    ControlStyles.SupportsTransparentBackColor, true
                );
        }
    }
}
