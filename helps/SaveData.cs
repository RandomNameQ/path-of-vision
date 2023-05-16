using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using Newtonsoft.Json;
using Poe_show_buff.settings;

namespace Poe_show_buff.helps
{
    static class SaveData
    {
        static string _pathToJsonFile = @"save/listBuffs.json";
        private static List<FullFillBuffsData.BuffData> _listBuffs;

        public static void IconData()
        {
            var _listBuffs = Program.ReturnBuffs();

            if (!File.Exists(_pathToJsonFile))
            {
                MessageBox.Show(_pathToJsonFile + " cant find file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string json = JsonConvert.SerializeObject(_listBuffs.ListAllBuffs, Formatting.Indented);

            File.WriteAllText(_pathToJsonFile, json);
        }

        public static void IconPosition(Vector2 iconPos, string buffName)
        {

            var tempFix = Program.ReturnBuffs();
            _listBuffs = tempFix.ListAllBuffs;
            for (int i = 0; i < _listBuffs.Count; i++)
            {
                var buffsName = _listBuffs[i].name;
                if (buffName == buffsName)
                {
                    var tempBuffData = _listBuffs[i];

                    tempBuffData.x = (int)iconPos.X;
                    tempBuffData.y = (int)iconPos.Y;

                    _listBuffs[i] = tempBuffData;
                    IconData();
                    break;
                }
            }

        }
    }
}
