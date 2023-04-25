using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Numerics;
using System.Drawing.Imaging;

namespace Poe_show_buff
{
    public class GetDynamicIconPosition
    {
        private Color _pixelColor;
        private List<int> imageWidthPixels = new List<int>();
        private List<int> exampleImagePixel = new List<int>();
        private List<int> buffPixel = new List<int>(5);
        private List<int> deBuffPixel = new List<int>(5);
        private List<int> brandPixel = new List<int>(5);
        private List<int> summonPixel = new List<int>(5);

        private Dictionary<string, List<int>> samplesImages = new Dictionary<string, List<int>>();

        private Vector3 iconPosition;
        private Dictionary<string, Vector3> sampleImages = new Dictionary<string, Vector3>();

        private string folderPath = @"Icons\Prototype\";
        private string buffPath;
        private string deBuffPath;
        private string summonPath;
        private string brandPath;
        private List<string> iconNames = new List<string>();
        private List<string> iconsName = new List<string>();

        private List<int> _iconPositions = new List<int>();

        private Bitmap _image;

        private int _availableDifferenceBetweenPixels = 10000;

        private Graphics gfxSmallPicture;
        private Rectangle cropBounds;
        public GetDynamicIconPosition()
        {
            /* buffPath = folderPath + "buff.png";
             deBuffPath = folderPath + "debuff.png";
             summonPath = folderPath + "sum0.png";
             brandPath = folderPath + "brand.png";*/

            iconsName.Add("summon");
            iconsName.Add("brand");
            iconsName.Add("buff");
            iconsName.Add("flask");
            iconsName.Add("charges");

        }

        public void GetLinePixels(Bitmap image)
        {


            // TODO add trigger for ui
            /*SaveTypesIcon();*/

            string json = File.ReadAllText(@"exampleIcon.json");
            samplesImages = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(json);

            TakePixelFromImg();
        }


        private void SaveTypesIcon()
        {

            for (int i = 0; i < iconsName.Count; i++)
            {
                var buffPixel = new List<int>();
                var iconName = iconsName[i];
                Bitmap image = new Bitmap(iconName);
                int distanceToBorderIcon = 8;
                int widthImage = image.Width;
                int heightCenter = image.Height - (image.Height - distanceToBorderIcon);
                int posBorderPixel = 10;
                int pixelCount = 0;

                // if iconName == brand, need another puxel
                if (i == 1)
                {
                    posBorderPixel = 13;
                }
                else
                {
                    posBorderPixel = 9;
                }

                for (int j = posBorderPixel; j < widthImage; j++)
                {



                    _pixelColor = image.GetPixel(j, heightCenter);
                    buffPixel.Add(_pixelColor.ToArgb());
                    image.SetPixel(j, heightCenter, Color.White);
                    pixelCount++;
                    if (pixelCount >= 5)
                    {
                        break;
                    }
                }
                image.Save(i + ".jpg");
                samplesImages.Add(iconName, buffPixel);
            }

            string json = JsonConvert.SerializeObject(samplesImages, Formatting.Indented);
            File.WriteAllText(@"exampleIcon.json", json);
            samplesImages.Clear();

        }

