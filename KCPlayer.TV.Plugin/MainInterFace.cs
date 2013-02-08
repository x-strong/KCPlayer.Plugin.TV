using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KCPlayer.TV.Plugin
{
    public class MainInterFace
    {
        public Control Owner { get; set; }
        public Control OwnerParent { get; set; }

        private Image _customDrawing = null;
        public Image CustomDrawing
        {
            get { return _customDrawing; }
            set { _customDrawing = value; }
        }

        Guid _guid = new Guid();
        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public MainInterFace()
        {
        }

        public MainInterFace(Control control)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            object[] attrs = assembly.GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
            Guid = new Guid(((GuidAttribute)attrs[0]).Value);
            OwnerParent = control;
        }
        /// <summary>
        /// 公有调用方法
        /// </summary>
        /// <param name="owner">panel容器</param>
        public void Main(Control owner)
        {
            Owner = owner; 
            Init();
        }

        /// <summary>
        /// 当磁铁添加进入后触发事件
        /// 代码移位一下我操作卡
        /// </summary>
        public void Shown()
        {

        }

        private List<Tv> TvList =new List<Tv>();

        private class Tv
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
        private const int Navheight = 35;
        private readonly Uri TvListPath = new Uri("http://api.kcplayer.com/tv/list.c");
        public void Init()
        {
            Owner.Parent.Width = 824;
            Owner.Parent.Height = 467 + Navheight;
            using (var wc = new WebClient())
            {
                //wc.Proxy = null;
                wc.DownloadStringAsync(TvListPath);
                wc.DownloadStringCompleted += wc_DownloadStringCompleted;
            }
            
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Owner.Controls.Clear();
            TvList.Clear();
            var listStr = AES_Dec_Str(e.Result, @"其实你不用猜我没");
            if (string.IsNullOrEmpty(listStr))
            {
                return;
            }
            var lists = listStr.Split('|');
            foreach (var list in lists)
            {
                var strs = list.Split('@');
                var item = new Tv {Name = strs[0], Url = strs[1]};
                TvList.Add(item);
            }
            if (TvList == null || TvList.Count <= 0)
            {
                MessageBox.Show(@"获取电视列表失败！");
                return;
            }
            var iNav = new Panel
            {
                Dock = DockStyle.Top,
                Size = new Size(1,Navheight),
                BackColor = Color.Transparent,
            };
            var iFly = new EnFlyPanel
            {
                Size = new Size(Owner.Parent.Width-20, Navheight),
                Location = new Point(10, 0),
                BackColor = Color.Transparent,
            };
            foreach (var v in TvList)
            {
                var btn = new EnButton
                {
                    Size = new Size(80, Navheight - 6),
                    ForeColor = Color.White,
                    Text = v.Name,
                    Tag = v.Url,
                    AutoSize = false,
                    Font = new Font("宋体", 12F),
                };
                iFly.Controls.Add(btn);
                btn.Click += btn_Click;
            }
            var iTv = new EnBrowser
            {
                Dock = DockStyle.Fill,
                IsWebBrowserContextMenuEnabled = false,
                ScriptErrorsSuppressed = true,
                ScrollBarsEnabled = false,
            };
            Owner.Controls.Add(iTv);
            iNav.Controls.Add(iFly);
            Owner.Controls.Add(iNav);
            iTv.Navigate(TvList[0].Url);
        }


        void btn_Click(object sender, EventArgs e)
        {
            foreach (var v in Owner.Controls)
            {
                var browser = v as EnBrowser;
                if (browser == null) continue;
                try
                {
                    browser.Navigate(((EnButton) sender).Tag.ToString());
                }
                catch
                {

                }
            }
        }

        //当绘制方式为Custome的时候即可使用
        public void MouseMove(MouseEventArgs arg)
        {

        }
        public void MouseLeave()
        {

        }
        public void MouseEnter()
        {

        }
        public void MouseDown(MouseEventArgs arg)
        {

        }
        public void MouseUp(MouseEventArgs arg)
        {

        }

        //给容器添加控件儿
        //说整错了    sil.u..。跑了 yinggaishiReadToend
        //Owner.Controls.Add(label);
        //获取基础大小
        //Size size = (Size)control.GetType().GetField("_normalSize", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(control);
        //CustomDrawing = new Bitmap(size.Width, size.Height);
        //父窗体刷新事件  刷新磁铁特效等
        //OwnerParent.GetType().GetMethod("RefreshControl").Invoke(OwnerParent, new object[] { id });
        //更新接口
        //OwnerParent.GetType().GetMethod("IWantUpdate", BindingFlags.Public | BindingFlags.Instance).Invoke(OwnerParent, new object[] { id, "2.0.0.0", "http://dl_dir.qq.com/qqfile/qq/QQ2013/QQ2013Beta1.exe" });

        //默认密钥向量 
        private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        #region AES对称解密算法AES_Dec
        public string AES_Dec_Str(string toDecrypt, string DecKey)
        {
            // 256-AES 32位Key
            try
            {
                return Encoding.UTF8.GetString(AES_Dec(toDecrypt, DecKey)).Replace("\0", "");
            }
            catch { return null; }
        }
        public byte[] AES_Dec(string toDecrypt, string DecKey)
        {
            var cipherText = Convert.FromBase64String(toDecrypt);
            using (var des = Rijndael.Create())
            {
                des.Key = Encoding.UTF8.GetBytes(DecKey);
                des.IV = _key1;
                byte[] decryptBytes = new byte[cipherText.Length];
                try
                {
                    using (var ms = new MemoryStream(cipherText))
                    {
                        using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            cs.Read(decryptBytes, 0, decryptBytes.Length);
                            cs.Close();
                            ms.Close();
                        }
                    }
                    return decryptBytes;
                }
                catch { return null; }
            }
        }
        #endregion

        #region AES对称加密算法AES_Enc
        public string AES_Enc_Str(string toEncrypt, string EncKey)
        {
            // 256-AES 32位Key
            try
            {
                return Convert.ToBase64String(AES_Enc(toEncrypt, EncKey));
            }
            catch { return null; }
        }
        public byte[] AES_Enc(string toEncrypt, string AesKey)
        {
            //分组加密算法
            using (var des = Rijndael.Create())
            {
                try
                {
                    var inputByteArray = Encoding.UTF8.GetBytes(toEncrypt);
                    des.Key = Encoding.UTF8.GetBytes(AesKey);
                    des.IV = _key1;
                    byte[] cipherBytes = null;
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                            cipherBytes = ms.ToArray();
                            cs.Close();
                            ms.Close();
                        }
                    }
                    return cipherBytes;
                }
                catch { return null; }
            }
        }
        #endregion
    }
}
