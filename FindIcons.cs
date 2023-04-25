using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Poe_show_buff
{
    public class FindIcons
    {
        // получает картинку. должно находить все иконки
        // с
        private Dictionary<string, Vector3> iconPosition = new Dictionary<string, Vector3>();
        private Dictionary<string, List<int>> iconPixels = new Dictionary<string, List<int>>();
        private Dictionary<string, List<int>> buffsIconPixel = new Dictionary<string, List<int>>();
        private string pathToExamplePos = @"examplePosition.json";
        private string pathToExamplePixel = @"iconPixels.json";
        private string pathToBuffIcon = @"Icons\Buff\";

        private Bitmap _image;

        public bool HaveSummon { get; set; }
        public bool HaveBrand { get; set; }
        public bool HaveFlask { get; set; }

        private List<int> _imageWidthPixels = new List<int>();
        private List<int> _pixeList = new List<int>();
        private Color _pixelColor;

        private int _firstPixelIcon = 7;
        private int _lastIconPixel = 30;
        private int _positionToCropImage = 0;

        private int _detectIconPosition = 0;

        private int currentIconState = 0;

        private enum FirstIcon
        {
            Summon,
            Brand,
            Buff

        }
        private FirstIcon _firstIcon;


        public FindIcons()
        {
            string json = File.ReadAllText(pathToExamplePos);
            iconPosition = JsonConvert.DeserializeObject<Dictionary<string, Vector3>>(json);

            json = File.ReadAllText(pathToExamplePixel);
            iconPixels = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(json);


        }

        public void FindIcon(Bitmap image)
        {
            _image = image;

            Bitmap image1 = new Bitmap("_image.jpg");
            /*_image = image1;*/
            GetPixelFromImg();
            DetectSamePixels();
        }

        private void GetPixelFromImg()
        {
            _imageWidthPixels.Clear();
            int distanceToBorderIcon = 16;
            int widthImage = _image.Width;
            int heightCenter = _image.Height - (_image.Height - distanceToBorderIcon);

            int test = 255 + 7;

            int asd = 0;
            for (int i = 0; i < widthImage; i++)
            {
                _pixelColor = _image.GetPixel(i, heightCenter);
                _imageWidthPixels.Add(_pixelColor.ToArgb());

            }
            _image.Save("image.jpg", ImageFormat.Jpeg);

            DetectFirstIcon();
        }

        private void SaveIconBuffs()
        {

            /* var random = new Random();
             string fileName = "debuff" + random.Next(1000000, 9999999) + ".png";
             imageToSave.Save(Path.Combine(pathFolder, fileName));*/



            /*
            foreach (var image in allDebuffImages)
            {
                int centerX = image.Width / 2;
                int centerY = image.Height / 2;
                _pixelsToCheck.Clear();
                for (int i = 0; i < _countPixels; i++)
                {
                    Color pixelColor = image.GetPixel(centerX + i, centerY);
                    _pixelsToCheck.Add(pixelColor.ToArgb());
                }
                bool isEqual = _pixels.SequenceEqual(_pixelsToCheck);
                if (isEqual)
                {
                    hasMatch = true;
                    break;
                }
            }
            if (!hasMatch)
            {
                var random = new Random();
                string fileName = "debuff" + random.Next(1000000, 9999999) + ".png";
                imageToSave.Save(Path.Combine(pathFolder, fileName));
            }*/

        }

        private void LoadAllImages()
        {
            var allDebuffImages = SearchMatchHelper.LoadBitmapsFromFolder(pathToBuffIcon);
            bool hasMatch = false;
            foreach (var image in allDebuffImages)
            {
                ImgToPixels.PixelToList(image, 5, _pixeList);
            }
        }

        // if 1 icon is summin then = x, if brand = x1 and buff =x2
        private void DetectFirstIcon()
        {
            SniperDetectPixel();
        }



        private void DetectNotFirstIcons()
        {

            switch (_firstIcon)
            {
                case FirstIcon.Summon:
                    IconSummonDetect();
                    break;
                case FirstIcon.Brand:
                    break;
                case FirstIcon.Buff:
                    break;
                default:
                    break;
            }
        }



        private void IconSummonDetect()
        {
            _firstPixelIcon += 70;
            _lastIconPixel = _firstPixelIcon + 60;
            SniperDetectPixel();

        }
        // если что-то отодвигается на пиксель, то и все остальные двигаются
        // brand > buff =77 78
        // summ > brand 93 92
        // sum > buff = 97
        // sum > sum = 74 75
        // buf >buf = 73

        private bool SniperDetectPixel()
        {
            int exampleIconLoop = 0;
            int test = 0;

            var iconList = SearchMatchHelper.FindAllIcons(_imageWidthPixels, _image);

            _image.Save(_positionToCropImage + "test1.png", ImageFormat.Png);

            return false;

            // received pixels
            /*for (int i = _firstPixelIcon; i < _imageWidthPixels.Count; i++)
            {
                // saved pixel
                for (int x = 0; x < iconPixels.Count; x++)
                {
                    string key = iconPixels.Keys.ElementAt(x);
                    List<int> pixels = iconPixels.Values.ElementAt(x);

                    if (SearchMatchHelper.FindSequence(_imageWidthPixels, pixels, i))
                    {
                       
                    }

                }
            }*/



            for (int i = _firstPixelIcon; i < _imageWidthPixels.Count; i++)
            {
                exampleIconLoop = 0;
                foreach (var icon in iconPixels)
                {

                    if (SearchMatchHelper.FindSequence(_imageWidthPixels, icon.Value, i))
                    {
                        string firstIcon = icon.Key;
                        // if was summon and turnted to not summon > more pixel space
                        if (_firstIcon == FirstIcon.Summon)
                        {
                            if (exampleIconLoop != 0)
                            {
                                _firstPixelIcon += 30;
                            }
                        }

                        _firstIcon = (FirstIcon)exampleIconLoop;

                        _positionToCropImage = i - 9;
                        if (exampleIconLoop == 1)
                        {
                            _positionToCropImage = i - 13;
                        }

                        _firstPixelIcon = i;

                        CreateIcon();
                        DetectNotFirstIcons();

                        return true;
                    }
                    exampleIconLoop++;
                }

                if (_lastIconPixel == i)
                {
                    break;
                }
            }

            return false;
        }


        private void DetectSamePixels()
        {
            for (int i = _firstPixelIcon; i < _imageWidthPixels.Count; i++)
            {

            }
        }

        private void CreateIcon()
        {
            var cropRegion = new Rectangle(_positionToCropImage, 8, 64, 80);

            // Crop the image
            var croppedImage = new Bitmap(cropRegion.Width, cropRegion.Height);
            using (var g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(_image, new Rectangle(0, 0, croppedImage.Width, croppedImage.Height), cropRegion, GraphicsUnit.Pixel);
            }

            // Save the cropped image to a file
            croppedImage.Save(_positionToCropImage + "_image.png", ImageFormat.Png);
        }

        // TODO еще я могу искать внутри главного картинного массива пиксели, которые отображают иконку, а потом доходить до правой грани иконки и снова искать шото, но тогда мне придется искать до конца

    }
}
