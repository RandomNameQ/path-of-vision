using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Poe_show_buff.ListBuffs;

namespace Poe_show_buff
{
    public class ShowBuff
    {
        private Vector2 iconPosition;
        private bool isDragging = false;
        private string _buffName;
        public void ShowMETHEWAY(Bitmap bitmap, string buffName,Vector2 iconPos)
        {

            _buffName = buffName;


            Thread thread = new Thread(() =>
            {
                // Create a form to display the image
                using (Form form = new Form())
                {
                    // Set form properties
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.WindowState = FormWindowState.Maximized;
                    form.BackColor = Color.Black;
                    form.TransparencyKey = Color.Black;
                    form.TopMost = true;
                    form.ShowInTaskbar = false;


                    // Create a picture box control to display the image
                    // Create a picture box control to display the image
                    using (PictureBox pictureBox = new PictureBox())
                    {
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Size = new Size(bitmap.Width, bitmap.Height);

                        // Set picture box location to center on screen
                        int x = (form.Width - pictureBox.Width) / 2;
                        int y = (form.Height - pictureBox.Height) / 2;
                        pictureBox.Location = new Point((int)iconPos.X, (int)iconPos.Y);
                        // Set picture box image
                        pictureBox.Image = bitmap;

                        // Add picture box to form
                        form.Controls.Add(pictureBox);

                        // Set the opacity of the form to display the image with some transparency
                        form.Opacity = 0.8;

                        // Add click event handler to the form

                        // Declare a boolean variable to keep track of whether the mouse button is currently pressed down or not
                        bool isDragging = false;

                        // Set up the picture box to receive focus
                        pictureBox.TabStop = true;
                        pictureBox.TabIndex = 1;

                        // Handle the MouseDown event of the picture box to set the isDragging variable to true and store the mouse cursor position relative to the picture box
                        pictureBox.MouseDown += (sender, e) =>
                        {
                            isDragging = true;
                            pictureBox.Tag = e.Location;
                        };

                        // Handle the MouseMove event of the picture box to move the picture box when the mouse is moved while the left mouse button is held down
                        pictureBox.MouseMove += (sender, e) =>
                        {
                            if (isDragging)
                            {
                                Point offset = (Point)pictureBox.Tag;
                                Point newLocation = new Point(pictureBox.Left - (offset.X - e.X), pictureBox.Top - (offset.Y - e.Y));
                                pictureBox.Location = newLocation;
                            }
                        };

                        // Handle the MouseUp event of the picture box to set the isDragging variable to false when the left mouse button is released
                        pictureBox.MouseUp += (sender, e) =>
                        {
                            isDragging = false;
                            var newX = pictureBox.Location.X;
                            var newY = pictureBox.Location.Y;
                            iconPosition = new Vector2(newX, newY);

                            SaveNewPositionIcon();


                        };

                        // Show the form and wait for it to close

                        form.ShowDialog();


                    }
                }


            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();


        }

        private void SaveNewPositionIcon()
        {
            CheckIncomingIcons.SaveNewPositionIcon(iconPosition, _buffName);
        }

    }
}
