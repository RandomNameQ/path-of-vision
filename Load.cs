using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using static Poe_show_buff.ListBuffs;

namespace Poe_show_buff
{
    public class Load
    {
        private string _pathToJsonFile = @"save/listBuffs.json";
        private ListBuffs _listBuffs;

        public void LoadData(ListBuffs listBuffs)
        {
            _listBuffs = listBuffs;
            
            if (File.Exists(_pathToJsonFile))
            {
                string json = File.ReadAllText(_pathToJsonFile);
                _listBuffs.ListAllBuffs = JsonConvert.DeserializeObject<List<BuffData>>(json);
            }
            else
            {   
                throw new FileNotFoundException("File not found", _pathToJsonFile);
            }
        }
    }

   
}
