using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class CapthaGenerate
    {
        public static string generatedCaptha = "";


        public static Bitmap Gena(int width, int height)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            char[] randomString = new char[4];

            for (int i = 0; i < 4; i++)
            {
                randomString[i] = chars[random.Next(chars.Length)];
            }

            generatedCaptha = new string(randomString);
            return GenerateImageCaptha(generatedCaptha, width, height);
        }

        private static Bitmap GenerateImageCaptha(string captchaText, int width, int height)
        {
            Random rand = new Random();
            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(Color.White);

                // Зашумление фона
                for (int i = 0; i < width * height / 2; i++)
                {
                    var x = rand.Next(width);
                    var y = rand.Next(height);
                    var color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                    bitmap.SetPixel(x, y, color);
                }

                // Вывод текста капчи
                var font = new Font("Comic Sans MS", 18, FontStyle.Strikeout);
                var textBrush = new SolidBrush(Color.Black);
                var textPositon = new PointF(5, 6);
                var textPositon1 = new PointF(45, 10);
                var textPositon2 = new PointF(75, 1);
                var textPositon3 = new PointF(95, 23);
                graphics.DrawString(captchaText[0].ToString(), font, textBrush, textPositon);
                graphics.DrawString(captchaText[1].ToString(), font, textBrush, textPositon1);
                graphics.DrawString(captchaText[2].ToString(), font, textBrush, textPositon2);
                graphics.DrawString(captchaText[3].ToString(), font, textBrush, textPositon3);

                graphics.Flush();
            }
            return bitmap;
        }
    }
}
