using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poe_show_buff.helps
{
    static public class Helper
    {
        public static List<ShowIconsInGame> ThreadWhichShowImages = new List<ShowIconsInGame>();
        public static int MinimumComparedPixelToDetect = 3;
        static public void SaveImage(Bitmap image, string pathToSave, string name)
        {
            image.Save(pathToSave + @"\" + name + ".png", ImageFormat.Png);
        }

        public static bool DifferenceBetweenPixels(int currentPixel, int searchPixel, int maxDiffrence)
        {
            int diffrenceBetweenPixels = currentPixel - searchPixel;

            if (diffrenceBetweenPixels < 0)
            {
                diffrenceBetweenPixels *= -1;
            }
            if (diffrenceBetweenPixels <= maxDiffrence)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public static bool DifferenceBetweenPixelsList(List<int> buffPixels, List<int> gamePixels, int maxDiffrence)
        {


            int comparedPixels = 0;
            bool isEqual = false;

            for (int i = 0; i < buffPixels.Count; i++)
            {
                if (DiffrencePixels(buffPixels[i], gamePixels[i], maxDiffrence))
                {
                    comparedPixels++;
                }

                if (MinimumComparedPixelToDetect == comparedPixels)
                {
                    isEqual = true;
                    return isEqual;

                }
            }

            return isEqual;
        }



        public static bool DiffrencePixels(int currentPixel, int searchPixel, int maxDiffrence)
        {
            int diffrenceBetweenPixels = currentPixel - searchPixel;

            if (diffrenceBetweenPixels < 0)
            {
                diffrenceBetweenPixels *= -1;
            }
            if (diffrenceBetweenPixels <= maxDiffrence)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ReadImgPixelInFolder()
        {
            List<int> imagePixels = new List<int>();
            Dictionary<string, List<int>> imagesPixelData = new Dictionary<string, List<int>>();
            string pathToFolder = @"test";
            string[] iconsName = Directory.GetFiles(pathToFolder, "*",
                                         SearchOption.TopDirectoryOnly);
            int coun = 0;
            foreach (var iconName in iconsName)
            {

                Bitmap image = new Bitmap(iconName);
                ImgToPixels.PixelToList(image, 5, imagePixels);
                imagesPixelData.Add(iconName, imagePixels);

                for (int x = 0; x < 30; x++)
                {
                    int centerX = image.Width / 2;
                    int centerY = image.Height / 2;
                    image.SetPixel(centerX + x, centerY, Color.White);
                }

                image.Save(iconName + coun + ".png", ImageFormat.Png);
                coun++;
            }
        }


        // 0 - buuf, 1 debuff
        public static void GetThread(ShowIconsInGame thread)
        {

            if (!ThreadWhichShowImages.Contains(thread))
            {
                ThreadWhichShowImages.Add(thread);
            }
        }
    }
}