        public void TakePixelFromPrototype()
        {
            string pathFolder = "";
            int loopCount = 0;
            Dictionary<string, List<int>> dataTypePrototype = new Dictionary<string, List<int>>();

            foreach (var folderName in iconsName)
            {
                pathFolder = @"Icons\Prototype\" + folderName;
                var allDebuffImages = SearchMatchHelper.LoadBitmapsFromFolder(pathFolder);

                for (int i = 0; i < allDebuffImages.Count; i++)
                {
                    var buffPixel = new List<int>();
                    Bitmap image = allDebuffImages[i];
                    int distanceToBorderIcon = 8;
                    int widthImage = image.Width;
                    int heightCenter = image.Height - (image.Height - distanceToBorderIcon);
                    int posBorderPixel = 9;
                    int pixelCount = 0;

                    if (folderName == "brand")
                    {
                        posBorderPixel = 13;
                    }
                    else
                    {
                        posBorderPixel = 9;
                    }

                    for (int j = posBorderPixel; j < widthImage; j++)
                    {
                        Color pixelColor = image.GetPixel(j, heightCenter);
                        buffPixel.Add(pixelColor.ToArgb());
                        image.SetPixel(j, heightCenter, Color.White);
                        pixelCount++;
                        if (pixelCount >= 5)
                        {
                            break;
                        }
                    }

                    string key = folderName + loopCount;
                    if (!dataTypePrototype.Values.Any(list => list.SequenceEqual(buffPixel)))
                    {
                        dataTypePrototype.Add(key, buffPixel);
                        
                        loopCount++;
                    }
                    image.Save(@"Icons\Prototype\result\" + key + ".png");
                }
            }

            string json = JsonConvert.SerializeObject(dataTypePrototype, Formatting.Indented);
            File.WriteAllText(@"iconPixels.json", json);
        }





        private void TakePixelFromImg()
        {
            _image = new Bitmap(@"buff.png");

            int distanceToBorderIcon = 16;
            int widthImage = _image.Width;
            int heightCenter = _image.Height - (_image.Height - distanceToBorderIcon);

            for (int i = 0; i < widthImage; i++)
            {
                _pixelColor = _image.GetPixel(i, heightCenter);
                imageWidthPixels.Add(_pixelColor.ToArgb());
            }

            FindIcons();
        }

        private void FindIcons()
        {
            List<int> testPixelCheck = new List<int>();
            int iconPos = 0;

            for (int i = 0; i < imageWidthPixels.Count; i++)
            {


                foreach (var icon in samplesImages)
                {


                    if (SearchMatchHelper.FindSequence(imageWidthPixels, icon.Value, i))
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            if (icon.Key.Contains("brand"))
                            {
                                iconPos = i - 13;
                            }
                            else
                            {
                                iconPos = i - 9;
                            }
                            _image.SetPixel(iconPos, x + _image.Height / 2, Color.White);

                        }
                        _iconPositions.Add(iconPos);
                    }

                }
            }

            /*for (int i = 0; i < imageWidthPixels.Count; i++)
            {
                if (i == 546)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        _pixelColor = _image.GetPixel(i + 9, x + _image.Height / 2);
                        testPixelCheck.Add(_pixelColor.ToArgb());
                        _image.SetPixel(i + 9, x + _image.Height / 2, Color.White);



                    }
                }
            }*/

            iconPosition = new Vector3(_iconPositions[0], _iconPositions[1], _iconPositions[2]);
            sampleImages.Add("normal", iconPosition);

            if (File.Exists(@"examplePosition.json"))
            {

            }
            else
            {
                // ?>>>?
            }
            string json = JsonConvert.SerializeObject(sampleImages, Formatting.Indented);
            File.WriteAllText(@"examplePosition.json", json);

            SaveImg(_image, "test.png");
            CreateIconImage();
        }

        // если что-то отодвигается на пиксель, то и все остальные двигаются
        // brand > buff =77 78
        // summ > brand 93 92
        // sum > buff = 97
        // sum > sum = 74 75
        // buf >buf = 73


        private void SaveImg(Bitmap image, string name)
        {
            image.Save(name);
        }



        private void FullFillExampleIcons()
        {
            iconNames.Add("buff");
            iconNames.Add("sum");
            iconNames.Add("brand");

            int distanceToBorder = 7;

            int cycle = 0;
            exampleImagePixel.Clear();
            foreach (var iconName in iconNames)
            {
                Bitmap image = new Bitmap(folderPath + iconName);

                int widthImage = image.Width;
                int heightCenter = image.Height / 3;

                for (int i = distanceToBorder; i < widthImage; i++)
                {
                    _pixelColor = image.GetPixel(i, heightCenter);
                    exampleImagePixel.Add(_pixelColor.ToArgb());
                    image.SetPixel(i, heightCenter, Color.White);
                    cycle++;

                    if (cycle >= 4)
                    {
                        break;
                    }
                }
                /*image.Save("test1.png");*/
                samplesImages.Add(iconName, exampleImagePixel);
                exampleImagePixel.Clear();
            }


        }

        private void CreateIconImage()
        {

            for (int i = 0; i < _iconPositions.Count; i++)
            {
                var cropRegion = new Rectangle(_iconPositions[i], 8, 64, 80);

                // Crop the image
                var croppedImage = new Bitmap(cropRegion.Width, cropRegion.Height);
                using (var g = Graphics.FromImage(croppedImage))
                {
                    g.DrawImage(_image, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height), cropRegion, GraphicsUnit.Pixel);
                }

                // Save the cropped image to a file
                croppedImage.Save(i + "_image.jpg", ImageFormat.Jpeg);
            }











        }
    }
}
