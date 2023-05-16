using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poe_show_buff.settings;

namespace Poe_show_buff.helps
{
    public class DraggIcon
    {
        private bool _isDragging = false;
        private int _newX = 0;
        private int _newY = 0;
        private string _buffName;
        public void DraggImage(PictureBox pictureBox, bool isDragging, string buffName)
        {

            _buffName = "";
            _isDragging = false;

            _buffName = buffName;
            _isDragging = isDragging;
            pictureBox.MouseDown += (sender, e) =>
            {

                _isDragging = true;
                pictureBox.Tag = e.Location;
            };


            pictureBox.MouseMove += (sender, e) =>
            {
                if (_isDragging)
                {
                    Point offset = (Point)pictureBox.Tag;
                    Point newLocation = new Point(pictureBox.Left - (offset.X - e.X), pictureBox.Top - (offset.Y - e.Y));
                    pictureBox.Location = newLocation;
                }
            };


            DraggOff(pictureBox);
        }

        public void DraggOff(PictureBox pictureBox)
        {
            pictureBox.MouseUp += (sender, e) =>
            {
                _isDragging = false;
                _newX = pictureBox.Location.X;
                _newY = pictureBox.Location.Y;
                Vector2 iconPosition = new Vector2(_newX, _newY);
                SaveData.IconPosition(iconPosition, pictureBox.Name);



            };
        }

    }
}
