using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Poe_show_buff.settings.FullFillBuffsData;

namespace Poe_show_buff
{
    public class _MakeScreenShot
    {
        private int _countPixels=5;
        private List<int> _pixels = new List<int>();
        private List<int> _pixelsToCheck = new List<int>();
        private List<int> _debuffPixels = new List<int>(11);
        private List<int> _pixelTemp = new List<int>(11);
        private int disstanceFromStart = 11;
        private int disstanceFromTop = (8*4)-3+64;
        private int disstanceBetweenRegularBuff = 8;
        private int disstanceChanger = 0;
        private int widthImage = 65;
        private int heightImage = 80;
        public Dictionary<Bitmap,List<int>> ImagesAndPixels = new Dictionary<Bitmap,List<int>>();

  

        public _MakeScreenShot()
        {
           
        }

        public void MakeShoot()
        {


          
            ImagesAndPixels.Clear();

          
            Rectangle gameBounds = new Rectangle(
                x: 0,
                y: 0,
                width: (int)(Screen.PrimaryScreen.Bounds.Width * 0.4),
                height: (int)(Screen.PrimaryScreen.Bounds.Height * 0.2));

            // Take a screenshot of the game window
            using Bitmap screenshot = new Bitmap(gameBounds.Width, gameBounds.Height);
            using Graphics gfxScreenshot = Graphics.FromImage(screenshot);
            gfxScreenshot.CopyFromScreen(gameBounds.Location, Point.Empty, gameBounds.Size);

           
            try
            {
                screenshot.Save("buffs.png");
            }
            catch (Exception)
            {

               
            }
            



            int cycle = 0;
            disstanceChanger = disstanceFromStart;
            // Loop through the game window and save small pictures
            for (int x = disstanceChanger; x < gameBounds.Width; x += widthImage)
            {
                if (cycle != 0)
                {

                    x += disstanceBetweenRegularBuff;
                }
                

                // Define the bounds of the small picture
                Rectangle smallPictureBounds = new Rectangle(
                    x: x,
                    y: disstanceFromTop,
                    width: widthImage,
                    height: heightImage);

                // Create a bitmap to store the small picture
                Bitmap smallPicture = new Bitmap(widthImage, heightImage);

                // Create a graphics object from the bitmap
                using Graphics gfxSmallPicture = Graphics.FromImage(smallPicture);

                // Copy the contents of the game window to the small picture
                gfxSmallPicture.CopyFromScreen(
                    sourceX: gameBounds.Left + smallPictureBounds.Left,
                    sourceY: gameBounds.Top + smallPictureBounds.Top,
                    destinationX: 0,
                    destinationY: 0,
                    blockRegionSize: smallPictureBounds.Size,
                    copyPixelOperation: CopyPixelOperation.SourceCopy);

                // Extract the pixels to the right of the center point
                _pixels.Clear();
                int centerX = smallPicture.Width / 2;
                int centerY = smallPicture.Height / 2;
                for (int i = 0; i < _countPixels; i++)
                {
                    Color pixelColor = smallPicture.GetPixel(centerX + i, centerY);
                    _pixels.Add(pixelColor.ToArgb());
                }

                


                string filename = $"picture_{cycle}.png";
                IsDebuff(smallPicture);
                var stopSearch = CheckIfDebuff();
                if (!stopSearch)
                {
                    break;
                }
                SaveDebuffImg(smallPicture);
                smallPicture.Save(filename);
                
                cycle++;
                

            }

            try
            {
                
            }
            catch (Exception)
            {

                MessageBox.Show("Fail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            

           

        }

        

        private void IsDebuff(Bitmap image)
        {
            
            _pixelTemp.Clear();
            int centerX = image.Width / 2;
            int centerY = image.Height / 2;
            int count = 0;


            for (int i = 0; i < _countPixels*2; i++)
            {
                Color pixelColor = image.GetPixel(0 + i, centerY);
                _pixelTemp.Add(pixelColor.ToArgb());
            }

           
        }

        
        private void DebuffPixels()
        {

            using (Bitmap image = new Bitmap(@"Icons\Debuff\debuffMain.png"))
            {
                _debuffPixels.Clear();
                int centerX = image.Width / 2;
                int centerY = image.Height / 2;
                int count = 0;

                for (int i = 0; i < _countPixels * 2; i++)
                {
                    Color pixelColor = image.GetPixel(0+i, centerY);
                    _debuffPixels.Add(pixelColor.ToArgb());
                }
            }
        }

        private bool CheckIfDebuff()
        {
            bool isEqual = _pixelTemp.SequenceEqual(_debuffPixels);
           
            return isEqual;
        }

        private void SaveDebuffImg(Bitmap imageToSave)
        {
            string pathFolder = @"Icons\Debuff\";
            var allDebuffImages = LoadBitmapsFromFolder();
            bool hasMatch = false;
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
            }
        }


        public List<Bitmap> LoadBitmapsFromFolder()
        {
            string folderPath = @"Icons\Debuff\";
            List<Bitmap> bitmaps = new List<Bitmap>();
            string[] files = Directory.GetFiles(folderPath, "*.png");

            foreach (string file in files)
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(file);
                bitmaps.Add(bitmap);
            }

            return bitmaps;
        }
    }
}
