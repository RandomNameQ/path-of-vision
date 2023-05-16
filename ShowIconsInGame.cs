using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Poe_show_buff.helps;
using Poe_show_buff.settings;
using static System.Net.Mime.MediaTypeNames;

namespace Poe_show_buff
{

    public class ShowIconsInGame
    {

        private bool _isMyFirstTime = true;
        private Form form;
        private Thread _threadIcon;
        private FullFillBuffsData _searchedIconsl;
        private List<PictureBox> _pictureBoxes = new List<PictureBox>();

        public ShowIconsInGame()
        {
            // fix return this thread to form1 for control to stop
            Helper.GetThread(this);

            _searchedIconsl = Program.ReturnBuffs();



            _threadIcon = new Thread(() =>
            {
                _threadIcon.IsBackground = true;




                form = new Form();
                _isMyFirstTime = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
                form.BackColor = Color.Black;
                form.TransparencyKey = Color.Black;
                form.TopMost = true;
                form.ShowInTaskbar = false;

                Panel panel = new Panel();
                panel.Dock = DockStyle.Fill;
                panel.BackColor = Color.Transparent;

                // marshal the panel creation to the UI thread

                form.Controls.Add(panel);



                for (int i = 0; i < _searchedIconsl.ListAllBuffs.Count; i++)
                {
                    if (!_searchedIconsl.ListAllBuffs[i].isActivated)
                    {
                        continue;
                    }
                    PictureBox pictureBox = new PictureBox();
                    _pictureBoxes.Add(pictureBox);
                    pictureBox.Name = _searchedIconsl.ListAllBuffs[i].name;
                    pictureBox.Size = new Size(64, 80);
                    pictureBox.Location = new Point((int)_searchedIconsl.ListAllBuffs[i].x, (int)_searchedIconsl.ListAllBuffs[i].y);
                    pictureBox.Image = new Bitmap(_searchedIconsl.ListAllBuffs[i].path);

                    pictureBox.Visible = false;
                    // marshal the picture box creation to the UI thread
                    panel.Controls.Add(pictureBox);

                }

                form.ShowDialog();
                System.Windows.Forms.Application.Run(form);

            });

            _threadIcon.SetApartmentState(ApartmentState.STA);
            _threadIcon.Start();
        }
        private Dictionary<string, Bitmap> _namesAndImages = new Dictionary<string, Bitmap>();
        private Dictionary<PictureBox, Bitmap> _imageAndPictureBox = new Dictionary<PictureBox, Bitmap>();
        public void UpdateIconState()
        {
            _imageAndPictureBox.Clear();
            _namesAndImages.Clear();

            List<string> iconName = new List<string>();
            GetIconNames(iconName);



            for (int i = 0; i < iconName.Count; i++)
            {

                IsNameEquel(iconName[i]);


            }
            ShowIcons();



        }

        private void GetIconNames(List<string> iconName)
        {
            iconName.Clear();
            for (int i = 0; i < _searchedIconsl.ListAllBuffs.Count; i++)
            {
                if (!_searchedIconsl.ListAllBuffs[i].isActivated)
                {
                    continue;
                }

                if (_searchedIconsl.ListAllBuffs[i].isDiscovered)
                {
                    string name = _searchedIconsl.ListAllBuffs[i].name;
                    _namesAndImages.Add(name, _searchedIconsl.ListAllBuffs[i].gamesIcon);
                    iconName.Add(name);
                }

            }
        }

        private void IsNameEquel(string nameIcon)
        {
            for (int x = 0; x < _pictureBoxes.Count; x++)
            {
                if (_pictureBoxes[x].Name == nameIcon)
                {


                    _pictureBoxes[x].Invoke((MethodInvoker)delegate
                    {

                        foreach (var icon in _namesAndImages)
                        {
                            if (icon.Key == nameIcon)
                            {
                                _imageAndPictureBox.Add(_pictureBoxes[x], icon.Value);
                                break;
                            }
                        }

                    });

                    return;

                }
            }

        }

        private void ShowIcons()
        {
            bool isSame = false;
            Bitmap image = null;
            for (int x = 0; x < _pictureBoxes.Count; x++)
            {
                image = null;
                isSame = false;
                foreach (var icon in _imageAndPictureBox)
                {
                    if (icon.Key == _pictureBoxes[x])
                    {
                        image = icon.Value;
                        isSame = true;
                    }
                }

                if (isSame)
                {
                    _pictureBoxes[x].Invoke((MethodInvoker)delegate
                    {
                        _pictureBoxes[x].Image = image;
                        _pictureBoxes[x].Visible = true;
                    });
                }
                else
                {
                    _pictureBoxes[x].Invoke((MethodInvoker)delegate
                    {
                        _pictureBoxes[x].Visible = false;
                    });
                }

            }
        }

        public void HideIcons()
        {
            for (int x = 0; x < _pictureBoxes.Count; x++)
            {

                _pictureBoxes[x].Invoke((MethodInvoker)delegate
                {
                    _pictureBoxes[x].Visible = false;
                });
            }


        }

    }
}
