
namespace KCPlayer.TV.Client
{
    public partial class FrmMain : System.Windows.Forms.Form
    {
        public FrmMain()
        {
            InitializeComponent();

            #region Frm Set

            SetClassLong(Handle, GclStyle, GetClassLong(Handle, GclStyle) | CsDropShadow);
            SetStyle(
                System.Windows.Forms.ControlStyles.ResizeRedraw |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);

            #endregion
        }

        #region Frm Set

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                // 窗体移动的消息，控制窗体不会移出屏幕外
                //case 0x0216:
                //    {
                //        var left = Marshal.ReadInt32(m.LParam, 0);
                //        var top = Marshal.ReadInt32(m.LParam, 4);
                //        var right = Marshal.ReadInt32(m.LParam, 8);
                //        var bottom = Marshal.ReadInt32(m.LParam, 12);
                //        left = Math.Min(Math.Max(0, left), Screen.PrimaryScreen.Bounds.Width - Width);
                //        top = Math.Min(Math.Max(0, top), Screen.PrimaryScreen.Bounds.Height - Height);
                //        right = Math.Min(Math.Max(Width, right), Screen.PrimaryScreen.Bounds.Width);
                //        bottom = Math.Min(Math.Max(Height, bottom), Screen.PrimaryScreen.Bounds.Height);
                //        Marshal.WriteInt32(m.LParam, 0, left);
                //        Marshal.WriteInt32(m.LParam, 4, top);
                //        Marshal.WriteInt32(m.LParam, 8, right);
                //        Marshal.WriteInt32(m.LParam, 12, bottom);
                //    }
                //    break;
                // 重载最小化
                case 0x112:
                    {
                        if (m.WParam.ToInt32() == 0xF020)
                        {
                            Visible = false;
                        }
                        base.WndProc(ref m);
                    }
                    break;
                case 0x0084:
                    {
                        // 拖动
                        if ((int)m.Result == 0x1)
                        {
                            m.Result = (System.IntPtr)0x2;
                        }
                        //// 改动大小
                        //var vPoint = new Point((int) m.LParam & 0xFFFF,
                        //                       (int) m.LParam >> 16 & 0xFFFF);
                        //vPoint = PointToClient(vPoint);
                        //if (vPoint.X <= 5)
                        //    if (vPoint.Y <= 5)
                        //        m.Result = (IntPtr) Httopleft;
                        //    else if (vPoint.Y >= ClientSize.Height - 5)
                        //        m.Result = (IntPtr) Htbottomleft;
                        //    else m.Result = (IntPtr) Htleft;
                        //else if (vPoint.X >= ClientSize.Width - 5)
                        //    if (vPoint.Y <= 5)
                        //        m.Result = (IntPtr) Httopright;
                        //    else if (vPoint.Y >= ClientSize.Height - 5)
                        //        m.Result = (IntPtr) Htbottomright;
                        //    else m.Result = (IntPtr) Htright;
                        //else if (vPoint.Y <= 5)
                        //    m.Result = (IntPtr) Httop;
                        //else if (vPoint.Y >= ClientSize.Height - 5)
                        //    m.Result = (IntPtr) Htbottom;
                    }
                    break;
                case 0x0201: //鼠标左键按下的消息
                    {
                        m.Msg = 0x00A1; //更改消息为非客户区按下鼠标 
                        m.LParam = System.IntPtr.Zero; //默认值 
                        m.WParam = new System.IntPtr(2); //鼠标放在标题栏内 
                        base.WndProc(ref m);
                    }
                    break;
                //case 0x000C:
                //    {
                //    }
                //    break;
                //// 边框
                //case 0x0086:
                //    {
                //        _borderColor = m.WParam.ToInt32() == 1 ? Color.CornflowerBlue : Color.Transparent;
                //        Invalidate();
                //    }
                //    break;
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        //取消方向键对控件的焦点的控件，用自己自定义的函数处理各个方向键的处理函数
        {
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Up:
                    {
                    }
                    return true;
                case System.Windows.Forms.Keys.Down:
                    {
                    }
                    return true;
                case System.Windows.Forms.Keys.Left:
                    {
                    }
                    return true;
                case System.Windows.Forms.Keys.Right:
                    {
                    }
                    return true;
                case System.Windows.Forms.Keys.Delete:
                    {
                    }
                    break;
                case System.Windows.Forms.Keys.Escape:
                    {
                    }
                    break;
                // 更换库外图
                case System.Windows.Forms.Keys.F8:
                    {
                    }
                    break;
                // 添加到库内
                case System.Windows.Forms.Keys.F9:
                    {
                    }
                    break;
                // 切换全屏
                case System.Windows.Forms.Keys.F10:
                    {
                    }
                    break;
                // 切换视图
                case System.Windows.Forms.Keys.F12:
                    {
                    }
                    break;
                // 进行诊断
                case System.Windows.Forms.Keys.F11:
                    {
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private const int CsDropShadow = 0x20000;
        private const int GclStyle = (-26);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int SetClassLong(System.IntPtr hwnd, int nIndex, int dwNewLong);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetClassLong(System.IntPtr hwnd, int nIndex);

        #endregion
    }
}
