using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Poe_show_buff
{
    public class MakeScreenFromMonitor : IDisposable
    {

        // TODO GetDeviceCaps 
        // 93 pixel from top to pixel hunting
        private const int _debuffTopPosition = 93;
        private Rectangle _gameBounds = new Rectangle();
        private string _screenName = "whatISee.png";
        private Bitmap _screenshot;
        private Graphics _graphicImage;

        private Areas _areas = Areas.Buff;
        public enum Areas
        {
            Buff,
            DeBuff,
            Trade
        }
 
        public MakeScreenFromMonitor(Areas _areas)
        {
            this._areas = _areas;
            PickAreaToScreen();
        }

        
        public Bitmap CreateScreen()
        {
            _graphicImage.CopyFromScreen(_gameBounds.Location, Point.Empty, _gameBounds.Size);
            _screenshot.Save(_screenName);
            return _screenshot;
            //Dispose();


        }

        public void Dispose()
        {
            _screenshot.Dispose();
            _graphicImage.Dispose();
        }


        private void PickAreaToScreen(float changeWidth = 0.9f)
        {

            int x = 0, y = 0;
            float width = changeWidth, height = 0.1f;

           

            _gameBounds.X = x;
            _gameBounds.Y = y;
            _gameBounds.Width = (int)(Screen.PrimaryScreen.Bounds.Width * width);
            _gameBounds.Height = (int)(Screen.PrimaryScreen.Bounds.Height * height);


            _screenshot = new Bitmap(_gameBounds.Width, _gameBounds.Height);
            _graphicImage = Graphics.FromImage(_screenshot);


        }


    }
}
