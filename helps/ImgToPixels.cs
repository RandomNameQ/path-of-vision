using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Poe_show_buff.helps
{
    static class ImgToPixels
    {
        static Bitmap _image;
        static bool _isTest = false;
        static int centerX = 0;
        static int centerY = 0;

        public static void PixelsFromImage(Bitmap image, List<int> pixelsArr, int pixelCount, int pixelPlace = 0, bool fromLeftToRight = false, bool needColorTest = false)
        {
            pixelsArr.Clear();
            int x = 0;
            int y = 0;

            // -10 чтобы брали пиксиля прямиком из центра, а не "тоыка центрка+10 пиксейелй = смещение от центра"
            int widthFix = 5;
            int heightFix = 10;
            int xPosition = image.Width / 2 - widthFix;
            int yPosition = image.Height / 2 - heightFix;



            switch (pixelPlace)
            {
                case 0:
                    xPosition -= 5;
                    yPosition -= 5;
                    break;
                case 1:

                    break;
                case 2:
                    xPosition += 5;
                    yPosition += 5;
                    break;

            }

            if (fromLeftToRight)
            {
                x = 1;
                xPosition = image.Width / 2 - widthFix;
            }
            else
            {
                y = 1;
                yPosition = image.Height / 2 - heightFix;

            }

            for (int i = 0; i < pixelCount; i++)
            {
                Color pixelColor = image.GetPixel(xPosition + i * x, yPosition + i * y);
                pixelsArr.Add(pixelColor.ToArgb());

                if (needColorTest)
                {
                    image.SetPixel(xPosition + i * x, yPosition + i * y, Color.White);
                }
            }


            Random random = new Random();

            if (needColorTest)
            {


                image.Save(@"test\prototypeIconWhiteColor\" + pixelPlace + "_" + random.Next(0, 999999999) + ".png", ImageFormat.Png);
            }


        }

        public static void DeleteFilesInsideFolder(string pathToFOlder)
        {

            DirectoryInfo di = new DirectoryInfo(pathToFOlder);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }



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
                    //Color pixelColor = image.GetPixel(centerX + i, centerY);
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
