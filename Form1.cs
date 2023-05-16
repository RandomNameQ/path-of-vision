using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Timer = System.Windows.Forms.Timer;
using System.Drawing.Imaging;
using Poe_show_buff.settings;
using Poe_show_buff.helps;

namespace Poe_show_buff
{
    public partial class Form1 : Form
    {

        private SettingIconOnScreen _settingIcons = new SettingIconOnScreen();
        private UISettings _uISettings;
        private FullFillBuffsData _listBuffs;
        private GetIconsFromGame _findIcons = new GetIconsFromGame();
        private Timer timer;
        private Bitmap _image;
        private bool _detectBuffs;
        private bool _detectDebuffs;

        private MakeScreenFromMonitor _buffArea = new MakeScreenFromMonitor(MakeScreenFromMonitor.Areas.Buff);
        /* CreateMainImages deBuffArea = new CreateMainImages(CreateMainImages.Areas.DeBuff);*/

        private bool _testBool;
        private string _testString;

        public Form1(FullFillBuffsData listBuffs)
        {
            _listBuffs = listBuffs;
            InitializeComponent();
            _uISettings = new UISettings(listBuffs, checkedListBox1);
            _detectBuffs = true;
            checkBox1.Checked = true;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (timer != null)
            {
                return;
            }
            _image = _buffArea.CreateScreen();
            _findIcons.FindIcon(_image);

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            var image = _buffArea.CreateScreen();
            _findIcons.FindIcon(image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
                for (int i = 0; i < Helper.ThreadWhichShowImages.Count; i++)
                {
                    Helper.ThreadWhichShowImages[i].HideIcons();
                }


            }

        }




        // show icon and give control to drag them
        private void button4_Click(object sender, EventArgs e)
        {
            _settingIcons.LoadIcons();

        }

        // save icon pos = "end thread"
        private void button3_Click(object sender, EventArgs e)
        {
            _settingIcons.ShowOffIcons();
        }

        // reset icon pos
        private void button5_Click(object sender, EventArgs e)
        {
            _settingIcons.RestoreIconPosition();
        }


        // load images in folder to detect what pixel they have
        private void button6_Click(object sender, EventArgs e)
        {
            Helper.ReadImgPixelInFolder();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _detectBuffs = true;
            }
            else
            {
                _detectBuffs = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                _detectDebuffs = true;
            }
            else
            {
                _detectDebuffs = false;
            }
        }

        // make screenshot to debug
        private void button7_Click(object sender, EventArgs e)
        {
            GetIconsFromGame getIconsFromGame = new GetIconsFromGame();
            getIconsFromGame.NeedScreenShoot = true;
            var images = getIconsFromGame.FindIcon(_image);
            for (int i = 0; i < images.Count; i++)
            {
                images[i].Save($"{i}.png", ImageFormat.Png);
            }
        }


    }

}
