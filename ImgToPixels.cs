using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Poe_show_buff
{
    static class ImgToPixels
    {
        static Bitmap _image;
        static bool _isTest = false;
        static int centerX = 0;
        static int centerY = 0;
        public static void PixelToList(Bitmap image, int countPixels, List<int> pixelList, bool isNormalIcon = true, bool isForSummon = false)
        {

            _image = image;
            pixelList.Clear();
            /*image = new Bitmap(iconPath);*/

            centerX = image.Width / 2;
            centerY = image.Height / 2;
            if (isNormalIcon)
            {

                for (int i = 0; i < countPixels; i++)
                {
                    Color pixelColor = image.GetPixel(centerX + i, centerY);
                    pixelList.Add(pixelColor.ToArgb());
                }
            }
            else
            {
                if (isForSummon)
                {
                    for (int i = 0; i < countPixels; i++)
                    {
                        Color pixelColor = image.GetPixel(centerX, 0 + i);
                        pixelList.Add(pixelColor.ToArgb());
                    }
                    return;
                }

                for (int i = 0; i < countPixels; i++)
                {
                    Color pixelColor = image.GetPixel(0 + i, centerY);
                    pixelList.Add(pixelColor.ToArgb());
                }
            }

            if (_isTest)
            {
                ChangeToWhitePixel();
            }

        }

        public static void ChangeToWhitePixel()
        {
            for (int i = 0; i < 5; i++)
            {
                _image.SetPixel(centerX + i, centerY, Color.White);

            }
            _image.Save("iconTest.jpg", ImageFormat.Jpeg);
        }

        
    }
}
