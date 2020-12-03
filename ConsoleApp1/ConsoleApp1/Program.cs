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
        static void Main(string[] args)
        {
          var jisEncode =   Encoding.GetEncoding("Shift_JIS");
          var unicode =   Encoding.GetEncoding("unicode");
            StringBuilder sb = new StringBuilder();

            int count = 0;
            for (int i = 0x20; i < 0x7E; i++)
            {
                sb.Append(jisEncode.GetString(new byte[] { (byte)i }));
                if (count % 50 == 0)
                {
                    sb.AppendLine();
                }
                count++;
            }

            for (int i = 0xA1; i < 0xDF; i++)
            {
                sb.Append(jisEncode.GetString(new byte[] { (byte)i }));
                if (count % 50 == 0)
                {
                    sb.AppendLine();
                }
                count++;
            }


            Action<int, int, int, int> action = (x1, x2, y1, y2) =>
            {
                for (int i = x1; i < x2; i++)
                {
                    for (int l = y1; l < y2; l++)
                    {
                        sb.Append(jisEncode.GetString(new byte[] { (byte)i, (byte)l }));
                        if (count % 50 == 0)
                        {
                            sb.AppendLine();
                        }

                        count++;
                    }

                }
            };

            action(0x81, 0x9F, 0xE0, 0xEF);
            action(0x40, 0x7E, 0x80, 0xFC);
            action(0xF0, 0xFC, 0x40, 0x7E); 
            action(0xF0, 0xFC, 0x80, 0xFC);

            File.WriteAllText("D://output.txt", sb.ToString());
        }
    }
}
