using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Poe_show_buff
{
    public class Save
    {
        private string _pathToJsonFile = @"save/listBuffs.json";
        private ListBuffs _listBuffs;

        public void SaveToFile(ListBuffs listBuffs)
        {
            _listBuffs = listBuffs;

            if (File.Exists(_pathToJsonFile))
            {

            }
            else
            {
                MessageBox.Show(_pathToJsonFile + " cant find file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string json = JsonConvert.SerializeObject(_listBuffs.ListAllBuffs, Formatting.Indented);

            File.WriteAllText(_pathToJsonFile, json);
        }
    }
}
