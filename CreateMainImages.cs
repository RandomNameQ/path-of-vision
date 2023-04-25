using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Poe_show_buff
{
    public class CreateMainImages : IDisposable
    {

        // TODO GetDeviceCaps 
        // 93 pixel from top to pixel hunting
        private const int _debuffTopPosition = 93;
        private Rectangle _gameBounds = new Rectangle();
        private string _screenName = "none.png";
        private Bitmap _screenshot;
        private Graphics _gfxScreenshot;

        private Areas _areas = Areas.Buff;
        public enum Areas
        {
            Buff,
            DeBuff,
            Trade
        }
        CreateIconImages buffArea;
        CreateIconImages deBuffArea;
        public CreateMainImages(Areas _areas)
        {
            this._areas = _areas;
            PickAreaToScreen();
        }

        
        public Bitmap CreateScreen()
        {
            _gfxScreenshot.CopyFromScreen(_gameBounds.Location, Point.Empty, _gameBounds.Size);
            _screenshot.Save(_screenName);

            buffArea.CreateIconsImg(_screenshot);
            return _screenshot;
            /*Dispose();*/


        }

        public void Dispose()
        {
            _screenshot.Dispose();
            _gfxScreenshot.Dispose();
        }


        private void PickAreaToScreen(float changeWidth = 0.9f)
        {

            int x = 0, y = 0;
            float width = changeWidth, height = 0.1f;

            switch (_areas)
            {

                case Areas.Buff:
                    y = 0;
                    _screenName = @"watch/buff/buff.png";
                    buffArea = new CreateIconImages("buff");
                    break;
                case Areas.DeBuff:
                    y = _debuffTopPosition;
                    _screenName = @"watch/debuff/deBuff.png";
                    deBuffArea = new CreateIconImages("debuff");
                    break;
                case Areas.Trade:
                    break;
                default:
                    break;
            }

            _gameBounds.X = x;
            _gameBounds.Y = y;
            _gameBounds.Width = (int)(Screen.PrimaryScreen.Bounds.Width * width);
            _gameBounds.Height = (int)(Screen.PrimaryScreen.Bounds.Height * height);


            _screenshot = new Bitmap(_gameBounds.Width, _gameBounds.Height);
            _gfxScreenshot = Graphics.FromImage(_screenshot);


        }


    }
}
