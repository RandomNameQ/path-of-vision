using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poe_show_buff.settings
{
    public class UISettings
    {
        private FullFillBuffsData _listBuffs;
        private CheckedListBox _checkedListBox;

        public UISettings(FullFillBuffsData listBuffs, CheckedListBox checkedListBox)
        {

            _listBuffs = listBuffs;
            _checkedListBox = checkedListBox;

            // fullfill _checkedListBox with objects and mark 
            _checkedListBox.ItemCheck += checkedListBox1_ItemCheck;
            AddBuffsToCheckedListBox(_listBuffs.ListAllBuffs);




        }

        public void AddBuffsToCheckedListBox(List<FullFillBuffsData.BuffData> listBuffs)
        {
            _checkedListBox.Items.Clear();

            for (int i = 0; i < listBuffs.Count; i++)
            {
                FullFillBuffsData.BuffData buff = listBuffs[i];
                if (buff.isActivated)
                {
                    _checkedListBox.Items.Add(buff.name, CheckState.Checked);
                }
                else
                {
                    _checkedListBox.Items.Add(buff.name, CheckState.Unchecked);
                }
            }
        }


        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
           
            string itemName = _checkedListBox.Items[e.Index].ToString();
            bool isChecked = (e.NewValue == CheckState.Checked);


            if (isChecked)
            {
                BuffStateControl(itemName, needActivate: true);


            }
            else
            {
                BuffStateControl(itemName, needActivate: false);

            }




            _checkedListBox.Items[e.Index] = itemName;
        }


        private void BuffStateControl(string buffName, bool needActivate)
        {

            for (int i = 0; i < _listBuffs.ListAllBuffs.Count; i++)
            {
                FullFillBuffsData.BuffData buff = _listBuffs.ListAllBuffs[i];
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

                    break;
                }
            }

        }


    }
}
