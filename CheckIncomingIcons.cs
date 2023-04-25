using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Poe_show_buff
{
    public static class CheckIncomingIcons
    {
        public static ShowBuff showBuff = new ShowBuff();
        private static List<ListBuffs.BuffData> _listBuffs;
        private static List<Bitmap> _iconList;
        private static List<int> _pixelsIcon = new List<int>();
        private static int pixelCount = 5;
        public static void TakeListBuffs(List<ListBuffs.BuffData> listBuffs)
        {

            _listBuffs = listBuffs;
        }

        public static void IsRequiredBuff(List<Bitmap> iconList)
        {
            _iconList = new(iconList);



            for (int i = 0; i < _listBuffs.Count; i++)
            {


                if (_listBuffs[i].isActivated)
                {
                    var pixelToFind = _listBuffs[i].pixels;
                    for (int q = 0; q < _iconList.Count; q++)
                    {
                        _pixelsIcon.Clear();
                        ImgToPixels.PixelToList(_iconList[q], pixelCount, _pixelsIcon);
                        bool isEqual = _pixelsIcon.SequenceEqual(pixelToFind);
                        if (isEqual)
                        {
                            Vector2 iconPos = new Vector2(_listBuffs[i].x, _listBuffs[i].y);
                            showBuff.ShowMETHEWAY(_iconList[q], _listBuffs[i].name,iconPos);
                            break;
                        }
                    }
                }


               




            }
            iconList.Clear();

        }

        public static void SaveNewPositionIcon(Vector2 iconPos,string iconName)
        {
            for (int i = 0; i < _listBuffs.Count; i++)
            {
                var buffName = _listBuffs[i].name;
                if (iconName == buffName)
                {
                    var tempBuffData = _listBuffs[i];

                    tempBuffData.x = (int)iconPos.X;
                    tempBuffData.y = (int)iconPos.Y;

                    _listBuffs[i] = tempBuffData;
                    Program.SaveIconData();
                    break;
                }
            }
            
        }

        // TODO сделать шоб искать 5 пикселей внутри 20
    }
}
