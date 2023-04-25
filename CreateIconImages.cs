using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poe_show_buff
{
    public class CreateIconImages
    {
        // 15 pixel = suumon (but some times need 8 pixel bug), 8 brand
        // if summon anpther thing then 15 pixel
        private int widthImage = 66;
        private int heightImage = 80;
        private int _listCount = 11;
        private List<int> _defaultIconPosition = new List<int>(15);
        private List<int> _tempIconPosition = new List<int>(15);

        // 7 po defolt
        private int distanceToFirstIcon = 10;
        private int distanceFromTopFirstIcon = 8;
        private int distanceBetweenRegularIcon = 8;
        private int offsetFromSummon = 10;
        private int offsetFromBrand = 10;
        private int normalSummonPosition = 8;
        private int distanceBetweenIcons = 73;

        private string pathToMainImg = "";
        private string pathToFolder = "";
        private string pathToNormalIcon = "";
        private string pathToSummonIcon = "";
        private string pathToBrandIcon = "";

        private Bitmap mainImage;
        private Bitmap smallPicture;
        private List<Bitmap> iconList = new List<Bitmap>();
        private Graphics gfxSmallPicture;
        private Rectangle cropBounds;

        private int pixelCount = 5;
        public int NormalCountBuffs { get; set; }


        private List<int> buffExamplePixel = new List<int>(5);
        private List<int> newBuffPixel = new List<int>(5);
        private List<int> summunPixel = new List<int>(5);
        private List<int> brandPixel = new List<int>(5);

        private bool hasBeenChanged, isCorruptedSummon;


        public CreateIconImages(string whatSearch)
        {
            NormalCountBuffs = 5;

            if (!File.Exists($"watch/{whatSearch}/{whatSearch}.png"))
            {
                MessageBox.Show($"Dont find main images for {whatSearch}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            pathToFolder = $"watch/{whatSearch}/";
            pathToMainImg = $"watch/{whatSearch}/{whatSearch}.png";
            pathToNormalIcon = $"{pathToFolder}ICONEXAMPLE.png";
            pathToSummonIcon = $"{pathToFolder}SUMEXAMPLE.png";
            pathToBrandIcon = $"{pathToFolder}BRANDEXAMPLE.png";


            int iconPosition = distanceToFirstIcon;
            _defaultIconPosition.Add(iconPosition);

            for (int i = 1; i < _listCount; i++)
            {
                iconPosition += widthImage + distanceBetweenRegularIcon;
                _defaultIconPosition.Add(iconPosition);

            }
            _tempIconPosition = new(_defaultIconPosition);

            /*mainImage = new Bitmap(pathToMainImg);*/
            smallPicture = new Bitmap(widthImage, heightImage);
            gfxSmallPicture = Graphics.FromImage(smallPicture);

            IsIconExists();




        }

        public void CreateIconsImg(Bitmap mainImage)
        {
            this.mainImage = mainImage;

            /*ChecksBuffs();*/
            /*return;*/

            iconList.Clear();
            for (int i = 0; i < _defaultIconPosition.Count; i++)
            {
                newBuffPixel.Clear();

                cropBounds = new Rectangle(
                   x: _defaultIconPosition[i],
                   y: distanceFromTopFirstIcon,
                   width: widthImage,
                   height: heightImage);

                
                gfxSmallPicture.DrawImage(mainImage, new Rectangle(0, 0, widthImage, heightImage), cropBounds, GraphicsUnit.Pixel);
                ImgToPixels.PixelToList(smallPicture, pixelCount, newBuffPixel);



                Bitmap newIcon = new Bitmap(smallPicture);
                iconList.Add(newIcon);

                /*CheckIncomingIcons.IsRequiredBuff(newBuffPixel, smallPicture);*/



                if (i == 6)
                {
                    bool isEqual = buffExamplePixel.SequenceEqual(newBuffPixel);

                    if (!isEqual)
                    {
                        break;
                    }
                }


                smallPicture.Save($"{pathToFolder}{i}.png");
            }
          

            CheckIncomingIcons.IsRequiredBuff(iconList);
            


        }

        private void ChecksBuffs()
        {
            CheckForSummon();
            CheckForBrand();

        }


        // check for summon, their count and if true change icon position
        private void CheckForSummon()
        {
            // current icon summon normal or corrupted?
            bool isEqual = false;

            int changedSummonPos = 15;
            int offsetFromSummon = 24;
            if (isCorruptedSummon)
            {
                normalSummonPosition = changedSummonPos;
            }
            else
            {
                normalSummonPosition = 8;
            }

            for (int i = 0; i < 2; i++)
            {
                newBuffPixel.Clear();

                cropBounds = new Rectangle(
                   x: normalSummonPosition,
                   y: distanceFromTopFirstIcon,
                   width: widthImage,
                   height: heightImage);

                gfxSmallPicture.DrawImage(mainImage, new Rectangle(0, 0, widthImage, heightImage), cropBounds, GraphicsUnit.Pixel);
                ImgToPixels.PixelToList(smallPicture, pixelCount, newBuffPixel, isNormalIcon: false, isForSummon: true);

                // img for test
                /*smallPicture.Save($"{pathToFolder}{i}.png");*/
                isEqual = summunPixel.SequenceEqual(newBuffPixel);

                if (isEqual)
                {
                    int numberIconToChangePos = HowManySummons(normalSummonPosition);
                    ChangeIconPositions(numberIconToChangePos);
                    break;
                }
                isCorruptedSummon = true;
                offsetFromSummon += changedSummonPos - normalSummonPosition;
                normalSummonPosition = changedSummonPos;
            }

        }

        private void CheckForBrand()
        {

        }

        private int HowManySummons(int summonPosition)
        {




            for (int i = 0; i < _defaultIconPosition.Count; i++)
            {
                newBuffPixel.Clear();

                if (i >= 1)
                {
                    normalSummonPosition += 1;
                }

                cropBounds = new Rectangle(
                   x: normalSummonPosition + i * distanceBetweenIcons,
                   y: distanceFromTopFirstIcon,
                   width: widthImage,
                   height: heightImage);



                gfxSmallPicture.DrawImage(mainImage, new Rectangle(0, 0, widthImage, heightImage), cropBounds, GraphicsUnit.Pixel);
                ImgToPixels.PixelToList(smallPicture, pixelCount, newBuffPixel, isNormalIcon: false, isForSummon: true);
                bool isEqual = summunPixel.SequenceEqual(newBuffPixel);
                // -9409969 == pixel, summon icons have diffrent pixels, this == stone golem type (dark i think)

                if (!isEqual)
                {
                    if (newBuffPixel[0] == -9409969)
                    {
                        isEqual = true;
                    }
                    else
                    {
                        return i;
                    }

                }
                    /*smallPicture.Save($"{pathToFolder}{i}.png");*/

            }
            return 0;

        }
        private void ChangeIconPositions(int fromWhatIconNeedChange)
        {


            /*offsetFromSummon += 22;*/
            // TODO FCK PIXELS
            offsetFromSummon = 25;
            _defaultIconPosition.Clear();
            _defaultIconPosition.AddRange(_tempIconPosition);
            for (int x = 0; x < _defaultIconPosition.Count; x++)
            {

                if (fromWhatIconNeedChange-1 >= x)
                {
                    if (x==1)
                    {
                        _defaultIconPosition[x] += x;
                    }
                    if (x == 2)
                    {
                        _defaultIconPosition[x] += x;
                    }
                    if (x == 3)
                    {
                        _defaultIconPosition[x] += x+1;
                    }
                }
                else
                {
                    _defaultIconPosition[x] += offsetFromSummon;
                }

            }



        }

        private void IsIconExists()
        {
            var image = new Bitmap(pathToNormalIcon);
            ImgToPixels.PixelToList(image, pixelCount, buffExamplePixel, isNormalIcon: false);

            image = new Bitmap(pathToSummonIcon);
            ImgToPixels.PixelToList(image, pixelCount, summunPixel, isNormalIcon: false, isForSummon: true);

            image = new Bitmap(pathToBrandIcon);
            ImgToPixels.PixelToList(image, pixelCount, brandPixel, isNormalIcon: false);

        }

    }
}
