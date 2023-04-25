using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Poe_show_buff
{
    public class IconsPlace
    {
        private int _iconSize;
        private int _countIconPlace = 10;
        private int _offsetXY = 1;
        // vector2 icon coordinate, bool  isClose
        public Dictionary<Vector2, bool> _iconPlace = new Dictionary<Vector2, bool>();


        public void GeneratePlacePoints(int iconSize)
        {
            this._iconSize = iconSize;
            // Get the center of the screen
            int centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            int centerY = Screen.PrimaryScreen.Bounds.Height / 2;

            // Calculate the total width and height of the icon placement area
            int width = (_countIconPlace * iconSize) + ((_countIconPlace - 1) * _offsetXY);
            int height = iconSize;

            // Calculate the starting point of the icon placement area
            int startX = centerX - (width / 2);
            int startY = centerY - (height / 2);

            // Generate the coordinates of the icon placement points
            for (int i = 0; i < _countIconPlace; i++)
            {
                int x = startX + (i * iconSize) + (i * _offsetXY);
                int y = startY;
                _iconPlace.Add(new Vector2(x, y), false);
            }
        }
    }
}
