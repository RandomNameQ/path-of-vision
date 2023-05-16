using Newtonsoft.Json;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poe_show_buff
{
    static public class SearchMatchHelper
    {
        public static int AvailableDifferenceBetweenPixels = 1000;
        public static Dictionary<string, List<int>> IconPixels = new Dictionary<string, List<int>>();
        public static List<int> ImagePixels;
        public static string PathToExamplePixel = @"iconPixels.json";
        public static int CurrentIconState = 0;
        public static string CurrentIconType = "";
        public static string PreviousIconType = "";

        public static int MainLoop = 0;

        public static int CurrentDistanceBetweenIcons = 7;
        public static int PreviousIconPos = 0;
        public static int CurrentIconPos = 0;

        public static Bitmap MainImage;

        public static List<Bitmap> iconsList = new List<Bitmap>();

        public enum FirstIcon
        {
            Summon,
            Brand,
            Buff,
            Flask

        }
        public static FirstIcon FirstIcon_;

        static SearchMatchHelper()
        {
            string json = File.ReadAllText(PathToExamplePixel);
            IconPixels = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(json);
        }



        public static List<Bitmap> FindAllIcons(List<int> imagePixels, Bitmap image)
        {
            iconsList.Clear();
            MainLoop = 0;
            CurrentIconState = 0;
            MainImage = image;
            ImagePixels = imagePixels;
            // 10 cos first icon start only with 11 pxl
            for (MainLoop = 10; MainLoop < ImagePixels.Count; MainLoop++)
            {
                CheckPixel(MainLoop);

            }

            return iconsList;
        }


        // если мы получали иконку бафы или фласки, то не можем проверять сумонов или брандов
        // если получили бранда, то не можем чекать сумонов
        // если получили сумона, то позиция следующего пикселя либо дефолтная (следующий сумон), либо сверх дальняя

        public static void CheckPixel(int currentLoop)
        {

            bool pixelsSame = false;

            for (int x = CurrentIconState; x < IconPixels.Count; x++)
            {
                string key = IconPixels.Keys.ElementAt(x);
                CurrentIconType = key;
                List<int> pixels = IconPixels.Values.ElementAt(x);

                for (int i = 0; i < pixels.Count; i++)
                {
                    int pixel = pixels[i];
                    pixelsSame = DifferenceBetweenPixels(ImagePixels[currentLoop], pixel);



                    if (pixelsSame)
                    {

                        if (CheckOtherPixels(currentLoop, pixels, i))
                        {
                            CreateIcon();
                            return;
                        }

                    }

                }

            }
            return;
        }


        public static bool CheckOtherPixels(int currentLoop, List<int> pixel, int savedIconLoop)
        {
            bool pixelsSame = false;
            int loop = 0;
            int needSamePixel = 1;
            int countMatch = 0;


            for (int i = savedIconLoop; i < pixel.Count; i++)
            {
                pixelsSame = false;
                // defend
                if (currentLoop + loop >= ImagePixels.Count)
                {
                    return false;
                }

                pixelsSame = DifferenceBetweenPixels(pixel[savedIconLoop], ImagePixels[currentLoop + loop]);

                if (pixelsSame)
                {
                    countMatch++;

                }
                if (countMatch >= needSamePixel)
                {
                    MainLoop -= savedIconLoop;
                    ChangeIconState();

                    return true;
                }
                loop++;
            }
            return false;
        }

        public static void ChangeIconState()
        {
            if (CurrentIconType.Contains("summon"))
            {
                FirstIcon_ = FirstIcon.Summon;
                CurrentIconState = 0;
            }
            else if (CurrentIconType.Contains("brand"))
            {
                FirstIcon_ = FirstIcon.Brand;
                CurrentIconState = 2;
            }
            else
            {
                FirstIcon_ = FirstIcon.Buff;
                CurrentIconState = 6;
            }
        }


        public static bool DifferenceBetweenPixels(int firstDigit, int secondDigit)
        {



            int diffrenceBetweenPixels = firstDigit - secondDigit;

            if (diffrenceBetweenPixels < 0)
            {
                diffrenceBetweenPixels *= -1;
            }

            if (diffrenceBetweenPixels <= AvailableDifferenceBetweenPixels)
            {

                return true;
            }
            else
            {
                return false;
            }
        }



        // необходимо проверить пиксель на соответствие.
        // если тру, тогда смотрим есть ли еще совпадение.
        // если тру, тогда наш обьект = сохраняем его = меняем текущий цикл на +73 (ми

        public static void CreateIcon()
        {





            int posToCrop = 0;
            posToCrop = MainLoop;

            int disatnceToBorder = 9;
            if (CurrentIconType.Contains("brand"))
            {
                disatnceToBorder = 13;
            }
            else
            {
                disatnceToBorder = 9;
            }
            posToCrop -= disatnceToBorder;

            CurrentIconPos = posToCrop;

            // защита от ситуации, когда фотаю неправильную область из-за того, что дистанция между сумонами и регулярными иконками разная
            if (PreviousIconType.Contains("summon") && !CurrentIconType.Contains("summon"))
            {
                // distance between sum > another = 90+-, distance regular icon ~70
                PreviousIconPos += 80;
                if (PreviousIconPos > CurrentIconPos)
                {
                    MainLoop = PreviousIconPos+95;
                    return;
                }
            }



            var cropRegion = new Rectangle(posToCrop, 8, 64, 80);

            var croppedImage = new Bitmap(cropRegion.Width, cropRegion.Height);
            using (var g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(MainImage, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height), cropRegion, GraphicsUnit.Pixel);
            }

            //croppedImage.Save(posToCrop + "_image.png", ImageFormat.Png);


            // 64 width img, -numbers margin of safety, icons can be siwtched in pos on min ~1pxl some times
            MainLoop = posToCrop + 64 + disatnceToBorder - 2;

           

            PreviousIconType = CurrentIconType;
            PreviousIconPos = posToCrop;
            iconsList.Add(croppedImage);
        }
    }
}
