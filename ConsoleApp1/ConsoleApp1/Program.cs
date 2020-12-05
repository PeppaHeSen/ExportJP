using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// Shift_JIS编码字节结构
        ///以下字符在Shift_JIS使用一个字节来表示。
        ///ASCII字符（0x20-0x7E），但“\”被“¥”取代
        ///ASCII控制字符（0x00-0x1F、0x7F）
        ///JIS X 0201标准内的半角标点及片假名（0xA1-0xDF）
        ///在部分操作系统中，0xA0用来放置“不换行空格”。
        ///以下字符在Shift_JIS使用两个字节来表示。
        ///JIS X 0208字集的所有字符
        ///“第一位字节”使用0x81-0x9F、0xE0-0xEF（共47个）
        ///“第二位字节”使用0x40-0x7E、0x80-0xFC（共188个）
        ///使用者定义区
        ///“第一位字节”使用0xF0-0xFC（共13个）
        ///“第二位字节”使用0x40-0x7E、0x80-0xFC（共188个）
        ///在Shift_JIS编码表中，并未使用0xFD、0xFE及0xFF。
        ///在微软及IBM的日语电脑系统中，在0xFA、0xFB及0xFC的两字节区域，加入了388个JIS X 0208没有收录的符号和汉字。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var jisEncode = Encoding.GetEncoding("Shift_JIS");
            StringBuilder sb = new StringBuilder();
            int count = 0;
            byte[] lineByte = new byte[1];
            byte[] doubleByte = new byte[2];
            void lineAction(int x1, int x2)
            {
                for (int i = x1; i < x2; i++)
                {
                    lineByte[0] = (byte)i;
                    sb.Append(jisEncode.GetString(lineByte));
                    if (count % 50 == 0)
                    {
                        sb.AppendLine();
                    }
                    count++;
                }
            }

            void doubleAction(int x1, int x2, int y1, int y2)
            {
                for (int i = x1; i < x2; i++)
                {
                    for (int l = y1; l < y2; l++)
                    {
                        doubleByte[0] = (byte)i;
                        doubleByte[1] = (byte)l;
                        sb.Append(jisEncode.GetString(doubleByte));
                        if (count % 50 == 0)
                        {
                            sb.AppendLine();
                        }

                        count++;
                    }
                }
            }

            lineAction(0x20, 0x7E);
            lineAction(0x00, 0x1F);
            lineAction(0x00, 0x7F);
            lineAction(0xA1, 0xDF);

            doubleAction(0x81, 0x9F, 0x40, 0x7E);
            doubleAction(0x81, 0x9F, 0x80, 0xFC);
            doubleAction(0xE0, 0xEF, 0x40, 0x7E);
            doubleAction(0xE0, 0xEF, 0x80, 0xFC);

            doubleAction(0xF0, 0xFC, 0x40, 0x7E);
            doubleAction(0xF0, 0xFC, 0x80, 0xFC);

            //string str = sb.ToString();
            //sb.Clear();
            //var charAaaay = str.ToCharArray();
            //for (int i = 0; i < charAaaay.Length; i++)
            //{
            //    string format = string.Format("\\u{0:x4}", (int)charAaaay[i]);
            //    sb.Append(DecodeString(format));
            //    if (count % 50 == 0)
            //    {
            //        sb.AppendLine();
            //    }

            //    count++;
            //}

            File.WriteAllText("D://output.txt", sb.ToString());
        }

        //public static string DecodeString(string unicode)
        //{
        //    if (string.IsNullOrEmpty(unicode))
        //    {
        //        return string.Empty;
        //    }

        //    string[] ls = unicode.Split(new string[] { "\\u" }, StringSplitOptions.RemoveEmptyEntries);
        //    StringBuilder builder = new StringBuilder();
        //    int len = ls.Length;
        //    for (int i = 0; i < len; i++)
        //    {
        //        builder.Append(Convert.ToChar(ushort.Parse(ls[i], System.Globalization.NumberStyles.HexNumber)));
        //    }

        //    return builder.ToString();
        //}
    }
}
