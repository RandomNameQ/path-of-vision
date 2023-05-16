using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Poe_show_buff.settings;
using static Poe_show_buff.settings.FullFillBuffsData;

namespace Poe_show_buff.helps
{


    static class LoadData
    {


        private static string _pathToJsonFile = @"save/listBuffs.json";
        private static FullFillBuffsData _currentListBuffs;
        private static List<BuffData> _loadedBuffs;

        public static void IconList()
        {
            _currentListBuffs = Program.ReturnBuffs();



            if (File.Exists(_pathToJsonFile))
            {
                string json = File.ReadAllText(_pathToJsonFile);
                _loadedBuffs = JsonConvert.DeserializeObject<List<BuffData>>(json);
            }
            else
            {
                throw new FileNotFoundException("File not found", _pathToJsonFile);
            }


            UpdateBuffData();

        }

        private static void UpdateBuffData()
        {
            bool needUpdate = false;
            for (int i = 0; i < _currentListBuffs.ListAllBuffs.Count; i++)
            {
                
                var buff = _currentListBuffs.ListAllBuffs[i];

                for (int x = 0; x < _loadedBuffs.Count; x++)
                {
                    var loadedBuff = _loadedBuffs[x];
                    if (buff.name== loadedBuff.name)
                    {
                        buff= loadedBuff;
                        needUpdate = true;
                    }
                }

                if (needUpdate)
                {
                    _currentListBuffs.ListAllBuffs[i] = buff;
                    needUpdate = false;
                }
            }
        }

        
    }



}


