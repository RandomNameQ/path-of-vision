using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Poe_show_buff
{
    public class GetIconsFromGame
    {
        private Bitmap _image;
        private CompareIconFromGameWithMy _compareIcons = new CompareIconFromGameWithMy();
        private List<int> _imageWidthPixels = new List<int>();
        private Color _pixelColor;

        private bool _needScreenShoot;

        public bool NeedScreenShoot
        {
            get { return _needScreenShoot; }
            set { _needScreenShoot = value; }
        }


        public List<Bitmap> FindIcon(Bitmap image)
        {
            _image = image;
            _imageWidthPixels.Clear();
            int distanceToBorderIcon = 16;
            int widthImage = _image.Width;
            int heightCenter = _image.Height - (_image.Height - distanceToBorderIcon);

            for (int i = 0; i < widthImage; i++)
            {
                _pixelColor = _image.GetPixel(i, heightCenter);
                _imageWidthPixels.Add(_pixelColor.ToArgb());

            }


            var iconList = SearchMatchHelper.FindAllIcons(_imageWidthPixels, _image);
            if (_needScreenShoot)
            {
                return iconList;
            }
            else
            {
                _compareIcons.DetectEqualIcons(iconList);
                return null;
            }

            

        }

    }
}
