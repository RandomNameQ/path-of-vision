using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poe_show_buff
{
    public class ListBuffs
    {
        private string _pathToIcons = @"\Icons\Buff";
        private int _countPixels = 5;
        // какие данные бафа надо хранить. мне сначало надо собрать данные, но допустим они есть. как их в дб закинуть. через Json?
        // пока на похуй хардкодим
        // храним имя файла, звук который хотим юзать для его отображения, местоположение (акак) и финалку массив пикселей, шоб чекать нужных контент


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
            public bool isStartSoundAcvitated;
            public bool isEndSoundAcvitated;
            public bool isActivated;
            public string placeToHold;
            public TypeBuff typeBuff;
            public List<int> pixels;
            public int x;
            public int y;
            /*public Bitmap image;*/
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


            string path = Directory.GetCurrentDirectory();
            path += _pathToIcons;
            string[] icons = Directory.GetFiles(path);
            foreach (string icon in icons)
            {
                BuffData buff = new BuffData();
                buff.name = Path.GetFileNameWithoutExtension(icon);
                buff.path = icon;
                buff.soundEffectStart = "";
                buff.placeToHold = "";
                buff.isStartSoundAcvitated = false;
                buff.isEndSoundAcvitated = false;
                buff.isActivated = false;
                if (icon.Contains("Flask"))
                {
                    buff.typeBuff = BuffData.TypeBuff.Flask;
                }
                else
                {
                    buff.typeBuff = BuffData.TypeBuff.NormalBuff;
                }
                buff.pixels = new List<int>();
                var image = new Bitmap(icon);
                /*buff.image = image;*/
                ImgToPixels.PixelToList(image, _countPixels, buff.pixels);
                _listAllBuffs.Add(buff);

            }


            

        }


    }
}
