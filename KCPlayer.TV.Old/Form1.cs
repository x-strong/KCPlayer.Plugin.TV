using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace KCPlayer.TV.Old
{
    public partial class Form1 : Form
    {
        public List<Tv> myObject = new List<Tv>
            {
                new Tv
                    {
                        Name = @"高清直播",
                        Url = @"http://www.kcplayer.com/tv/index.htm",
                    },
                new Tv
                {
                    Name = @"电视直播",
                    Url = @"http://www.kcplayer.com/tv/index2.htm",
                },
            };
        [Serializable]
        public class Tv
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
        public Form1()
        {
            InitializeComponent();
            Clipboard.Clear();
            Clipboard.SetDataObject(AES_Enc_Str(@"高清直播@http://www.kcplayer.com/tv/index.htm|64P直播@http://www.kcplayer.com/tv/index2.htm|电视盒子@http://www.x-99.cn/tv/tv/v/|颓废影音@http://tuifei.sinaapp.com/box/box3/movie/|悦读ＦＭ@http://yuedu.fm/|虾米ＦＭ@http://www.xiami.com/player/|收音广播@http://www.fifm.cn/fm12.htm|酷狗音乐@http://web.kugou.com/|凤凰直播@http://v.ifeng.com/live/", @"其实你不用猜我没"));

        }


        //默认密钥向量 
        private static readonly byte[] Key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

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
                des.IV = Key1;
                var decryptBytes = new byte[cipherText.Length];
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
                    des.IV = Key1;
                    byte[] cipherBytes;
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
