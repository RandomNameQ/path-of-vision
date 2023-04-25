using System;
using System.Windows.Forms;


namespace Poe_show_buff
{
    public partial class Form1 : Form
    {
        private UpdateInTime _updateInTime = new UpdateInTime();
        private ListBuffs _listBuffs;
        private bool _testBool;
        private string _testString;

        CreateMainImages buffArea = new CreateMainImages(CreateMainImages.Areas.Buff);
       /* CreateMainImages deBuffArea = new CreateMainImages(CreateMainImages.Areas.DeBuff);*/

        GetDynamicIconPosition getDynamicPos = new GetDynamicIconPosition();

        public Form1(ListBuffs listBuffs)
        {

            
            _listBuffs = listBuffs;
            InitializeComponent();
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            AddBuffsToCheckedListBox(_listBuffs.ListAllBuffs);
            CheckIncomingIcons.TakeListBuffs(_listBuffs.ListAllBuffs);

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            /*Opacity = 0.9;*/
            
            
        }



        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            string itemName = checkedListBox1.Items[e.Index].ToString();
            bool isChecked = (e.NewValue == CheckState.Checked);


            if (isChecked)
            {
                BuffStateControl(itemName, needActivate: true);


            }
            else
            {
                BuffStateControl(itemName, needActivate: false);

            }




            checkedListBox1.Items[e.Index] = itemName;
        }


        private void BuffStateControl(string buffName, bool needActivate)
        {

            for (int i = 0; i < _listBuffs.ListAllBuffs.Count; i++)
            {
                ListBuffs.BuffData buff = _listBuffs.ListAllBuffs[i];
                if (buffName == buff.name)
                {
                    if (needActivate)
                    {
                        buff.isActivated = true;
                    }
                    else
                    {
                        buff.isActivated = false;
                    }
                    _listBuffs.ListAllBuffs[i] = buff;
                    _testBool = needActivate;

                    break;
                }
            }

        }




        // TODO sort by flask, buffs, etc, png conctoncation
        private void AddBuffsToCheckedListBox(List<ListBuffs.BuffData> listBuffs)
        {

            checkedListBox1.Items.Clear();


            for (int i = 0; i < listBuffs.Count; i++)
            {
                ListBuffs.BuffData buff = listBuffs[i];

                if (buff.isActivated)
                {
                    checkedListBox1.Items.Add(buff.name, CheckState.Checked);
                }
                else
                {
                    checkedListBox1.Items.Add(buff.name, CheckState.Unchecked);
                }
            }


        }


        /*CreateIconImages createIconImages = new CreateIconImages("buff");*/
        FindIcons findIcons = new FindIcons();
        private void button1_Click(object sender, EventArgs e)
        {
            /*_updateInTime.Start();*/

           var image = buffArea.CreateScreen();
            /* getDynamicPos.GetLinePixels(image);*/

            /* getDynamicPos.TakePixelFromPrototype();*/

            findIcons.FindIcon(image);

            /*createIconImages.CreateIconsImg();*/







        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*_updateInTime.Stop();*/
            /*MessageBox.Show("End: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);*/

        }
         protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT

                return createParams;
            }

        }
    }
}