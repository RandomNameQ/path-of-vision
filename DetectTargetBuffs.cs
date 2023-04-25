using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Poe_show_buff.ListBuffs;

namespace Poe_show_buff
{
    public class DetectTargetBuffs
    {
        private ShowBuff _showBuff = new ShowBuff();
        private List<BuffData> _listBuffs;
        private int _maxPixels = 5;
        private int _countBuffs;

        public DetectTargetBuffs(List<BuffData> listBuffs)
        {
            _listBuffs = listBuffs;
        }

        public void DetectBuff(Dictionary<Bitmap, List<int>> currentBuffs)
        {
            _countBuffs = 0;
            for (var i = 0; i < currentBuffs.Count; i++)
            {
                var currentBuff = currentBuffs.ElementAt(i);
                CheckBuffForPixel(currentBuff.Value);
            }
        }
        
        private void CheckBuffForPixel(List<int> pixels)
        {
            // хуево перебирает все
            int numberBuff=0;
            int countMatch;
            foreach (var buff in _listBuffs)
            {
                countMatch = 0;
                
                foreach (var lookForPixel in buff.pixels)
                {

                    foreach (var pixel in pixels)
                    {

                        if (pixel == lookForPixel)
                        {
                            countMatch++;
                        }
                        break;
                    }
                    
                }

                if (countMatch == _maxPixels && buff.isActivated)
                {
                    _countBuffs++;
                    /*_showBuff.ShowMETHEWAY(_listBuffs[numberBuff].path);*/
                }
                /*if (buff.isActivated && buff.name== "MoltenShell")
                {
                    _countBuffs++;
                    _showBuff.ShowMETHEWAY(_listBuffs[numberBuff].path);
                }*/
                numberBuff++;
            }

            
            
        }

    }
}
