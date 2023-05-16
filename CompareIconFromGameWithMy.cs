using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poe_show_buff.helps;
using Poe_show_buff.settings;

namespace Poe_show_buff
{
    public class CompareIconFromGameWithMy
    {
        private ShowIconsInGame _displayIcons = new ShowIconsInGame();
        private List<int> _tempPixels = new List<int>();
        private List<int> _topPixels = new List<int>();
        private List<int> _midPixels = new List<int>();
        private List<int> _botPixels = new List<int>();
        private List<int> _allPixels = new List<int>();
        private List<int> _searchedPixels = new List<int>();
        private List<Bitmap> _imageList = new List<Bitmap>();
        private FullFillBuffsData _buffsPixel;

        private int _countPixels = 10;
        private int _minimumCountEqualPixels = 4;
        private bool PrepareThings(FullFillBuffsData.BuffData currentBuffsPixels, Bitmap gamesImage)
        {
            _allPixels.Clear();
            _searchedPixels.Clear();

            ImgToPixels.PixelsFromImage(gamesImage, _topPixels, _countPixels, 0, true);
            ImgToPixels.PixelsFromImage(gamesImage, _midPixels, _countPixels, 1, true);
            ImgToPixels.PixelsFromImage(gamesImage, _botPixels, _countPixels, 2, true);

            // обеденяем 3 инт листа в один для двух листов, чтобы легче было сравнивать

            _allPixels.AddRange(_topPixels);
            _allPixels.AddRange(_midPixels);
            _allPixels.AddRange(_botPixels);

            _searchedPixels.AddRange(currentBuffsPixels.topPixels);
            _searchedPixels.AddRange(currentBuffsPixels.middlePixels);
            _searchedPixels.AddRange(currentBuffsPixels.botPixels);


            if (CheckForSomeBuffs())
            {
                return true;
            }

            if (Helper.DifferenceBetweenPixelsList(_allPixels, _searchedPixels, 5000))
            {
                return true;
            }
            return false;
        }

        // custom fix cheks
        private bool CheckForSomeBuffs()
        {
            if (_searchedPixels[0] == -10471395)
            {

            }
            // quikc silver flask
            if (_allPixels[2] == -5471146 && _searchedPixels[2] == -6197181 || _allPixels[1] == -10471395 && _searchedPixels[0] == -10471395)
            {
                return true;
            }

            return false;
        }


        // открываем два массива, чтобы сравнить игровые (иконка из игры) пиксели с нашими сохранеными пикселями
        public void DetectEqualIcons(List<Bitmap> imageList)
        {

            _imageList = imageList;
            _buffsPixel = Program.ReturnBuffs();


            for (int i = 0; i < _buffsPixel.ListAllBuffs.Count; i++)
            {
                var buff = _buffsPixel.ListAllBuffs[i];
                buff.isDiscovered = false;
                _buffsPixel.ListAllBuffs[i] = buff;

                // если юзер выбрал скилл для поиска

                if (_buffsPixel.ListAllBuffs[i].isActivated)
                {

                    var currentPixelsBuffs = _buffsPixel.ListAllBuffs[i];

                    // открываем массив полученных изображений, чтобы сравнить currentPixelsBuffs со всеми фотками (мы ведь не знаем какая фотка нам нужна)
                    for (int x = 0; x < _imageList.Count; x++)
                    {
                     
                        if (PrepareThings(currentPixelsBuffs, _imageList[x]))
                        {

                            buff = _buffsPixel.ListAllBuffs[i];
                            buff.gamesIcon = _imageList[x];
                            buff.isDiscovered = true;
                            _buffsPixel.ListAllBuffs[i] = buff;
                            x = _imageList.Count;
                        }


                    }
                }
            }

           
            _displayIcons.UpdateIconState();

        }


    }
}
