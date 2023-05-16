

using Poe_show_buff.helps;
using Poe_show_buff.settings;

namespace Poe_show_buff
{

    internal static class Program
    {
        static string pathToBuffs = @"\\Icons\\Buff\";
        static FullFillBuffsData _listBuffs;

        [STAThread]
        static void Main()
        {
            _listBuffs = new FullFillBuffsData();
            _listBuffs.FullFillList();

            LoadData.IconList();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(_listBuffs));

            SaveData.IconData();
        }

        public static FullFillBuffsData ReturnBuffs()
        {
            return _listBuffs;
        }
    }
}
