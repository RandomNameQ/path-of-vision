using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Net.Http.Headers;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poe_show_buff.helps;
using Poe_show_buff.settings;

namespace Poe_show_buff
{
    public class ShowIconToChangePosition
    {
        private List<PictureBox> _pictureBoxes = new List<PictureBox>();
        private Thread _threadIcon;
        private List<Thread> _threads;
        DraggIcon draggIcon = new DraggIcon();
        public bool IsThreadNeed { get; set; }
        public void DrawImages(List<string> pathToImages, FullFillBuffsData iconList)
        {
            bool isIconLoaded = false;

            Vector2 iconPos = new Vector2(0, 0);

            

            _threadIcon = new Thread(() =>
            {
                
                using (Form form = new Form())
                {
                    // Set form properties
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.WindowState = FormWindowState.Maximized;
                    form.BackColor = Color.Black;
                    form.TransparencyKey = Color.Black;
                    form.TopMost = true;
                    form.ShowInTaskbar = false;

                    // Create a panel to hold the picture boxes
                    Panel panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    panel.BackColor = Color.Transparent;
                    form.Controls.Add(panel);

                    // Loop through each image path and create a new picture box to display it
                    foreach (var icon in iconList.ListAllBuffs)
                    {
                        if (!icon.isActivated)
                        {
                            continue;
                        }

                        string pathToIcon = icon.path;

                        // Create a new picture box for each image
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Name = icon.name;
                        //pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        //TODO SIZE
                        pictureBox.Size = new Size(64, 80);
                        pictureBox.Location = new Point((int)icon.x, (int)icon.y);
                        pictureBox.Image = new Bitmap(pathToIcon);
                        panel.Controls.Add(pictureBox);

                        bool isDragging = false;

                        // Allow the picture box to be dragged
                        draggIcon.DraggImage(pictureBox, isDragging, pathToIcon);
                        _pictureBoxes.Add(pictureBox);
                    }

                    form.Opacity = 0.8;
                    form.Show();
                    Application.Run(form);
                }
            });

            _threadIcon.SetApartmentState(ApartmentState.STA);
            _threadIcon.Start();

        }

        // fix = "thread stop"
        public void HideImgFromShow()
        {

            for (int x = 0; x < _pictureBoxes.Count; x++)
            {

                _pictureBoxes[x].Invoke((MethodInvoker)delegate {
                    _pictureBoxes[x].Visible = false;
                });

            }

           
        }
     

    }

}
