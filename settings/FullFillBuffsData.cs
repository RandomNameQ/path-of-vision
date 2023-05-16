using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Poe_show_buff.helps;

namespace Poe_show_buff.settings
{

    public class FullFillBuffsData
    {
        private string _pathToIcons = @"\Icons\Buff";
        private int _countPixels = 10;
        private List<BuffData> _listAllBuffs = new List<BuffData>();

        public List<BuffData> ListAllBuffs
        {
            get { return _listAllBuffs; }
            set { _listAllBuffs = value; }
        }



        public struct BuffData
        {

            public string name;
            public string path;
            public string soundEffectStart;
            public string soundEffectEnd;
            public string pathToIconFromGame;
            public bool isStartSoundAcvitated;
            public bool isEndSoundAcvitated;
            public bool isActivated;
            public bool isDiscovered;
            public bool isisDiscoveredInLastTime;
            public string placeToHold;
            [JsonIgnore]
            public Bitmap gamesIcon;
            public TypeBuff typeBuff;
            public List<int> topPixels;
            public List<int> middlePixels;
            public List<int> botPixels;
            public int x;
            public int y;
            public enum TypeBuff
            {
                NormalBuff,
                Debuff,
                Flask,
                Etc

            }

        }

        public void FullFillList()
        {

            ImgToPixels.DeleteFilesInsideFolder(@"test\prototypeIconWhiteColor\");

            string path = Directory.GetCurrentDirectory();
            path += _pathToIcons;
            string[] icons = Directory.GetFiles(path);
            foreach (string icon in icons)
            {
                BuffData buff = new BuffData();
                buff.name = Path.GetFileNameWithoutExtension(icon);
                buff.path = @icon;
                buff.soundEffectStart = "";
                buff.placeToHold = "";
                buff.isStartSoundAcvitated = false;
                buff.isEndSoundAcvitated = false;
                buff.isActivated = false;
                buff.isDiscovered = false;

                if (icon.Contains("Flask"))
                {
                    buff.typeBuff = BuffData.TypeBuff.Flask;
                }
                else
                {
                    buff.typeBuff = BuffData.TypeBuff.NormalBuff;
                }

                buff.topPixels = new List<int>();
                var image = new Bitmap(icon);
                // 0 - left, 1 center, 2 right
                // true left to right, false from top to bot
                ImgToPixels.PixelsFromImage(image, buff.topPixels, _countPixels, 0, true);

                buff.middlePixels = new List<int>();
                ImgToPixels.PixelsFromImage(image, buff.middlePixels, _countPixels, 1, true);

                buff.botPixels = new List<int>();
                ImgToPixels.PixelsFromImage(image, buff.botPixels, _countPixels, 2, true);

                _listAllBuffs.Add(buff);

            }






        }


    }
}
