using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Poe_show_buff.helps;

namespace Poe_show_buff.settings
{

    public class SettingIconOnScreen
    {
        private ShowIconToChangePosition _drawIcon = new ShowIconToChangePosition();
        private FullFillBuffsData _iconList;

        public void LoadIcons()
        {
            _iconList = Program.ReturnBuffs();
            ShowIcons();
        }

        public void RestoreIconPosition()
        {
            for (int i = 0; i < _iconList.ListAllBuffs.Count; i++)
            {
                var buff = _iconList.ListAllBuffs[i];
                buff.x = 0;
                buff.y = 0;
                _iconList.ListAllBuffs[i] = buff;
            }
            SaveData.IconData();
        }

        private void ShowIcons()
        {
            _drawIcon.IsThreadNeed = true;
            List<string> iconsPath = new List<string>();
            for (int i = 0; i < _iconList.ListAllBuffs.Count; i++)
            {
                iconsPath.Add(_iconList.ListAllBuffs[i].path);
            }

            _drawIcon.DrawImages(iconsPath, _iconList);

        }

        public void ShowOffIcons()
        {

            _drawIcon.HideImgFromShow();
        }


    }
}
